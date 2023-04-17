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

        Button boeing737Button = new Button() {

            Text = "Boeing 737"
        };


        Button airbusButton = new Button() {
            Text = "Airbus 330-200"
        };

        Button boeing787Button = new Button() {
            Text = "Boeing 787"
        };
        Add(boeing737Button,airbusButton,boeing787Button);
    }
}