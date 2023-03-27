using System;
using Terminal.Gui;


public class UserMenu : Toplevel
{
    // public MainMenu(User user)
    public UserMenu(string name)
    {
        Label nameLabel = new Label(){
            Text = $"Welkom {name}!",
        };

        Button goBackButton = new Button() {
            Text = name == "guest" ? "Terug" : "Uitloggen",
            Y = Pos.Bottom(nameLabel),
        };

        goBackButton.Clicked += () => { WindowManager.SetWindow(this, new LoginMenu()); };

        Add(nameLabel, goBackButton);
    }
}