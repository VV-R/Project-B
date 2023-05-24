using Terminal.Gui;
using Managers;
using Entities;

namespace Windows;
public class FlightPanel : Toplevel
{
    public ListView CategoryListView;
    public ListView FlightsListView;
    public FlightPanel(List<Flight> flights)
    {
        FrameView LeftPane = new FrameView("Bestemmingen")
        {
            X = 0,
            Y = 0,
            Width = 25,
            Height = 25,
            CanFocus = true,
        };

        List<string> categories = new List<string>() { "Alle vluchten" };
        categories.AddRange(WindowManager.Locations);

        CategoryListView = new ListView(categories)
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(0),
            Height = Dim.Fill(0),
            AllowsMarking = false,
            CanFocus = true,
        };

        CategoryListView.SelectedItemChanged += (e) =>
        {
            if (e.Item == 0)
            {
                FlightsListView?.SetSource(flights.ToList());
            }
            else
            {
                FlightsListView?.SetSource(flights.Where(f => f.ArrivalLocation.Contains((string)e.Value)).ToList());
            }
        };

        LeftPane.Add(CategoryListView);

        FrameView RightPane = new FrameView("Vluchten")
        {
            X = 25,
            Y = 0,
            Width = 186,
            Height = 25,
            CanFocus = true,
        };

        FlightsListView = new ListView(flights)
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(0),
            Height = Dim.Fill(0),
            AllowsMarking = false,
            CanFocus = true,
        }; ;

        RightPane.Add(FlightsListView);

        Add(LeftPane, RightPane);
         Button goBackButton = new Button() {
            Y = Pos.Bottom(CategoryListView) + 3,
            Text = "Terug",
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};

        Add(goBackButton);
    }
}

public class FlightPanelUser : FlightPanel
{
    public FlightPanelUser(List<Flight> flights) : base(flights)
    {
        FlightsListView.OpenSelectedItem += (flight) => { var n = MessageBox.Query("Passagiers", "Met hoeveel personen reist u?", "1", "2", "3", "4", "5", "6", "7", "8", "Annuleren"); if (n == 8) return; WindowManager.GoForwardOne(new BookingProcess(n + 1,(Flight)flight.Value)); };
        Button goBackButton = new Button()
        {
            Y = Pos.Bottom(CategoryListView) + 3,
            Text = "Terug",
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(goBackButton);
    }
}

public class FlightPanelAdmin : FlightPanel
{
    public FlightPanelAdmin(List<Flight> flights) : base(flights)
    {
        FlightsListView.OpenSelectedItem += (flight) => { WindowManager.GoForwardOne(new FlightInfoEdit((Flight)flight.Value)); };

        Button addFlightButton = new Button
        {
            Y = Pos.Bottom(CategoryListView) + 3,
            Text = "Toevoegen",
        };

        addFlightButton.Clicked += () => { WindowManager.GoForwardOne(new FlightInfoAdd()); };

        Button goBackButton = new Button()
        {
            Y = Pos.Bottom(CategoryListView) + 3,
            X = Pos.Right(addFlightButton) + 1,
            Text = "Terug",
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(addFlightButton, goBackButton);
    }
}