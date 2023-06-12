using System;
using Terminal.Gui;
using Managers;
using Entities;

namespace Windows;
public class MainMenu : Toplevel
{
    public MainMenu()
    {
        
        Button bookingButton = new Button()
        {
            Text = "Vlucht Boeken",
        };

        bookingButton.Clicked += () => { WindowManager.GoForwardOne(new FlightPanelUser(WindowManager.Flights)); };

        Button airplaneInformationButton = new Button()
        {
            Text = "Vliegtuig Informatie",
            Y = Pos.Bottom(bookingButton) + 1,
        };
        airplaneInformationButton.Clicked += () => { WindowManager.GoForwardOne(new AirplaneInformation()); };
           
        Button searchReservationButton = new Button() {
            Text = "Reservering Zoeken",
            Y = Pos.Bottom(airplaneInformationButton) + 1
        };
        
        searchReservationButton.Clicked += () => { WindowManager.GoForwardOne(new SearchReservationGuest()); };

        Button exitButton = new Button()
        {
            Text = "Afsluiten",
            Y = Pos.Bottom(searchReservationButton) + 1,
        };

        exitButton.Clicked += () => { Application.RequestStop(); };

        Add(bookingButton, airplaneInformationButton, searchReservationButton, exitButton);
    }
}
