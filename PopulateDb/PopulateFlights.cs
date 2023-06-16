using Newtonsoft.Json;
using Entities;
using Db;

public static class PopulateFlights
{
    private static string dbPath = "";
    private static string[] _planes;
    private static Random _rng = new Random();
    private static string[] _locations = {"Parijs", "London", "München", "Wenen", "Rome", "Barcelona", "Brussel", "Berlijn", "Madrid"};
    public static ListOfFlight Flights = new ListOfFlight();
    public static List<(int amount, string location)> updateLocation = new();
    public const int TOTAL_FLIGHTS_TO_GENERATE = 64;
    public const int MAX_CONCURRENT_FLIGHTS = 3;
    private static int _totalGenerated = 0;

    public static void Start(string db) {
        using (StreamReader reader = new StreamReader("../AirplaneApp/Airplanes.json")) {
            var airplanes = JsonConvert.DeserializeObject<Dictionary<string, Airplane>>(reader.ReadToEnd())!;
            if (airplanes == null) {
                _planes = new string[] {"Boeing 737", "Airbus 330", "Boeing 787"};
            } else {
                _planes = airplanes.Keys.ToArray();
            }
        }
        dbPath = db;
        using (var context = new ApplicationDbContext(db)) {
            Flights.SetList(context.Flights.ToList());
        }
        UpdateList();
    }

    public static void RemoveOld() => Flights.RemoveAll(fl => fl.ArrivalTime < DateTime.Now);
    public static void UpdateList() {
        RemoveOld();
        bool changed = false;
        foreach (string location in _locations) {
            int count = Flights.Where(fl => fl.ArrivalLocation == location || fl.DepartureLocation == location).Count();
            if (count != TOTAL_FLIGHTS_TO_GENERATE) {
                PopulateByAmount(TOTAL_FLIGHTS_TO_GENERATE - count, location);
                changed = true;
            }
        }
        if (changed) {
            Flights.Sort();
        }
    }

    public static void PopulateAll(string location)
    {
        string airplane = _planes[_rng.Next(_planes.Length)];
        TimeSpan flightDuration = location switch {
            "Parijs" => new TimeSpan(1, 15, 0),
            "London" => new TimeSpan(0, 55, 0),
            "München" => new TimeSpan(1, 25, 0),
            "Wenen" => new TimeSpan(1, 50, 0),
            "Rome" => new TimeSpan(2, 10, 0),
            "Barcelona" => new TimeSpan(2, 15, 0),
            "Brussel" => new TimeSpan(0, 45, 0),
            "Madrid" => new TimeSpan(2, 35, 0),
            "Berlijn" => new TimeSpan(1, 20, 0),
            _ => TimeSpan.Zero
        };

        if(flightDuration == TimeSpan.Zero)
            return;

        Flight latestFlight = new Flight("Rotterdam", DateTime.Now.AddHours(6 + _totalGenerated / MAX_CONCURRENT_FLIGHTS), location, DateTime.Now.AddHours(6 + _totalGenerated / MAX_CONCURRENT_FLIGHTS).Add(flightDuration), airplane);
        using (var context = new ApplicationDbContext(dbPath)) {
            for(int i = 0; i < TOTAL_FLIGHTS_TO_GENERATE / 2; i++) {
                if (i != 0)
                    latestFlight = new Flight("Rotterdam", latestFlight.ArrivalTime.AddHours(1), location, latestFlight.ArrivalTime.AddHours(1).Add(flightDuration), airplane);
                context.Add(latestFlight);
                latestFlight = new Flight(location, latestFlight.ArrivalTime.AddHours(1), "Rotterdam", latestFlight.ArrivalTime.AddHours(1).Add(flightDuration), airplane);
                context.Add(latestFlight);
            }
            context.SaveChanges();
        }
        _totalGenerated++;
    }

    private static void PopulateByAmount(int amount, string location)
    {
        if (amount == TOTAL_FLIGHTS_TO_GENERATE) {
            PopulateAll(location);
            return;
        }

        TimeSpan flightDuration = location switch {
            "Parijs" => new TimeSpan(1, 15, 0),
            "London" => new TimeSpan(0, 55, 0),
            "München" => new TimeSpan(1, 25, 0),
            "Wenen" => new TimeSpan(1, 50, 0),
            "Rome" => new TimeSpan(2, 10, 0),
            "Barcelona" => new TimeSpan(2, 15, 0),
            "Brussel" => new TimeSpan(0, 45, 0),
            "Madrid" => new TimeSpan(2, 35, 0),
            _ => TimeSpan.Zero
        };

        if(flightDuration == TimeSpan.Zero)
            return;

        Flight latestFlight = Flights.Where(fl => fl.ArrivalLocation == location || fl.DepartureLocation == location).Last();
        using (var context = new ApplicationDbContext(dbPath)) {
            for (int i = amount; i > 0; i--) {
                if (latestFlight.ArrivalLocation == location) {
                    latestFlight = new Flight(location, latestFlight.ArrivalTime.AddHours(1), "Rotterdam", latestFlight.ArrivalTime.AddHours(1).Add(flightDuration), latestFlight.Airplane);
                } else {
                    latestFlight = new Flight("Rotterdam", latestFlight.ArrivalTime.AddHours(1), location, latestFlight.ArrivalTime.AddHours(1).Add(flightDuration), latestFlight.Airplane);
                }
                context.Flights.Add(latestFlight);
            }
            context.SaveChanges();
        }
    }
}
