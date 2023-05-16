namespace Entities;
public class Ticket
{
    public Flight Flight { get; set; }
    public int UserId { get; set; }
    public string SeatNumber { get; set; }
    public DateTime BoardingTime { get; set; } // Departure time - x amount of time

    public Ticket(Flight flight, int userId, string seatNumber, DateTime boardingTime)
    {
        Flight = flight;
        UserId = userId;
        SeatNumber = seatNumber;
        BoardingTime = boardingTime;
    }

    public override string ToString() => $"Vlucht: {Flight.DepartureLocation} - {Flight.ArrivalLocation}; UserID: {UserId}; Stoelnummer: {SeatNumber}; Boarding tijd: {BoardingTime}";
}
