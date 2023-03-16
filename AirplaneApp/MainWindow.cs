using System;
using System.Text;
using Terminal.Gui;

public class MainWindow : Window
{
    public StringBuilder AirlineText;

    public MainWindow()
    {
        Title = "Rotterdam Airline";
        AirlineText = new StringBuilder();
        AirlineText.AppendLine(@" _____       _   _               _                            _      _ _");
        AirlineText.AppendLine(@"|  __ \     | | | |             | |                     /\   (_)    | (_)");
        AirlineText.AppendLine(@"| |__) |___ | |_| |_ ___ _ __ __| | __ _ _ __ ___      /  \   _ _ __| |_ _ __   ___");
        AirlineText.AppendLine(@"|  _  // _ \| __| __/ _ \ '__/ _` |/ _` | '_ ` _ \    / /\ \ | | '__| | | '_ \ / _ \");
        AirlineText.AppendLine(@"| | \ \ (_) | |_| ||  __/ | | (_| | (_| | | | | | |  / ____ \| | |  | | | | | |  __/");
        AirlineText.AppendLine(@"|_|  \_\___/ \__|\__\___|_|  \__,_|\__,_|_| |_| |_| /_/    \_\_|_|  |_|_|_| |_|\___|");

        Label airlineTextLabel = new Label() {
            Text = AirlineText.ToString(),
        };

        LineView line = new LineView() {
            Y = 7,
        };

        var startingWindow = new LoginMenu(){
            Y = 8,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Colors.Base,
        };

        Add(airlineTextLabel, line, startingWindow);
    }
}