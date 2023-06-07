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

    public FlightOverview(Flight flight, List<UserInfo> userInfos, List<string> selectedSeats)
    {
        Label FirstnameLabel = new Label()
        {
            Text = ""
        };

        Add(FirstnameLabel);
    }
    
}
