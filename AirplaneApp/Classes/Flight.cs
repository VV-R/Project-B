public class Flight
{
    public string FlightNumber;
    public int GateNumber;
    public string DepartureLocation;
    public DateTime DepartureTime;
    public string ArrivalLocation;
    public DateTime ArrivalTime;

    public Flight(string flightNumber, int gateNumber, string departureLocation, DateTime departureTime, string arrivalLocation, DateTime arrivalTime)
    {
        FlightNumber = flightNumber;
        GateNumber = gateNumber;
        DepartureLocation = departureLocation;
        DepartureTime = departureTime;
        ArrivalLocation = arrivalLocation;
        ArrivalTime = arrivalTime;
    }
}