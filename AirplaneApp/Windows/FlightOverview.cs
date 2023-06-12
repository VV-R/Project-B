using System;
using System.Net.Mail;
using Newtonsoft.Json;
using Terminal.Gui;
using Managers;
using Entities;
using System.Data;
using Db;

namespace Windows;

public class FlightOverview : Toplevel
{
    private Flight? flight;
    private List<UserInfo>? userInfos;
    private List<Seat>? selectedSeats;

    public FlightOverview(Flight flight, List<UserInfo> userInfos, List<Seat> selectedSeats)
    {
        #region PersonalInformation
        Label personalinformationLabel = new Label(){
            Text = "Persoonlijke informatie",
        };

        Label nameLabel = new Label() {
            Text = "Naam:",
            Y = Pos.Bottom(personalinformationLabel) + 1,
        };
        Label firstlastnameLabel = new Label(){
            Text = $"{userInfos[0].FirstName} {userInfos[0].LastName}",
            Y = Pos.Top(nameLabel),
            X = Pos.Right(nameLabel) + 15,
        };

        Label emailLabel = new Label() {
            Text = "E-mailadres:",
            Y = Pos.Bottom(nameLabel) + 1,
        };
        Label emailuserLabel = new Label() {
            Text = $"{userInfos[0].Email}",
            X = Pos.Right(emailLabel) + 8,
            Y = Pos.Top(emailLabel),
        };

        Label phoneLabel = new Label() {
            Text = "Telefoonnummer:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(emailLabel) + 1,
        };
        Label phoneuserLabel = new Label() {
            Text = $"{userInfos[0].PhoneNumber}",
            X = Pos.Left(emailuserLabel),
            Y = Pos.Top(phoneLabel),
        };

        Label dateOfBirthLabel = new Label() {
            Text = "Geboortedatum:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(phoneLabel) + 1,
        };
        Label dateofBirthFielduserLabel = new Label() {
            Text = $"{userInfos[0].DateOfBirth}",
            X = Pos.Left(emailuserLabel),
            Y = Pos.Top(dateOfBirthLabel),
        };

        Label nationalityLabel = new Label() {
            Text = "Nationaliteit:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(dateOfBirthLabel) + 1,
        };
        Label nationalityuserLabel = new Label() {
            Text = $"{userInfos[0].Nationality}",
            X = Pos.Left(emailuserLabel),
            Y = Pos.Top(nationalityLabel),
        };

        Label documentTypeLabel = new Label() {
            Text = "Document type:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(nationalityLabel) + 1
        };
        Label documentTypeuserLabel = new Label() {
            Text = $"{userInfos[0].DocumentType}",
            X = Pos.Left(emailuserLabel),
            Y = Pos.Top(documentTypeLabel),
        };

        Label documentNumberLabel = new Label() {
            Text = "Document nummer:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(documentTypeLabel) + 1,
        };
        Label documentNumberuserLabel = new Label() {
            Text = $"{userInfos[0].DocumentNumber}",
            X = Pos.Left(emailuserLabel),
            Y = Pos.Top(documentNumberLabel),
        };

        Add(personalinformationLabel, nameLabel, firstlastnameLabel, emailLabel, emailuserLabel, phoneLabel, phoneuserLabel, dateOfBirthLabel, dateofBirthFielduserLabel, nationalityLabel, nationalityuserLabel, documentTypeLabel, documentTypeuserLabel, documentNumberLabel, documentNumberuserLabel);
        #endregion

        #region FlightInformation
        Label flightinformationLabel = new Label(){
            Text  = "Vlucht informatie",
            Y = Pos.Bottom(documentNumberLabel) + 2,
        };

        Label flightNumber = new Label(){
            Text = $"Vluchtnummer:",
            Y = Pos.Bottom(flightinformationLabel) + 1,
        };
        Label userflightNumber = new Label(){
            Text = $"{flight.FlightNumber}",
            X = Pos.Left(emailuserLabel) + 1,
            Y = Pos.Top(flightNumber),
        };

        Label departureLocation = new Label(){
            Text = $"Vertek vanuit:",
            Y = Pos.Bottom(flightNumber) + 1,
        };
        Label userdepartureLocation = new Label(){
            Text = $"{flight.DepartureLocation}",
            X = Pos.Left(emailuserLabel) + 1,
            Y = Pos.Top(departureLocation),
        };

        Label departureTime = new Label(){
            Text = $"Vertrek tijd:",
            Y = Pos.Bottom(departureLocation) + 1,
        };
        Label userdepartureTime = new Label(){
            Text = $"{flight.DepartureTime}",
            X = Pos.Left(emailuserLabel) + 1,
            Y = Pos.Top(departureTime),
        };

        Label arrivalLocation = new Label(){
            Text = $"Aankomst:",
            Y = Pos.Bottom(departureTime) + 1,
        };
        Label userarrivalLocation = new Label(){
            Text = $"{flight.ArrivalLocation}",
            X = Pos.Left(emailuserLabel) + 1,
            Y = Pos.Top(arrivalLocation),
        };

        Label arrivalTime = new Label(){
            Text = $"Aankomst tijd:",
            Y = Pos.Bottom(arrivalLocation) + 1,
        };
        Label userarrivalTime = new Label(){
            Text = $"{flight.ArrivalTime}",
            X = Pos.Left(emailuserLabel) + 1,
            Y = Pos.Top(arrivalTime),
        };

        Label airplaneType = new Label(){
            Text = $"Vliegtuig type:",
            Y = Pos.Bottom(arrivalTime) + 1,
        };
        Label userairplaneType = new Label(){
            Text = $"{flight.Airplane}",
            X = Pos.Left(emailuserLabel) + 1,
            Y = Pos.Top(airplaneType),
        };

        Add(flightinformationLabel, flightNumber, userflightNumber, departureLocation, userdepartureLocation, departureTime, userdepartureTime, arrivalLocation, userarrivalLocation, arrivalTime, userarrivalTime, airplaneType, userairplaneType);
        #endregion

        #region totaal prijs

        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Stoelnummer");
        dataTable.Columns.Add("StoelType");
        dataTable.Columns.Add("Stoelprijs");
        dataTable.Columns.Add("Vluchtprijs");
        dataTable.Columns.Add("Totaal");

        double totalSeatPrice = 0;
        double totalFlightPrice = 0;

        Dictionary<string, double> seatPrices = new();
        using (StreamReader reader = new StreamReader("SeatingPrice.json")) {
            seatPrices = JsonConvert.DeserializeObject<Dictionary<string, double>>(reader.ReadToEnd())!;
        }
        TimeSpan flightDuration = flight.ArrivalTime - flight.DepartureTime;
        double totalPrice = 0;
        View previousView = airplaneType;
        foreach (Seat seat in selectedSeats)
        {
            double seatPrice = seatPrices[seat.SeatType];
            double flightPrice = InvoiceManager.PRICE_COEFFICIENT * flightDuration.TotalMinutes;

            totalSeatPrice += seatPrice;
            totalFlightPrice += flightPrice;

            double total = seatPrice + flightPrice;
            totalPrice += total;
            dataTable.Rows.Add(seat.Text, seat.SeatType, $"€{seatPrice}", $"€{flightPrice}", $"€{total}");
        }
        TableView costTable = new TableView()
        {
            X = 0,
            Y = Pos.Bottom(previousView) + 1,
            Width = 49,
            Height = 20,
        };

        dataTable.Rows.Add("", "", "----", "----", "----");
        dataTable.Rows.Add("", "", $"€{totalSeatPrice}", $"€{totalFlightPrice}", $"€{totalPrice}");

        costTable.Table = dataTable;

        Add(costTable);

        #endregion

        #region extrabooking

        if (userInfos.Count > 1)
        {
            Label extraBooking = new Label(){
            Text = "Extra passagiers",
            Y = Pos.Top(personalinformationLabel),
            X = Pos.Right(firstlastnameLabel) + 30,
            };

            Add(extraBooking);
            int xOffset = 40;
            int rows = 2;
            int labelHeight = 9;
            for (int i = 1; i < userInfos.Count; i++) {
                Label passagierNumber = new Label(){
                    Text = $"Extra passagier: {i}",
                    Y = 2 + (labelHeight * ((i - 1) / rows)) + 1,
                    X = Pos.Right(firstlastnameLabel) + 30 + (xOffset * ((i - 1) % rows)),
                };
                Add(passagierNumber);
                Label fieldLabel = new Label() {
                    Text = "Naam:\nGeboortedatum:\nNationaliteit:\nDocument type:\nDocument nummer:\n",
                    Y = Pos.Bottom(passagierNumber) + 1,
                    X = Pos.Left(passagierNumber),
                };
                Label extrafirstlastnameLabel = new Label(){
                    Text = $"{userInfos[i].FirstName} {userInfos[i].LastName}",
                    Y = Pos.Top(fieldLabel),
                    X = Pos.Right(fieldLabel) + 1,
                };
                Label extradateofBirthFielduserLabel = new Label() {
                    Text = $"{userInfos[i].DateOfBirth}",
                    X = Pos.Left(extrafirstlastnameLabel),
                    Y = Pos.Bottom(extrafirstlastnameLabel),
                };
                Label extranationalityuserLabel = new Label() {
                    Text = $"{userInfos[i].Nationality}",
                    X = Pos.Left(extrafirstlastnameLabel),
                    Y = Pos.Bottom(extradateofBirthFielduserLabel),
                };
                Label extradocumentTypeuserLabel = new Label() {
                    Text = $"{userInfos[i].DocumentType}",
                    X = Pos.Left(extrafirstlastnameLabel),
                    Y = Pos.Bottom(extranationalityuserLabel),
                };
                 Label extradocumentNumberuserLabel = new Label() {
                    Text = $"{userInfos[i].DocumentNumber}",
                    X = Pos.Left(extrafirstlastnameLabel),
                    Y = Pos.Bottom(extradocumentTypeuserLabel),
                };

                Add(fieldLabel, extrafirstlastnameLabel, extradateofBirthFielduserLabel, extranationalityuserLabel, extradocumentTypeuserLabel, extradocumentNumberuserLabel);

                }
            }
            #endregion
            Button reservationButton = new Button(){
                Text = "Reserveren",
                Y = Pos.Top(personalinformationLabel),
                X = Pos.Center(),
            };

            reservationButton.Clicked += () => {
                using (var context = new ApplicationDbContext()) {
                    for (int i = 0; i < userInfos.Count; i++) {
                        if (i == 0 && WindowManager.CurrentUser != null)
                            continue;
                        context.UserInfo.Add(userInfos[i]);
                    }
                    context.SaveChanges();
                }
                string invoice = InvoiceManager.MakeInvoicePdf(userInfos[0], selectedSeats, flight);
                string invoiceNumber = invoice.Split('.')[0];
                EmailManager.SendInvoice($"Factuur {invoiceNumber}","Beste Klant,\n\nHierbij de factuur van uw geboekte vlucht.\n\nMet Vriendelijke Groeten,\nRotterdam Airport", userInfos[0], invoice);
                using (var context = new ApplicationDbContext()) {
                    for (int i = 0; i < userInfos.Count; i++) {
                        context.Tickets.Add(new Ticket(flight.FlightNumber, userInfos[i].Id, selectedSeats[i].Text, flight.DepartureTime.AddMinutes(-30), invoiceNumber));
                    }
                    context.SaveChanges();
                }
                if (WindowManager.CurrentUser == null)
                    WindowManager.GoToFirst();
                else
                    WindowManager.GoForwardOne(new UserMenu(WindowManager.CurrentUser));
            };

            Button closeButton = new Button() {
                Text = "Afsluiten",
                Y = Pos.Top(reservationButton),
                X = Pos.Right(reservationButton) + 1,
            };
            
            closeButton.Clicked += () => { 
                if (WindowManager.CurrentUser == null)
                    WindowManager.GoToFirst();
                else
                    WindowManager.GoForwardOne(new UserMenu(WindowManager.CurrentUser)); 
            };

            Add(reservationButton, closeButton);

        }
}


