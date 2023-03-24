using Terminal.Gui;

Application.Init();

try {
    Application.Driver.SetCursorVisibility(CursorVisibility.Underline);
    Application.Run<MainWindow>();
}
finally {
    Application.Shutdown();
}