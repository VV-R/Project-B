using System;
using Terminal.Gui;

public static class WindowManager
{
    public static ColorScheme currentColor = Colors.Base;
    public static string CurrentTime {get {return DateTime.Now.ToString();}}
    public static void SetWindow(Toplevel oldWindow, Toplevel newWindow)
    {
        Application.MainLoop.Invoke(() => {
            Application.Current.Remove(oldWindow);
            newWindow.Y = 8;
            newWindow.Width = 238;
            newWindow.Height = 53;
            newWindow.ColorScheme = currentColor;
            Application.Current.Add(newWindow);
            Application.Current.ColorScheme = currentColor;
            Application.Current.SetNeedsDisplay();
        });
    }
}