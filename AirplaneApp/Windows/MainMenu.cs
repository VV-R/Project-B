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

        List<Flight> flights = new List<Flight>() {
            new Flight("0", 1, "Rotterdam", new DateTime(2023, 3, 4), "Parijs", new DateTime(2023, 4, 5)),
            new Flight("1", 2, "Rotterdam", new DateTime(2023, 3, 4), "Madrid", new DateTime(2023, 8, 4)),
            new Flight("2", 3, "Rotterdam", new DateTime(2023, 3, 4), "Berlijn", new DateTime(2023, 4, 6)),
            new Flight("3", 1, "Rotterdam", new DateTime(2023, 3, 6), "Parijs", new DateTime(2023, 3, 20))
        };

        Button flightScheduleButton = new Button() {
            Text = "Vlucht Schemas",
            Y = Pos.Bottom(bookingButton) + 1,
        };
        flightScheduleButton.Clicked += () => { WindowManager.GoForwardOne(new FlightPanel(flights)); };

        Button airplaneInformationButton = new Button()
        {
            Text = "Vliegtuig Informatie",
            Y = Pos.Bottom(flightScheduleButton) + 1,
        };
        airplaneInformationButton.Clicked += () => { WindowManager.GoForwardOne(new AirplaneInformation()); };

        Button Test = new Button()
        {
            Text = "Test",
            Y = Pos.Bottom(airplaneInformationButton) + 1,
        };

        Test.Clicked += () => { WindowManager.GoForwardOne(new SeattingPlan()); };

        Button exitButton = new Button()
        {
            Text = "Afsluiten",
            Y = Pos.Bottom(airplaneInformationButton) + 1 + 2,
        };

        exitButton.Clicked += () => { Application.RequestStop(); };

        Add(bookingButton, flightScheduleButton, airplaneInformationButton, exitButton, Test);
    }
}