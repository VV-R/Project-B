using System;
using Terminal.Gui;

public static class WindowManager
{
    public static ColorScheme currentColor {get {return Application.Current.ColorScheme;} set {Application.Current.ColorScheme = value;}}
    public static string CurrentTime {get {return DateTime.Now.ToString();}}
    public static Toplevel PreviousWindow = new Toplevel();
    public static void SetWindow(Toplevel oldWindow, Toplevel newWindow)
    {
        Application.MainLoop.Invoke(() => {
            Application.Current.Remove(oldWindow);
            newWindow.Y = 8;
            newWindow.Width = 238;
            newWindow.Height = 53;
            newWindow.ColorScheme = currentColor;
            PreviousWindow = oldWindow;
            Application.Current.Add(newWindow);
            Application.Current.SetNeedsDisplay();
        });
    }

    public static void GoBackOne(Toplevel oldwindow) => SetWindow(oldwindow, PreviousWindow);
}