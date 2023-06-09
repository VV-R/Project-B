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
    public static int MaxSeats;
    public static int SeatCount = 0;

    public InteraciveSeat(Seat seat, bool occupied)
    {
        Seat = seat;
        Text = Seat.Text;
        X = Seat.X;
        Y = Seat.Y;
        Occupied = occupied;
        if (Occupied) {
            ColorScheme = Colors.ColorSchemes["SeatTaken"];
            DefualtColor = Colors.ColorSchemes["SeatTaken"];
        } else {
            var result = seat.SeatType switch {
                "Economy Plus" => Colors.ColorSchemes["Economy Plus"],
                "Comfort" => Colors.ColorSchemes["Comfort"],
                "Front cabin seat" => Colors.ColorSchemes["Front cabin seat"],
                "Duo seats" => Colors.ColorSchemes["Duo seats"],
                "Club Class" => Colors.ColorSchemes["Club Class"],
                "United BusinessFirst" => Colors.ColorSchemes["United BusinessFirst"],
                _ => Colors.ColorSchemes["Economy"],
            };

            ColorScheme = result;
            DefualtColor = result;
        }
    }

    public override void OnClicked()
    {
        if (!Occupied)
        {
            if (!IsClicked && InteraciveSeat.SeatCount < InteraciveSeat.MaxSeats)
            {
                ColorScheme = Colors.ColorSchemes["SeatSelected"];
                InteraciveSeat.SeatCount++;
                IsClicked = !IsClicked;
            }
            else if (IsClicked && InteraciveSeat.SeatCount <= InteraciveSeat.MaxSeats)
            {
                if (ColorScheme != DefualtColor)
                    InteraciveSeat.SeatCount--;
                ColorScheme = DefualtColor;
                IsClicked = !IsClicked;
            }
        }
    }
}