using Terminal.Gui;
using System.Data;
using Managers;
using Entities;

namespace Windows;
public class Dashboard : Toplevel
{
    public Dashboard()
    {

        DataTable destinationsTable = new DataTable();
        destinationsTable.Columns.Add("VLUCHT", typeof(int));
        destinationsTable.Columns.Add("BESTEMMING", typeof(string));
        destinationsTable.Columns.Add("TIJD", typeof(string));
        destinationsTable.Columns.Add("STATUS", typeof(string));

        DataTable arrivalsTable = new DataTable();
        arrivalsTable.Columns.Add("VLUCHT", typeof(int));
        arrivalsTable.Columns.Add("VERTREK LOCATIE", typeof(string));
        arrivalsTable.Columns.Add("TIJD", typeof(string));
        arrivalsTable.Columns.Add("STATUS", typeof(string));

        foreach(Flight flight in WindowManager.Flights) {
            string? status = GetStatus(flight);
            if (status == null)
                continue;
            if (flight.ArrivalLocation == "Rotterdam") {
                string time = $"{flight.ArrivalTime.Hour.ToString().PadLeft(2, '0')}:{flight.ArrivalTime.Minute.ToString().PadLeft(2, '0')}";
                arrivalsTable.Rows.Add(flight.FlightNumber, flight.DepartureLocation, time, status);
            } else {
                string time = $"{flight.DepartureTime.Hour.ToString().PadLeft(2, '0')}:{flight.DepartureTime.Minute.ToString().PadLeft(2, '0')}";
                destinationsTable.Rows.Add(flight.FlightNumber, flight.ArrivalLocation, time, status);
            }
        }

        TableView destinationsTableView = new TableView() {
            X = 0,
            Y = 2,
            Width = 50,
            Height = 30,
            AutoSize = true,
        };

        destinationsTableView.Table = destinationsTable;

        Label destinationsLabel = new Label() {
            Text = "Bestemmingen:",
            X = Pos.Left(destinationsTableView),
            Y = 0
        };

        LineView destinationsLine = new LineView() {
            X = 0,
            Y = Pos.Bottom(destinationsTableView),
            Width = destinationsTableView.Width,
        };


        Add(destinationsTableView, destinationsLabel, destinationsLine);

        TableView arrivalsTableView = new TableView() {
            X = Pos.Right(destinationsTableView) + 2,
            Y = Pos.Top(destinationsTableView),
            Width = 50,
            Height = 30,
            AutoSize = true,
        };

        arrivalsTableView.Table = arrivalsTable;

        Label arrivalsLabel = new Label() {
            Text = "Aankomsten:",
            X = Pos.Left(arrivalsTableView),
            Y = Pos.Top(destinationsLabel)
        };

        LineView arrivalsLine = new LineView() {
            X = Pos.Left(arrivalsTableView),
            Y = Pos.Bottom(arrivalsTableView),
            Width = arrivalsTableView.Width,
        };

        Add(arrivalsTableView, arrivalsLabel, arrivalsLine);

        Button goBackButton = new Button("Terug") {
            X = Pos.Left(destinationsTableView),
            Y = Pos.Bottom(destinationsTableView) + 2
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(goBackButton);
    }

    private string? GetStatus(Flight flight)
    {
        if (flight.DepartureTime < DateTime.Now && flight.ArrivalTime > DateTime.Now)
            return "ONDERWEG";
        else if (flight.DepartureTime.AddMinutes(-30) < DateTime.Now && flight.DepartureTime > DateTime.Now)
            return "BOARDING";
        else if (flight.ArrivalTime.AddMinutes(30) > DateTime.Now && flight.ArrivalTime < DateTime.Now)
            return "DISEMBARK";
        else if (flight.ArrivalTime.AddMinutes(30) < DateTime.Now)
            return "COMPLETED";
        else return "VERWACHT";
    }
}
