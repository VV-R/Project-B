using System;
using Terminal.Gui;
using Newtonsoft.Json;

public class SeattingPlan : Toplevel
{
    public SeattingPlan()
    {
        var colorForground = Color.Black;
        var colorBackground = Color.Green;
        bool clicked = false;

        Button goBackButton = new Button()
        {
            Text = "Terug"
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Label Test = new Label()
        {
            Text = "test",
            Y = Pos.Bottom(goBackButton) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        Test.Clicked += () =>
        {
            clicked = !clicked;

            if (clicked)
            {
                Test.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            }
            else if (!clicked)
            {
                Test.ColorScheme = Colors.ColorSchemes["SeatOpen"];
            }
        };

        Add(goBackButton, Test);
    }
}