using System;
using System.Net.Mail;
using Terminal.Gui;

public static class WindowManager
{
    private static List<Toplevel> _windows = new List<Toplevel>();

    private static MainMenu _firstWindow = new MainMenu() {
            Y = 8,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Colors.Base};

    public static ColorScheme CurrentColor {get {return Application.Current.ColorScheme;} set {Application.Current.ColorScheme = value;}}
    public static string CurrentTime {get {return DateTime.Now.ToString();}}
    public static Toplevel CurrentWindow {get {return _windows.Last();}}
    public static User? CurrentUser = null;

    public static List<Flight> Flights = new List<Flight>() {
        new Flight(0, 1, "Rotterdam", DateTime.Now.AddDays(1), "Parijs", DateTime.Now.AddDays(2), "Boeing 737"),
        new Flight(1, 2, "Rotterdam", DateTime.Now.AddDays(1), "Madrid", DateTime.Now.AddDays(1.2), "Airbus 330"),
        new Flight(2, 3, "Rotterdam", DateTime.Now.AddDays(1), "Berlijn", DateTime.Now.AddDays(1.1), "Boeing 737"),
        new Flight(3, 1, "Rotterdam", DateTime.Now.AddDays(1), "Parijs", DateTime.Now.AddDays(1.7), "Boeing 787")
    };

    public static List<string> Locations = new List<string>() {"London", "Parijs", "Amsterdam", "München", "Wenen", "Rome", "Barcelona", "Brussels", "Berlijn", "Rotterdam", "Madrid"};

    static WindowManager()
    {
        Locations.Sort();
        _windows.Add(_firstWindow);
    }
    private static void SetWindow(Toplevel oldWindow, Toplevel newWindow)
    {
        Application.MainLoop.Invoke(() => {
            Application.Current.Remove(oldWindow);
            newWindow.Y = 8;
            newWindow.Width = 238;
            newWindow.Height = 53;
            newWindow.Width = Dim.Fill();
            newWindow.Height = Dim.Fill();
            newWindow.ColorScheme = CurrentColor;
            Application.Current.Add(newWindow);
            Application.Current.SetNeedsDisplay();
        });

    }
    public static void GoBackOne(Toplevel oldwindow) {
        _windows.Remove(oldwindow);
        SetWindow(oldwindow, _windows.Last());
    }
    public static void GoForwardOne(Toplevel newwindow) {
        SetWindow(_windows.Last(), newwindow);
        _windows.Add(newwindow);
    }

    public static void GoToFirst() {
        SetWindow(CurrentWindow, _firstWindow);
        _windows = new List<Toplevel>() { _firstWindow};
    }
}