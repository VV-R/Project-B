using System;
using System.Text;
using Terminal.Gui;
using Newtonsoft.Json;
using Managers;
using Entities;

namespace Windows;
public class SeattingPlan : Toplevel
{
    ComboBox PlaneType;
    public StringBuilder RightWing;
    public StringBuilder LeftWing;
    public StringBuilder Cockpit;
    public StringBuilder Back;
    private int seatsCount;
    private Flight flight;
    private List<UserInfo> userInfos;
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

    public SeattingPlan(Flight flight, List<UserInfo> userInfos, int seatsCount)
    {
        this.seatsCount = seatsCount;
        this.userInfos = userInfos;
        this.flight = flight;
        if (flight.Airplane == "Boeing 737")
            PlanBoeing_737();
        else if (flight.Airplane == "Airbus 330")
            PlanAirbus_330();
        else if (flight.Airplane == "Boeing 787")
            PlanBoeing_787();

        Label maxSeats = new Label()
        {
            Text = $"Maximaal aantal te selecteren stoelen: {InteraciveSeat.MaxSeats}",
            X = 5,
            Y = 2
        };

        Add(maxSeats);
    }

    private void PlanBoeing_737()
    {
        List<string> occupied = new List<string>() { "F2", "D5", "A7", "A16" };

        StreamReader reader = new StreamReader("Boeing_737.json");
        List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;
        List<InteraciveSeat> activeSeats = new();

        foreach (Seat seat in seats_list)
        {
            if (occupied.Contains(seat.Text))
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, true);
                Add(interSeat);
                activeSeats.Add(interSeat);
            }
            else
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, false);
                Add(interSeat);
                activeSeats.Add(interSeat);
            }
        }

        #region Drawing
        RightWing = new StringBuilder();
        RightWing.AppendLine(@"          /               |");
        RightWing.AppendLine(@"         /                |");
        RightWing.AppendLine(@"        /                 |");
        RightWing.AppendLine(@"       /                  |");
        RightWing.AppendLine(@"      /                   |");
        RightWing.AppendLine(@"     /                    |");
        RightWing.AppendLine(@"    /                     |");
        RightWing.AppendLine(@"   /                      |");
        RightWing.AppendLine(@"  /                       |");
        RightWing.AppendLine(@" /                        |");
        RightWing.AppendLine(@"/                         |");
        RightWing.AppendLine(@"---------------------------");

        Label rightWing = new Label()
        {
            Text = RightWing.ToString(),
            Y = 5,
            X = 72
        };

        Label rightWall = new Label()
        {
            Text = "--------------------------------------------------------------------------------------------------------------------------------------",
            Y = 16,
            X = 31,
        };

        Label leftWall = new Label()
        {
            Text = "--------------------------------------------------------------------------------------------------------------------------------------",
            Y = 26,
            X = 31,
        };

        LeftWing = new StringBuilder();
        LeftWing.AppendLine(@"---------------------------");
        LeftWing.AppendLine(@"\                         |");
        LeftWing.AppendLine(@" \                        |");
        LeftWing.AppendLine(@"  \                       |");
        LeftWing.AppendLine(@"   \                      |");
        LeftWing.AppendLine(@"    \                     |");
        LeftWing.AppendLine(@"     \                    |");
        LeftWing.AppendLine(@"      \                   |");
        LeftWing.AppendLine(@"       \                  |");
        LeftWing.AppendLine(@"        \                 |");
        LeftWing.AppendLine(@"         \                |");
        LeftWing.AppendLine(@"          \               |");

        Label leftWing = new Label()
        {
            Text = LeftWing.ToString(),
            Y = 26,
            X = 72
        };

        Cockpit = new StringBuilder();
        Cockpit.AppendLine(@"     /");
        Cockpit.AppendLine(@"    /");
        Cockpit.AppendLine(@"   /");
        Cockpit.AppendLine(@"  /");
        Cockpit.AppendLine(@" /");
        Cockpit.AppendLine(@"(");
        Cockpit.AppendLine(@" \");
        Cockpit.AppendLine(@"  \");
        Cockpit.AppendLine(@"   \");
        Cockpit.AppendLine(@"    \");
        Cockpit.AppendLine(@"     \");

        Label cockpit = new Label()
        {
            Text = Cockpit.ToString(),
            Y = 16,
            X = 28
        };

        Back = new StringBuilder();
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");

        Label back = new Label()
        {
            Text = Back.ToString(),
            Y = 16,
            X = 164
        };

        Add(rightWall, leftWall, rightWing, leftWing, cockpit, back);
        #endregion

        #region Legend
        Label economy1 = new Label()
        {
            Text = "X00",
            X = 63,
            Y = 41,
            ColorScheme = Colors.ColorSchemes["Economy"]
        };

        Label economy2 = new Label()
        {
            Text = " => Economy",
            X = Pos.Right(economy1),
            Y = 41
        };

        Label comfort1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(economy2) + 5,
            Y = 41,
            ColorScheme = Colors.ColorSchemes["Comfort"]
        };

        Label comfort2 = new Label()
        {
            Text = " => Comfort",
            X = Pos.Right(comfort1),
            Y = 41
        };

        Label taken1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(comfort2) + 5,
            Y = 41,
            ColorScheme = Colors.ColorSchemes["SeatTaken"]
        };

        Label taken2 = new Label()
        {
            Text = " => Bezet",
            X = Pos.Right(taken1),
            Y = 41
        };

        Label selected1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(taken2) + 5,
            Y = 41,
            ColorScheme = Colors.ColorSchemes["SeatSelected"]
        };

        Label selected2 = new Label()
        {
            Text = " => Gekozen",
            X = Pos.Right(selected1),
            Y = 41
        };

        Add(economy1, economy2, comfort1, comfort2, taken1, taken2, selected1, selected2);
        #endregion

        Button reservation = new Button()
        {
            Text = "Reserveer",
            Y = 41,
            X = Pos.Right(selected2) + 18
        };

        reservation.Clicked += () =>
        {
            var seats = activeSeats.Where(seat => seat.IsClicked).ToList();
            List<Seat> selectedSeats = new List<Seat>();
            if (seats.Count == 0)
                MessageBox.Query("Geen stoelen geselecteerd", "U heeft geen stoelen gekozen", "Ok");
            else if (InteraciveSeat.SeatCount < InteraciveSeat.MaxSeats)
                MessageBox.ErrorQuery("Te weinig stoelen geselecteerd", "U heeft te weinig stoelen gekozen", "Ok");
            else {            
                seats.ForEach(s => selectedSeats.Add(s.Seat));
                WindowManager.GoForwardOne(new FlightOverview(flight, userInfos, selectedSeats));
            }
        };
        Add(reservation);
    }

    private void PlanAirbus_330()
    {
        List<string> occupied = new List<string>() { "F23", "D5", "A7", "A16" };

        StreamReader reader = new StreamReader("Airbus_330.json");
        List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;
        List<InteraciveSeat> activeSeats = new();

        foreach (Seat seat in seats_list)
        {
            if (occupied.Contains(seat.Text))
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, true);
                Add(interSeat);
                activeSeats.Add(interSeat);
            }
            else
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, false);
                Add(interSeat);
                activeSeats.Add(interSeat);
            }
        }

        #region Drawing
        LeftWing = new StringBuilder();
        LeftWing.AppendLine(@"|                                          \");
        LeftWing.AppendLine(@"|                                           \");
        LeftWing.AppendLine(@"|                                            \");
        LeftWing.AppendLine(@"|                                             \");
        LeftWing.AppendLine(@"|                                              \");
        LeftWing.AppendLine(@"|                                               \");
        LeftWing.AppendLine(@"|                                                \");
        LeftWing.AppendLine(@"---------------------------------------------------");

        Label lefttwing = new Label()
        {
            Text = LeftWing.ToString(),
            Y = 2,
            X = 75
        };

        Label rightWall = new Label()
        {
            Text = "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
            Y = 23,
            X = 6,
        };

        Label leftWall = new Label()
        {
            Text = "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
            Y = 9,
            X = 6,
        };

        RightWing = new StringBuilder();
        RightWing.AppendLine(@"---------------------------------------------------");
        RightWing.AppendLine(@"|                                                /");
        RightWing.AppendLine(@"|                                               /");
        RightWing.AppendLine(@"|                                              /");
        RightWing.AppendLine(@"|                                             /");
        RightWing.AppendLine(@"|                                            /");
        RightWing.AppendLine(@"|                                           /");
        RightWing.AppendLine(@"|                                          /");

        Label rightWing = new Label()
        {
            Text = RightWing.ToString(),
            Y = 23,
            X = 75
        };

        Cockpit = new StringBuilder();
        Cockpit.AppendLine(@"\");
        Cockpit.AppendLine(@" \");
        Cockpit.AppendLine(@"  \");
        Cockpit.AppendLine(@"   \");
        Cockpit.AppendLine(@"    \");
        Cockpit.AppendLine(@"     \");
        Cockpit.AppendLine(@"      \");
        Cockpit.AppendLine(@"       )");
        Cockpit.AppendLine(@"      /");
        Cockpit.AppendLine(@"     /");
        Cockpit.AppendLine(@"    /");
        Cockpit.AppendLine(@"   /");
        Cockpit.AppendLine(@"  /");
        Cockpit.AppendLine(@" /");
        Cockpit.AppendLine(@"/");

        Label cockpit = new Label()
        {
            Text = Cockpit.ToString(),
            Y = 9,
            X = 194
        };

        Back = new StringBuilder();
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");

        Label back = new Label()
        {
            Text = Back.ToString(),
            Y = 9,
            X = 6
        };

        Add(rightWall, leftWall, lefttwing, rightWing, cockpit, back);
        #endregion

        #region Legend
        Label economy1 = new Label()
        {
            Text = "X00",
            X = 27,
            Y = 34,
            ColorScheme = Colors.ColorSchemes["Economy"]
        };

        Label economy2 = new Label()
        {
            Text = " => Economy",
            X = Pos.Right(economy1),
            Y = 34
        };

        Label comfort1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(economy2) + 5,
            Y = 34,
            ColorScheme = Colors.ColorSchemes["Comfort"]
        };

        Label comfort2 = new Label()
        {
            Text = " => Comfort",
            X = Pos.Right(comfort1),
            Y = 34
        };

        Label frontSeats1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(comfort2) + 5,
            Y = 34,
            ColorScheme = Colors.ColorSchemes["Front seats"]
        };

        Label frontSeats2 = new Label()
        {
            Text = " => Stoelen voorin de cabine",
            X = Pos.Right(frontSeats1),
            Y = 34
        };

        Label duo1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(frontSeats2) + 5,
            Y = 34,
            ColorScheme = Colors.ColorSchemes["Duo seats"]
        };

        Label duo2 = new Label()
        {
            Text = " => Duo stoelen",
            X = Pos.Right(duo1),
            Y = 34
        };

        Label clubClass1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(duo2) + 5,
            Y = 34,
            ColorScheme = Colors.ColorSchemes["Club Class"]
        };

        Label clubClass2 = new Label()
        {
            Text = " => Club Class",
            X = Pos.Right(clubClass1),
            Y = 34
        };

        Label taken1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(clubClass2) + 5,
            Y = 34,
            ColorScheme = Colors.ColorSchemes["SeatTaken"]
        };

        Label taken2 = new Label()
        {
            Text = " => Bezet",
            X = Pos.Right(taken1),
            Y = 34
        };

        Label selected1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(taken2) + 5,
            Y = 34,
            ColorScheme = Colors.ColorSchemes["SeatSelected"]
        };

        Label selected2 = new Label()
        {
            Text = " => Gekozen",
            X = Pos.Right(selected1),
            Y = 34
        };

        Add(economy1, economy2, comfort1, comfort2, frontSeats1, frontSeats2, duo1, duo2, clubClass1, clubClass2, taken1, taken2, selected1, selected2);
        #endregion

        Button reservation = new Button()
        {
            Text = "Reserveer",
            Y = 34,
            X = Pos.Right(selected2) + 18
        };

        reservation.Clicked += () =>
        {
            var seats = activeSeats.Where(seat => seat.IsClicked).ToList();
            List<Seat> selectedSeats = new List<Seat>();
            if (seats.Count == 0)
                MessageBox.Query("Geen stoelen geselecteerd", "U heeft geen stoelen gekozen", "Ok");
            else if (InteraciveSeat.SeatCount < InteraciveSeat.MaxSeats)
                MessageBox.ErrorQuery("Te weinig stoelen geselecteerd", "U heeft te weinig stoelen gekozen", "Ok");
            else {            
                seats.ForEach(s => selectedSeats.Add(s.Seat));
                WindowManager.GoForwardOne(new FlightOverview(flight, userInfos, selectedSeats));
            }
        };
        Add(reservation);
    }

    private void PlanBoeing_787()
    {
        List<string> occupied = new List<string>() { "F23", "D5", "A7", "A16" };

        StreamReader reader = new StreamReader("Boeing_787.json");
        List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;
        List<InteraciveSeat> activeSeats = new();

        foreach (Seat seat in seats_list)
        {
            if (occupied.Contains(seat.Text))
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, true);
                Add(interSeat);
                activeSeats.Add(interSeat);
            }
            else
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, false);
                Add(interSeat);
                activeSeats.Add(interSeat);
            }
        }

        #region Drawing
        RightWing = new StringBuilder();
        RightWing.AppendLine(@"    /                                              |");
        RightWing.AppendLine(@"   /                                               |");
        RightWing.AppendLine(@"  /                                                |");
        RightWing.AppendLine(@" /                                                 |");
        RightWing.AppendLine(@"/                                                  |");
        RightWing.AppendLine(@"----------------------------------------------------");

        Label rightwing = new Label()
        {
            Text = RightWing.ToString(),
            Y = 4,
            X = 66
        };

        Label rightWall = new Label()
        {
            Text = "-----------------------------------------------------------------------------------------------------------------------------------------",
            Y = 9,
            X = 38,
        };

        Label leftWall = new Label()
        {
            Text = "-----------------------------------------------------------------------------------------------------------------------------------------",
            Y = 23,
            X = 38,
        };

        LeftWing = new StringBuilder();
        LeftWing.AppendLine(@"----------------------------------------------------");
        LeftWing.AppendLine(@"\                                                  |");
        LeftWing.AppendLine(@" \                                                 |");
        LeftWing.AppendLine(@"  \                                                |");
        LeftWing.AppendLine(@"   \                                               |");
        LeftWing.AppendLine(@"    \                                              |");

        Label leftwing = new Label()
        {
            Text = LeftWing.ToString(),
            Y = 23,
            X = 66
        };

        Cockpit = new StringBuilder();
        Cockpit.AppendLine(@"       /");
        Cockpit.AppendLine(@"      /");
        Cockpit.AppendLine(@"     /");
        Cockpit.AppendLine(@"    /");
        Cockpit.AppendLine(@"   /");
        Cockpit.AppendLine(@"  /");
        Cockpit.AppendLine(@" /");
        Cockpit.AppendLine(@"(");
        Cockpit.AppendLine(@" \");
        Cockpit.AppendLine(@"  \");
        Cockpit.AppendLine(@"   \");
        Cockpit.AppendLine(@"    \");
        Cockpit.AppendLine(@"     \");
        Cockpit.AppendLine(@"      \");
        Cockpit.AppendLine(@"       \");

        Label cockpit = new Label()
        {
            Text = Cockpit.ToString(),
            Y = 9,
            X = 31
        };

        Back = new StringBuilder();
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");
        Back.AppendLine(@"|");

        Label back = new Label()
        {
            Text = Back.ToString(),
            Y = 9,
            X = 175
        };

        Add(rightWall, leftWall, rightwing, leftwing, cockpit, back);
        #endregion

        #region Legend
        Label economy1 = new Label()
        {
            Text = "X00",
            X = 55,
            Y = 33,
            ColorScheme = Colors.ColorSchemes["Economy"]
        };

        Label economy2 = new Label()
        {
            Text = " => Economy",
            X = Pos.Right(economy1),
            Y = 33
        };

        Label economyPlus1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(economy2) + 5,
            Y = 33,
            ColorScheme = Colors.ColorSchemes["Economy Plus"]
        };

        Label economyPlus2 = new Label()
        {
            Text = " => Economy Plus",
            X = Pos.Right(economyPlus1),
            Y = 33
        };

        Label UBF1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(economyPlus2) + 5,
            Y = 33,
            ColorScheme = Colors.ColorSchemes["United BusinessFirst"]
        };

        Label UBF2 = new Label()
        {
            Text = " => United BusinessFirst",
            X = Pos.Right(UBF1),
            Y = 33
        };

        Label taken1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(UBF2) + 5,
            Y = 33,
            ColorScheme = Colors.ColorSchemes["SeatTaken"]
        };

        Label taken2 = new Label()
        {
            Text = " => Bezet",
            X = Pos.Right(taken1),
            Y = 33
        };

        Label selected1 = new Label()
        {
            Text = "X00",
            X = Pos.Right(taken2) + 5,
            Y = 33,
            ColorScheme = Colors.ColorSchemes["SeatSelected"]
        };

        Label selected2 = new Label()
        {
            Text = " => Gekozen",
            X = Pos.Right(selected1),
            Y = 33
        };

        Add(economy1, economy2, economyPlus1, economyPlus2, UBF1, UBF2, taken1, taken2, selected1, selected2);
        #endregion

        Button reservation = new Button()
        {
            Text = "Reserveer",
            Y = 33,
            X = Pos.Right(selected2) + 18
        };

        reservation.Clicked += () =>
        {
            var seats = activeSeats.Where(seat => seat.IsClicked).ToList();
            List<Seat> selectedSeats = new List<Seat>();
            if (seats.Count == 0)
                MessageBox.Query("Geen stoelen geselecteerd", "U heeft geen stoelen gekozen", "Ok");
            else if (InteraciveSeat.SeatCount < InteraciveSeat.MaxSeats)
                MessageBox.ErrorQuery("Te weinig stoelen geselecteerd", "U heeft te weinig stoelen gekozen", "Ok");
            else {            
                seats.ForEach(s => selectedSeats.Add(s.Seat));
                WindowManager.GoForwardOne(new FlightOverview(flight, userInfos, selectedSeats));
            }
        };
        Add(reservation);
    }
}