using System.Collections;
using Newtonsoft.Json;

namespace Entities;

public enum FlightStatus {
    Waiting = 0,
    Delayed = 1,
    Boarding = 2,
    Departed = 3,
    Disembarking = 4,
    Completed = 5,
    Cancelled = 6
}

public class Flight : IComparable<Flight>
{
    public int FlightNumber { get; set; }
    public string DepartureLocation { get; set; }
    public DateTime DepartureTime { get; set; }
    public string ArrivalLocation { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Airplane { get; set; }
    public FlightStatus Status { get {
        if (IsCancelled) return FlightStatus.Cancelled;
        if (ArrivalTime.AddMinutes(30) < DateTime.Now) return FlightStatus.Completed;
        if (ArrivalTime.AddMinutes(30) > DateTime.Now && ArrivalTime < DateTime.Now) return FlightStatus.Disembarking;
        if (DateTime.Now > DepartureTime) return FlightStatus.Departed;
        if (DateTime.Now.AddMinutes(30) > DepartureTime) return FlightStatus.Boarding;
        return FlightStatus.Waiting;
    } }
    public bool IsCancelled { get; set; }

    [JsonConstructor]
    public Flight(int flightNumber, string departureLocation, DateTime departureTime, string arrivalLocation, DateTime arrivalTime, string airplane)
    {
        FlightNumber = flightNumber;
        DepartureLocation = departureLocation;
        DepartureTime = departureTime;
        ArrivalLocation = arrivalLocation;
        ArrivalTime = arrivalTime;
        Airplane = airplane;
    }
    public Flight(string departureLocation, DateTime departureTime, string arrivalLocation, DateTime arrivalTime, string airplane) {
        DepartureLocation = departureLocation;
        DepartureTime = departureTime;
        ArrivalLocation = arrivalLocation;
        ArrivalTime = arrivalTime;
        Airplane = airplane;
    }


    public Flight() {}

    public static string StatusString(FlightStatus status) => status switch {
        FlightStatus.Waiting => "VERWACHT",
        FlightStatus.Delayed => "VERTRAGING",
        FlightStatus.Boarding => "BOARDING",
        FlightStatus.Departed => "VERTROKKEN",
        FlightStatus.Disembarking => "DISEMBARKING",
        FlightStatus.Completed => "COMPLETED",
        FlightStatus.Cancelled => "GEANNULEERD"
    };

    public string StatusString() => StatusString(Status);

    public override string ToString() {
        string flightString = $"Bestemming: {ArrivalLocation};" .PadRight(23);
        flightString += $"Tijd van Aankomst: {ArrivalTime.ToString("dd/MM/yyyy HH:mm")}; ";
        flightString += $"Vertrek Locatie: {DepartureLocation}; ".PadRight(28);
        flightString += $"Vertrek Tijd: {DepartureTime.ToString("dd/MM/yyyy HH:mm")}; ";
        flightString += $" Vliegtuig: {Airplane}";
        return flightString;
    }
    public string ToNewLineString() => $"Vertrek Locatie: {DepartureLocation}\nVertrek Tijd: {DepartureTime}\nBestemming: {ArrivalLocation}\nTijd van Aankomst: {ArrivalTime}\nVliegtuig: {Airplane}";

    int IComparable<Flight>.CompareTo(Flight? other)
    {
        if (other == null)
            return 1;
        if (other.DepartureTime < DepartureTime)
            return 1;
        else if (other.DepartureTime > DepartureTime)
            return -1;
        else return 0;
    }
}

public class ListOfFlight : IEnumerable<Flight>
{
    private List<Flight> _flights = new List<Flight>();
    public int Count {get {return _flights.Count;}}

    public IEnumerator<Flight> GetEnumerator() => _flights.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public void SetList(List<Flight> flights) => _flights = flights;
    public void Add(Flight flight) => _flights.Add(flight);
    public void RemoveAll(Predicate<Flight> match) => _flights.RemoveAll(match);

    public void Sort()
    {
        _flights.Sort();
        for (int i = 0; i < _flights.Count; i++) {
            _flights[i].FlightNumber = i;
        }
    }

    public Flight? Min(string location) => _flights.Where(fl => fl.DepartureLocation == location ||fl.ArrivalLocation == location).Min();
    public Flight? Max(string location) => _flights.Where(fl => fl.DepartureLocation == location ||fl.ArrivalLocation == location).Max();
}
