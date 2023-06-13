using Terminal.Gui;
using Microsoft.EntityFrameworkCore;
using Entities;
using Managers;
using Db;

namespace Windows;
public class SearchReservation : Toplevel
{
    public ListView ReservationsView;
    public TextField SearchBox;
    public SearchReservation(User? user)
    {
        if (user == null || user.Reservations == null)
            WindowManager.GoBackOne(this);

        Label searchBoxLabel = new Label() {
            Text = "Zoeken:"
        };

        SearchBox = new TextField("") {
            Width = 20,
            X = Pos.Right(searchBoxLabel) + 1,
        };

        ReservationsView = new ListView() {
            Y = Pos.Bottom(SearchBox) + 1,
            Height = 5,
            Width = Dim.Fill(),
        };

        Button goBackButton = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(ReservationsView) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(searchBoxLabel, SearchBox, ReservationsView, goBackButton);
    }
}

public class SearchReservationAdmin : SearchReservation
{
    public SearchReservationAdmin(User user) : base(user) {
        List<Ticket> reservations = user.Reservations;
        ReservationsView.SetSource(reservations);

        SearchBox.TextChanged += (text) => {ReservationsView.SetSource(reservations?.FindAll((x) => {
            return x.ToString().ToLower().Contains((string)SearchBox.Text.ToLower());
        }));};

        ReservationsView.OpenSelectedItem += (item) => { if (reservations != null)
            WindowManager.GoForwardOne(new ReservationPanelAdmin(user, reservations[item.Item])); };
    }
}

public class SearchReservationUser : SearchReservation
{
    public SearchReservationUser() : base(WindowManager.CurrentUser) {
        List<string> invoiceNumbers = new List<string>();
        List<string> infoNumbers = new List<string>();
        foreach (Ticket ticket in WindowManager.CurrentUser.Reservations) {
            if (!invoiceNumbers.Contains(ticket.InvoiceNumber)) {
                invoiceNumbers.Add(ticket.InvoiceNumber);
                infoNumbers.Add($"{ticket.InvoiceNumber}: {ticket.TheFlight}");
            }

        }
        ReservationsView.SetSource(infoNumbers);

        SearchBox.TextChanged += (text) => {ReservationsView.SetSource(invoiceNumbers?.FindAll((x) => {
            return x.ToLower().Contains((string)SearchBox.Text.ToLower());
        }));};

        ReservationsView.OpenSelectedItem += (item) => {
            WindowManager.GoForwardOne(new ReservationPanelUser(
                WindowManager.CurrentUser.Reservations.Where(t => t.InvoiceNumber == invoiceNumbers[item.Item]).ToList()));
        };
    }
}

public class SearchReservationGuest : Toplevel 
{
    private ReservationPanel? _reservationPanel;
    private TextField _searchField;
    public SearchReservationGuest() {
        Label infoLabel = new Label() {
            Text = "Om een reservatie te zoeken vul het factuur nummer in. FN-XXXXXXXX",
        };

        _searchField = new TextField("") {
            Y = Pos.Bottom(infoLabel) + 1,
            Width = 14,
        };

        Button searchButton = new Button() {
            Text = "Zoeken",
            Y = Pos.Top(_searchField),
            X = Pos.Right(_searchField) + 1,
        };
        
        // Should search for TextField input
        searchButton.Clicked += () => { ChangePanel((string)_searchField.Text); };

        Button goBackButton = new Button() {
            Text = "Terug",
            Y = Pos.Top(_searchField),
            X = Pos.Right(searchButton) + 1
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(infoLabel, _searchField, searchButton, goBackButton);
    }

    private bool ChangePanel(string invoceNumber) {
        Remove(_reservationPanel);

        List<Ticket> tickets;
        using (var context = new ApplicationDbContext()) {
            tickets = context.Tickets.Where(t => t.InvoiceNumber == invoceNumber).Include(t => t.TheFlight).Include(t => t.TheUserInfo).ToList();
        }

        if (tickets.Count == 0)
            return false;

        _reservationPanel = new ReservationPanel(tickets) {
            Y = Pos.Bottom(_searchField) + 1,
            ColorScheme = WindowManager.CurrentColor,
        };
        _reservationPanel.Remove(_reservationPanel.GoBackButton);

        Add(_reservationPanel);
        return true;   
    }
}

public class ReservationPanelAdmin : Toplevel
{
    public User CurrentUser;
    public Ticket CurrentTicket;
    public TextField SeatnumberText;
    public ReservationPanelAdmin(User user, Ticket ticket)
    {
        CurrentUser = user;
        CurrentTicket = ticket;
        Label flightLabel = new Label() {
            Text = $"Vlucht:\n{ticket.TheFlight.ToNewLineString()}",
        };

        Label userLabel = new Label() {
            Text = $"User:\n{user.ToNewLineString()}",
            Y = Pos.Bottom(flightLabel) + 1,
        };

        Label seatnumberLabel = new Label() {
            Text = "Ticket:\nStoelnummer:",
            Y = Pos.Bottom(userLabel) + 1,
        };

        SeatnumberText = new TextField(ticket.SeatNumber) {
            X = Pos.Right(seatnumberLabel) + 3,
            Y = Pos.Top(seatnumberLabel) + 1,
            Width = 5,
        };

        Label boardingTimeLabel = new Label() {
            Text = $"Boarding tijd: {ticket.BoardingTime}",
            Y = Pos.Bottom(seatnumberLabel),
        };

        Add(flightLabel, userLabel, seatnumberLabel, SeatnumberText, boardingTimeLabel);

        Button editButton = new Button() {
            Text = "Aanpassen",
            Y = Pos.Bottom(boardingTimeLabel) + 1,
        };

        editButton.Clicked += () => { EditTicket();};

        Button cancelButton = new Button() {
            Text = "Ticket Annuleren",
            X = Pos.Right(editButton) + 1,
            Y = Pos.Top(editButton),
        };

        cancelButton.Clicked += () => { CancelTicket(); };

        Button backButton = new Button() {
            Text = "Terug",
            X = Pos.Right(cancelButton) + 1,
            Y = Pos.Top(editButton),
        };

        backButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(editButton, cancelButton, backButton);
    }

