using System;
using Terminal.Gui;
using Managers;

namespace Windows;
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