using System;
using System.Net;
using System.Net.Mail;
using Terminal.Gui;
using Entities;
using Managers;

namespace Windows;
public class AdminMenu : Toplevel
{
    public AdminMenu()
    {
        Label nameLabel = new Label() {
            Text = $"Admin panel",
        };

        Button searchUsers = new Button() {
            Text = "Klanten zoeken",
            Y = Pos.Bottom(nameLabel) + 1,
        };

        searchUsers.Clicked += () => {WindowManager.GoForwardOne(new SearchUsers()); };

        Button searchReservation = new Button() {
            Text = "Reserveringen zoeken",
            Y = Pos.Bottom(searchUsers) + 1,
        };

        searchReservation.Clicked += () => {WindowManager.GoForwardOne(new SearchUsers(true)); };

        Button flightSchedule = new Button() {
            Text = "Vlucht Schemas",
            Y = Pos.Bottom(searchReservation) + 1,
        };

        flightSchedule.Clicked += () => { WindowManager.GoForwardOne(new FlightPanelAdmin(WindowManager.Flights));};

        Add(nameLabel, searchUsers, searchReservation, flightSchedule);
    }
}


public class SearchUsers : Toplevel
{
    public SearchUsers(bool isReservation = false)
    {
        Label searchBoxLabel = new Label() {
            Text = "Zoeken:"
        };
        TextField searchBox = new TextField("") {
            Width = 20,
            X = Pos.Right(searchBoxLabel) + 1,
        };

        // Want a list of users from the database
        List<User> users = new List<User>() {new User(1, "Levi", "van", "Daalen", "1234", new MailAddress("2004levi@gmail.com"), "+31|613856964", new DateTime(2004, 1, 19), "Nederland") {
            Reservations = new List<Ticket>() { new Ticket(1, 1, 1, "B3", DateTime.Now.AddDays(2))}},
            new User(2, "Steyn", "", "Hellendoorn", "password", new MailAddress("idk@gmail.com"), "+31|012345678", DateTime.Now.AddYears(-21), "Nederlands")
        };



        ListView usersView = new ListView() {
            Y = Pos.Bottom(searchBox) + 1,
            Height = 5,
            Width = Dim.Fill(),
        };

        usersView.SetSource(users);
        if (isReservation)
            usersView.OpenSelectedItem += (item) => {  WindowManager.GoForwardOne(new SearchReservation((User)item.Value)); };
        else
            usersView.OpenSelectedItem += (item) => { WindowManager.GoForwardOne(new EditUserInfoAdmin((users[item.Item]))); };

        searchBox.TextChanged += (text) => {usersView.SetSource(users.FindAll((x) => {
            if (x.ToString().ToLower().Contains((string)searchBox.Text.ToLower()))
                return true;
            else return false;
        }));};

        Button goBackButton = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(usersView) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(searchBoxLabel, searchBox, usersView, goBackButton);
    }
}

public class SearchReservation : Toplevel
{
    public SearchReservation(User user)
    {
        List<Ticket> reservations = user.Reservations;

        if (reservations == null)
            WindowManager.GoBackOne(this);

        Label searchBoxLabel = new Label() {
            Text = "Zoeken:"
        };

        TextField searchBox = new TextField("") {
            Width = 20,
            X = Pos.Right(searchBoxLabel) + 1,
        };

        ListView usersView = new ListView() {
            Y = Pos.Bottom(searchBox) + 1,
            Height = 5,
            Width = Dim.Fill(),
        };

        usersView.SetSource(reservations);

        usersView.OpenSelectedItem += (item) => { if (reservations != null)
            WindowManager.GoForwardOne(new ReservationPanel(user, reservations[item.Item])); };

        searchBox.TextChanged += (text) => {usersView.SetSource(reservations?.FindAll((x) => {
            if (x.ToString().ToLower().Contains((string)searchBox.Text.ToLower()))
                return true;
            else return false;
        }));};

        Button goBackButton = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(usersView) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(searchBoxLabel, searchBox, usersView, goBackButton);
    }
}

public class ReservationPanel : Toplevel
{
    public User CurrentUser;
    public Ticket CurrentTicket;
    public TextField SeatnumberText;
    public ReservationPanel(User user, Ticket ticket)
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

        string subject = $"Vlucht {CurrentTicket.TheFlight.DepartureLocation} - {CurrentTicket.TheFlight.ArrivalLocation}";
        string body = $"Beste Klant,\n\nUw zitplek van vlucht {CurrentTicket.TheFlight.DepartureLocation} - {CurrentTicket.TheFlight.ArrivalLocation} is aangepast.\nUw zitplek gaat van {CurrentTicket.SeatNumber} naar {SeatnumberText.Text}.\n\nMet vriendelijke groeten,\nRotterdam Airline Service";

        EmailManager.SendOneEmail(subject, body, CurrentUser.Email);
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

        sendButton.Clicked += () => { EmailManager.SendOneEmail($"Ticket {flight.DepartureLocation} - {flight.ArrivalLocation}", (string)textField.Text, CurrentUser.Email); WindowManager.GoBackOne(this); };

        Button goBack = new Button() {
            Text = "Terug",
            Y = Pos.Top(sendButton),
            X = Pos.Right(sendButton) + 1,
        };

        goBack.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(textField, sendButton, goBack);
    }
}
