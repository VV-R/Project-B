public class Ticket : Flight
{
    public string Name;
    public string SeatNumber;
    public DateTime BoardingTime; // Departure time - x amount of time

    public Ticket(string flightNumber, string name, int gateNumber, string seatNumber, string departureLocation, DateTime departureTime, string arrivalLocation, DateTime arrivalTime, DateTime boardingTime) : base(flightNumber, gateNumber, departureLocation, departureTime, arrivalLocation, arrivalTime)
    {
        Name = name;
        SeatNumber = seatNumber;
        BoardingTime = boardingTime;
    }

}