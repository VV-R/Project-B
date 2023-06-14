namespace AirplaneApp.Tests.EntitiesTests;
using Entities;
using System;
using System.Globalization;

[TestClass]
public class FlightTest {
    private static IFormatProvider _cultureInfo = new CultureInfo("en-US");

    private static Flight _createFlight(
        DateTime departureTime, DateTime arrivalTime, bool isCancelled
    ) =>  new Flight("Rotterdam", departureTime, "Den Haag", arrivalTime, "Thunderbid One") {
        FlightNumber = 1, IsCancelled = isCancelled
    };

    [TestMethod]
    [DataRow("1:00:0", "20:0:0", FlightStatus.Waiting)] // Arrival time is irrelevant.
    [DataRow("0:30:01", "20:0:0", FlightStatus.Waiting)] // Arrival time is irrelevant.
    [DataRow("0:30:00", "20:0:0", FlightStatus.Boarding)] // Arrival time is irrelevant.

    [DataRow("0", "20:0:0", FlightStatus.Departed)]
    [DataRow("-1", "20:0:0", FlightStatus.Departed)]

    [DataRow("-10", "-1", FlightStatus.Completed)]
    [DataRow("-10", "-0:25", FlightStatus.Disembarking)]
    public void TestStatus(string dto, string ato, FlightStatus expectedStatus) {
        var departureTimeSpan = TimeSpan.Parse(dto, _cultureInfo);
        var arrivalTimeSpan = TimeSpan.Parse(ato, _cultureInfo);
        var flight = _createFlight(
            DateTime.Now + departureTimeSpan, DateTime.Now + arrivalTimeSpan, false
        );

        Assert.AreEqual(expectedStatus, flight.Status);
    }

    [TestMethod]
    public void TestStatusCancelled() {
        var flight = _createFlight(DateTime.Now, DateTime.Now.AddHours(5), true);
        Assert.AreEqual(flight.Status, FlightStatus.Cancelled);
    }
}
