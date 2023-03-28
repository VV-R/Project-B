using System;
using Terminal.Gui;


public class AdminMenu : Toplevel
{
    // public AdminMenu(User user)
    public AdminMenu(string name)
    {
        Label nameLabel = new Label(){
            Text = $"Admin panel",
        };

        Button searchUsers = new Button() {
            Text = "Klanten zoeken",
            Y = Pos.Bottom(nameLabel) + 1,
        };

        Button searchReservation = new Button() {
            Text = "Reserveringen zoeken",
            Y = Pos.Bottom(searchUsers) + 1,
        };

        Button flightSchedule = new Button() {
            Text = "Vluchtschemas",
            Y = Pos.Bottom(searchReservation) + 1,
        };

        Button goBackButton = new Button() {
            Text = "Uitloggen",
            Y = Pos.Bottom(flightSchedule) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.currentColor = Colors.Base; WindowManager.SetWindow(this, new LoginMenu()); };

        Add(nameLabel, searchUsers, searchReservation, flightSchedule, goBackButton);
    }
}
