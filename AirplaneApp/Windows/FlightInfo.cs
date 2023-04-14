using System.Net;
using System.Net.Mail;
using Terminal.Gui;

public class FlightInfo : Toplevel
{
    public TextField GateNumber;
    public ComboBox DepartureLocationCombo;
    public ComboBox DepartureDayComboBox;
    public ComboBox DepartureMonthComboBox;
    public ComboBox DepartureYearComboBox;
    public TimeField DepatureTime;
    public ComboBox ArrivalLocationCombo;
    public ComboBox ArrivalDayComboBox;
    public ComboBox ArrivalMonthComboBox;
    public ComboBox ArrivalYearComboBox;
    public TimeField ArrivalTime;
    public ComboBox AirplaneCombobox;
    public FlightInfo()
    {
        int[] differentDays = {4, 6, 9, 11};
        int increaseDay = 1;

        #region Gate
        Label gateNumberLabel = new Label() {
            Text = "Gate nummer:",
        };

        GateNumber = new TextField("") {
            Y = Pos.Top(gateNumberLabel),
            X = Pos.Right(gateNumberLabel) + 6,
            Width = 8,
        };

        Add(gateNumberLabel, GateNumber);
        #endregion

        #region Departure
        Label departureLocationLabel = new Label() {
            Text = "Vertrek Locatie:",
            Y = Pos.Bottom(gateNumberLabel) + 1
        };

        DepartureLocationCombo = new ComboBox() {
            Y = Pos.Top(departureLocationLabel),
            X = Pos.Left(GateNumber),
            Width = Dim.Percent(20),
            Height = 4,
        };

        DepartureLocationCombo.SetSource(WindowManager.Locations);

        Add(departureLocationLabel, DepartureLocationCombo);

        Label departureTimeLabel = new Label() {
            Text = "Tijd van Vertrek:",
            Y = Pos.Bottom(departureLocationLabel) + 1,
        };

        DepartureDayComboBox = new ComboBox(){
            Y = Pos.Top(departureTimeLabel),
            X = Pos.Left(GateNumber),
            Height = 4,
            Width = 8,
        };

        DepartureDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

        DepartureMonthComboBox = new ComboBox(){
            X = Pos.Right(DepartureDayComboBox) + 1,
            Y = Pos.Top(DepartureDayComboBox),
            Height = 4,
            Width = 8,
        };

        DepartureMonthComboBox.SetSource(Enumerable.Range(1, 12).ToList());

        DepartureMonthComboBox.SelectedItemChanged += (e) => { int month = Convert.ToInt32(e.Value); if (month == 2) {
            DepartureDayComboBox.SetSource(Enumerable.Range(1, 28 + increaseDay).ToList());
            } else if (differentDays.Contains(month)) {
                DepartureDayComboBox.SetSource(Enumerable.Range(1, 30).ToList());
             }
            else {
                DepartureDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());
            } DepartureDayComboBox.SelectedItem = 0; };

        DepartureYearComboBox = new ComboBox(){
            X = Pos.Right(DepartureMonthComboBox) + 1 ,
            Y = Pos.Top(DepartureMonthComboBox),
            Height = 4,
            Width = 8,
        };

        DepartureYearComboBox.SetSource(Enumerable.Range(DateTime.Now.Year, 10).ToList());

