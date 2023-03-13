using System;
using Terminal.Gui;


public class MainMenu : Toplevel
{
    // public MainMenu(User user)
    public MainMenu(string name)
    {
        Label nameLabel = new Label(){
            Text = $"Hello {name}!",
        };

        Button goBackButton = new Button() {
            Text = name == "guest" ? "Terug" : "Uitloggen",
            Y = Pos.Bottom(nameLabel),
        };

        goBackButton.Clicked += () => { WindowManager.SetWindow(this, new LoginMenu()); };

        Add(nameLabel, goBackButton);
    }
}
