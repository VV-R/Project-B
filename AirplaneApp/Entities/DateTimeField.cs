using Terminal.Gui;

namespace Entities;
public class DateTimeField : View
{

    private ComboBox _dayComboBox;
    private ComboBox _monthComboBox;
    private ComboBox _yearComboBox;
    private List<int> _yearRange;
    public DateTimeField(List<int> yearRange)
    {
        _yearRange = yearRange;
        int[] differentDays = {4, 6, 9, 11};
        int increaseDay = 1;

        _dayComboBox = new ComboBox(){
            Height = 4,
            Width = 8,
        };

        _dayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

        _monthComboBox = new ComboBox(){
            X = Pos.Right(_dayComboBox) + 1,
            Y = Pos.Top(_dayComboBox),
            Height = 4,
            Width = 8,
        };

        _monthComboBox.SetSource(Enumerable.Range(1, 12).ToList());

        _monthComboBox.SelectedItemChanged += (e) => { int month = Convert.ToInt32(e.Value); if (month == 2) {
            _dayComboBox.SetSource(Enumerable.Range(1, 28 + increaseDay).ToList());
            } else if (differentDays.Contains(month)) {
                _dayComboBox.SetSource(Enumerable.Range(1, 30).ToList());
             }
            else {
                _dayComboBox.SetSource(Enumerable.Range(1, 31).ToList());
            } _dayComboBox.SelectedItem = 0; };

        _yearComboBox = new ComboBox(){
            X = Pos.Right(_monthComboBox) + 1 ,
            Y = Pos.Top(_monthComboBox),
            Height = 4,
            Width = 8,
        };

        _yearComboBox.SetSource(yearRange);

        _yearComboBox.SelectedItemChanged += (e) => {int year = Convert.ToInt32(e.Value);
            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) {
                increaseDay = 1;
                _monthComboBox.SelectedItem = 0;
            }
            else increaseDay = 0;
            };

        _yearComboBox.SelectedItem = 0;
        _monthComboBox.SelectedItem = 0;
        _dayComboBox.SelectedItem = 0;

        Width = _dayComboBox.Width + _monthComboBox.Width + _monthComboBox.Width + 3;
        Height = _dayComboBox.Height;

        Add(_dayComboBox, _monthComboBox, _yearComboBox);
    }


    public DateTime GetDateTime() => new DateTime(Convert.ToInt32(_yearComboBox.Text), Convert.ToInt32(_monthComboBox.Text), Convert.ToInt32(_dayComboBox.Text));

    public void SetDateTime(DateTime date) {
        _yearComboBox.SelectedItem = date.Year - _yearRange.Min();
        _monthComboBox.SelectedItem = date.Month - 1;
        _dayComboBox.SelectedItem = date.Day - 1;
    }
}
