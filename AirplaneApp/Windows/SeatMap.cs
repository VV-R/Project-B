using Terminal.Gui;
using Terminal.Gui.Graphs;
using Newtonsoft.Json;
using Entities;
using Db;

namespace Windows;
public class SeatMap : Toplevel
{
    public Label? LatestLabel;
    private List<InteraciveSeat>? _activeSeats;

    public SeatMap(string airplane) {
        Airplane? plane = null;
        using (StreamReader reader = new StreamReader("Airplanes.json")) {
            var airplanes = JsonConvert.DeserializeObject<Dictionary<string, Airplane>>(reader.ReadToEnd())!;
            if (airplanes == null)
                return;

            if (!airplanes.ContainsKey(airplane))
                return;

            plane = airplanes[airplane];
        }
        InteraciveSeat.MaxSeats = 0;
        InteraciveSeat.SeatCount = 0;
        foreach (Seat seat in plane.Seats) {
            Add(new InteraciveSeat(seat, false));
        }
        DrawBody(plane);
    }
    public SeatMap(Flight flight) {
        Airplane? plane = null;
        using (StreamReader reader = new StreamReader("Airplanes.json")) {
            var airplanes = JsonConvert.DeserializeObject<Dictionary<string, Airplane>>(reader.ReadToEnd())!;
            if (airplanes == null)
                return;

            if (!airplanes.ContainsKey(flight.Airplane))
                return;

            plane = airplanes[flight.Airplane];
        }

        List<string> occupied;
        using (var context = new ApplicationDbContext()) {
            var query = from ticket in context.Tickets
                        where ticket.FlightId == flight.FlightNumber
                        select ticket.SeatNumber;
            occupied = query.ToList();
        }

        _activeSeats = new();

        foreach (Seat seat in plane.Seats) {
            if (occupied.Contains(seat.Text)) {
                InteraciveSeat interSeat = new InteraciveSeat(seat, true);
                Add(interSeat);
                _activeSeats.Add(interSeat);
            } else {
                InteraciveSeat interSeat = new InteraciveSeat(seat, false);
                Add(interSeat);
                _activeSeats.Add(interSeat);
            }
        }

        DrawBody(plane);
    }
    private void DrawBody(Airplane plane) {
        ColorScheme = Colors.Base;

        Label rightWing = new Label() {
            Text = plane.RightWing,
            X = plane.RightWingPos.X,
            Y = plane.RightWingPos.Y
        };

        LineView rightWall = new LineView() {
            Width = plane.RightWallWidth,
            X = plane.RightWallPos.X,
            Y = plane.RightWallPos.Y,
        };

        LineView leftWall = new LineView() {
            Width = plane.LeftWallWidth,
            X = plane.LeftWallPos.X,
            Y = plane.LeftWallPos.Y,
        };

        Label leftWing = new Label() {
            Text = plane.LeftWing,
            X = plane.LeftWingPos.X,
            Y = plane.LeftWingPos.Y,
        };

        Label cockPit = new Label() {
            Text = plane.CockPit,
            X = plane.CockPitPos.X,
            Y = plane.CockPitPos.Y,
        };

        LineView back = new LineView(Orientation.Vertical) {
            Height = plane.BackHeigth,
            X = plane.BackPos.X,
            Y = plane.BackPos.Y,
        };

        Add(rightWall, leftWall, rightWing, leftWing, cockPit, back);

        List<Seat> distinctSeats = plane.Seats.DistinctBy(s => s.SeatType).ToList();
        Label? latestLabel = null;
        foreach (Seat seat in distinctSeats) {
            if (latestLabel == null) {
                Label firstLabelColor = new Label() {
                    Text = "X00",
                    X = plane.LegendPos.X,
                    Y = plane.LegendPos.Y,
                    ColorScheme = Colors.ColorSchemes[seat.SeatType],
                };

                Label firstName = new Label() {
                    Text = $"=> {seat.SeatType}",
                    X = Pos.Right(firstLabelColor) + 1,
                    Y = plane.LegendPos.Y,
                };

                latestLabel = firstName;
                Add(firstLabelColor, firstName);
                continue;
            }
            Label labelColor = new Label() {
                    Text = "X00",
                    X = Pos.Right(latestLabel) + 5,
                    Y = plane.LegendPos.Y,
                    ColorScheme = Colors.ColorSchemes[seat.SeatType],
                };

            Label name = new Label() {
                Text = $"=> {seat.SeatType}",
                X = Pos.Right(labelColor) + 1,
                Y = plane.LegendPos.Y,
            };

            latestLabel = name;
            Add(labelColor, name);
        }

        Label takenColor = new Label() {
            Text = "X00",
            X = Pos.Right(latestLabel) + 5,
            Y = plane.LegendPos.Y,
            ColorScheme = Colors.ColorSchemes["SeatTaken"]
        };

        Label takenInfo = new Label()
        {
            Text = "=> Bezet",
            X = Pos.Right(takenColor) + 1,
            Y = plane.LegendPos.Y,
        };

        Label selectedColor = new Label()
        {
            Text = "X00",
            X = Pos.Right(takenInfo) + 5,
            Y = plane.LegendPos.Y,
            ColorScheme = Colors.ColorSchemes["SeatSelected"]
        };

        LatestLabel = new Label()
        {
            Text = "=> Gekozen",
            X = Pos.Right(selectedColor) + 1,
            Y = plane.LegendPos.Y,
        };

        Add(takenColor, takenInfo, selectedColor, LatestLabel);
    }
    public List<InteraciveSeat>? GetSelectedSeats() => _activeSeats?.Where(s => s.IsClicked).ToList();
}