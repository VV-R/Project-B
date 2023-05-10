using Newtonsoft.Json;

namespace test;
public static class FlightScheduleGen
{
    private static string[] _planes = {"Boeing 737", "Airbus 330", "Boeing 787"};
    private static Random _rng = new Random();
    private static List<string> _locations = new List<string>() {"Parijs", "London", "München", "Wenen", "Rome", "Barcelona", "Brussel", "Berlijn", "Madrid"};
    public static ListOfFlight Flights = new ListOfFlight();
    public static List<(int amount, string location)> updateLocation = new();
    public static int AmountOfFlights = 64;
    static FlightScheduleGen()
    {
        // Get all current Flights from DB now its just JSON
        StreamReader reader = new StreamReader("../../../../AirplaneApp/Flights.json");
        Flights.SetList(JsonConvert.DeserializeObject<List<Flight>>(reader.ReadToEnd())!);
        reader.Close();
        reader.Dispose();
        UpdateList();
    }

    public static void RemoveOld() => Flights.RemoveAll(fl => fl.ArrivalTime < DateTime.Now);
    public static void UpdateList() {
        RemoveOld();
        bool changed = false;
        foreach (string location in _locations) {
            int count = Flights.Where(fl => fl.ArrivalLocation == location || fl.DepartureLocation == location).Count();
            if (count != AmountOfFlights) {
                PopulateByAmount(AmountOfFlights - count, location);
                changed = true;
            }
        }
        if (changed) {
            Flights.Sort();
            StoreInJson();
        }
    }

    public static void PopulateAll()
    {
        foreach (string location in _locations)
            PopulateByLocation(location);
    }

    public static void PopulateByLocation(string location)
    {
        if (Flights.Count >= AmountOfFlights * _locations.Count)
            return;
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

        for(int i = 0; i < AmountOfFlights / 2; i++) {
            if (i == 0)
                Flights.Add(new Flight(Flights.Count, "Rotterdam", DateTime.Now.AddHours(6), location, DateTime.Now.AddHours(6).Add(flightDuration), airplane));
            else
                Flights.Add(new Flight(Flights.Count, "Rotterdam", Flights.Last().ArrivalTime.AddHours(1), location, Flights.Last().ArrivalTime.AddHours(1).Add(flightDuration), airplane));
            Flights.Add(new Flight(Flights.Count, location, Flights.Last().ArrivalTime.AddHours(1), "Rotterdam", Flights.Last().ArrivalTime.AddHours(1).Add(flightDuration), airplane));
        }
        Flights.Sort();
    }

    private static void PopulateByAmount(int amount, string location)
    {
        if (amount == AmountOfFlights) {
            PopulateByLocation(location);
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

        // Even = terug naar rotterdam
        // Oneven = naar locatie to

        Console.WriteLine($"{location}: {amount}");

        for (int i = amount; i > 0; i--) {
            Flight latestFlight = Flights.Where(fl => fl.ArrivalLocation == location || fl.DepartureLocation == location).Last();
            if (latestFlight.ArrivalLocation == location)
                Flights.Add(new Flight(Flights.Count, location, latestFlight.ArrivalTime.AddHours(1), "Rotterdam", latestFlight.ArrivalTime.AddHours(1).Add(flightDuration), latestFlight.Airplane));
            else
                Flights.Add(new Flight(Flights.Count, "Rotterdam", latestFlight.ArrivalTime.AddHours(1), location, latestFlight.ArrivalTime.AddHours(1).Add(flightDuration), latestFlight.Airplane));
        }
    }

    private static void StoreInJson() {
        StreamWriter writer = new StreamWriter("../../../../AirplaneApp/Flights.json");
        writer.Write(JsonConvert.SerializeObject(Flights, Formatting.Indented));
        writer.Close();
        writer.Dispose();
    }
}