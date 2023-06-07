using System;
using System.Net.Mail;
using Terminal.Gui;
using Managers;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Windows;

public class FlightOverview : Toplevel
{
    public TextField? FirstnameText;
    private Flight? flight;
    private List<UserInfo>? userInfos;
    private List<string>? selectedSeats;

    public FlightOverview(Flight flight, List<UserInfo> userInfos, List<Seat> selectedSeats)
    {
        EmailManager.SendInvoice("", "", userInfos[0], selectedSeats, flight);
        Button closeButton = new Button() {
            Text = "Afsluiten",
        };

        closeButton.Clicked += () => { WindowManager.GoToFirst(); };
    }
    
}
