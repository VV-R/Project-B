using Terminal.Gui;

Application.Init();

try
{
    Application.Driver.SetCursorVisibility(CursorVisibility.Underline);
    Colors.ColorSchemes.Add("SeatOpen", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Green) });
    Colors.ColorSchemes.Add("SeatSelected", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightYellow) });
    Colors.ColorSchemes.Add("SeatTaken", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightRed) });
    Colors.TopLevel.Focus = Application.Driver.MakeAttribute(Color.Black, Color.Gray);
    Application.Run<MainWindow>();
}
finally
{
    Application.Shutdown();
}