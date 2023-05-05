using System;
using Terminal.Gui;
using Newtonsoft.Json;

public class SeattingPlan : Toplevel
{
    ComboBox PlaneType;
    public SeattingPlan()
    {

        Button goBackButton = new Button()
        {
            Text = "Terug"
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        PlaneType = new ComboBox()
        {
            Y = Pos.Bottom(goBackButton) + 1,
            Width = 20,
            Height = 7
        };
        PlaneType.SetSource(new List<string>() { "Boeing 737", "Airbus 330", "Boeing 787" });

        Button testButton = new Button()
        {
            Text = "Test Button",
            Y = Pos.Bottom(goBackButton) + 3
        };

        testButton.Clicked += () =>
        {
            if (PlaneType.Text == "Boeing 737")
            {
                PlanBoeing_737();
            }
            else if (PlaneType.Text == "Airbus 330")
            {
                PlanAirbus_330();
            }
            else if (PlaneType.Text == "Boeing 787")
            {
                PlanBoeing_787();
            }
        };

        Add(goBackButton, testButton, PlaneType);
    }

    private void PlanBoeing_737()
    {
        List<string> occupied = new List<string>() { "F2", "D5", "A7", "A16" };

        StreamReader reader = new StreamReader("Boeing_737.json");
        List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;

        foreach (Seat seat in seats_list)
        {
            if (occupied.Contains(seat.Text))
                Add(new InteraciveSeat(seat, true));
            else
                Add(new InteraciveSeat(seat, false));
        }

        Label rightWall = new Label()
        {
            Text = "--------------------------------------------------------------------------------------------------------------------------------",
            Y = 17,
            X = 34,
        };

        Label leftWall = new Label()
        {
            Text = "--------------------------------------------------------------------------------------------------------------------------------",
            Y = 25,
            X = 34,
        };

        Add(rightWall, leftWall);
    }

    private void PlanAirbus_330()
    {
        List<string> occupied = new List<string>() { "F23", "D5", "A7", "A16" };

        StreamReader reader = new StreamReader("Airbus_330.json");
        List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;

        foreach (Seat seat in seats_list)
        {
            if (occupied.Contains(seat.Text))
                Add(new InteraciveSeat(seat, true));
            else
                Add(new InteraciveSeat(seat, false));
        }

        Label rightWall = new Label()
        {
            Text = "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
            Y = 22,
            X = 5,
        };

        Label leftWall = new Label()
        {
            Text = "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
            Y = 10,
            X = 5,
        };

        Add(rightWall, leftWall);
    }

    private void PlanBoeing_787()
    {
        List<string> occupied = new List<string>() { "F23", "D5", "A7", "A16" };

        StreamReader reader = new StreamReader("Boeing_787.json");
        List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;

        foreach (Seat seat in seats_list)
        {
            if (occupied.Contains(seat.Text))
                Add(new InteraciveSeat(seat, true));
            else
                Add(new InteraciveSeat(seat, false));
        }

        Label rightWall = new Label()
        {
            Text = "------------------------------------------------------------------------------------------------------------------------------------",
            Y = 10,
            X = 41,
        };

        Label leftWall = new Label()
        {
            Text = "------------------------------------------------------------------------------------------------------------------------------------",
            Y = 22,
            X = 41,
        };

        Add(rightWall, leftWall);
    }
}