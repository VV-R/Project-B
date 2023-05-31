using System;
using System.Text;
using Managers;
using Terminal.Gui;

namespace Windows;
public class MainWindow : Window
{
    public static Button LoginButton = new Button();

    public MainWindow()
    {
        Title = "Rotterdam Airline";
        var airlineText = new StringBuilder();
        airlineText.AppendLine(@" _____       _   _               _                            _      _ _");
        airlineText.AppendLine(@"|  __ \     | | | |             | |                     /\   (_)    | (_)");
        airlineText.AppendLine(@"| |__) |___ | |_| |_ ___ _ __ __| | __ _ _ __ ___      /  \   _ _ __| |_ _ __   ___");
        airlineText.AppendLine(@"|  _  // _ \| __| __/ _ \ '__/ _` |/ _` | '_ ` _ \    / /\ \ | | '__| | | '_ \ / _ \");
        airlineText.AppendLine(@"| | \ \ (_) | |_| ||  __/ | | (_| | (_| | | | | | |  / ____ \| | |  | | | | | |  __/");
        airlineText.AppendLine(@"|_|  \_\___/ \__|\__\___|_|  \__,_|\__,_|_| |_| |_| /_/    \_\_|_|  |_|_|_| |_|\___|");

        Label airlineTextLabel = new Label() {
            Text = airlineText.ToString(),
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


        Button dashboardButton = new Button() {
            Text = "Dashboard",
            X = Pos.Left(aboutUs) - 14,
            Y = 6
        };

        dashboardButton.Clicked += () => { WindowManager.GoForwardOne(new Dashboard()); };

        LoginButton = new Button() {
            Text = "Inloggen",
            X = Pos.Left(dashboardButton) - 14,
            Y = 6
        };

        LoginButton.Clicked += () => {
            if (LoginButton.Text == "Inloggen") {
                WindowManager.GoForwardOne(new LoginScreen());
            }
            else if (LoginButton.Text == "Uitloggen") {
                WindowManager.CurrentUser = null;
                WindowManager.CurrentColor = Colors.Base;
                WindowManager.GoToFirst();
                LoginButton.Text = "Inloggen";
            } };

        LineView line = new LineView() {
            Y = 7,
        };

        Application.RootKeyEvent += (key) => {if (key.Key == Key.Esc)  {WindowManager.GoBackOne(); return true; } return false; };

        Add(airlineTextLabel, currentTime, line, WindowManager.CurrentWindow, aboutUs, dashboardButton, LoginButton);
    }
}
