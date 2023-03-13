using System;
using Terminal.Gui;

public static class WindowManager
{
    public static void SetWindow(Toplevel oldWindow, Toplevel newWindow)
    {
        Application.MainLoop.Invoke(() => {
            Application.Current.Remove(oldWindow);
            newWindow.Y = 8;
            newWindow.Width = 238;
            newWindow.Height = 53;
            newWindow.ColorScheme = Colors.Base;
            Application.Current.Add(newWindow);
            Application.Current.SetNeedsDisplay();
        });
    }
}