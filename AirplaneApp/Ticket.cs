public class Ticket
{
    public string FlightNumber;
    public string Name;
    public int GateNumber;
    public string SeatNumber;
    public string DepartureLocation;
    public DateTime DepartureTime;
    public string ArrivalLocation;
    public DateTime ArrivalTime;
    public DateTime BoardingTime; // Departure time - x amount of time

    public Ticket(string flightNumber, string name, int gateNumber, string seatNumber, string departureLocation, DateTime departureTime, string arrivalLocation, DateTime arrivalTime, DateTime boardingTime)
    {
        FlightNumber = flightNumber;
        Name = name;
        GateNumber = gateNumber;
        SeatNumber = seatNumber;
        DepartureLocation = departureLocation;
        DepartureTime = departureTime;
        ArrivalLocation = arrivalLocation;
        ArrivalTime = arrivalTime;
        BoardingTime = boardingTime;
    }

}