using System;
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