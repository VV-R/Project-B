using System;
using Terminal.Gui;


public class UserMenu : Toplevel
{
    private User user;
    public UserMenu(User user)
    {
        this.user = user;
        Label nameLabel = new Label(){
            Text = $"Welkom {WindowManager.CurrentUser.FirstName}!",
        };

        Button bookingButton = new Button()
        {
            Text = "Vlucht Boeken",
            Y = Pos.Bottom(nameLabel) + 1,
        };
        bookingButton.Clicked += () => { WindowManager.GoForwardOne(new Booking(user)); };

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
            Y = Pos.Bottom(airplaneInformationButton) + 1,
        };

        Test.Clicked += () => { WindowManager.GoForwardOne(new SeattingPlan()); };

        Button infoButton = new Button() {
            Text = "Mijn gegevens",
            Y = Pos.Bottom(Test) + 1,
        };
        
        infoButton.Clicked += () => { WindowManager.GoForwardOne(new EditUserInfo(WindowManager.CurrentUser)); };

        Button exitButton = new Button()
        {
            Text = "Afsluiten",
            Y = Pos.Bottom(infoButton   ) + 1,
        };

        exitButton.Clicked += () => { Application.RequestStop(); };

        Add(nameLabel, bookingButton, flightScheduleButton, airplaneInformationButton,Test, infoButton, exitButton);
    }
}
