using System;
using System.Text;
using Terminal.Gui;
using Newtonsoft.Json;
using Managers;
using Entities;

namespace Windows;
public class AirplaneInformation : Toplevel
{
    public AirplaneInformation()
    {
        Button boeing_737 = new Button()
        {
            Text = "Boeing 737",
        };

        Button airbus_330 = new Button()
        {
            Text = "Airbus 330",
            X = Pos.Right(boeing_737) + 1
        };

        Button boeing_787 = new Button()
        {
            Text = "Boeing 787",
            X = Pos.Right(airbus_330) + 1
        };

        Button terug = new Button()
        {
            Text = "Terug",
            X = Pos.Right(boeing_787) + 10
        };

        terug.Clicked += () => { WindowManager.GoBackOne(this); };

        boeing_737.Clicked += () =>
        {
            RemoveAll();
            Add(boeing_737, airbus_330, boeing_787, terug);

            Button planeInfo = new Button()
            {
                Text = "Informatie boeing 737 en stoel prijzen",
                Y = Pos.Bottom(boeing_737) + 1
            };

            planeInfo.Clicked += () => { WindowManager.GoForwardOne(new PlaneInfo("Boeing 737")); };

            Button seats = new Button()
            {
                Text = "Plattegrond boeing 737",
                Y = Pos.Bottom(boeing_737) + 1,
                X = Pos.Right(planeInfo) + 1
            };

            seats.Clicked += () => { WindowManager.GoForwardOne(new SeatsPlane("Boeing 737")); };

            Add(planeInfo, seats);
        };

        airbus_330.Clicked += () =>
        {
            RemoveAll();
            Add(boeing_737, airbus_330, boeing_787, terug);

            Button planeInfo = new Button()
            {
                Text = "Informatie airbus 330 en stoel prijzen",
                Y = Pos.Bottom(boeing_737) + 1
            };

            planeInfo.Clicked += () => { WindowManager.GoForwardOne(new PlaneInfo("Airbus 330")); };

            Button seats = new Button()
            {
                Text = "Plattegrond airbus 330",
                Y = Pos.Bottom(boeing_737) + 1,
                X = Pos.Right(planeInfo) + 1
            };

            seats.Clicked += () => { WindowManager.GoForwardOne(new SeatsPlane("Airbus 330")); };

            Add(planeInfo, seats);
        };

        boeing_787.Clicked += () =>
        {
            RemoveAll();
            Add(boeing_737, airbus_330, boeing_787, terug);

            Button planeInfo = new Button()
            {
                Text = "Informatie boeing 787 en stoel prijzen",
                Y = Pos.Bottom(boeing_737) + 1
            };

            planeInfo.Clicked += () => { WindowManager.GoForwardOne(new PlaneInfo("Boeing 787")); };

            Button seats = new Button()
            {
                Text = "Plattegrond boeing 787",
                Y = Pos.Bottom(boeing_737) + 1,
                X = Pos.Right(planeInfo) + 1
            };

            seats.Clicked += () => { WindowManager.GoForwardOne(new SeatsPlane("Boeing 787")); };

            Add(planeInfo, seats);
        };

        Add(boeing_737, airbus_330, boeing_787, terug);
    }
}