        DepartureYearComboBox.SelectedItemChanged += (e) => {int year = Convert.ToInt32(e.Value);
            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) {
                increaseDay = 1;
                DepartureMonthComboBox.SelectedItem = 0;
            }
            else increaseDay = 0;
            };

        DepartureYearComboBox.SelectedItem = 0;
        DepartureMonthComboBox.SelectedItem = 0;
        DepartureDayComboBox.SelectedItem = 0;

        Add(departureTimeLabel, DepartureDayComboBox, DepartureMonthComboBox, DepartureYearComboBox);

        DepatureTime = new TimeField() {
            X = Pos.Right(DepartureYearComboBox) + 1,
            Y = Pos.Top(DepartureMonthComboBox),
        };

        Label depatureTimeHelper = new Label() {
            Text = "*       *Format: HH:mm:ss",
            X = Pos.Right(DepatureTime),
            Y = Pos.Top(DepartureMonthComboBox),
        };

        Add(DepatureTime, depatureTimeHelper);
        #endregion

        #region Arrival
        Label arrivalLocationLabel = new Label() {
            Text = "Bestemming:",
            Y = Pos.Bottom(departureTimeLabel) + 1
        };

        ArrivalLocationCombo = new ComboBox() {
            Y = Pos.Top(arrivalLocationLabel),
            X = Pos.Left(GateNumber),
            Width = Dim.Percent(20),
            Height = 4,
        };

        ArrivalLocationCombo.SetSource(WindowManager.Locations);

        Add(arrivalLocationLabel, ArrivalLocationCombo);

        Label arrivalTimeLabel = new Label() {
            Text = "Tijd van Aankomst:",
            Y = Pos.Bottom(arrivalLocationLabel) + 1,
        };

        ArrivalDayComboBox = new ComboBox(){
            Y = Pos.Top(arrivalTimeLabel),
            X = Pos.Left(GateNumber),
            Height = 4,
            Width = 8,
        };

        ArrivalDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

        ArrivalMonthComboBox = new ComboBox(){
            X = Pos.Right(ArrivalDayComboBox) + 1,
            Y = Pos.Top(ArrivalDayComboBox),
            Height = 4,
            Width = 8,
        };

        ArrivalMonthComboBox.SetSource(Enumerable.Range(1, 12).ToList());

        ArrivalMonthComboBox.SelectedItemChanged += (e) => { int month = Convert.ToInt32(e.Value); if (month == 2) {
            ArrivalDayComboBox.SetSource(Enumerable.Range(1, 28 + increaseDay).ToList());
            } else if (differentDays.Contains(month)) {
                ArrivalDayComboBox.SetSource(Enumerable.Range(1, 30).ToList());
             }
            else {
                ArrivalDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());
            } ArrivalDayComboBox.SelectedItem = 0; };

        ArrivalYearComboBox = new ComboBox(){
            X = Pos.Right(ArrivalMonthComboBox) + 1 ,
            Y = Pos.Top(ArrivalMonthComboBox),
            Height = 4,
            Width = 8,
        };

        ArrivalYearComboBox.SetSource(Enumerable.Range(DateTime.Now.Year, 10).ToList());

        ArrivalYearComboBox.SelectedItemChanged += (e) => {int year = Convert.ToInt32(e.Value);
            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) {
                increaseDay = 1;
                ArrivalMonthComboBox.SelectedItem = 0;
            }
            else increaseDay = 0;
            };

        ArrivalYearComboBox.SelectedItem = 0;
        ArrivalMonthComboBox.SelectedItem = 0;
        ArrivalDayComboBox.SelectedItem = 0;

        Add(arrivalTimeLabel, ArrivalDayComboBox, ArrivalMonthComboBox, ArrivalYearComboBox);

        ArrivalTime = new TimeField() {
            X = Pos.Right(ArrivalYearComboBox) + 1,
            Y = Pos.Top(ArrivalMonthComboBox),
        };

        Label arrivalTimeHelper = new Label() {
            Text = "*       *Format: HH:mm:ss",
            X = Pos.Right(ArrivalTime),
            Y = Pos.Top(ArrivalMonthComboBox),
        };

        Add(ArrivalTime, arrivalTimeHelper);
        #endregion

        #region Airplane
        Label airplaneLabel = new Label() {
            Text = "Vliegtuig:",
            Y = Pos.Bottom(arrivalTimeLabel) + 1,
        };

        AirplaneCombobox = new ComboBox() {
            Y = Pos.Top(airplaneLabel),
            X = Pos.Left(GateNumber),
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
        GateNumber.Text = flight.GateNumber.ToString();
        DepartureLocationCombo.SelectedItem = DepartureLocationCombo.Source.ToList().IndexOf(flight.DepartureLocation);
        DepartureYearComboBox.SelectedItem = DepartureYearComboBox.Source.ToList().IndexOf(flight.DepartureTime.Year);
        DepartureMonthComboBox.SelectedItem = flight.DepartureTime.Month - 1;
        DepartureDayComboBox.SelectedItem = flight.DepartureTime.Day - 1;
        DepatureTime.Time = new TimeSpan(flight.DepartureTime.Hour, flight.DepartureTime.Minute, flight.DepartureTime.Second);;
        ArrivalLocationCombo.SelectedItem = ArrivalLocationCombo.Source.ToList().IndexOf(flight.ArrivalLocation);
        ArrivalYearComboBox.SelectedItem = ArrivalYearComboBox.Source.ToList().IndexOf(flight.ArrivalTime.Year);
        ArrivalMonthComboBox.SelectedItem = flight.ArrivalTime.Month - 1;
        ArrivalDayComboBox.SelectedItem = flight.ArrivalTime.Day - 1;
        ArrivalTime.Time = new TimeSpan(flight.ArrivalTime.Hour, flight.ArrivalTime.Minute, flight.ArrivalTime.Second);
        AirplaneCombobox.SelectedItem = AirplaneCombobox.Source.ToList().IndexOf(flight.Airplane);

        Button updateButton = new Button() {
            Text = "Update",
            Y = Pos.Bottom(AirplaneCombobox) + 1,
        };

        updateButton.Clicked += () => {
            DateTime departureTime = new DateTime(Convert.ToInt32(DepartureYearComboBox.Text),Convert.ToInt32(DepartureMonthComboBox.Text), Convert.ToInt32(DepartureDayComboBox.Text), DepatureTime.Time.Hours, DepatureTime.Time.Minutes, DepatureTime.Time.Seconds);
            DateTime arrivalTime = new DateTime(Convert.ToInt32(ArrivalYearComboBox.Text),Convert.ToInt32(ArrivalMonthComboBox.Text), Convert.ToInt32(ArrivalDayComboBox.Text), ArrivalTime.Time.Hours, ArrivalTime.Time.Minutes, ArrivalTime.Time.Seconds);
            if (UpdateFlight(flight, departureTime, arrivalTime) != null) {
                WindowManager.GoBackOne(this);
            }
        };

        Button cancelFlight = new Button() {
            Text = "Vlucht Annuleren",
            Y = Pos.Top(updateButton),
            X = Pos.Right(updateButton) + 1
        };

        cancelFlight.Clicked +=  () => { CancelFlight(flight); WindowManager.GoBackOne(this);};

        Button goBackButton = new Button() {
            Text = "Annuleren",
            Y = Pos.Top(updateButton),
            X = Pos.Right(cancelFlight) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(updateButton, cancelFlight ,goBackButton);
    }

    private Flight? UpdateFlight(Flight flight, DateTime departureTime, DateTime arrivalTime)
    {
        if (GateNumber.Text == "" || DepartureLocationCombo.Text == "" || ArrivalLocationCombo.Text == "" || AirplaneCombobox.Text == "") {
            MessageBox.ErrorQuery("Vlucht Updaten", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        if (!int.TryParse((string)GateNumber.Text, out int gateNumber)) {
            MessageBox.ErrorQuery("Vlucht Updaten", "Gate nummer is niet goed ingevuld.", "Ok");
            return null;
        }

        if (DateTime.Now > departureTime || departureTime > arrivalTime) {
            MessageBox.ErrorQuery("Vlucht Updaten", "Tijden zijn niet juist ingevuld.", "Ok");
            return null;
        }

        flight.GateNumber = gateNumber;
        flight.DepartureLocation = (string)DepartureLocationCombo.Text;
        flight.DepartureTime = departureTime;
        flight.ArrivalLocation = (string)ArrivalLocationCombo.Text;
        flight.ArrivalTime = arrivalTime;
        flight.Airplane = (string)AirplaneCombobox.Text;
        return flight;
    }

    private void CancelFlight(Flight flight)
    {
        var n = MessageBox.Query("Annuleren", "Wat is de reden van Annuleren", "Weer", "Storing", "Staking", "Strakke Tijdschema's","Annuleren");
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
                reason = "een te strakke tijdschema";
                break;
            default:
                return;
        };

        // Set up SMPT client
        MailAddress fromAddress = new MailAddress("rotterdamairline@outlook.com");
        SmtpClient smtp = new SmtpClient() {
            Host = "smtp.office365.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, "Team1Admin1234")
        };

        string body = $"Beste Klant,\n\nUw vlucht van {flight.DepartureLocation} - {flight.ArrivalLocation} is gecanceld vanwege {reason}.\nEen alternatief wordt geregeld. Wij houden U hierover op de hoogte.\n\nMet vriendelijke groeten,\nRotterdam Airline Service";

        // Notify customers via E-Mail
        List<MailAddress> emails = new List<MailAddress>() {new MailAddress("2004levi@gmail.com")};
        Application.MainLoop.Invoke(async () => {
            foreach(MailAddress email in emails) {
                using (MailMessage message = new MailMessage(fromAddress, email) {
                    Subject = $"Vlucht {flight.DepartureLocation} - {flight.ArrivalLocation}",
                    Body = body,
                }) {
                    await smtp.SendMailAsync(message);
                }
            }
        });

        // Remove it from the DB
        WindowManager.Flights.Remove(flight);
    }
}

public class FlightInfoAdd : FlightInfo
{
    public FlightInfoAdd()
    {
        DepartureMonthComboBox.SelectedItem = DateTime.Now.Month - 1;
        DepartureDayComboBox.SelectedItem = DateTime.Now.Day - 1;
        DepatureTime.Time = TimeSpan.Zero;
        ArrivalMonthComboBox.SelectedItem = DateTime.Now.Month - 1;
        ArrivalDayComboBox.SelectedItem = DateTime.Now.Day - 1;
        ArrivalTime.Time = TimeSpan.Zero;

        Button addButton = new Button() {
            Text = "Toevoegen",
            Y = Pos.Bottom(AirplaneCombobox) + 1,
        };

        addButton.Clicked += () => {
            DateTime departureTime = new DateTime(Convert.ToInt32(DepartureYearComboBox.Text),Convert.ToInt32(DepartureMonthComboBox.Text), Convert.ToInt32(DepartureDayComboBox.Text), DepatureTime.Time.Hours, DepatureTime.Time.Minutes, DepatureTime.Time.Seconds);
            DateTime arrivalTime = new DateTime(Convert.ToInt32(ArrivalYearComboBox.Text),Convert.ToInt32(ArrivalMonthComboBox.Text), Convert.ToInt32(ArrivalDayComboBox.Text), ArrivalTime.Time.Hours, ArrivalTime.Time.Minutes, ArrivalTime.Time.Seconds);
            Flight? flight = AddFlight(departureTime, arrivalTime);
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

    private Flight? AddFlight(DateTime departureTime, DateTime arrivalTime)
    {
        if (GateNumber.Text == "" || DepartureLocationCombo.Text == "" || ArrivalLocationCombo.Text == "" || AirplaneCombobox.Text == "") {
            MessageBox.ErrorQuery("Vlucht Updaten", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        if (!int.TryParse((string)GateNumber.Text, out int gateNumber)) {
            MessageBox.ErrorQuery("Vlucht Updaten", "Gate nummer is niet goed ingevuld.", "Ok");
            return null;
        }

        if (DateTime.Now > departureTime || departureTime > arrivalTime) {
            MessageBox.ErrorQuery("Vlucht Updaten", "Tijden zijn niet juist ingevuld.", "Ok");
            return null;
        }

        return new Flight(WindowManager.Flights.Count, gateNumber, (string)DepartureLocationCombo.Text, departureTime, (string)ArrivalLocationCombo.Text, arrivalTime, (string)AirplaneCombobox.Text);
    }
}