    private void EditTicket()
    {
        // Check if seat number is really an seat in the plane and if its not occupied
        // if (!plane.Seat.Contain(SeatnumberText.Text)) return
        if (CurrentUser.UserInfo.Email == null)
            return;

        string subject = $"Vlucht {CurrentTicket.TheFlight.DepartureLocation} - {CurrentTicket.TheFlight.ArrivalLocation}";
        string body = $"Beste Klant,\n\nUw zitplek van vlucht {CurrentTicket.TheFlight.DepartureLocation} - {CurrentTicket.TheFlight.ArrivalLocation} is aangepast.\nUw zitplek gaat van {CurrentTicket.SeatNumber} naar {SeatnumberText.Text}.\n\nMet vriendelijke groeten,\nRotterdam Airline Service";


        EmailManager.SendOneEmail(subject, body, CurrentUser.UserInfo.Email);
        CurrentTicket.SeatNumber = (string)SeatnumberText.Text;
        WindowManager.GoBackOne(this);
    }

    private void CancelTicket()
    {
        CurrentUser.Reservations.Remove(CurrentTicket);
        SpecialEmail(CurrentTicket.TheFlight);
    }

    private void SpecialEmail(Flight flight)
    {
        RemoveAll();
        TextView textField = new TextView() {
            Text = $"Beste Klant,\n\nUw ticket van {flight.DepartureLocation} - {flight.ArrivalLocation} is gecanceld vanwege %%.\nEen alternatief wordt geregeld. Wij houden U hierover op de hoogte.\n\nMet vriendelijke groeten,\nRotterdam Airline Service",
            Width = Dim.Percent(50),
            Height = Dim.Percent(30),
            Y = 1,
        };

        Button sendButton = new Button() {
            Text = "Versturen",
            Y = Pos.Bottom(textField) + 1
        };

        sendButton.Clicked += () => { EmailManager.SendOneEmail($"Ticket {flight.DepartureLocation} - {flight.ArrivalLocation}", (string)textField.Text, CurrentUser.UserInfo.Email); WindowManager.GoBackOne(this); };

        Button goBack = new Button() {
            Text = "Terug",
            Y = Pos.Top(sendButton),
            X = Pos.Right(sendButton) + 1,
        };

        goBack.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(textField, sendButton, goBack);
    }
}

public class ReservationPanelUser : Toplevel
{
    List<Ticket> Tickets;
    public ReservationPanelUser(List<Ticket> tickets) {
        Tickets = tickets;
        Start();
    }
    private void Start() {
        RemoveAll();
        ReservationPanel panel = new ReservationPanel(Tickets) { ColorScheme = WindowManager.CurrentColor };

        Button cancelTicketButton = new Button() {
            Text = "Ticket Cancelen",
            X = Pos.Right(panel.GoBackButton) + 1,
            Y = Pos.Top(panel.GoBackButton)
        };

        cancelTicketButton.Clicked += () => {
            if (Tickets[0].BoardingTime.AddDays(-5) > DateTime.Now)
                CancelTickets();
            else MessageBox.ErrorQuery("Cancelen", "U bent buiten de 7 weken tijd om te cancelen!", "OK"); };
        Add(panel, cancelTicketButton);
    }

