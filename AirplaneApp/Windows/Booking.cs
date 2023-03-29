using System;
using Terminal.Gui;

public class Booking : Toplevel
{
    public Booking()
    {
        Button goBackButton = new Button() {
            Text = "Terug"
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};

        Add(goBackButton);
    }
}