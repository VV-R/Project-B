using Terminal.Gui;

namespace Entities;
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
    ColorScheme DefualtColor;
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
        if (Occupied)
        {
            ColorScheme = Colors.ColorSchemes["SeatTaken"];
        }
        else
        {
            var result = seat.SeatType switch
            {
                "Economy" => Colors.ColorSchemes["Economy"],
                "Economy Plus" => Colors.ColorSchemes["Economy Plus"],
                "Comfort" => Colors.ColorSchemes["Comfort"],
                "Front cabin seat" => Colors.ColorSchemes["Front seats"],
                "Duo seats" => Colors.ColorSchemes["Duo seats"],
                "Club Class" => Colors.ColorSchemes["Club Class"],
                "United BusinessFirst" => Colors.ColorSchemes["United BusinessFirst"],
            };

            ColorScheme = result;
            DefualtColor = result;
        }
    }

    public override void OnClicked()
    {
        IsClicked = !IsClicked;
        if (!Occupied)
        {
            if (IsClicked)
                ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!IsClicked)
                ColorScheme = DefualtColor;
        }
    }
}