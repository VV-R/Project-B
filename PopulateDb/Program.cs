using System.Text.Json;
using System.IO;
using Db;
using Entities;
using Microsoft.EntityFrameworkCore;

class Program {
    public static void Main(string[] args) {
        string data = args[0];
        string db = args[1];

        Console.WriteLine(db);
        if (data.ToLower() == "flights"){
            PopulateFlights.Start(db);
            return;
        }

        using (var context = new ApplicationDbContext(db)) {
           AddEntities<User>(data, "Users.json", context.Users);
           AddEntities<Flight>(data, "Flights.json", context.Flights);
           context.SaveChanges();
        }

    }



    private static void AddEntities<T>(string data, string fileName, DbSet<T> set) where T : class {
        string jsonString;
        using (StreamReader sr = new StreamReader(data + fileName)) {
            jsonString = sr.ReadToEnd();
        }

        List<T>? objs = JsonSerializer.Deserialize<List<T>>(jsonString);
        foreach (var obj in objs) {
            set.Add(obj);
        }
    }
}
