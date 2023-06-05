using Entities;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Managers;
public static class InvoiceManager
{
    const double PRICE_COEFFICIENT = 0.25;
    public static string MakeInvoicePdf(UserInfo userInfo, List<Seat> seats, Flight flight) {
        string invoceNumber = $"FN-{flight.FlightNumber.ToString("X4")}{userInfo.Id.ToString("X4")}";
        string fileName = $"{invoceNumber}.pdf";
        string fullName =  $"{userInfo.FirstName}{(userInfo.Preposition != "" ? $" {userInfo.Preposition}" : "")} {userInfo.LastName}";
        string date = DateTime.Now.ToString("dd-MM-yyyy");

        Dictionary<string, double> seatPrices = new();
        using (StreamReader reader = new StreamReader("SeatingPrice.json")) {
            seatPrices = JsonConvert.DeserializeObject<Dictionary<string, double>>(reader.ReadToEnd())!;
        }

        Document document = new Document();
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
        document.Open();

        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 36);
        Font header2Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
        Font boldNormalText = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        Font normalText = FontFactory.GetFont(FontFactory.HELVETICA, 12);

        float leadingHeader = 12f;
        Paragraph header = new Paragraph("Factuur", headerFont);
        PdfPTable headerTopTable = new PdfPTable(2) { WidthPercentage = 100};
        headerTopTable.DefaultCell.Border = Rectangle.NO_BORDER;

        headerTopTable.AddCell(new PdfPCell(header) { Border = Rectangle.NO_BORDER});
        headerTopTable.AddCell(new PdfPCell(new Paragraph(invoceNumber, header2Font)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

        document.Add(headerTopTable);
        document.Add(new Paragraph(""));

        PdfPTable headerTable = new PdfPTable(2) { WidthPercentage = 100 };
        headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

        Phrase invocePhrase = new Phrase($"Datum: {date}\n", header2Font);
        invocePhrase.Add(new Phrase($"{fullName}\n", boldNormalText));
        invocePhrase.Add(new Phrase($"{userInfo.Nationality}\n{string.Join(' ', userInfo.PhoneNumber.Split("|"))}\n{userInfo.Email?.Address}\n", normalText));

        TimeSpan flightDuration = flight.ArrivalTime - flight.DepartureTime;
        Phrase flightPhrase = new Phrase("\nVlucht:\n", header2Font);
        flightPhrase.Add(new Phrase($"{flight.DepartureLocation}-{flight.ArrivalLocation}\n", normalText));
        flightPhrase.Add(new Phrase($"{flight.Airplane}\nDuur (minuten): {flightDuration.TotalMinutes}", normalText));

        PdfPCell invoceInfoCell = new PdfPCell() { Border = Rectangle.NO_BORDER };
        invoceInfoCell.AddElement(invocePhrase);
        invoceInfoCell.AddElement(flightPhrase);
        invoceInfoCell.SetLeading(leadingHeader, 1);
        headerTable.AddCell(invoceInfoCell);

        Phrase companyPhrase = new Phrase("Rotterdam Airline\n", header2Font);
        companyPhrase.Add(new Phrase("Driesmanssteeweg 107\n3011 WN\nRotterdam\n", normalText));
        companyPhrase.Add(new Phrase("+31 6 59 45 51 09\nBank: NL91 INGB 0751 3740 59", normalText));

        PdfPCell companyCell = new PdfPCell(companyPhrase) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT};
        companyCell.SetLeading(leadingHeader, 1);
        headerTable.AddCell(companyCell);

        document.Add(headerTable);
        document.Add(new Paragraph("\n\n\n"));

        PdfPTable pricingTable = new PdfPTable(3) { WidthPercentage = 100 };
        pricingTable.SetWidths(new float[] {4f, 2f, 2f});

        PdfPCell descriptionCell = new PdfPCell() { NoWrap = true};
        PdfPCell priceCell = new PdfPCell() { NoWrap = true };
        PdfPCell totalPriceCell = new PdfPCell() { NoWrap = true };

        descriptionCell.AddElement(new Paragraph("Omschrijving:", boldNormalText));
        priceCell.AddElement(new Paragraph("Bedrag:", boldNormalText));
        totalPriceCell.AddElement(new Paragraph("Totaal:", boldNormalText));

        double totalPrice = 0;

        foreach (Seat seat in seats) {
            descriptionCell.AddElement(new Paragraph($"Stoel: {seat.Text}, Type: {seat.SeatType}", normalText));
            priceCell.AddElement(new Paragraph($"€ {seatPrices[seat.SeatType].ToString("0.00")}", normalText));
            double total = PRICE_COEFFICIENT * flightDuration.TotalMinutes + seatPrices[seat.SeatType];
            totalPriceCell.AddElement(new Paragraph($"€ {total.ToString("0.00")}", normalText));
            totalPrice += total;
        }

        pricingTable.AddCell(descriptionCell);
        pricingTable.AddCell(priceCell);
        pricingTable.AddCell(totalPriceCell);

        document.Add(pricingTable);
        PdfPTable totalPriceTable = new PdfPTable(3) { WidthPercentage = 100 };
        totalPriceTable.DefaultCell.Border = Rectangle.NO_BORDER;
        totalPriceTable.SetWidths(new float[] {4f, 2f, 2f});

        totalPriceTable.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER} );
        totalPriceTable.AddCell(new PdfPCell(new Paragraph($"Totale Prijs", boldNormalText)) { Border = Rectangle.NO_BORDER} );
        totalPriceTable.AddCell(new PdfPCell(new Paragraph($"€ {totalPrice.ToString("0.00")}", normalText)) { Border = Rectangle.NO_BORDER});

        document.Add(totalPriceTable);
        document.Close();
        return fileName;
    }
}