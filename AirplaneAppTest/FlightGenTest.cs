namespace test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestRemoveOldFlights()
    {
        FlightScheduleGen.Flights.SetList(new List<Flight>() {
            new Flight(0, "Rotterdam", DateTime.Now.AddDays(-2), "Parijs", DateTime.Now.AddDays(-1), "Boeing 737"),
            new Flight(1, "Rotterdam", DateTime.Now.AddDays(-2), "Madrid", DateTime.Now.AddDays(-2.2), "Airbus 330"),
            new Flight(2, "Rotterdam", DateTime.Now.AddDays(1), "Berlijn", DateTime.Now.AddDays(1.1), "Boeing 737"),
            new Flight(3, "Rotterdam", DateTime.Now.AddDays(1), "Parijs", DateTime.Now.AddDays(1.7), "Boeing 787")
        });
        FlightScheduleGen.RemoveOld();
        Assert.AreEqual(FlightScheduleGen.Flights.Count, 2);
    }

    [TestMethod]
    public void TestPopulateOne()
    {
        FlightScheduleGen.Flights.SetList(new List<Flight>());
        FlightScheduleGen.PopulateByLocation("");
        Assert.AreEqual(FlightScheduleGen.Flights.Count, 0);
        FlightScheduleGen.PopulateByLocation("Parijs");
        Assert.AreEqual(FlightScheduleGen.Flights.Count, FlightScheduleGen.AmountOfFlights);
        Assert.IsFalse(FlightScheduleGen.Flights.Where(fl => fl.ArrivalLocation == "Parijs").Count() == 0);
    }

    [TestMethod]
    public void TestPopulateTwo()
    {
        FlightScheduleGen.Flights.SetList(new List<Flight>());
        FlightScheduleGen.PopulateByLocation("");
        Assert.AreEqual(FlightScheduleGen.Flights.Count, 0);
        FlightScheduleGen.PopulateByLocation("Parijs");
        Assert.AreEqual(FlightScheduleGen.Flights.Count, FlightScheduleGen.AmountOfFlights);
        Assert.IsFalse(FlightScheduleGen.Flights.Where(fl => fl.ArrivalLocation == "Parijs").Count() == 0);
        FlightScheduleGen.PopulateByLocation("Madrid");
        Assert.AreEqual(FlightScheduleGen.Flights.Count, FlightScheduleGen.AmountOfFlights * 2);
        Assert.IsFalse(FlightScheduleGen.Flights.Where(fl => fl.ArrivalLocation == "Madrid").Count() == 0);
    }

    [TestMethod]
    public void TestPopulateAll()
    {
        FlightScheduleGen.Flights.SetList(new List<Flight>());
        FlightScheduleGen.PopulateAll();
        Assert.AreEqual(FlightScheduleGen.Flights.Count, FlightScheduleGen.AmountOfFlights * 9);
    }

    [TestMethod]
    public void TestUpdateFeature()
    {
        Assert.AreEqual(FlightScheduleGen.Flights.Count, 576);
    }
}