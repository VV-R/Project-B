using Terminal.Gui;

Application.Init();

try
{
    Application.Driver.SetCursorVisibility(CursorVisibility.Underline);
    Colors.ColorSchemes.Add("Economy", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightGreen) });
    Colors.ColorSchemes.Add("Economy Plus", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Cyan) });
    Colors.ColorSchemes.Add("Comfort", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightMagenta) });
    Colors.ColorSchemes.Add("Front seats", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Magenta) });
    Colors.ColorSchemes.Add("Duo seats", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightBlue) });
    Colors.ColorSchemes.Add("Club Class", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.DarkGray) });
    Colors.ColorSchemes.Add("United BusinessFirst", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Brown) });
    Colors.ColorSchemes.Add("SeatSelected", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightYellow) });
    Colors.ColorSchemes.Add("SeatTaken", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightRed) });
    Colors.TopLevel.Focus = Application.Driver.MakeAttribute(Color.Black, Color.Gray);
    Application.Run<MainWindow>();
}
finally
{
    Application.Shutdown();
}