using System;
using Terminal.Gui;
using System.Net.Mail;


public class AdminMenu : Toplevel
{
    public AdminMenu(User user)
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
        List<User> users = new List<User>() {new User(1, "Levi", "van", "Daalen", "1234", new MailAddress("2004levi@gmail.com"), "+31|613856964", new DateTime(2004, 1, 19), "Nederland")};


        ListView usersView = new ListView() {
            Y = Pos.Bottom(searchBox) + 1,
            Height = 5,
            Width = Dim.Fill(),
        };

        usersView.SetSource(users);
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
