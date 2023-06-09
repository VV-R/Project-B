using Terminal.Gui;
using Entities;
using Managers;


namespace Windows;
public class SearchReservation : Toplevel
{
    public ListView ReservationsView;
    public TextField SearchBox;
    public SearchReservation(User? user)
    {
        if (user == null || user.Reservations == null)
            WindowManager.GoBackOne(this);

        Label searchBoxLabel = new Label() {
            Text = "Zoeken:"
        };

        SearchBox = new TextField("") {
            Width = 20,
            X = Pos.Right(searchBoxLabel) + 1,
        };

        ReservationsView = new ListView() {
            Y = Pos.Bottom(SearchBox) + 1,
            Height = 5,
            Width = Dim.Fill(),
        };

        Button goBackButton = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(ReservationsView) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(searchBoxLabel, SearchBox, ReservationsView, goBackButton);
    }
}

public class SearchReservationAdmin : SearchReservation
{
    public SearchReservationAdmin(User user) : base(user) {
        List<Ticket> reservations = user.Reservations;
        ReservationsView.SetSource(reservations);

        SearchBox.TextChanged += (text) => {ReservationsView.SetSource(reservations?.FindAll((x) => {
            return x.ToString().ToLower().Contains((string)SearchBox.Text.ToLower());
        }));};

        ReservationsView.OpenSelectedItem += (item) => { if (reservations != null)
            WindowManager.GoForwardOne(new ReservationPanelAdmin(user, reservations[item.Item])); };
    }
}

public class SearchReservationUser : SearchReservation
{
    public SearchReservationUser() : base(WindowManager.CurrentUser) {
        List<Flight> flights = new List<Flight>();
        foreach (Ticket ticket in WindowManager.CurrentUser.Reservations) {
            if (!flights.Contains(ticket.TheFlight))
                flights.Add(ticket.TheFlight);
        }
        ReservationsView.SetSource(flights);

        SearchBox.TextChanged += (text) => {ReservationsView.SetSource(flights?.FindAll((x) => {
            return x.ToString().ToLower().Contains((string)SearchBox.Text.ToLower());
        }));};

        ReservationsView.OpenSelectedItem += (item) => {
            WindowManager.GoForwardOne(new ReservationPanelUser(
                WindowManager.CurrentUser.Reservations.Where(t => t.FlightId == ((Flight)item.Value).FlightNumber).ToList()));
        };
    }
}

public class ReservationPanelAdmin : Toplevel
{
    public User CurrentUser;
    public Ticket CurrentTicket;
    public TextField SeatnumberText;
    public ReservationPanelAdmin(User user, Ticket ticket)
    {
        CurrentUser = user;
        CurrentTicket = ticket;
        Label flightLabel = new Label() {
            Text = $"Vlucht:\n{ticket.TheFlight.ToNewLineString()}",
        };

        Label userLabel = new Label() {
            Text = $"User:\n{user.ToNewLineString()}",
            Y = Pos.Bottom(flightLabel) + 1,
        };

        Label seatnumberLabel = new Label() {
            Text = "Ticket:\nStoelnummer:",
            Y = Pos.Bottom(userLabel) + 1,
        };

        SeatnumberText = new TextField(ticket.SeatNumber) {
            X = Pos.Right(seatnumberLabel) + 3,
            Y = Pos.Top(seatnumberLabel) + 1,
            Width = 5,
        };

        Label boardingTimeLabel = new Label() {
            Text = $"Boarding tijd: {ticket.BoardingTime}",
            Y = Pos.Bottom(seatnumberLabel),
        };

        Add(flightLabel, userLabel, seatnumberLabel, SeatnumberText, boardingTimeLabel);

        Button editButton = new Button() {
            Text = "Aanpassen",
            Y = Pos.Bottom(boardingTimeLabel) + 1,
        };

        editButton.Clicked += () => { EditTicket();};

        Button cancelButton = new Button() {
            Text = "Ticket Annuleren",
            X = Pos.Right(editButton) + 1,
            Y = Pos.Top(editButton),
        };

        cancelButton.Clicked += () => { CancelTicket(); };

        Button backButton = new Button() {
            Text = "Terug",
            X = Pos.Right(cancelButton) + 1,
            Y = Pos.Top(editButton),
        };

        backButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(editButton, cancelButton, backButton);
    }

