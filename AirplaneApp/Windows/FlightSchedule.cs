using System;
using Terminal.Gui;

public class FlightSchedule : Toplevel
{
    public FlightSchedule()
    {
        Button goBackButton = new Button() {
        Text = "Terug"
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};

        Add(goBackButton);
    }
}