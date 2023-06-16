using Terminal.Gui;
using Newtonsoft.Json;
using Entities;
using Managers;
using System.Text;

namespace Windows;
public class CreateAirplane : Toplevel
{
    List<SeatTypeView> SeatTypeViews = new List<SeatTypeView>();
    List<Button> RemoveButtons = new List<Button>();
    string AirplaneName = "";

    public CreateAirplane() {
        Start();
    }
    public void Start() {
        RemoveAll();
        List<string> seatTypes;
        using (StreamReader reader = new StreamReader("SeatingPrice.json")) {
            seatTypes = JsonConvert.DeserializeObject<Dictionary<string, double>>(reader.ReadToEnd())!.Keys.ToList();
        }

        Button addSeatTypeButton = new Button() {
            Text = "Stoel Typen Toevoegen",
        };

        Label airplaneNameLabel = new Label() {
            Text = "Vliegtuig naam:",
            Y = Pos.Bottom(addSeatTypeButton) + 1,
        };

        TextField airplaneName = new TextField(AirplaneName) {
            X = Pos.Right(airplaneNameLabel) + 1,
            Y = Pos.Top(airplaneNameLabel),
            Width = 20,
        };

        int xOffset = 44;
        int rows = 3;
        int boxHeight = 10;
        if (SeatTypeViews.Count != 0) {
            for (int i = 0; i < SeatTypeViews.Count; i++) {
                    SeatTypeViews[i].Y = Pos.Bottom(airplaneNameLabel) + (boxHeight * (i / rows)) + 1;
                    SeatTypeViews[i].X = xOffset * (i % rows);
                    Add(SeatTypeViews[i]);
            }
        }

        addSeatTypeButton.Clicked += () => {
            SeatTypeView seatTypeView = new SeatTypeView(seatTypes) {
                Y = Pos.Bottom(airplaneNameLabel) + (boxHeight * (SeatTypeViews.Count / rows)) + 1,
                X = xOffset * (SeatTypeViews.Count % rows),
                Width = xOffset,
                Height = boxHeight,
            };

            seatTypeView.RemoveButton.Clicked += () => {
                foreach (SeatTypeView seatType in SeatTypeViews) {
                    Remove(seatType);
                }
                SeatTypeViews.Remove(seatTypeView);

                for (int i = 0; i < SeatTypeViews.Count; i++) {
                    SeatTypeViews[i].Y = Pos.Bottom(airplaneNameLabel) + (boxHeight * (i / rows)) + 1;
                    SeatTypeViews[i].X = xOffset * (i % rows);
                    Add(SeatTypeViews[i]);
                }
            };
            SeatTypeViews.Add(seatTypeView);
            Add(seatTypeView);
        };

        Button previewButton = new Button() {
            Text = "Voorbeeld",
            X = Pos.Right(addSeatTypeButton) + 1
        };

        previewButton.Clicked += () => { AirplaneName = (string)airplaneName.Text; Preview();};

        Button addButton = new Button() {
            Text = "Toevoegen",
            X = Pos.Right(previewButton) + 1,
        };

        addButton.Clicked += () => { AirplaneName = (string)airplaneName.Text; AddAirplane(Generate()); WindowManager.GoBackOne(this); };

        Button goBackButton = new Button() {
            Text = "Terug",
            X = Pos.Right(addButton) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(addSeatTypeButton, previewButton, addButton, airplaneNameLabel, airplaneName);
    }

    public void Preview() {
        RemoveAll();
        Airplane plane = Generate();
        Button addButton = new Button() {
            Text = "Toevoegen",
        };

        addButton.Clicked += () => { AddAirplane(plane); WindowManager.GoBackOne(this); };

        Button goBackButton = new Button() {
            Text = "Terug",
            X = Pos.Right(addButton) + 1,
        };

        goBackButton.Clicked += Start;
        Add(addButton, goBackButton);

        Add(new SeatMap(plane) {Y = Pos.Bottom(goBackButton), ColorScheme = WindowManager.CurrentColor});
    }

    public void AddAirplane(Airplane plane) {
        Dictionary<string, Airplane> airplanes = new Dictionary<string, Airplane>();
        using (StreamReader reader = new StreamReader("Airplanes.json")) {
            airplanes = JsonConvert.DeserializeObject<Dictionary<string, Airplane>>(reader.ReadToEnd())!;
        }

        if (airplanes.ContainsKey(AirplaneName)) {
            MessageBox.ErrorQuery("Vliegtuig Toevoegen", $"Er bestaald al een vliegtuig genaamd: '{AirplaneName}'", "OK");
            return;
        }
        airplanes.Add(AirplaneName, plane);
        using (StreamWriter writer = new StreamWriter("Airplanes.json")) {
            writer.Write(JsonConvert.SerializeObject(airplanes, Formatting.Indented));
        }
    }

    public Airplane Generate() {
        StringBuilder rightWing = new StringBuilder();
        rightWing.AppendLine(@"          /               |");
        rightWing.AppendLine(@"         /                |");
        rightWing.AppendLine(@"        /                 |");
        rightWing.AppendLine(@"       /                  |");
        rightWing.AppendLine(@"      /                   |");
        rightWing.AppendLine(@"     /                    |");
        rightWing.AppendLine(@"    /                     |");
        rightWing.AppendLine(@"   /                      |");
        rightWing.AppendLine(@"  /                       |");
        rightWing.AppendLine(@" /                        |");
        rightWing.AppendLine(@"/                         |");

        StringBuilder leftWing = new StringBuilder();
        leftWing.AppendLine(@"\                         |");
        leftWing.AppendLine(@" \                        |");
        leftWing.AppendLine(@"  \                       |");
        leftWing.AppendLine(@"   \                      |");
        leftWing.AppendLine(@"    \                     |");
        leftWing.AppendLine(@"     \                    |");
        leftWing.AppendLine(@"      \                   |");
        leftWing.AppendLine(@"       \                  |");
        leftWing.AppendLine(@"        \                 |");
        leftWing.AppendLine(@"         \                |");
        leftWing.AppendLine(@"          \               |");


        int totalRows = 0;
        int maxColumns = 0;
        foreach (SeatTypeView seatTypeView in SeatTypeViews) {
            totalRows += int.Parse((string)seatTypeView.RowField.Text);
            int columns = int.Parse((string)seatTypeView.ColumnField.Text);
            if (maxColumns < columns) {
                maxColumns = columns;
            }
        }
        int heigth = maxColumns + 3;
        int halfHeigth = heigth / 2;
        int rightWingY = 4;
        int rightWallY = rightWingY + 11;
        int leftWallY = rightWallY + heigth;
        int currentRow = 1;
        int xOffset = 5;

        List<Seat> seats = new List<Seat>();
        foreach (SeatTypeView seatTypeView in SeatTypeViews) {
            int rows = int.Parse((string)seatTypeView.RowField.Text);
            double columns = double.Parse((string)seatTypeView.ColumnField.Text);
            for (double y = 0; y < maxColumns; y += (double)maxColumns / columns) {
                for (int x = 0; x < rows; x++) {
                    seats.Add(new Seat($"{x + currentRow}{Convert.ToChar(65 + (int)Math.Round(y))}",
                                        (currentRow * xOffset + ((x + 1) * xOffset)),
                                        rightWallY + (int)Math.Round(y) + 2,
                                        (string)seatTypeView.ComboBox.Text));
                }
            }
            currentRow += rows;
        }

        StringBuilder cockPit = new StringBuilder();
        for (int i = 0; i < heigth; i++) {
            string line = "";
            if (i == halfHeigth) {
                line = "(";
            } else if (i < halfHeigth) {
                line = "/".PadLeft(halfHeigth - i + 1);
            } else if (i > halfHeigth) {
                line = "\\".PadLeft(i - halfHeigth + 1);
            }
            cockPit.AppendLine(line);
        }
        int width = totalRows * xOffset;
        int halfWidth = width / 2;

        return new Airplane(rightWing.ToString(), (rightWingY, halfWidth),
                            (rightWallY, halfHeigth + xOffset), width,
                            (leftWallY, halfHeigth + xOffset), width, leftWing.ToString(),
                            (leftWallY + 1, halfWidth), cockPit.ToString(), (rightWallY, 4),
                            (rightWallY, width + xOffset * 2), heigth, (leftWallY + 14, xOffset * 5), seats);
    }
}

public class SeatTypeView : View
{
    public ComboBox ComboBox;
    public TextField RowField;
    public TextField ColumnField;
    public Button RemoveButton;
    public SeatTypeView(List<string> seatTypes)
    {
        Label seatTypeLabel = new Label(){
            Text = "Stoel Type:"
        };

        ComboBox = new ComboBox() {
            X = Pos.Right(seatTypeLabel) + 5,
            Width = seatTypes.Max(s => s.Length) + 2,
            Height = 6,
        };

        ComboBox.SetSource(seatTypes);

        Label rowLabel = new Label() {
            Text = "Aantal rijen:",
            Y = Pos.Bottom(seatTypeLabel) + 1,
        };

        RowField = new TextField("0") {
            Y = Pos.Top(rowLabel),
            X = Pos.Left(ComboBox),
            Width = 10,
        };

        RowField.TextChanged += (text) => {
        if (!int.TryParse(RowField.Text == "" ? "0" : (string)RowField.Text, out _))
            RowField.Text = text == "" ? "" : text;
        else if (RowField.Text.Length > 5)
            RowField.Text = text;
        RowField.CursorPosition = RowField.Text.Length;};

        Label columnLabel = new Label() {
            Text = "Aantal kolommen:",
            Y = Pos.Bottom(rowLabel) + 1,
        };

        ColumnField = new TextField("0") {
            Y = Pos.Top(columnLabel),
            X = Pos.Left(ComboBox),
            Width = 10,
        };

        ColumnField.TextChanged += (text) => {
        if (!int.TryParse(ColumnField.Text == "" ? "0" : (string)ColumnField.Text, out _))
            ColumnField.Text = text == "" ? "" : text;
        else if (ColumnField.Text.Length > 5)
            ColumnField.Text = text;
        ColumnField.CursorPosition = ColumnField.Text.Length;};

        RemoveButton = new Button() {
            Text = "Stoel Type Weghalen",
            Y = Pos.Bottom(columnLabel) + 1
        };

        Add(seatTypeLabel, ComboBox, rowLabel, RowField, columnLabel, ColumnField, RemoveButton);
    }
}