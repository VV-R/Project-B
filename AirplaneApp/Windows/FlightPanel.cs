using Terminal.Gui;

public class FlightPanel : Toplevel
{
    public ListView CategoryListView;
    public ListView FlightsListView;
    public FlightPanel(List<Flight> flights)
    {
        FrameView LeftPane = new FrameView("Bestemmingen") {
            X = 0,
            Y = 0,
            Width = 25,
            Height = 25,
			CanFocus = true,
        };

        CategoryListView = new ListView(new List<string>() {"Alle vluchten", "Parijs", "Madrid", "Berlijn"}) {
            X = 0,
            Y = 0,
            Width = Dim.Fill(0),
            Height = Dim.Fill(0),
            AllowsMarking = false,
            CanFocus = true,
        };

        CategoryListView.SelectedItemChanged += (e) => {
            if (e.Item == 0) {
                FlightsListView?.SetSource(flights.ToList());
            } else {
                FlightsListView?.SetSource(flights.Where(f => f.ArrivalLocation.Contains((string)e.Value)).ToList());
            } };

        LeftPane.Add(CategoryListView);

        FrameView RightPane = new FrameView ("Vluchten") {
            X = 25,
            Y = 0,
            Width = 186,
            Height = 25,
            CanFocus = true,
        };

        FlightsListView = new ListView(flights) {
            X = 0,
            Y = 0,
            Width = Dim.Fill(0),
            Height = Dim.Fill(0),
            AllowsMarking = false,
            CanFocus = true,
        };;

        RightPane.Add(FlightsListView);

        Add(LeftPane, RightPane);

        Button goBackButton = new Button() {
            Y = Pos.Bottom(LeftPane) + 1,
            Text = "Terug",
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};

        Add(goBackButton);

    }
}