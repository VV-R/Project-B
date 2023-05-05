using Terminal.Gui;

public class Seat
{
    public string Text;
    public int X;
    public int Y;
    public string SeatType;

    public Seat(string text, int x, int y, string seatType)
    {
        Text = text;
        X = x;
        Y = y;
        SeatType = seatType;
    }
}

public class InteraciveSeat : Label
{
    public bool IsClicked = false;
    public bool Occupied;
    public Seat Seat;

    public InteraciveSeat(Seat seat, bool occupied)
    {
        Seat = seat;
        Text = Seat.Text;
        X = Seat.X;
        Y = Seat.Y;
        Occupied = occupied;
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