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

        DeserializeOptions = new ();
        DeserializeOptions.Converters.Add(new EmailJsonConverter());

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

        List<T>? objs = JsonSerializer.Deserialize<List<T>>(jsonString, DeserializeOptions);
        foreach (var obj in objs) {
            set.Add(obj);
        }
    }
}
