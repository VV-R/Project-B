using Terminal.Gui;

Application.Init();

try
{
    Application.Driver.SetCursorVisibility(CursorVisibility.Underline);
    Colors.ColorSchemes.Add("SeatOpen", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Green) });
    Colors.ColorSchemes.Add("SeatSelected", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightYellow) });
    Colors.ColorSchemes.Add("SeatTaken", new ColorScheme() { Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.BrightRed) });
    Colors.ColorSchemes.Add("TextCorrect", new ColorScheme() { Focus = Terminal.Gui.Attribute.Make(Color.Green, Color.Gray)});
    Colors.ColorSchemes.Add("TextIncorrect", new ColorScheme() { Focus = Terminal.Gui.Attribute.Make(Color.Red, Color.Gray)});
    Colors.TopLevel.Focus = Application.Driver.MakeAttribute(Color.Black, Color.Gray);
    Application.Run<MainWindow>();
}
finally
{
    Application.Shutdown();
}