    private void EditTicket()
    {
        // Check if seat number is really an seat in the plane and if its not occupied
        // if (!plane.Seat.Contain(SeatnumberText.Text)) return
        if (CurrentUser.UserInfo.Email == null)
            return;

        string subject = $"Vlucht {CurrentTicket.TheFlight.DepartureLocation} - {CurrentTicket.TheFlight.ArrivalLocation}";
        string body = $"Beste Klant,\n\nUw zitplek van vlucht {CurrentTicket.TheFlight.DepartureLocation} - {CurrentTicket.TheFlight.ArrivalLocation} is aangepast.\nUw zitplek gaat van {CurrentTicket.SeatNumber} naar {SeatnumberText.Text}.\n\nMet vriendelijke groeten,\nRotterdam Airline Service";


        EmailManager.SendOneEmail(subject, body, CurrentUser.UserInfo.Email);
        CurrentTicket.SeatNumber = (string)SeatnumberText.Text;
        WindowManager.GoBackOne(this);
    }

    private void CancelTicket()
    {
        CurrentUser.Reservations.Remove(CurrentTicket);
        SpecialEmail(CurrentTicket.TheFlight);
    }

    private void SpecialEmail(Flight flight)
    {
        RemoveAll();
        TextView textField = new TextView() {
            Text = $"Beste Klant,\n\nUw ticket van {flight.DepartureLocation} - {flight.ArrivalLocation} is gecanceld vanwege %%.\nEen alternatief wordt geregeld. Wij houden U hierover op de hoogte.\n\nMet vriendelijke groeten,\nRotterdam Airline Service",
            Width = Dim.Percent(50),
            Height = Dim.Percent(30),
            Y = 1,
        };

        Button sendButton = new Button() {
            Text = "Versturen",
            Y = Pos.Bottom(textField) + 1
        };

        sendButton.Clicked += () => { EmailManager.SendOneEmail($"Ticket {flight.DepartureLocation} - {flight.ArrivalLocation}", (string)textField.Text, CurrentUser.UserInfo.Email); WindowManager.GoBackOne(this); };

        Button goBack = new Button() {
            Text = "Terug",
            Y = Pos.Top(sendButton),
            X = Pos.Right(sendButton) + 1,
        };

        goBack.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(textField, sendButton, goBack);
    }
}

public class ReservationPanelUser : Toplevel
{
    public ReservationPanelUser(List<Ticket> tickets)
    {
        Label flightLabel = new Label() {
            Text = $"Vlucht:\n{tickets[0].TheFlight.ToNewLineString()}",
        };
        Add(flightLabel);

        int xOffset = 40;
        int rows = 2;
        int labelHeight = 6;
        for (int i = 0; i < tickets.Count; i++) {
            Label ticketLabel = new Label() {
                Text = $"Stoelnummer: {tickets[i].SeatNumber}\nBoarding tijd: {tickets[i].BoardingTime}",
                Y = Pos.Bottom(flightLabel) + (labelHeight * (i / rows)) + 1,
                X = xOffset * (i % rows)
            };
            UserInfo userInfo = tickets[i].TheUserInfo;
            Label userLabel = new Label() {
                Text = $"{userInfo.FirstName} {userInfo.Preposition} {userInfo.LastName}",
                Y = Pos.Bottom(ticketLabel) + 1,
                X = Pos.Left(ticketLabel),
            };

            Add(ticketLabel, userLabel);
        }

        Button goBackButton = new Button() {
            Y = Pos.Bottom(flightLabel) + 1 + labelHeight * ((tickets.Count + 1) / rows),
            Text = "Terug",
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};

        Add(goBackButton);
    }
}
