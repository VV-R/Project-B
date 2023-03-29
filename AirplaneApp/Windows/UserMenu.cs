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

        infoButton.Clicked += () => { WindowManager.GoForwardOne(new EditUserInfo(name)); };

        Add(nameLabel, infoButton);
    }
}
