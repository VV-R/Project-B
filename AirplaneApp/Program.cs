using Terminal.Gui;

Application.Init();

try {
    Application.Driver.SetCursorVisibility(CursorVisibility.Box);
    Application.Run<MainWindow>();
}
finally {
    Application.Shutdown();
}