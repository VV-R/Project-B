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
                Text = "De Boeing 737-800 is een van de meest voorkomende commerciële vliegtuigtypes ter wereld. Hieronder vind je wat informatie over de specificaties van dit type vliegtuig:\nAantal passagiers: De 737-800 kan comfortabel plaats bieden aan maximaal 189 passagiers in een standaard configuratie.\nBereik: Het vliegbereik van de 737-800 is ongeveer 5.665 km, afhankelijk van de belading en de weersomstandigheden.\nSnelheid: De kruissnelheid van de 737-800 is ongeveer 840 km/u, maar dit kan variëren afhankelijk van de hoogte en de weersomstandigheden.\nMotoren: De 737-800 is uitgerust met twee CFM56-7B motoren, die elk een stuwkracht van ongeveer 27.000 tot 29.000 pond genereren.\nLengte en spanwijdte: De lengte van de 737-800 is ongeveer 39,5 meter en de spanwijdte is ongeveer 34 meter.\nEerste vlucht en in gebruik name: De 737-800 vloog voor het eerst op 31 juli 1997 en werd geïntroduceerd in 1998.\nFabrikant: De 737-800 is geproduceerd door Boeing, een van de grootste vliegtuigbouwers ter wereld.\nDe 737-800 wordt vaak gebruikt door luchtvaartmaatschappijen voor korte tot middellange afstanden, zoals binnenlandse vluchten en intercontinentale reizen binnen Europa en naar de Verenigde Staten."
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
                Text = "De Airbus A330-200 is een wide-body passagiersvliegtuig dat is geproduceerd door Airbus, een toonaangevende Europese vliegtuigbouwer. Hieronder vind je wat informatie over de specificaties van dit type vliegtuig:\nAantal passagiers: De A330-200 kan tussen de 246 en 406 passagiers vervoeren, afhankelijk van de configuratie van de cabine.\nBereik: Het vliegbereik van de A330-200 is ongeveer 12.500 kilometer, afhankelijk van de belading en de weersomstandigheden.\nSnelheid: De kruissnelheid van de A330-200 is ongeveer 871 km/u, maar dit kan variëren afhankelijk van de hoogte en de weersomstandigheden.\nMotoren: De A330-200 is uitgerust met twee Rolls-Royce Trent 700 of General Electric CF6-80E1 motoren, die elk een stuwkracht van ongeveer 71.000 tot 72.000 pond genereren.\nLengte en spanwijdte: De lengte van de A330-200 is ongeveer 59 meter en de spanwijdte is ongeveer 60 meter.\nEerste vlucht en in gebruik name: De A330-200 vloog voor het eerst op 13 augustus 1997 en werd geïntroduceerd in 1998.\nFabrikant: De A330-200 is geproduceerd door Airbus, een van de grootste vliegtuigbouwers ter wereld.\nDe A330-200 wordt vaak gebruikt door luchtvaartmaatschappijen voor langeafstandsvluchten, zoals intercontinentale vluchten tussen Europa en Azië, Noord-Amerika en Zuid-Amerika. Het vliegtuig heeft een goede reputatie vanwege zijn efficiëntie, comfort en betrouwbaarheid."
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
                Text = "De Boeing 787 Dreamliner is een wide-body passagiersvliegtuig dat is geproduceerd door Boeing, een van de grootste vliegtuigbouwers ter wereld. Hieronder vind je wat informatie over de specificaties van dit type vliegtuig:\nAantal passagiers: De 787 Dreamliner kan tussen de 242 en 330 passagiers vervoeren, afhankelijk van de configuratie van de cabine.\nBereik: Het vliegbereik van de 787 Dreamliner is ongeveer 14.800 tot 15.700 kilometer, afhankelijk van de variant en de belading.\nSnelheid: De kruissnelheid van de 787 Dreamliner is ongeveer 913 km/u, maar dit kan variëren afhankelijk van de hoogte en de weersomstandigheden.\nMotoren: De 787 Dreamliner is uitgerust met twee General Electric GEnx of Rolls-Royce Trent 1000 motoren, die elk een stuwkracht van ongeveer 74.000 tot 78.000 pond genereren.\nLengte en spanwijdte: De lengte van de 787 Dreamliner varieert tussen de 56 en 68 meter, afhankelijk van de variant, en de spanwijdte is ongeveer 60 meter.\nEerste vlucht en in gebruik name: De 787 Dreamliner vloog voor het eerst op 15 december 2009 en werd in 2011 in gebruik genomen.\nFabrikant: De 787 Dreamliner is geproduceerd door Boeing, een van de grootste vliegtuigbouwers ter wereld.\nDe 787 Dreamliner wordt vaak gebruikt door luchtvaartmaatschappijen voor langeafstandsvluchten, zoals intercontinentale vluchten tussen Europa en Azië, Noord-Amerika en Zuid-Amerika.\nHet vliegtuig is bekend om zijn brandstofefficiëntie, comfort en geavanceerde technologieën zoals het gebruik van lichtgewicht materialen, elektronische vliegbesturing en grote ramen met elektronisch dimmend glas."
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
            Y = 17
        };

        terug.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(terug);
    }
}

