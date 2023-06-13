using System.Text.Json;
using System.Globalization;
using System.Text.Json.Serialization;
using System.IO;
using Db;
using Entities;
using Microsoft.EntityFrameworkCore;

class Program {
    static JsonSerializerOptions DeserializeOptions;

    public static void Main(string[] args) {
        string data = args[0];
        string db = args[1];

        if (data.ToLower() == "flights"){
            PopulateFlights.Start(db);
            return;
        }
        DeserializeOptions = new ();
        DeserializeOptions.Converters.Add(new EmailJsonConverter());
        DeserializeOptions.Converters.Add(new JsonStringEnumConverter());

        using (var context = new ApplicationDbContext(db)) {
           AddEntities<User>(data, "Users.json", context.Users);
           PopulateFlights.Start(db);
           context.SaveChanges();
        }

        // Code to create a few tickets for testing
        // using (var context = new ApplicationDbContext(db)) {
        //     var user = context.Users.First(u => u.Role == "User");
        //     var flight1 = context.Flights.First(f => f.ArrivalLocation == "Parijs");
        //     var flight2 = context.Flights.First(f => f.ArrivalLocation == "London");
        //     var lastFlight = context.Flights.OrderBy(f => f.DepartureTime).Last();

        //     context.Tickets.Add(new Ticket(flight1.FlightNumber, user.UserInfoId, "B3", flight1.DepartureTime.AddMinutes(-30), "FN-00010001"));
        //     context.Tickets.Add(new Ticket(flight1.FlightNumber, user.UserInfoId, "B2", flight1.DepartureTime.AddMinutes(-30), "FN-00010001"));
        //     context.Tickets.Add(new Ticket(flight1.FlightNumber, user.UserInfoId, "B1", flight1.DepartureTime.AddMinutes(-30), "FN-00010001"));
        //     context.Tickets.Add(new Ticket(flight2.FlightNumber, user.UserInfoId, "C1", flight2.DepartureTime.AddMinutes(-30), "FN-00650001"));
        //     context.Tickets.Add(new Ticket(flight2.FlightNumber, user.UserInfoId, "C2", flight2.DepartureTime.AddMinutes(-30), "FN-00650001"));
        //     context.Tickets.Add(new Ticket(lastFlight.FlightNumber, user.UserInfoId, "D2", lastFlight.DepartureTime.AddMinutes(-30), "FN-01230001"));
        //     context.SaveChanges();
        // }
    }



    private static void AddEntities<T>(string data, string fileName, DbSet<T> set) where T : class {
        string jsonString;
        using (StreamReader sr = new StreamReader(data + fileName)) {
            jsonString = sr.ReadToEnd();
        }

        List<T>? objs = JsonSerializer.Deserialize<List<T>>(jsonString, DeserializeOptions);
        foreach (var obj in objs) {
            set.Add(obj);
        }
    }
}
