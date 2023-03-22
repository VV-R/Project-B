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

        Button infoButton = new Button() {
            Text = "Mijn gegevens",
            Y = Pos.Bottom(nameLabel) + 1,
        };

        infoButton.Clicked += () => { WindowManager.SetWindow(this, new EditUserInfo(name)); };

        Button goBackButton = new Button() {
            Text = "Uitloggen",
            Y = Pos.Bottom(infoButton) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.SetWindow(this, new LoginMenu()); };

        Add(nameLabel, infoButton, goBackButton);
    }
}
