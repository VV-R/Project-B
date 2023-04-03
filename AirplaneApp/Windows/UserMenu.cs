using System;
using Terminal.Gui;


public class UserMenu : Toplevel
{
    public UserMenu(User user)
    {
        Label nameLabel = new Label(){
            Text = $"Welkom {user.FirstName}!",
        };

        Button infoButton = new Button() {
            Text = "Mijn gegevens",
            Y = Pos.Bottom(nameLabel) + 1,
        };

        infoButton.Clicked += () => { WindowManager.GoForwardOne(new EditUserInfo(user)); };

        Add(nameLabel, infoButton);
    }
}
