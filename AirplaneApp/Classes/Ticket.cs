public class Ticket
{
    public Flight Flight;
    public int UserId;
    public string SeatNumber;
    public DateTime BoardingTime; // Departure time - x amount of time

    public Ticket(Flight flight, int userId, string seatNumber, DateTime boardingTime)
    {
        Flight = flight;
        UserId = userId;
        SeatNumber = seatNumber;
        BoardingTime = boardingTime;
    }

    public override string ToString() => $"Vlucht: {Flight.DepartureLocation} - {Flight.ArrivalLocation}; UserID: {UserId}; Stoelnummer: {SeatNumber}; Boarding tijd: {BoardingTime}";
}