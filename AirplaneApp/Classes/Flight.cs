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
    public override string ToString() => $"Gate: {GateNumber}; Vertrek Locatie: {DepartureLocation}; Vertrek Tijd: {DepartureTime} Bestemming: {ArrivalLocation}; Tijd van Aankomst: {ArrivalTime}; Vliegtuig: {Airplane}";
}