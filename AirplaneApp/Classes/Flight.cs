public class Flight
{
    public int FlightNumber;
    public int GateNumber;
    public string DepartureLocation;
    public DateTime DepartureTime;
    public string ArrivalLocation;
    public DateTime ArrivalTime;
    public string Airplane;

    public Flight(int flightNumber, int gateNumber, string departureLocation, DateTime departureTime, string arrivalLocation, DateTime arrivalTime, string airplane)
    {
        FlightNumber = flightNumber;
        GateNumber = gateNumber;
        DepartureLocation = departureLocation;
        DepartureTime = departureTime;
        ArrivalLocation = arrivalLocation;
        ArrivalTime = arrivalTime;
        Airplane = airplane;
    }
    public override string ToString() => $"Bestemming: {ArrivalLocation}; Tijd van Aankomst: {ArrivalTime}; Vertrek Locatie: {DepartureLocation}; Vertrek Tijd: {DepartureTime}; Vliegtuig: {Airplane}";
    public string ToNewLineString() => $"Bestemming: {ArrivalLocation}\nTijd van Aankomst: {ArrivalTime}\nVertrek Locatie: {DepartureLocation}\nVertrek Tijd: {DepartureTime}\nVliegtuig: {Airplane}";
}