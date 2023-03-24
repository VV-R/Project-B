using System;
using Terminal.Gui;


public class AdminMenu : Toplevel
{
    // public MainMenu(User user)
    public AdminMenu(string name)
    {
        Label nameLabel = new Label(){
            Text = $"Welkom {name}!",
        };

        Button goBackButton = new Button() {
            Text = name == "guest" ? "Terug" : "Uitloggen",
            Y = Pos.Bottom(nameLabel) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.currentColor = Colors.Base; WindowManager.SetWindow(this, new LoginMenu()); };

        Add(nameLabel, goBackButton);
    }
}