    private void CancelTickets() {
        RemoveAll();
        Label flightLabel = new Label() {
            Text = $"Vlucht:\n{Tickets[0].TheFlight.ToNewLineString()}",
        };

        Label helpLabel = new Label() {
            Text = "Klik voor de tickets op het '-' om te selecteren welke ticket u wilt cancelen.\nKlik daarna op OK om doortegaan of STOP om te stoppen\nLET OP! Als de hoofdboeker zijn ticket canceled worden alle tickets gecanceled",
            X = Pos.Right(flightLabel) + 2,
        };

        Add(flightLabel, helpLabel);

        CheckBox[] checkBoxes = new CheckBox[Tickets.Count];
        int xOffset = 42;
        int rows = 2;
        int labelHeight = 6;
        for (int i = 0; i < Tickets.Count; i++) {
            CheckBox checkBox = new CheckBox() {
                Y = Pos.Bottom(flightLabel) + (labelHeight * (i / rows)) + 1,
                X = xOffset * (i % rows)
            };

            checkBoxes[i] = checkBox;

            Label ticketLabel = new Label() {
                Text = $"Stoelnummer: {Tickets[i].SeatNumber}\nBoarding tijd: {Tickets[i].BoardingTime}",
                Y = Pos.Top(checkBox),
                X = Pos.Right(checkBox) + 1
            };
            UserInfo userInfo = Tickets[i].TheUserInfo;
            Label userLabel = new Label() {
                Text = $"{userInfo.FirstName} {userInfo.Preposition} {userInfo.LastName}",
                Y = Pos.Bottom(ticketLabel) + 1,
                X = Pos.Left(ticketLabel),
            };

            Add(checkBox, ticketLabel, userLabel);
        }

        Button okButton = new Button() {
            Text = "OK",
            Y = Pos.Bottom(flightLabel) + 1 + labelHeight * ((Tickets.Count + 1) / rows),
        };

        okButton.Clicked += () => {
            List<Ticket> ticketsToCancel = new();
            for (int i = 0; i < checkBoxes.Length; i++) {
                if (i == 0 && checkBoxes[i].Checked) {
                    ticketsToCancel = Tickets;
                    break;
                }
                if(checkBoxes[i].Checked)
                    ticketsToCancel.Add(Tickets[i]);
            }
            CancelTickets(ticketsToCancel);
            Start();
        };

        Button cancelTicketButton = new Button() {
            Text = "STOP",
            X = Pos.Right(okButton) + 1,
            Y = Pos.Top(okButton)
        };

        cancelTicketButton.Clicked += Start;

        Add(okButton, cancelTicketButton);
    }

    private void CancelTickets(List<Ticket> tickets) {
        using (var context = new ApplicationDbContext()) {
            context.Tickets.RemoveRange(tickets);
            context.SaveChanges();
        }
        foreach(var ticket in tickets) {
            Tickets.Remove(ticket);
        }
    }
}

public class ReservationPanel : Toplevel
{
    public Button GoBackButton;
    public ReservationPanel(List<Ticket> tickets)
    {
        Label flightLabel = new Label() {
            Text = $"Vlucht:\n{tickets[0].TheFlight.ToNewLineString()}",
        };
        Add(flightLabel);

        int xOffset = 40;
        int rows = 2;
        int labelHeight = 6;
        for (int i = 0; i < tickets.Count; i++) {
            Label ticketLabel = new Label() {
                Text = $"Stoelnummer: {tickets[i].SeatNumber}\nBoarding tijd: {tickets[i].BoardingTime}",
                Y = Pos.Bottom(flightLabel) + (labelHeight * (i / rows)) + 1,
                X = xOffset * (i % rows)
            };
            UserInfo userInfo = tickets[i].TheUserInfo;
            Label userLabel = new Label() {
                Text = $"{userInfo.FirstName} {userInfo.Preposition} {userInfo.LastName}",
                Y = Pos.Bottom(ticketLabel) + 1,
                X = Pos.Left(ticketLabel),
            };

            Add(ticketLabel, userLabel);
        }

        GoBackButton = new Button() {
            Y = Pos.Bottom(flightLabel) + 1 + labelHeight * ((tickets.Count + 1) / rows),
            Text = "Terug",
        };

        GoBackButton.Clicked += () => { WindowManager.GoBackOne();};

        Add(GoBackButton);
    }
}
