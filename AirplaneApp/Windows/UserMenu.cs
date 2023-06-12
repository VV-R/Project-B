using System;
using Terminal.Gui;
using Managers;
using Entities;


namespace Windows;
public class UserMenu : Toplevel
{
    public UserMenu(User user)
    {
        Label nameLabel = new Label(){
            Text = $"Welkom {user.UserInfo.FirstName}!",
        };

        Button bookingButton = new Button()
        {
            Text = "Vlucht Boeken",
            Y = Pos.Bottom(nameLabel) + 1,
        };
        bookingButton.Clicked += () => { WindowManager.GoForwardOne(new FlightPanelUser(WindowManager.Flights)); };

        Button flightScheduleButton = new Button() {
            Text = "Vlucht Schemas",
            Y = Pos.Bottom(bookingButton) + 1,
        };
        flightScheduleButton.Clicked += () => { WindowManager.GoForwardOne(new FlightPanel(WindowManager.Flights)); };


        Button airplaneInformationButton = new Button()
        {
            Text = "Vliegtuig Informatie",
            Y = Pos.Bottom(flightScheduleButton) + 1,
        };
        airplaneInformationButton.Clicked += () => { WindowManager.GoForwardOne(new AirplaneInformation()); };

        Button infoButton = new Button() {
            Text = "Mijn gegevens",
            Y = Pos.Bottom(airplaneInformationButton) + 1,
        };

        infoButton.Clicked += () => { WindowManager.GoForwardOne(new EditUserInfo()); };

        Button searchReservation = new Button() {
            Text = "Reserveringen",
            Y = Pos.Bottom(infoButton) + 1,
        };

        searchReservation.Clicked += () => {WindowManager.GoForwardOne(new SearchReservationUser()); };

        Button exitButton = new Button()
        {
            Text = "Afsluiten",
            Y = Pos.Bottom(searchReservation) + 1,
        };

        exitButton.Clicked += () => { Application.RequestStop(); };

        Add(nameLabel, bookingButton, flightScheduleButton, airplaneInformationButton, infoButton, searchReservation, exitButton);
    }
}