public class SeatsPlane : Toplevel
{
    public SeatsPlane(string plane)
    {
        if (plane == "Boeing 737")
        {
            StreamReader reader = new StreamReader("Boeing_737.json");
            List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;

            foreach (Seat seat in seats_list)
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, false);
                Add(interSeat);
            }

            #region Drawing
            StringBuilder RightWing = new StringBuilder();
            RightWing.AppendLine(@"          /               |");
            RightWing.AppendLine(@"         /                |");
            RightWing.AppendLine(@"        /                 |");
            RightWing.AppendLine(@"       /                  |");
            RightWing.AppendLine(@"      /                   |");
            RightWing.AppendLine(@"     /                    |");
            RightWing.AppendLine(@"    /                     |");
            RightWing.AppendLine(@"   /                      |");
            RightWing.AppendLine(@"  /                       |");
            RightWing.AppendLine(@" /                        |");
            RightWing.AppendLine(@"/                         |");
            RightWing.AppendLine(@"---------------------------");

            Label rightWing = new Label()
            {
                Text = RightWing.ToString(),
                Y = 5,
                X = 72
            };

            Label rightWall = new Label()
            {
                Text = "--------------------------------------------------------------------------------------------------------------------------------------",
                Y = 16,
                X = 31,
            };

            Label leftWall = new Label()
            {
                Text = "--------------------------------------------------------------------------------------------------------------------------------------",
                Y = 26,
                X = 31,
            };

            StringBuilder LeftWing = new StringBuilder();
            LeftWing.AppendLine(@"---------------------------");
            LeftWing.AppendLine(@"\                         |");
            LeftWing.AppendLine(@" \                        |");
            LeftWing.AppendLine(@"  \                       |");
            LeftWing.AppendLine(@"   \                      |");
            LeftWing.AppendLine(@"    \                     |");
            LeftWing.AppendLine(@"     \                    |");
            LeftWing.AppendLine(@"      \                   |");
            LeftWing.AppendLine(@"       \                  |");
            LeftWing.AppendLine(@"        \                 |");
            LeftWing.AppendLine(@"         \                |");
            LeftWing.AppendLine(@"          \               |");

            Label leftWing = new Label()
            {
                Text = LeftWing.ToString(),
                Y = 26,
                X = 72
            };

