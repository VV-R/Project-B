using System;
using System.Net.Mail;
using Terminal.Gui;

public static class WindowManager
{
    private static List<Toplevel> _windows = new List<Toplevel> () {
        new MainMenu() {
            Y = 8,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = Colors.Base}
    };
    public static ColorScheme CurrentColor {get {return Application.Current.ColorScheme;} set {Application.Current.ColorScheme = value;}}
    public static string CurrentTime {get {return DateTime.Now.ToString();}}
    public static Toplevel CurrentWindow {get {return _windows.Last();}}
    public static User? CurrentUser = null;

    public static List<Flight> Flights = new List<Flight>() {
        new Flight(0, 1, "Rotterdam", new DateTime(2023, 3, 4, 12, 13, 00), "Parijs", new DateTime(2023, 4, 5), "Boeing 737"),
        new Flight(1, 2, "Rotterdam", new DateTime(2023, 3, 4, 14, 20, 00), "Madrid", new DateTime(2023, 8, 4), "Airbus 330"),
        new Flight(2, 3, "Rotterdam", new DateTime(2023, 3, 4, 16, 20, 00), "Berlijn", new DateTime(2023, 4, 6), "Boeing 737"),
        new Flight(3, 1, "Rotterdam", new DateTime(2023, 3, 6, 8, 30, 00), "Parijs", new DateTime(2023, 3, 20), "Boeing 787")
    };

    public static List<string> Locations = new List<string>() {"London", "Parijs", "Amsterdam", "München", "Wenen", "Rome", "Barcelona", "Brussels", "Berlijn", "Rotterdam", "Madrid"};

    static WindowManager()
    {
        Locations.Sort();
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

}