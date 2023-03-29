using System;
using Terminal.Gui;

public class AirplaneInformation : Toplevel
{
    public AirplaneInformation()
    {
        Button goBackButton = new Button() {
            Text = "Terug"
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};

        Add(goBackButton);
    }
}