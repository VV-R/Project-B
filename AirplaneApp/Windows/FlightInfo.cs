using System.Net;
using System.Net.Mail;
using Terminal.Gui;
using Managers;
using Entities;

namespace Windows;
public class FlightInfo : Toplevel
{
    public ComboBox DepartureLocationCombo;
    public DateTimeField DepatureDateField;
    public TimeField DepatureTime;
    public Label ArrivalLocationLabel;
    public ComboBox AirplaneCombobox;
    public FlightInfo()
    {
        #region Departure
        Label departureLocationLabel = new Label() {
            Text = "Vertrek Locatie:",
        };

        DepartureLocationCombo = new ComboBox() {
            Y = Pos.Top(departureLocationLabel),
            X = Pos.Right(departureLocationLabel) + 4,
            Width = Dim.Percent(20),
            Height = 4,
        };

        DepartureLocationCombo.SetSource(WindowManager.Locations);

        Add(departureLocationLabel, DepartureLocationCombo);

        Label departureTimeLabel = new Label() {
            Text = "Tijd van Vertrek:",
            Y = Pos.Bottom(departureLocationLabel) + 1,
        };

        DepatureDateField = new DateTimeField(Enumerable.Range(DateTime.Now.Year, 10).ToList()) {
            Y = Pos.Top(departureTimeLabel),
            X = Pos.Left(DepartureLocationCombo),
        };

        Add(departureTimeLabel, DepatureDateField);

        DepatureTime = new TimeField() {
            X = Pos.Right(DepatureDateField) + 1,
            Y = Pos.Top(DepatureDateField),
        };

        Label depatureTimeHelper = new Label() {
            Text = "*       *Format: HH:mm:ss",
            X = Pos.Right(DepatureTime),
            Y = Pos.Top(DepatureDateField),
        };

        Add(DepatureTime, depatureTimeHelper);
        #endregion

        #region Arrival
        ArrivalLocationLabel = new Label() {
            Text = "Bestemming:",
            Y = Pos.Bottom(departureTimeLabel) + 1
        };

        Add(ArrivalLocationLabel);
        #endregion

        #region Airplane
        Label airplaneLabel = new Label() {
            Text = "Vliegtuig:",
            Y = Pos.Bottom(ArrivalLocationLabel) + 1,
        };

        AirplaneCombobox = new ComboBox() {
            Y = Pos.Top(airplaneLabel),
            X = Pos.Left(DepartureLocationCombo),
            Width = Dim.Percent(20),
            Height = 4,
        };

        AirplaneCombobox.SetSource(new List<string>() {"Boeing 737", "Airbus 330", "Boeing 787"});

        Add(airplaneLabel, AirplaneCombobox);
        #endregion
    }
}

