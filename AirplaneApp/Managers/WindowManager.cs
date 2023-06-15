using Terminal.Gui;
using Db;
using Entities;
using Windows;

namespace Managers;
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
    
    public static List<Flight> Flights;

    public static List<string> Locations = new List<string>() {"London", "Parijs", "München", "Wenen", "Rome", "Barcelona", "Brussel", "Rotterdam", "Madrid"};

    static WindowManager()
    {
        Locations.Sort();
        _windows.Add(_firstWindow);
        using (var context = new ApplicationDbContext()) {
            Flights = context.Flights.Where(f => f.ArrivalTime.AddMinutes(30) >= DateTime.Now && !f.IsCancelled).ToList();
        }
        Flights.Sort();
    }
    private static void SetWindow(Toplevel oldWindow, Toplevel newWindow)
    {
        Application.MainLoop.Invoke(() => {
            Application.Current.Remove(oldWindow);
            newWindow.Y = 8;
            newWindow.Width = Console.WindowWidth;
            newWindow.Height = Console.WindowHeight;
            newWindow.Width = Dim.Fill();
            newWindow.Height = Dim.Fill();
            newWindow.ColorScheme = CurrentColor;
            Application.Current.Add(newWindow);
            Application.Current.SetNeedsDisplay();
        });

    }
    public static void GoBackOne(Toplevel oldwindow) {
        if (_windows.Count == 1)
            Application.RequestStop();
        else {
            _windows.Remove(oldwindow);
            SetWindow(oldwindow, _windows.Last());
        }
    }
    public static void GoBackOne() {
        if (CurrentWindow is UserMenu) {
            MessageBox.Query("Uitgelogd", "U bent nu uitgelogd", "Ok");
            MainWindow.LoginButton.Text = "Inloggen";
            CurrentUser = null;
        }
        GoBackOne(CurrentWindow);
    }

    public static void GoForwardOne(Toplevel newwindow) {
        SetWindow(_windows.Last(), newwindow);
        _windows.Add(newwindow);
    }

    public static void GoToFirst() {
        if (CurrentUser == null) {
            SetWindow(CurrentWindow, _firstWindow);
            _windows = new List<Toplevel>() { _firstWindow};
        } else {
            var window = new UserMenu(CurrentUser);
            SetWindow(CurrentWindow, window);
            _windows = new List<Toplevel>() { _firstWindow, window };
        }
    }
}
