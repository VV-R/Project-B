using System;
using System.Text;
using Terminal.Gui;

public class MainWindow : Window
{
    public StringBuilder AirlineText;
    public static Button LoginButton = new Button();

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

        Label currentTime = new Label() {
            Text = WindowManager.CurrentTime,
            X = Pos.AnchorEnd(21),
        };

        Application.MainLoop.AddTimeout(TimeSpan.FromSeconds(1), (e) => { currentTime.Text = WindowManager.CurrentTime; return true; });

        Button aboutUs = new Button() {
            Text = "Contact en Over Ons",
            X = Pos.AnchorEnd(25),
            Y = Pos.At(6),
        };

        aboutUs.Clicked += () => { WindowManager.GoForwardOne(new AboutUs()); };

        LoginButton = new Button() {
            Text = "Inloggen",
            X = Pos.Left(aboutUs) - 14,
            Y = 6
        };

        LoginButton.Clicked += () => {
            if (LoginButton.Text == "Inloggen") {
                WindowManager.GoForwardOne(new LoginScreen());
            }
            else if (LoginButton.Text == "Uitloggen") {
                WindowManager.CurrentColor = Colors.Base;
                WindowManager.GoForwardOne(new MainMenu());
                LoginButton.Text = "Inloggen";
            } };

        LineView line = new LineView() {
            Y = 7,
        };

        Add(airlineTextLabel, currentTime, line, WindowManager.CurrentWindow, aboutUs, LoginButton);
    }
}