            StringBuilder Cockpit = new StringBuilder();
            Cockpit.AppendLine(@"     /");
            Cockpit.AppendLine(@"    /");
            Cockpit.AppendLine(@"   /");
            Cockpit.AppendLine(@"  /");
            Cockpit.AppendLine(@" /");
            Cockpit.AppendLine(@"(");
            Cockpit.AppendLine(@" \");
            Cockpit.AppendLine(@"  \");
            Cockpit.AppendLine(@"   \");
            Cockpit.AppendLine(@"    \");
            Cockpit.AppendLine(@"     \");

            Label cockpit = new Label()
            {
                Text = Cockpit.ToString(),
                Y = 16,
                X = 28
            };

            StringBuilder Back = new StringBuilder();
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");

            Label back = new Label()
            {
                Text = Back.ToString(),
                Y = 16,
                X = 164
            };

            Add(rightWall, leftWall, rightWing, leftWing, cockpit, back);
            #endregion

            #region Legend
            Label economy1 = new Label()
            {
                Text = "X00",
                X = 63,
                Y = 41,
                ColorScheme = Colors.ColorSchemes["Economy"]
            };

            Label economy2 = new Label()
            {
                Text = " => Economy",
                X = Pos.Right(economy1),
                Y = 41
            };

            Label comfort1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(economy2) + 5,
                Y = 41,
                ColorScheme = Colors.ColorSchemes["Comfort"]
            };

            Label comfort2 = new Label()
            {
                Text = " => Comfort",
                X = Pos.Right(comfort1),
                Y = 41
            };

            Label taken1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(comfort2) + 5,
                Y = 41,
                ColorScheme = Colors.ColorSchemes["SeatTaken"]
            };

            Label taken2 = new Label()
            {
                Text = " => Bezet",
                X = Pos.Right(taken1),
                Y = 41
            };

            Label selected1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(taken2) + 5,
                Y = 41,
                ColorScheme = Colors.ColorSchemes["SeatSelected"]
            };

            Label selected2 = new Label()
            {
                Text = " => Gekozen",
                X = Pos.Right(selected1),
                Y = 41
            };

            Add(economy1, economy2, comfort1, comfort2, taken1, taken2, selected1, selected2);
            #endregion
        }
        else if (plane == "Airbus 330")
        {
            StreamReader reader = new StreamReader("Airbus_330.json");
            List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;

            foreach (Seat seat in seats_list)
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, false);
                Add(interSeat);
            }

