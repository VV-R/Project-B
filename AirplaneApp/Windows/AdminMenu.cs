using System;
using Terminal.Gui;


public class AdminMenu : Toplevel
{
    // public AdminMenu(User user)
    public AdminMenu(string name)
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

        Button flightSchedule = new Button() {
            Text = "Vluchtschemas",
            Y = Pos.Bottom(searchReservation) + 1,
        };

        Add(nameLabel, searchUsers, searchReservation, flightSchedule);
    }
}


public class SearchUsers : Toplevel
{
    public SearchUsers()
    {
        Label searchBoxLabel = new Label() {
            Text = "Zoeken:"
        };
        TextField searchBox = new TextField("") {
            Width = 20,
            X = Pos.Right(searchBoxLabel) + 1,
        };

        // Want a list of users from the database
        List<string> users = new List<string>() {"ID: 1 Naam: Cas", "ID: 2 Naam: Steyn", "ID: 3 Naam: Levi", "ID: 4 Naam: Janell", "ID: 5 Naam: Ruben",
                                                "ID: 6 Naam: Rick", "ID: 7 Naam: Jesse", "ID: 8 Naam: John", "ID: 9 Naam: Jane", "ID: 10 Naam: Doe"};

        ListView usersView = new ListView() {
            Y = Pos.Bottom(searchBox) + 1,
            Height = 5,
            Width = 20,
        };

        usersView.SetSource(users);
        usersView.OpenSelectedItem += (item) => { WindowManager.GoForwardOne(new EditUserInfoAdmin((string)item.Value)); };

        searchBox.TextChanged += (text) => {usersView.SetSource(users.FindAll((x) => {
            if (x.ToLower().Contains((string)searchBox.Text.ToLower()))
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
