using System;
using System.Net;
using System.Net.Mail;
using Terminal.Gui;
using Newtonsoft.Json;
using Entities;
using Managers;
using Microsoft.EntityFrameworkCore;

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

        Button changeSeatingPrice = new Button() {
            Text = "Stoel prijzen",
            Y = Pos.Bottom(flightSchedule) + 1,
        };

        changeSeatingPrice.Clicked += () => { WindowManager.GoForwardOne(new ChangeSeatingPrice()); };

        Add(nameLabel, searchUsers, searchReservation, flightSchedule, changeSeatingPrice);
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

        List<User> users;
        using (var context = new Db.ApplicationDbContext()) {
            users = context.Users.Include(u => u.UserInfo).ToList();
        }

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
            return x.ToString().ToLower().Contains((string)searchBox.Text.ToLower());
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

public class ChangeSeatingPrice : Toplevel
{
    public ChangeSeatingPrice()
    {

        Dictionary<string, double> SeatPrices = new();
        using (StreamReader reader = new StreamReader("SeatingPrice.json")) {
            SeatPrices = JsonConvert.DeserializeObject<Dictionary<string, double>>(reader.ReadToEnd())!;
        }

        Dictionary<string, TextField>  pricesTextField = new();
        int allignOffset = SeatPrices.Keys.Max().Length + 2;
        foreach (var kvp in SeatPrices) {
            if (pricesTextField.Count == 0) {
                Label seatLabel = new Label($"{kvp.Key}:"); 

                Label euroSymbol = new Label("€") {
                    X = allignOffset,
                    Y = Pos.Top(seatLabel),
                };

                TextField priceField = new TextField() {
                    Text = kvp.Value.ToString("0.00"),
                    X = Pos.Right(euroSymbol),
                    Width = 8,
                };


                priceField.TextChanged += (text) => {
                    if (!double.TryParse(priceField.Text == "" ? "0" : (string)priceField.Text, out _))
                        priceField.Text = text == "" ? "" : text;
                    else if (priceField.Text.Length > 9)
                        priceField.Text = text;
                    priceField.CursorPosition = priceField.Text.Length;};

                Add(seatLabel, euroSymbol ,priceField);
                pricesTextField.Add(kvp.Key, priceField);
            } else {                    
                Label seatLabel = new Label($"{kvp.Key}:") {
                    Y = Pos.Bottom(pricesTextField.Values.Last()) + 1,
                };

                Label euroSymbol = new Label("€") {
                    X = allignOffset,
                    Y = Pos.Top(seatLabel),
                };

                TextField priceField = new TextField() {
                    Text = kvp.Value.ToString("0.00"),
                    X = Pos.Right(euroSymbol),
                    Y = Pos.Top(seatLabel),
                    Width = 8,
                };

                priceField.TextChanged += (text) => {
                    if (!double.TryParse(priceField.Text == "" ? "0" : (string)priceField.Text, out _))
                        priceField.Text = text == "" ? "" : text;
                    else if (priceField.Text.Length > 9)
                        priceField.Text = text;
                    priceField.CursorPosition = priceField.Text.Length;};

                Add(seatLabel, euroSymbol, priceField);
                pricesTextField.Add(kvp.Key, priceField); 
            }
        }

        Button updatePrices = new Button() {
            Text = "Prijzen Updaten",
            Y = Pos.Bottom(pricesTextField.Values.Last()) + 1,
        };

        updatePrices.Clicked += () => { UpdatePrices(SeatPrices, pricesTextField);};

        Button exitButton = new Button() {
            Text = "Annuleren",
            X = Pos.Right(updatePrices) + 1,
            Y = Pos.Top(updatePrices),
        };

        exitButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(updatePrices, exitButton);
   }


    private void UpdatePrices(Dictionary<string, double> seatPrices, Dictionary<string, TextField> changes) {
        foreach (var kvp in changes) {
            if (kvp.Value.Text.Length == 0) {
                MessageBox.ErrorQuery("Stoel Prijzen", $"Prijs voor stoel {kvp.Key} niet correct ingevuld.", "Ok");
                return;
            }
            seatPrices[kvp.Key] = double.Parse((string)kvp.Value.Text);
        }

        using (StreamWriter writer = new StreamWriter("SeatingPrice.json")) {
            writer.Write(JsonConvert.SerializeObject(seatPrices));
        }
        WindowManager.GoBackOne(this);
    }
}
