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
            foreach (var user in users) {
                user.Reservations = context.Tickets.Where(t => t.UserId == user.UserInfoId).Include(t => t.TheFlight).Include(t => t.TheUserInfo).ToList();
            }
        }

        ListView usersView = new ListView() {
            Y = Pos.Bottom(searchBox) + 1,
            Height = 5,
            Width = Dim.Fill(),
        };

        usersView.SetSource(users);
        if (isReservation)
            usersView.OpenSelectedItem += (item) => {  WindowManager.GoForwardOne(new SearchReservationAdmin((User)item.Value)); };
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
