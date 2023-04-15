using Terminal.Gui;

public class Seat
{
    public string Text;
    public int X;
    public int Y;

    public Seat(string text, int x, int y)
    {
        Text = text;
        X = x;
        Y = y;
    }
}

public class InteraciveSeat : Label
{
    public bool IsClicked = false;
    public bool Occupied = false;
    public Seat Seat;

    public InteraciveSeat(Seat seat)
    {
        Seat = seat;
        Text = Seat.Text;
        X = Seat.X;
        Y = Seat.Y;
        ColorScheme = Occupied ? Colors.ColorSchemes["SeatTaken"] : Colors.ColorSchemes["SeatOpen"];
    }

    public override void OnClicked()
    {
        IsClicked = !IsClicked;
        if (!Occupied)
        {
            if (IsClicked)
                ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!IsClicked)
                ColorScheme = Colors.ColorSchemes["SeatOpen"];
        }
    }
}