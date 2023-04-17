using System;
using Terminal.Gui;

public class AirplaneInformation : Toplevel
{
    public AirplaneInformation()
    {
        Button goBackButton = new Button() {
            Y = 52,
            Text = "Terug"
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};

        Add(goBackButton);

        Button boeing737Button = new Button() {
            Y = 1,

            Text = "Boeing 737"
        };
      


        Button airbusButton = new Button() {
            Y = 2,
            Text = "Airbus 330-200"
        };

        Button boeing787Button = new Button() {
            Y = 3,
            Text = "Boeing 787"
        };
        Add(boeing737Button,airbusButton,boeing787Button);

        boeing737Button.Clicked += () =>
        {
            Remove(boeing737Button);
            Remove(airbusButton);
            Remove(boeing787Button);
            goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};
            Boeing737Info();
        };
        airbusButton.Clicked += () =>
        {
            Remove(boeing737Button);
            Remove(airbusButton);
            Remove(boeing787Button);
            goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};
            AirbusInfo();
        };
        boeing787Button.Clicked += () =>
        {
            Remove(boeing737Button);
            Remove(airbusButton);
            Remove(boeing787Button);
            goBackButton.Clicked += () => { WindowManager.GoBackOne(this);};
            Boeing787Info();
        };
    }
    public void Boeing737Info()
    {
        Text = "De Boeing 737-800 is een van de meest voorkomende commerciële vliegtuigtypes ter wereld. Hieronder vind je wat informatie over de specificaties van dit type vliegtuig:\nAantal passagiers: De 737-800 kan comfortabel plaats bieden aan maximaal 189 passagiers in een standaard configuratie.\nBereik: Het vliegbereik van de 737-800 is ongeveer 5.665 km, afhankelijk van de belading en de weersomstandigheden.\nSnelheid: De kruissnelheid van de 737-800 is ongeveer 840 km/u, maar dit kan variëren afhankelijk van de hoogte en de weersomstandigheden.\nMotoren: De 737-800 is uitgerust met twee CFM56-7B motoren, die elk een stuwkracht van ongeveer 27.000 tot 29.000 pond genereren.\nLengte en spanwijdte: De lengte van de 737-800 is ongeveer 39,5 meter en de spanwijdte is ongeveer 34 meter.\nEerste vlucht en in gebruik name: De 737-800 vloog voor het eerst op 31 juli 1997 en werd geïntroduceerd in 1998.\nFabrikant: De 737-800 is geproduceerd door Boeing, een van de grootste vliegtuigbouwers ter wereld.\nDe 737-800 wordt vaak gebruikt door luchtvaartmaatschappijen voor korte tot middellange afstanden, zoals binnenlandse vluchten en intercontinentale reizen binnen Europa en naar de Verenigde Staten.";
       
    }
    public void AirbusInfo()
    {
        Text = "De Airbus A330-200 is een wide-body passagiersvliegtuig dat is geproduceerd door Airbus, een toonaangevende Europese vliegtuigbouwer. Hieronder vind je wat informatie over de specificaties van dit type vliegtuig:\nAantal passagiers: De A330-200 kan tussen de 246 en 406 passagiers vervoeren, afhankelijk van de configuratie van de cabine.\nBereik: Het vliegbereik van de A330-200 is ongeveer 12.500 kilometer, afhankelijk van de belading en de weersomstandigheden.\nSnelheid: De kruissnelheid van de A330-200 is ongeveer 871 km/u, maar dit kan variëren afhankelijk van de hoogte en de weersomstandigheden.\nMotoren: De A330-200 is uitgerust met twee Rolls-Royce Trent 700 of General Electric CF6-80E1 motoren, die elk een stuwkracht van ongeveer 71.000 tot 72.000 pond genereren.\nLengte en spanwijdte: De lengte van de A330-200 is ongeveer 59 meter en de spanwijdte is ongeveer 60 meter.\nEerste vlucht en in gebruik name: De A330-200 vloog voor het eerst op 13 augustus 1997 en werd geïntroduceerd in 1998.\nFabrikant: De A330-200 is geproduceerd door Airbus, een van de grootste vliegtuigbouwers ter wereld.\nDe A330-200 wordt vaak gebruikt door luchtvaartmaatschappijen voor langeafstandsvluchten, zoals intercontinentale vluchten tussen Europa en Azië, Noord-Amerika en Zuid-Amerika. Het vliegtuig heeft een goede reputatie vanwege zijn efficiëntie, comfort en betrouwbaarheid.";
    }
    public void Boeing787Info()
    {
        Text = "De Boeing 787 Dreamliner is een wide-body passagiersvliegtuig dat is geproduceerd door Boeing, een van de grootste vliegtuigbouwers ter wereld. Hieronder vind je wat informatie over de specificaties van dit type vliegtuig:\nAantal passagiers: De 787 Dreamliner kan tussen de 242 en 330 passagiers vervoeren, afhankelijk van de configuratie van de cabine.\nBereik: Het vliegbereik van de 787 Dreamliner is ongeveer 14.800 tot 15.700 kilometer, afhankelijk van de variant en de belading.\nSnelheid: De kruissnelheid van de 787 Dreamliner is ongeveer 913 km/u, maar dit kan variëren afhankelijk van de hoogte en de weersomstandigheden.\nMotoren: De 787 Dreamliner is uitgerust met twee General Electric GEnx of Rolls-Royce Trent 1000 motoren, die elk een stuwkracht van ongeveer 74.000 tot 78.000 pond genereren.\nLengte en spanwijdte: De lengte van de 787 Dreamliner varieert tussen de 56 en 68 meter, afhankelijk van de variant, en de spanwijdte is ongeveer 60 meter.\nEerste vlucht en in gebruik name: De 787 Dreamliner vloog voor het eerst op 15 december 2009 en werd in 2011 in gebruik genomen.\nFabrikant: De 787 Dreamliner is geproduceerd door Boeing, een van de grootste vliegtuigbouwers ter wereld.\nDe 787 Dreamliner wordt vaak gebruikt door luchtvaartmaatschappijen voor langeafstandsvluchten, zoals intercontinentale vluchten tussen Europa en Azië, Noord-Amerika en Zuid-Amerika.\nHet vliegtuig is bekend om zijn brandstofefficiëntie, comfort en geavanceerde technologieën zoals het gebruik van lichtgewicht materialen, elektronische vliegbesturing en grote ramen met elektronisch dimmend glas.";
    }
}