using System;
using Terminal.Gui;

public class MainMenu : Toplevel
{
    public MainMenu()
    {
        Button bookingButton = new Button()
        {
            Text = "Vlucht Boeken",
        };
        bookingButton.Clicked += () => { WindowManager.GoForwardOne(new Booking()); };

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

        Button Test = new Button()
        {
            Text = "Vliegtuig plattegronden",
            Y = 10,
        };

        Test.Clicked += () => { WindowManager.GoForwardOne(new SeattingPlan()); };

        Button exitButton = new Button()
        {
            Text = "Afsluiten",
            Y = Pos.Bottom(airplaneInformationButton) + 1,
        };

        exitButton.Clicked += () => { Application.RequestStop(); };

        Add(bookingButton, flightScheduleButton, airplaneInformationButton, exitButton, Test);
    }
}