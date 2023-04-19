using Terminal.Gui;

Application.Init();

try {
    Application.Driver.SetCursorVisibility(CursorVisibility.Underline);
    Colors.TopLevel.Focus = Application.Driver.MakeAttribute(Color.Black, Color.Gray);
    Application.Run<MainWindow>();
}
finally {
    Application.Shutdown();
}