public class FlightInfoEdit : FlightInfo
{
    public FlightInfoEdit(Flight flight)
    {
        Label arrivalLocation = new Label() {
            Text = flight.ArrivalLocation,
            X = Pos.Left(DepartureLocationCombo),
            Y = Pos.Top(ArrivalLocationLabel),
        };

        Add(arrivalLocation);

        DepartureLocationCombo.SelectedItem = DepartureLocationCombo.Source.ToList().IndexOf(flight.DepartureLocation);
        DepatureDateField.SetDateTime(flight.DepartureTime);
        DepatureTime.Time = new TimeSpan(flight.DepartureTime.Hour, flight.DepartureTime.Minute, flight.DepartureTime.Second);
        AirplaneCombobox.SelectedItem = AirplaneCombobox.Source.ToList().IndexOf(flight.Airplane);

        Button updateButton = new Button() {
            Text = "Update",
            Y = Pos.Bottom(AirplaneCombobox) + 1,
        };

        updateButton.Clicked += () => {
            if (UpdateFlight(flight, DepatureDateField.GetDateTime().Add(DepatureTime.Time)) != null) {
                WindowManager.GoBackOne(this);
            }
        };

        Button cancelFlight = new Button() {
            Text = "Vlucht Annuleren",
            Y = Pos.Top(updateButton),
            X = Pos.Right(updateButton) + 1
        };

        cancelFlight.Clicked += () => { CancelFlight(flight); };

        Button delayButton = new Button() {
            Text = "Vertraging invoeren",
            Y = Pos.Top(updateButton),
            X = Pos.Right(cancelFlight) + 1,
        };

        delayButton.Clicked += () => { DelayFlight(flight); };

        Button goBackButton = new Button() {
            Text = "Annuleren",
            Y = Pos.Top(updateButton),
            X = Pos.Right(delayButton) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(updateButton, cancelFlight , delayButton, goBackButton);
    }

    private Flight? UpdateFlight(Flight flight, DateTime departureTime)
    {
        if (DepartureLocationCombo.Text == "" || AirplaneCombobox.Text == "") {
            MessageBox.ErrorQuery("Vlucht Updaten", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        if (DateTime.Now > departureTime) {
            MessageBox.ErrorQuery("Vlucht Updaten", "Tijden zijn niet juist ingevuld.", "Ok");
            return null;
        }

        TimeSpan flightDuration = (string)DepartureLocationCombo.Text switch {
            "Parijs" => new TimeSpan(1, 15, 0),
            "London" => new TimeSpan(0, 55, 0),
            "München" => new TimeSpan(1, 25, 0),
            "Wenen" => new TimeSpan(1, 50, 0),
            "Rome" => new TimeSpan(2, 10, 0),
            "Barcelona" => new TimeSpan(2, 15, 0),
            "Brussel" => new TimeSpan(0, 45, 0),
            "Madrid" => new TimeSpan(2, 35, 0),
            "Berlijn" => new TimeSpan(1, 20, 0),
            _ => TimeSpan.Zero
        };

        DateTime arrivalTime = departureTime.Add(flightDuration);

        string subject = $"Vlucht {flight.DepartureLocation} - {flight.ArrivalLocation}";
        string body = $"Beste Klant,\n\nUw vlucht van {flight.DepartureLocation} - {flight.ArrivalLocation} is gewijzigd.\nDit zijn de wijzigingen:\n";
        body += $"Vertrek Locatie:\t {flight.DepartureLocation} --> {(string)DepartureLocationCombo.Text}\n";
        body += $"Tijd van Vertrek:\t {flight.DepartureTime} --> {departureTime}\n";
         body += $"Tijd van Aankomst:\t{flight.ArrivalTime} --> {arrivalTime}\n";
        body += $"Vliegtuig:\t\t\t{flight.Airplane} --> {(string)AirplaneCombobox.Text}\n\n";
        body += "Met vriendelijke groeten,\nRotterdam Airline Service";

        SendEmails(subject, body);

        // Update the rest of the DB

        flight.DepartureLocation = (string)DepartureLocationCombo.Text;
        flight.DepartureTime = departureTime;
        flight.ArrivalTime = arrivalTime;
        flight.Airplane = (string)AirplaneCombobox.Text;
        return flight;
    }

    private void CancelFlight(Flight flight)
    {
        string body = $"Beste Klant,\n\nUw vlucht van {flight.DepartureLocation} - {flight.ArrivalLocation} is gecanceld vanwege {{0}}.\nEen alternatief wordt geregeld. Wij houden U hierover op de hoogte.\n\nMet vriendelijke groeten,\nRotterdam Airline Service";
        var n = MessageBox.Query("Annuleren", "Wat is de reden van Annuleren", "Weer", "Storing", "Staking", "Vertraging", "Bijzondere reden", "Stoppen");
        string reason;
        switch(n) {
            case 0:
                reason = "extreme weersomstandigheden";
                break;
            case 1:
                reason = "een storing";
                break;
            case 2:
                reason = "een staking";
                break;
            case 3:
                reason = "een opgelopen vertraging";
                break;
            case 4:
                SpecialEmail(flight, string.Format(body, "%%"));
                return;
            default:
                WindowManager.GoBackOne(this);
                return;
        };

        SendEmails($"Vlucht {flight.DepartureLocation} - {flight.ArrivalLocation}", String.Format(body, reason));

        // Remove it from the DB
        WindowManager.Flights.Remove(flight);
        WindowManager.GoBackOne(this);
    }

    private void SendEmails(string subject, string body)
    {
        // Notify customers via E-Mail
        List<MailAddress> emails = new List<MailAddress>() {new MailAddress("2004levi@gmail.com"), new MailAddress("vandaalenlevi@gmail.com")};
        EmailManager.SendEmails(subject, body, emails);
    }

    private void SpecialEmail(Flight flight, string body)
    {
        RemoveAll();
        TextView textField = new TextView() {
            Text = body,
            Width = Dim.Percent(50),
            Height = Dim.Percent(30),
            Y = 1,
        };

        Button sendButton = new Button() {
            Text = "Versturen",
            Y = Pos.Bottom(textField) + 1
        };

        sendButton.Clicked += () => { SendEmails($"Vlucht {flight.DepartureLocation} - {flight.ArrivalLocation}", (string)textField.Text); WindowManager.GoBackOne(this); };

        Button goBack = new Button() {
            Text = "Terug",
            Y = Pos.Top(sendButton),
            X = Pos.Right(sendButton) + 1,
        };

        goBack.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(textField, sendButton, goBack);
    }

    private void DelayFlight(Flight flight) {
        TimeSpan delay = TimeSpan.Zero;
        while (true) {
            int n = MessageBox.Query("Vertraging", $"Voeg minuten toe\nHudige vertraging: {delay}", "5 Minuten", "10 Minuten", "15 Minuten", "30 Minuten", "60 Minuten reden", "Invoeren","Stoppen");
            TimeSpan minutes = n switch {
            0 => new TimeSpan(0, 5, 0),
            1 => new TimeSpan(0, 10, 0),
            2 => new TimeSpan(0, 15, 0),
            3 => new TimeSpan(0, 30, 0),
            4 => new TimeSpan(1, 0, 0),
            _ => TimeSpan.Zero
            };

            delay += minutes;

            if (n == 5)
                DelayFlight(flight, delay);

            if (minutes == TimeSpan.Zero)
                break;
        }
        WindowManager.GoBackOne(this);
    }

    private void DelayFlight(Flight flight, TimeSpan delayTime) {
        flight.ArrivalTime += delayTime;
        // Shift all next flights with on this course up
        // Notify customers....
    }
}

public class FlightInfoAdd : FlightInfo
{
    public ComboBox ArrivalLocationCombo;

    public FlightInfoAdd()
    {

        ArrivalLocationCombo = new ComboBox() {
            X = Pos.Left(DepartureLocationCombo),
            Y = Pos.Top(ArrivalLocationLabel),
            Width = 25,
            Height = 4,
        };

        ArrivalLocationCombo.SetSource(WindowManager.Locations);

        Add(ArrivalLocationCombo);

        DepatureTime.Time = TimeSpan.Zero;
        DepatureDateField.SetDateTime(DateTime.Now);

        Button addButton = new Button() {
            Text = "Toevoegen",
            Y = Pos.Bottom(AirplaneCombobox) + 1,
        };

        addButton.Clicked += () => {
            Flight? flight = AddFlight(DepatureDateField.GetDateTime().Add(DepatureTime.Time));
            if (flight != null) {
                WindowManager.Flights.Add(flight);
                WindowManager.GoBackOne(this);
            }
        };

        Button goBackButton = new Button() {
            Text = "Annuleren",
            Y = Pos.Top(addButton),
            X = Pos.Right(addButton) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(addButton, goBackButton);
    }

    private Flight? AddFlight(DateTime departureTime)
    {
        if (DepartureLocationCombo.Text == "" || AirplaneCombobox.Text == "") {
            MessageBox.ErrorQuery("Vlucht Updaten", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        if (DateTime.Now > departureTime) {
            MessageBox.ErrorQuery("Vlucht Updaten", "Tijden zijn niet juist ingevuld.", "Ok");
            return null;
        }

        TimeSpan flightDuration = (string)DepartureLocationCombo.Text switch {
            "Parijs" => new TimeSpan(1, 15, 0),
            "London" => new TimeSpan(0, 55, 0),
            "München" => new TimeSpan(1, 25, 0),
            "Wenen" => new TimeSpan(1, 50, 0),
            "Rome" => new TimeSpan(2, 10, 0),
            "Barcelona" => new TimeSpan(2, 15, 0),
            "Brussel" => new TimeSpan(0, 45, 0),
            "Madrid" => new TimeSpan(2, 35, 0),
            "Berlijn" => new TimeSpan(1, 20, 0),
            _ => TimeSpan.Zero
        };

        return new Flight(WindowManager.Flights.Count, (string)DepartureLocationCombo.Text, departureTime, (string)ArrivalLocationCombo.Text, departureTime.Add(flightDuration), (string)AirplaneCombobox.Text);
    }
}

