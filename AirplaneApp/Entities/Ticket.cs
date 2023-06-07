namespace Entities;
public class Ticket
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public Flight TheFlight { get; set; }
    public int UserId { get; set; }
    public UserInfo TheUserInfo { get; set; }
    public string SeatNumber { get; set; }
    public DateTime BoardingTime { get; set; } // Departure time - x amount of time

    public Ticket(int flightId, int userId, string seatNumber, DateTime boardingTime)
    {
        FlightId = flightId;
        UserId = userId;
        SeatNumber = seatNumber;
        BoardingTime = boardingTime;
    }

    public Ticket(int id, int flightId, int userId, string seatNumber, DateTime boardingTime)
    {
        Id = id;
        FlightId = flightId;
        UserId = userId;
        SeatNumber = seatNumber;
        BoardingTime = boardingTime;
    }

    public override string ToString() => $"Vlucht: {TheFlight.DepartureLocation} - {TheFlight.ArrivalLocation}; UserID: {UserId}; Stoelnummer: {SeatNumber}; Boarding tijd: {BoardingTime}";
}