public class PlaneInfo : Toplevel
{
    public PlaneInfo(string plane)
    {
        Dictionary<string, double> SeatPrices = new();
        using (StreamReader reader = new StreamReader("SeatingPrice.json"))
        {
            SeatPrices = JsonConvert.DeserializeObject<Dictionary<string, double>>(reader.ReadToEnd())!;
        }

        if (plane == "Boeing 737")
        {
            Label info = new Label()
            {
                Text = "Boeing 737-800 Specificaties:\n\n" +
                    "- Aantal passagiers: De 737-800 kan comfortabel plaats bieden aan maximaal 189 passagiers in een standaard configuratie.\n\n" +
                    "- Bereik: Het vliegbereik van de 737-800 is ongeveer 5.665 km, afhankelijk van de belading en weersomstandigheden.\n\n" +
                    "- Snelheid: De kruissnelheid van de 737-800 is ongeveer 840 km/u, variërend op basis van de hoogte en weersomstandigheden.\n\n" +
                    "- Motoren: De 737-800 is uitgerust met twee CFM56-7B motoren, elk met een stuwkracht van ongeveer 27.000 tot 29.000 pond.\n\n" +
                    "- Lengte en spanwijdte: De lengte van de 737-800 is ongeveer 39,5 meter en de spanwijdte is ongeveer 34 meter.\n\n" +
                    "- Eerste vlucht en in gebruik name: De 737-800 maakte zijn eerste vlucht op 31 juli 1997 en werd geïntroduceerd in 1998.\n\n" +
                    "- Fabrikant: De 737-800 is geproduceerd door Boeing, een van de grootste vliegtuigbouwers ter wereld.\n\n" +
                    "De Boeing 737-800 wordt vaak gebruikt door luchtvaartmaatschappijen voor korte tot middellange afstanden, zoals binnenlandse vluchten en intercontinentale reizen binnen Europa en naar de Verenigde Staten.",
            };

            Label price = new Label()
            {
                Text = "Stoel prijzen:",
                Y = Pos.Bottom(info) + 1
            };

            Label seats = new Label()
            {
                Text = $"Economy stoelen = €{SeatPrices["Economy"]} + €0,50 per gevlogen minuut.\nComfort stoelen = €{SeatPrices["Comfort"]} + €0,50 per gevlogen minuut.",
                Y = Pos.Bottom(price)
            };

            Add(info, price, seats);
        }
        else if (plane == "Airbus 330")
        {
            Label info = new Label()
            {
                Text = "Airbus A330-200 Specificaties:\n\n" +
                    "- Aantal passagiers: De A330-200 kan tussen de 246 en 406 passagiers vervoeren, afhankelijk van de cabineconfiguratie.\n\n" +
                    "- Bereik: Het vliegbereik van de A330-200 is ongeveer 12.500 kilometer, afhankelijk van de belading en weersomstandigheden.\n\n" +
                    "- Snelheid: De kruissnelheid van de A330-200 is ongeveer 871 km/u, variërend op basis van de hoogte en weersomstandigheden.\n\n" +
                    "- Motoren: De A330-200 is uitgerust met twee Rolls-Royce Trent 700 of General Electric CF6-80E1 motoren, elk met een stuwkracht van ongeveer 71.000 tot 72.000 pond.\n\n" +
                    "- Lengte en spanwijdte: De lengte van de A330-200 is ongeveer 59 meter en de spanwijdte is ongeveer 60 meter.\n\n" +
                    "- Eerste vlucht en in gebruik name: De A330-200 maakte zijn eerste vlucht op 13 augustus 1997 en werd geïntroduceerd in 1998.\n\n" +
                    "- Fabrikant: De A330-200 is geproduceerd door Airbus, een vooraanstaande vliegtuigbouwer wereldwijd.\n\n" +
                    "De Airbus A330-200 wordt veelvuldig gebruikt door luchtvaartmaatschappijen voor langeafstandsvluchten, waaronder intercontinentale vluchten tussen Europa en Azië, Noord-Amerika en Zuid-Amerika.\nHet vliegtuig geniet een uitstekende reputatie vanwege zijn efficiëntie, comfort en betrouwbaarheid.",
            };

            Label price = new Label()
            {
                Text = "Stoel prijzen:",
                Y = Pos.Bottom(info) + 1
            };

            Label seats = new Label()
            {
                Text = $"Economy stoelen          = €{SeatPrices["Economy"]} + €0,50 per gevlogen minuut.\nComfort stoelen          = €{SeatPrices["Comfort"]} + €0,50 per gevlogen minuut.\nStoelen voorin de cabine = €{SeatPrices["Front cabin seat"]} + €0,50 per gevlogen minuut.\nDuo stoelen              = €{SeatPrices["Duo seats"]} + €0,50 per gevlogen minuut.\nClub Class stoelen       = €{SeatPrices["Club Class"]} + €0,50 per gevlogen minuut.",
                Y = Pos.Bottom(price)
            };

            Add(info, price, seats);
        }
        else if (plane == "Boeing 787")
        {
            Label info = new Label()
            {
                Text = "Boeing 787 Dreamliner Specificaties:\n\n" +
                    "- Aantal passagiers: De 787 Dreamliner kan tussen de 242 en 330 passagiers vervoeren, afhankelijk van de cabineconfiguratie.\n\n" +
                    "- Bereik: Het vliegbereik van de 787 Dreamliner is ongeveer 14.800 tot 15.700 kilometer, afhankelijk van de variant en belading.\n\n" +
                    "- Snelheid: De kruissnelheid van de 787 Dreamliner is ongeveer 913 km/u, variërend op basis van de hoogte en weersomstandigheden.\n\n" +
                    "- Motoren: De 787 Dreamliner is uitgerust met twee General Electric GEnx of Rolls-Royce Trent 1000 motoren, elk met een stuwkracht van ongeveer 74.000 tot 78.000 pond.\n\n" +
                    "- Lengte en spanwijdte: De lengte van de 787 Dreamliner varieert tussen de 56 en 68 meter, afhankelijk van de variant, en de spanwijdte is ongeveer 60 meter.\n\n" +
                    "- Eerste vlucht en in gebruik name: De 787 Dreamliner maakte zijn eerste vlucht op 15 december 2009 en werd in 2011 in gebruik genomen.\n\n" +
                    "- Fabrikant: De 787 Dreamliner is geproduceerd door Boeing, een van de grootste vliegtuigbouwers ter wereld.\n\n" +
                    "De Boeing 787 Dreamliner wordt veelvuldig gebruikt door luchtvaartmaatschappijen voor langeafstandsvluchten, waaronder intercontinentale vluchten tussen Europa en Azië, Noord-Amerika en Zuid-Amerika.\n" +
                    "Het vliegtuig staat bekend om zijn brandstofefficiëntie, comfort en geavanceerde technologieën zoals het gebruik van lichtgewicht materialen, elektronische vliegbesturing en grote ramen met elektronisch dimmend glas.",
            };

            Label price = new Label()
            {
                Text = "Stoel prijzen:",
                Y = Pos.Bottom(info) + 1
            };

            Label seats = new Label()
            {
                Text = $"Economy stoelen              = €{SeatPrices["Economy"]} + €0,50 per gevlogen minuut.\nComfort stoelen              = €{SeatPrices["Economy Plus"]} + €0,50 per gevlogen minuut.\nUnited BusinessFirst stoelen = €{SeatPrices["United BusinessFirst"]} + €0,50 per gevlogen minuut.",
                Y = Pos.Bottom(price)
            };

            Add(info, price, seats);
        }

        Button terug = new Button()
        {
            Text = "Terug",
            Y = 30,
        };

        terug.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(terug);
    }
}

public class SeatsPlane : Toplevel
{
    public SeatsPlane(string plane)
    {
        SeatMap seatMap = new SeatMap(plane);
        Add(seatMap);

        Button terug = new Button()
        {
            Text = "Terug",
        };

        terug.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(terug);
    }
}