            #region Drawing
            StringBuilder LeftWing = new StringBuilder();
            LeftWing.AppendLine(@"|                                          \");
            LeftWing.AppendLine(@"|                                           \");
            LeftWing.AppendLine(@"|                                            \");
            LeftWing.AppendLine(@"|                                             \");
            LeftWing.AppendLine(@"|                                              \");
            LeftWing.AppendLine(@"|                                               \");
            LeftWing.AppendLine(@"|                                                \");
            LeftWing.AppendLine(@"---------------------------------------------------");

            Label lefttwing = new Label()
            {
                Text = LeftWing.ToString(),
                Y = 2,
                X = 75
            };

            Label rightWall = new Label()
            {
                Text = "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                Y = 23,
                X = 6,
            };

            Label leftWall = new Label()
            {
                Text = "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                Y = 9,
                X = 6,
            };

            StringBuilder RightWing = new StringBuilder();
            RightWing.AppendLine(@"---------------------------------------------------");
            RightWing.AppendLine(@"|                                                /");
            RightWing.AppendLine(@"|                                               /");
            RightWing.AppendLine(@"|                                              /");
            RightWing.AppendLine(@"|                                             /");
            RightWing.AppendLine(@"|                                            /");
            RightWing.AppendLine(@"|                                           /");
            RightWing.AppendLine(@"|                                          /");

            Label rightWing = new Label()
            {
                Text = RightWing.ToString(),
                Y = 23,
                X = 75
            };

            StringBuilder Cockpit = new StringBuilder();
            Cockpit.AppendLine(@"\");
            Cockpit.AppendLine(@" \");
            Cockpit.AppendLine(@"  \");
            Cockpit.AppendLine(@"   \");
            Cockpit.AppendLine(@"    \");
            Cockpit.AppendLine(@"     \");
            Cockpit.AppendLine(@"      \");
            Cockpit.AppendLine(@"       )");
            Cockpit.AppendLine(@"      /");
            Cockpit.AppendLine(@"     /");
            Cockpit.AppendLine(@"    /");
            Cockpit.AppendLine(@"   /");
            Cockpit.AppendLine(@"  /");
            Cockpit.AppendLine(@" /");
            Cockpit.AppendLine(@"/");

            Label cockpit = new Label()
            {
                Text = Cockpit.ToString(),
                Y = 9,
                X = 194
            };

            StringBuilder Back = new StringBuilder();
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");

            Label back = new Label()
            {
                Text = Back.ToString(),
                Y = 9,
                X = 6
            };

            Add(rightWall, leftWall, lefttwing, rightWing, cockpit, back);
            #endregion

            #region Legend
            Label economy1 = new Label()
            {
                Text = "X00",
                X = 27,
                Y = 34,
                ColorScheme = Colors.ColorSchemes["Economy"]
            };

            Label economy2 = new Label()
            {
                Text = " => Economy",
                X = Pos.Right(economy1),
                Y = 34
            };

            Label comfort1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(economy2) + 5,
                Y = 34,
                ColorScheme = Colors.ColorSchemes["Comfort"]
            };

            Label comfort2 = new Label()
            {
                Text = " => Comfort",
                X = Pos.Right(comfort1),
                Y = 34
            };

            Label frontSeats1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(comfort2) + 5,
                Y = 34,
                ColorScheme = Colors.ColorSchemes["Front seats"]
            };

            Label frontSeats2 = new Label()
            {
                Text = " => Stoelen voorin de cabine",
                X = Pos.Right(frontSeats1),
                Y = 34
            };

            Label duo1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(frontSeats2) + 5,
                Y = 34,
                ColorScheme = Colors.ColorSchemes["Duo seats"]
            };

            Label duo2 = new Label()
            {
                Text = " => Duo stoelen",
                X = Pos.Right(duo1),
                Y = 34
            };

            Label clubClass1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(duo2) + 5,
                Y = 34,
                ColorScheme = Colors.ColorSchemes["Club Class"]
            };

            Label clubClass2 = new Label()
            {
                Text = " => Club Class",
                X = Pos.Right(clubClass1),
                Y = 34
            };

            Label taken1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(clubClass2) + 5,
                Y = 34,
                ColorScheme = Colors.ColorSchemes["SeatTaken"]
            };

            Label taken2 = new Label()
            {
                Text = " => Bezet",
                X = Pos.Right(taken1),
                Y = 34
            };

            Label selected1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(taken2) + 5,
                Y = 34,
                ColorScheme = Colors.ColorSchemes["SeatSelected"]
            };

            Label selected2 = new Label()
            {
                Text = " => Gekozen",
                X = Pos.Right(selected1),
                Y = 34
            };

            Add(economy1, economy2, comfort1, comfort2, frontSeats1, frontSeats2, duo1, duo2, clubClass1, clubClass2, taken1, taken2, selected1, selected2);
            #endregion
        }
        else if (plane == "Boeing 787")
        {
            StreamReader reader = new StreamReader("boeing_787.json");
            List<Seat> seats_list = JsonConvert.DeserializeObject<List<Seat>>(reader.ReadToEnd())!;

            foreach (Seat seat in seats_list)
            {
                InteraciveSeat interSeat = new InteraciveSeat(seat, false);
                Add(interSeat);
            }

            #region Drawing
            StringBuilder RightWing = new StringBuilder();
            RightWing.AppendLine(@"    /                                              |");
            RightWing.AppendLine(@"   /                                               |");
            RightWing.AppendLine(@"  /                                                |");
            RightWing.AppendLine(@" /                                                 |");
            RightWing.AppendLine(@"/                                                  |");
            RightWing.AppendLine(@"----------------------------------------------------");

            Label rightwing = new Label()
            {
                Text = RightWing.ToString(),
                Y = 4,
                X = 66
            };

            Label rightWall = new Label()
            {
                Text = "-----------------------------------------------------------------------------------------------------------------------------------------",
                Y = 9,
                X = 38,
            };

            Label leftWall = new Label()
            {
                Text = "-----------------------------------------------------------------------------------------------------------------------------------------",
                Y = 23,
                X = 38,
            };

            StringBuilder LeftWing = new StringBuilder();
            LeftWing.AppendLine(@"----------------------------------------------------");
            LeftWing.AppendLine(@"\                                                  |");
            LeftWing.AppendLine(@" \                                                 |");
            LeftWing.AppendLine(@"  \                                                |");
            LeftWing.AppendLine(@"   \                                               |");
            LeftWing.AppendLine(@"    \                                              |");

            Label leftwing = new Label()
            {
                Text = LeftWing.ToString(),
                Y = 23,
                X = 66
            };

            StringBuilder Cockpit = new StringBuilder();
            Cockpit.AppendLine(@"       /");
            Cockpit.AppendLine(@"      /");
            Cockpit.AppendLine(@"     /");
            Cockpit.AppendLine(@"    /");
            Cockpit.AppendLine(@"   /");
            Cockpit.AppendLine(@"  /");
            Cockpit.AppendLine(@" /");
            Cockpit.AppendLine(@"(");
            Cockpit.AppendLine(@" \");
            Cockpit.AppendLine(@"  \");
            Cockpit.AppendLine(@"   \");
            Cockpit.AppendLine(@"    \");
            Cockpit.AppendLine(@"     \");
            Cockpit.AppendLine(@"      \");
            Cockpit.AppendLine(@"       \");

            Label cockpit = new Label()
            {
                Text = Cockpit.ToString(),
                Y = 9,
                X = 31
            };

            StringBuilder Back = new StringBuilder();
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");
            Back.AppendLine(@"|");

            Label back = new Label()
            {
                Text = Back.ToString(),
                Y = 9,
                X = 175
            };

            Add(rightWall, leftWall, rightwing, leftwing, cockpit, back);
            #endregion

            #region Legend
            Label economy1 = new Label()
            {
                Text = "X00",
                X = 55,
                Y = 33,
                ColorScheme = Colors.ColorSchemes["Economy"]
            };

            Label economy2 = new Label()
            {
                Text = " => Economy",
                X = Pos.Right(economy1),
                Y = 33
            };

            Label economyPlus1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(economy2) + 5,
                Y = 33,
                ColorScheme = Colors.ColorSchemes["Economy Plus"]
            };

            Label economyPlus2 = new Label()
            {
                Text = " => Economy Plus",
                X = Pos.Right(economyPlus1),
                Y = 33
            };

            Label UBF1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(economyPlus2) + 5,
                Y = 33,
                ColorScheme = Colors.ColorSchemes["United BusinessFirst"]
            };

            Label UBF2 = new Label()
            {
                Text = " => United BusinessFirst",
                X = Pos.Right(UBF1),
                Y = 33
            };

            Label taken1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(UBF2) + 5,
                Y = 33,
                ColorScheme = Colors.ColorSchemes["SeatTaken"]
            };

            Label taken2 = new Label()
            {
                Text = " => Bezet",
                X = Pos.Right(taken1),
                Y = 33
            };

            Label selected1 = new Label()
            {
                Text = "X00",
                X = Pos.Right(taken2) + 5,
                Y = 33,
                ColorScheme = Colors.ColorSchemes["SeatSelected"]
            };

            Label selected2 = new Label()
            {
                Text = " => Gekozen",
                X = Pos.Right(selected1),
                Y = 33
            };

            Add(economy1, economy2, economyPlus1, economyPlus2, UBF1, UBF2, taken1, taken2, selected1, selected2);
            #endregion
        }

        Button terug = new Button()
        {
            Text = "Terug",
        };

        terug.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(terug);
    }
}