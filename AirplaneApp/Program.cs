using Terminal.Gui;
using Windows;
using System.IO;

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
    Colors.ColorSchemes.Add("TextCorrect", new ColorScheme() { Focus = Terminal.Gui.Attribute.Make(Color.Green, Color.Gray)});
    Colors.ColorSchemes.Add("TextIncorrect", new ColorScheme() { Focus = Terminal.Gui.Attribute.Make(Color.Red, Color.Gray)});
    Colors.TopLevel.Focus = Application.Driver.MakeAttribute(Color.Black, Color.Gray);
    try {
        Application.Run<MainWindow>();
    }
    finally
    {
        Application.Shutdown();
    }
}
catch (Exception e) {
    string path = $"logs/{DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss")}.log";
    if (!Directory.Exists("logs")) {
        Directory.CreateDirectory("logs");
    }
    using (var writer = new StreamWriter(path)) {
        writer.Write(e.ToString());
    }
    Console.WriteLine($"The program crashed, please read '{path}' for more info.");
}

