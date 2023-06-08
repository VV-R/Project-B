using System;
using System.Text;
using Terminal.Gui;
using Newtonsoft.Json;
using Managers;
using Entities;

namespace Windows;
public class SeattingPlan : Toplevel
{
    private int seatsCount;
    private Flight flight;
    private List<UserInfo> userInfos;

    public SeattingPlan(Flight flight, List<UserInfo> userInfos, int seatsCount)
    {
        this.seatsCount = seatsCount;
        this.userInfos = userInfos;
        this.flight = flight;

        SeatMap seatMap = new SeatMap(flight.Airplane);
        InteraciveSeat.MaxSeats = userInfos.Count;
        InteraciveSeat.SeatCount = 0;
        Add(seatMap);

        Button goBackButton = new Button() {
            Text = "Terug"
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(); };

        Add(goBackButton);

        Button reservation = new Button()
        {
            Text = "Reserveer",
            Y = Pos.Top(seatMap.LatestLabel),
            X = Pos.Right(seatMap.LatestLabel) + 18
        };

        reservation.Clicked += () => {
            var seats = seatMap.GetSelectedSeats();
            List<Seat> selectedSeats = new List<Seat>();
            if (seats == null || seats.Count == 0)
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