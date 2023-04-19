using System;
using Terminal.Gui;


public class UserMenu : Toplevel
{
    public UserMenu()
    {
        Label nameLabel = new Label(){
            Text = $"Welkom {WindowManager.CurrentUser.FirstName}!",
        };

        Button infoButton = new Button() {
            Text = "Mijn gegevens",
            Y = Pos.Bottom(nameLabel) + 1,
        };

        infoButton.Clicked += () => { WindowManager.GoForwardOne(new EditUserInfo(WindowManager.CurrentUser)); };

        Add(nameLabel, infoButton);
    }
}
