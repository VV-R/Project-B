namespace Entities;
public class Airplane
{
    public string RightWing;
    public (int Y, int X) RightWingPos;
    public (int Y, int X) RightWallPos;
    public int RightWallWidth;
    public (int Y, int X) LeftWallPos;
    public int LeftWallWidth;
    public string LeftWing;
    public (int Y, int X) LeftWingPos;
    public string CockPit;
    public (int Y, int X) CockPitPos;
    public (int Y, int X) BackPos;
    public int BackHeigth;
    public (int Y, int X) LegendPos;
    public List<Seat> Seats;

    public Airplane(string rWing, (int, int) rWingPos, (int, int) rWallPos, int rWallWidth, (int, int) lWallPos, int lWallWidth, string lWing, (int, int) lWingPos, string cockPit, (int, int) cockPitPos, (int, int) backPos, int backHeigth, (int, int) legendPos, List<Seat> seats) {
        RightWing = rWing;
        RightWingPos = rWingPos;
        RightWallPos = rWallPos;
        RightWallWidth = rWallWidth;
        LeftWallPos = lWallPos;
        LeftWallWidth = lWallWidth;
        LeftWing = lWing;
        LeftWingPos = lWingPos;
        CockPit = cockPit;
        CockPitPos = cockPitPos;
        BackPos = backPos;
        BackHeigth = backHeigth;
        LegendPos = legendPos;
        Seats = seats;
    }
}