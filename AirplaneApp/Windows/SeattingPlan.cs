using System;
using Terminal.Gui;
using Newtonsoft.Json;

public class SeattingPlan : Toplevel
{
    ComboBox PlaneType;
    public SeattingPlan()
    {
        bool clicked = false;

        Button goBackButton = new Button()
        {
            Text = "Terug"
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        PlaneType = new ComboBox()
        {
            Y = Pos.Bottom(goBackButton) + 1,
            Width = 20,
            Height = 7
        };
        PlaneType.SetSource(new List<string>() { "Boeing 737", "Airbus 330" });

        Label Test = new Label()
        {
            Text = "test",
            Y = Pos.Bottom(goBackButton) + 10,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        Test.Clicked += () =>
        {
            clicked = !clicked;

            if (clicked)
            {
                Test.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            }
            else if (!clicked)
            {
                Test.ColorScheme = Colors.ColorSchemes["SeatOpen"];
            }
        };

        Button testButton = new Button()
        {
            Text = "Test Button",
            Y = Pos.Bottom(goBackButton) + 3
        };

        testButton.Clicked += () => 
        {
            if (PlaneType.Text == "Boeing 737")
            {
                PlanBoeing_737();
            }
            else if (PlaneType.Text == "Airbus 330")
            {
                
            }
        };

        Add(goBackButton, testButton, PlaneType);
    }

    private void PlanBoeing_737()
    {
        /////////
        //Row F//
        /////////

        #region Row F
        bool F2_Clicked = false; 
        Label F2 = new Label()
        {
            Text = "F2",
            Y = 18,
            X = 40,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F2.Clicked += () =>
        {
            F2_Clicked = !F2_Clicked;
            if (F2_Clicked)
                F2.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F2_Clicked)
                F2.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F3_Clicked = false; 
        Label F3 = new Label()
        {
            Text = "F3",
            Y = 18,
            X = Pos.Right(F2) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F3.Clicked += () =>
        {
            F3_Clicked = !F3_Clicked;

            if (F3_Clicked)
                F3.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F3_Clicked)
                F3.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F4_Clicked = false; 
        Label F4 = new Label()
        {
            Text = "F4",
            Y = 18,
            X = Pos.Right(F3) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F4.Clicked += () =>
        {
            F4_Clicked = !F4_Clicked;

            if (F4_Clicked)
                F4.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F4_Clicked)
                F4.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F5_Clicked = false; 
        Label F5 = new Label()
        {
            Text = "F5",
            Y = 18,
            X = Pos.Right(F4) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F5.Clicked += () =>
        {
            F5_Clicked = !F5_Clicked;

            if (F5_Clicked)
                F5.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F5_Clicked)
                F5.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F6_Clicked = false; 
        Label F6 = new Label()
        {
            Text = "F6",
            Y = 18,
            X = Pos.Right(F5) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F6.Clicked += () =>
        {
            F6_Clicked = !F6_Clicked;

            if (F6_Clicked)
                F6.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F6_Clicked)
                F6.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F7_Clicked = false; 
        Label F7 = new Label()
        {
            Text = "F7",
            Y = 18,
            X = Pos.Right(F6) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F7.Clicked += () =>
        {
            F7_Clicked = !F7_Clicked;

            if (F7_Clicked)
                F7.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F7_Clicked)
                F7.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F8_Clicked = false; 
        Label F8 = new Label()
        {
            Text = "F8",
            Y = 18,
            X = Pos.Right(F7) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F8.Clicked += () =>
        {
            F8_Clicked = !F8_Clicked;

            if (F8_Clicked)
                F8.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F8_Clicked)
                F8.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F9_Clicked = false; 
        Label F9 = new Label()
        {
            Text = "F9",
            Y = 18,
            X = Pos.Right(F8) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F9.Clicked += () =>
        {
            F9_Clicked = !F9_Clicked;

            if (F9_Clicked)
                F9.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F9_Clicked)
                F9.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F10_Clicked = false; 
        Label F10 = new Label()
        {
            Text = "F10",
            Y = 18,
            X = Pos.Right(F9) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F10.Clicked += () =>
        {
            F10_Clicked = !F10_Clicked;

            if (F10_Clicked)
                F10.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F10_Clicked)
                F10.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F11_Clicked = false; 
        Label F11 = new Label()
        {
            Text = "F11",
            Y = 18,
            X = Pos.Right(F10) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F11.Clicked += () =>
        {
            F11_Clicked = !F11_Clicked;

            if (F11_Clicked)
                F11.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F11_Clicked)
                F11.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F12_Clicked = false; 
        Label F12 = new Label()
        {
            Text = "F12",
            Y = 18,
            X = Pos.Right(F11) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F12.Clicked += () =>
        {
            F12_Clicked = !F12_Clicked;

            if (F12_Clicked)
                F12.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F12_Clicked)
                F12.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F13_Clicked = false; 
        Label F13 = new Label()
        {
            Text = "F13",
            Y = 18,
            X = Pos.Right(F12) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F13.Clicked += () =>
        {
            F13_Clicked = !F13_Clicked;

            if (F13_Clicked)
                F13.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F13_Clicked)
                F13.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F14_Clicked = false; 
        Label F14 = new Label()
        {
            Text = "F14",
            Y = 18,
            X = Pos.Right(F13) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F14.Clicked += () =>
        {
            F14_Clicked = !F14_Clicked;

            if (F14_Clicked)
                F14.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F14_Clicked)
                F14.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F15_Clicked = false; 
        Label F15 = new Label()
        {
            Text = "F15",
            Y = 18,
            X = Pos.Right(F14) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F15.Clicked += () =>
        {
            F15_Clicked = !F15_Clicked;

            if (F15_Clicked)
                F15.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F15_Clicked)
                F15.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F16_Clicked = false; 
        Label F16 = new Label()
        {
            Text = "F16",
            Y = 18,
            X = Pos.Right(F15) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F16.Clicked += () =>
        {
            F16_Clicked = !F16_Clicked;

            if (F16_Clicked)
                F16.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F16_Clicked)
                F16.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F17_Clicked = false; 
        Label F17 = new Label()
        {
            Text = "F17",
            Y = 18,
            X = Pos.Right(F16) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F17.Clicked += () =>
        {
            F17_Clicked = !F17_Clicked;

            if (F17_Clicked)
                F17.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F17_Clicked)
                F17.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F18_Clicked = false; 
        Label F18 = new Label()
        {
            Text = "F18",
            Y = 18,
            X = Pos.Right(F17) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F18.Clicked += () =>
        {
            F18_Clicked = !F18_Clicked;

            if (F18_Clicked)
                F18.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F13_Clicked)
                F18.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F19_Clicked = false; 
        Label F19 = new Label()
        {
            Text = "F19",
            Y = 18,
            X = Pos.Right(F18) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F19.Clicked += () =>
        {
            F19_Clicked = !F19_Clicked;

            if (F19_Clicked)
                F19.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F19_Clicked)
                F19.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F20_Clicked = false; 
        Label F20 = new Label()
        {
            Text = "F20",
            Y = 18,
            X = Pos.Right(F19) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F20.Clicked += () =>
        {
            F20_Clicked = !F20_Clicked;

            if (F20_Clicked)
                F20.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F20_Clicked)
                F20.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F21_Clicked = false; 
        Label F21 = new Label()
        {
            Text = "F21",
            Y = 18,
            X = Pos.Right(F20) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F21.Clicked += () =>
        {
            F21_Clicked = !F21_Clicked;

            if (F21_Clicked)
                F21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F21_Clicked)
                F21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F22_Clicked = false; 
        Label F22 = new Label()
        {
            Text = "F22",
            Y = 18,
            X = Pos.Right(F21) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F22.Clicked += () =>
        {
            F22_Clicked = !F22_Clicked;

            if (F22_Clicked)
                F22.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F22_Clicked)
                F22.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F23_Clicked = false; 
        Label F23 = new Label()
        {
            Text = "F23",
            Y = 18,
            X = Pos.Right(F22) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F23.Clicked += () =>
        {
            F23_Clicked = !F21_Clicked;

            if (F23_Clicked)
                F21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F23_Clicked)
                F21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F24_Clicked = false; 
        Label F24 = new Label()
        {
            Text = "F24",
            Y = 18,
            X = Pos.Right(F23) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F24.Clicked += () =>
        {
            F24_Clicked = !F24_Clicked;

            if (F24_Clicked)
                F24.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F24_Clicked)
                F24.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F25_Clicked = false; 
        Label F25 = new Label()
        {
            Text = "F25",
            Y = 18,
            X = Pos.Right(F24) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F25.Clicked += () =>
        {
            F25_Clicked = !F25_Clicked;

            if (F25_Clicked)
                F25.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F25_Clicked)
                F25.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F26_Clicked = false; 
        Label F26 = new Label()
        {
            Text = "F26",
            Y = 18,
            X = Pos.Right(F25) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F26.Clicked += () =>
        {
            F26_Clicked = !F26_Clicked;

            if (F26_Clicked)
                F26.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F26_Clicked)
                F26.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F27_Clicked = false; 
        Label F27 = new Label()
        {
            Text = "F27",
            Y = 18,
            X = Pos.Right(F26) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F27.Clicked += () =>
        {
            F27_Clicked = !F27_Clicked;

            if (F27_Clicked)
                F27.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F27_Clicked)
                F27.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F28_Clicked = false; 
        Label F28 = new Label()
        {
            Text = "F28",
            Y = 18,
            X = Pos.Right(F27) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F28.Clicked += () =>
        {
            F28_Clicked = !F28_Clicked;

            if (F28_Clicked)
                F28.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F28_Clicked)
                F28.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F29_Clicked = false; 
        Label F29 = new Label()
        {
            Text = "F29",
            Y = 18,
            X = Pos.Right(F28) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F29.Clicked += () =>
        {
            F29_Clicked = !F29_Clicked;

            if (F29_Clicked)
                F29.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F29_Clicked)
                F29.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F30_Clicked = false; 
        Label F30 = new Label()
        {
            Text = "F30",
            Y = 18,
            X = Pos.Right(F29) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F30.Clicked += () =>
        {
            F30_Clicked = !F30_Clicked;

            if (F30_Clicked)
                F30.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F30_Clicked)
                F30.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F31_Clicked = false; 
        Label F31 = new Label()
        {
            Text = "F31",
            Y = 18,
            X = Pos.Right(F30) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F31.Clicked += () =>
        {
            F31_Clicked = !F31_Clicked;

            if (F31_Clicked)
                F31.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F31_Clicked)
                F31.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F32_Clicked = false; 
        Label F32 = new Label()
        {
            Text = "F32",
            Y = 18,
            X = Pos.Right(F31) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F32.Clicked += () =>
        {
            F32_Clicked = !F32_Clicked;

            if (F32_Clicked)
                F32.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F32_Clicked)
                F32.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool F33_Clicked = false; 
        Label F33 = new Label()
        {
            Text = "F33",
            Y = 18,
            X = Pos.Right(F32) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        F33.Clicked += () =>
        {
            F33_Clicked = !F33_Clicked;

            if (F33_Clicked)
                F33.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!F33_Clicked)
                F33.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        Add(F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24, F25, F26, F27, F28, F29, F30, F31, F32, F33);
        #endregion

        /////////
        //Row E//
        /////////

        #region Row E
        bool E2_Clicked = false; 
        Label E2 = new Label()
        {
            Text = "E2",
            Y = Pos.Bottom(F2),
            X = 40,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E2.Clicked += () =>
        {
            E2_Clicked = !E2_Clicked;
            if (E2_Clicked)
                E2.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E2_Clicked)
                E2.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E3_Clicked = false; 
        Label E3 = new Label()
        {
            Text = "E3",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E2) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E3.Clicked += () =>
        {
            E3_Clicked = !E3_Clicked;

            if (E3_Clicked)
                E3.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E3_Clicked)
                E3.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E4_Clicked = false; 
        Label E4 = new Label()
        {
            Text = "E4",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E3) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E4.Clicked += () =>
        {
            E4_Clicked = !E4_Clicked;

            if (E4_Clicked)
                E4.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E4_Clicked)
                E4.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E5_Clicked = false; 
        Label E5 = new Label()
        {
            Text = "E5",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E4) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E5.Clicked += () =>
        {
            E5_Clicked = !E5_Clicked;

            if (E5_Clicked)
                E5.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E5_Clicked)
                E5.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E6_Clicked = false; 
        Label E6 = new Label()
        {
            Text = "E6",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E5) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E6.Clicked += () =>
        {
            E6_Clicked = !E6_Clicked;

            if (E6_Clicked)
                E6.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E6_Clicked)
                E6.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E7_Clicked = false; 
        Label E7 = new Label()
        {
            Text = "E7",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E6) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E7.Clicked += () =>
        {
            E7_Clicked = !E7_Clicked;

            if (E7_Clicked)
                E7.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E7_Clicked)
                E7.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E8_Clicked = false; 
        Label E8 = new Label()
        {
            Text = "E8",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E7) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E8.Clicked += () =>
        {
            E8_Clicked = !E8_Clicked;

            if (E8_Clicked)
                E8.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E8_Clicked)
                E8.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E9_Clicked = false; 
        Label E9 = new Label()
        {
            Text = "E9",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E8) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E9.Clicked += () =>
        {
            E9_Clicked = !E9_Clicked;

            if (E9_Clicked)
                E9.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E9_Clicked)
                E9.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E10_Clicked = false; 
        Label E10 = new Label()
        {
            Text = "E10",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E9) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E10.Clicked += () =>
        {
            E10_Clicked = !E10_Clicked;

            if (E10_Clicked)
                E10.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E10_Clicked)
                E10.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E11_Clicked = false; 
        Label E11 = new Label()
        {
            Text = "E11",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E10) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E11.Clicked += () =>
        {
            E11_Clicked = !E11_Clicked;

            if (E11_Clicked)
                E11.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E11_Clicked)
                E11.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E12_Clicked = false; 
        Label E12 = new Label()
        {
            Text = "E12",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E11) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E12.Clicked += () =>
        {
            E12_Clicked = !E12_Clicked;

            if (E12_Clicked)
                E12.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E12_Clicked)
                E12.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E13_Clicked = false; 
        Label E13 = new Label()
        {
            Text = "E13",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E12) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E13.Clicked += () =>
        {
            E13_Clicked = !E13_Clicked;

            if (E13_Clicked)
                E13.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E13_Clicked)
                E13.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E14_Clicked = false; 
        Label E14 = new Label()
        {
            Text = "E14",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E13) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E14.Clicked += () =>
        {
            E14_Clicked = !E14_Clicked;

            if (E14_Clicked)
                E14.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E14_Clicked)
                E14.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E15_Clicked = false; 
        Label E15 = new Label()
        {
            Text = "E15",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E14) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E15.Clicked += () =>
        {
            E15_Clicked = !E15_Clicked;

            if (E15_Clicked)
                E15.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E15_Clicked)
                E15.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E16_Clicked = false; 
        Label E16 = new Label()
        {
            Text = "E16",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E15) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E16.Clicked += () =>
        {
            E16_Clicked = !E16_Clicked;

            if (E16_Clicked)
                E16.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E16_Clicked)
                E16.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E17_Clicked = false; 
        Label E17 = new Label()
        {
            Text = "E17",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E16) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E17.Clicked += () =>
        {
            E17_Clicked = !E17_Clicked;

            if (E17_Clicked)
                E17.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E17_Clicked)
                E17.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E18_Clicked = false; 
        Label E18 = new Label()
        {
            Text = "E18",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E17) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E18.Clicked += () =>
        {
            E18_Clicked = !E18_Clicked;

            if (E18_Clicked)
                E18.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E13_Clicked)
                E18.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E19_Clicked = false; 
        Label E19 = new Label()
        {
            Text = "E19",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E18) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E19.Clicked += () =>
        {
            E19_Clicked = !E19_Clicked;

            if (E19_Clicked)
                E19.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E19_Clicked)
                E19.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E20_Clicked = false; 
        Label E20 = new Label()
        {
            Text = "E20",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E19) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E20.Clicked += () =>
        {
            E20_Clicked = !E20_Clicked;

            if (E20_Clicked)
                E20.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E20_Clicked)
                E20.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E21_Clicked = false; 
        Label E21 = new Label()
        {
            Text = "E21",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E20) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E21.Clicked += () =>
        {
            E21_Clicked = !E21_Clicked;

            if (E21_Clicked)
                E21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E21_Clicked)
                E21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E22_Clicked = false; 
        Label E22 = new Label()
        {
            Text = "E22",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E21) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E22.Clicked += () =>
        {
            E22_Clicked = !E22_Clicked;

            if (E22_Clicked)
                E22.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E22_Clicked)
                E22.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E23_Clicked = false; 
        Label E23 = new Label()
        {
            Text = "E23",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E22) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E23.Clicked += () =>
        {
            E23_Clicked = !E21_Clicked;

            if (E23_Clicked)
                E21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E23_Clicked)
                E21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E24_Clicked = false; 
        Label E24 = new Label()
        {
            Text = "E24",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E23) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E24.Clicked += () =>
        {
            E24_Clicked = !E24_Clicked;

            if (E24_Clicked)
                E24.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E24_Clicked)
                E24.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E25_Clicked = false; 
        Label E25 = new Label()
        {
            Text = "E25",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E24) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E25.Clicked += () =>
        {
            E25_Clicked = !E25_Clicked;

            if (E25_Clicked)
                E25.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E25_Clicked)
                E25.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E26_Clicked = false; 
        Label E26 = new Label()
        {
            Text = "E26",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E25) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E26.Clicked += () =>
        {
            E26_Clicked = !E26_Clicked;

            if (E26_Clicked)
                E26.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E26_Clicked)
                E26.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E27_Clicked = false; 
        Label E27 = new Label()
        {
            Text = "E27",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E26) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E27.Clicked += () =>
        {
            E27_Clicked = !E27_Clicked;

            if (E27_Clicked)
                E27.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E27_Clicked)
                E27.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E28_Clicked = false; 
        Label E28 = new Label()
        {
            Text = "E28",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E27) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E28.Clicked += () =>
        {
            E28_Clicked = !E28_Clicked;

            if (E28_Clicked)
                E28.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E28_Clicked)
                E28.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E29_Clicked = false; 
        Label E29 = new Label()
        {
            Text = "E29",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E28) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E29.Clicked += () =>
        {
            E29_Clicked = !E29_Clicked;

            if (E29_Clicked)
                E29.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E29_Clicked)
                E29.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E30_Clicked = false; 
        Label E30 = new Label()
        {
            Text = "E30",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E29) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E30.Clicked += () =>
        {
            E30_Clicked = !E30_Clicked;

            if (E30_Clicked)
                E30.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E30_Clicked)
                E30.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E31_Clicked = false; 
        Label E31 = new Label()
        {
            Text = "E31",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E30) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E31.Clicked += () =>
        {
            E31_Clicked = !E31_Clicked;

            if (E31_Clicked)
                E31.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E31_Clicked)
                E31.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E32_Clicked = false; 
        Label E32 = new Label()
        {
            Text = "E32",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E31) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E32.Clicked += () =>
        {
            E32_Clicked = !E32_Clicked;

            if (E32_Clicked)
                E32.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E32_Clicked)
                E32.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool E33_Clicked = false; 
        Label E33 = new Label()
        {
            Text = "E33",
            Y = Pos.Bottom(F2),
            X = Pos.Right(E32) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        E33.Clicked += () =>
        {
            E33_Clicked = !E33_Clicked;

            if (E33_Clicked)
                E33.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!E33_Clicked)
                E33.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        Add(E2, E3, E4, E5, E6, E7, E8, E9, E10, E11, E12, E13, E14, E15, E16, E17, E18, E19, E20, E21, E22, E23, E24, E25, E26, E27, E28, E29, E30, E31, E32, E33);
        #endregion

        /////////
        //Row D//
        /////////

        #region Row D
        bool D2_Clicked = false; 
        Label D2 = new Label()
        {
            Text = "D2",
            Y = Pos.Bottom(E2),
            X = 40,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D2.Clicked += () =>
        {
            D2_Clicked = !D2_Clicked;
            if (D2_Clicked)
                D2.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D2_Clicked)
                D2.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D3_Clicked = false; 
        Label D3 = new Label()
        {
            Text = "D3",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D2) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D3.Clicked += () =>
        {
            D3_Clicked = !D3_Clicked;

            if (D3_Clicked)
                D3.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D3_Clicked)
                D3.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D4_Clicked = false; 
        Label D4 = new Label()
        {
            Text = "D4",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D3) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D4.Clicked += () =>
        {
            D4_Clicked = !D4_Clicked;

            if (D4_Clicked)
                D4.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D4_Clicked)
                D4.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D5_Clicked = false; 
        Label D5 = new Label()
        {
            Text = "D5",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D4) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D5.Clicked += () =>
        {
            D5_Clicked = !D5_Clicked;

            if (D5_Clicked)
                D5.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D5_Clicked)
                D5.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D6_Clicked = false; 
        Label D6 = new Label()
        {
            Text = "D6",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D5) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D6.Clicked += () =>
        {
            D6_Clicked = !D6_Clicked;

            if (D6_Clicked)
                D6.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D6_Clicked)
                D6.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D7_Clicked = false; 
        Label D7 = new Label()
        {
            Text = "D7",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D6) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D7.Clicked += () =>
        {
            D7_Clicked = !D7_Clicked;

            if (D7_Clicked)
                D7.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D7_Clicked)
                D7.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D8_Clicked = false; 
        Label D8 = new Label()
        {
            Text = "D8",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D7) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D8.Clicked += () =>
        {
            D8_Clicked = !D8_Clicked;

            if (D8_Clicked)
                D8.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D8_Clicked)
                D8.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D9_Clicked = false; 
        Label D9 = new Label()
        {
            Text = "D9",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D8) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D9.Clicked += () =>
        {
            D9_Clicked = !D9_Clicked;

            if (D9_Clicked)
                D9.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D9_Clicked)
                D9.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D10_Clicked = false; 
        Label D10 = new Label()
        {
            Text = "D10",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D9) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D10.Clicked += () =>
        {
            D10_Clicked = !D10_Clicked;

            if (D10_Clicked)
                D10.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D10_Clicked)
                D10.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D11_Clicked = false; 
        Label D11 = new Label()
        {
            Text = "D11",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D10) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D11.Clicked += () =>
        {
            D11_Clicked = !D11_Clicked;

            if (D11_Clicked)
                D11.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D11_Clicked)
                D11.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D12_Clicked = false; 
        Label D12 = new Label()
        {
            Text = "D12",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D11) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D12.Clicked += () =>
        {
            D12_Clicked = !D12_Clicked;

            if (D12_Clicked)
                D12.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D12_Clicked)
                D12.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D13_Clicked = false; 
        Label D13 = new Label()
        {
            Text = "D13",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D12) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D13.Clicked += () =>
        {
            D13_Clicked = !D13_Clicked;

            if (D13_Clicked)
                D13.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D13_Clicked)
                D13.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D14_Clicked = false; 
        Label D14 = new Label()
        {
            Text = "D14",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D13) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D14.Clicked += () =>
        {
            D14_Clicked = !D14_Clicked;

            if (D14_Clicked)
                D14.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D14_Clicked)
                D14.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D15_Clicked = false; 
        Label D15 = new Label()
        {
            Text = "D15",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D14) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D15.Clicked += () =>
        {
            D15_Clicked = !E15_Clicked;

            if (D15_Clicked)
                D15.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D15_Clicked)
                D15.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D16_Clicked = false; 
        Label D16 = new Label()
        {
            Text = "D16",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D15) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D16.Clicked += () =>
        {
            D16_Clicked = !D16_Clicked;

            if (D16_Clicked)
                D16.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D16_Clicked)
                D16.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D17_Clicked = false; 
        Label D17 = new Label()
        {
            Text = "D17",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D16) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D17.Clicked += () =>
        {
            D17_Clicked = !D17_Clicked;

            if (D17_Clicked)
                D17.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D17_Clicked)
                D17.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D18_Clicked = false; 
        Label D18 = new Label()
        {
            Text = "D18",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D17) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D18.Clicked += () =>
        {
            D18_Clicked = !D18_Clicked;

            if (D18_Clicked)
                D18.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D13_Clicked)
                D18.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D19_Clicked = false; 
        Label D19 = new Label()
        {
            Text = "D19",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D18) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D19.Clicked += () =>
        {
            D19_Clicked = !D19_Clicked;

            if (D19_Clicked)
                D19.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D19_Clicked)
                D19.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D20_Clicked = false; 
        Label D20 = new Label()
        {
            Text = "D20",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D19) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D20.Clicked += () =>
        {
            D20_Clicked = !D20_Clicked;

            if (D20_Clicked)
                D20.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D20_Clicked)
                D20.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D21_Clicked = false; 
        Label D21 = new Label()
        {
            Text = "D21",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D20) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D21.Clicked += () =>
        {
            D21_Clicked = !D21_Clicked;

            if (D21_Clicked)
                D21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D21_Clicked)
                D21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D22_Clicked = false; 
        Label D22 = new Label()
        {
            Text = "D22",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D21) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D22.Clicked += () =>
        {
            D22_Clicked = !D22_Clicked;

            if (D22_Clicked)
                D22.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D22_Clicked)
                D22.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D23_Clicked = false; 
        Label D23 = new Label()
        {
            Text = "D23",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D22) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D23.Clicked += () =>
        {
            D23_Clicked = !D21_Clicked;

            if (D23_Clicked)
                D21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D23_Clicked)
                D21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D24_Clicked = false; 
        Label D24 = new Label()
        {
            Text = "D24",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D23) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D24.Clicked += () =>
        {
            D24_Clicked = !D24_Clicked;

            if (D24_Clicked)
                D24.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D24_Clicked)
                D24.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D25_Clicked = false; 
        Label D25 = new Label()
        {
            Text = "D25",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D24) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D25.Clicked += () =>
        {
            D25_Clicked = !D25_Clicked;

            if (D25_Clicked)
                D25.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D25_Clicked)
                D25.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D26_Clicked = false; 
        Label D26 = new Label()
        {
            Text = "D26",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D25) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D26.Clicked += () =>
        {
            D26_Clicked = !D26_Clicked;

            if (D26_Clicked)
                D26.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D26_Clicked)
                D26.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D27_Clicked = false; 
        Label D27 = new Label()
        {
            Text = "D27",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D26) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D27.Clicked += () =>
        {
            D27_Clicked = !D27_Clicked;

            if (D27_Clicked)
                D27.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D27_Clicked)
                D27.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D28_Clicked = false; 
        Label D28 = new Label()
        {
            Text = "D28",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D27) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D28.Clicked += () =>
        {
            D28_Clicked = !D28_Clicked;

            if (D28_Clicked)
                D28.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D28_Clicked)
                D28.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D29_Clicked = false; 
        Label D29 = new Label()
        {
            Text = "D29",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D28) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D29.Clicked += () =>
        {
            D29_Clicked = !D29_Clicked;

            if (D29_Clicked)
                D29.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D29_Clicked)
                D29.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D30_Clicked = false; 
        Label D30 = new Label()
        {
            Text = "D30",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D29) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D30.Clicked += () =>
        {
            D30_Clicked = !D30_Clicked;

            if (D30_Clicked)
                D30.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D30_Clicked)
                D30.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D31_Clicked = false; 
        Label D31 = new Label()
        {
            Text = "D31",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D30) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D31.Clicked += () =>
        {
            D31_Clicked = !D31_Clicked;

            if (D31_Clicked)
                D31.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D31_Clicked)
                D31.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D32_Clicked = false; 
        Label D32 = new Label()
        {
            Text = "D32",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D31) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D32.Clicked += () =>
        {
            D32_Clicked = !D32_Clicked;

            if (D32_Clicked)
                D32.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D32_Clicked)
                D32.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool D33_Clicked = false; 
        Label D33 = new Label()
        {
            Text = "D33",
            Y = Pos.Bottom(E2),
            X = Pos.Right(D32) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        D33.Clicked += () =>
        {
            D33_Clicked = !D33_Clicked;

            if (D33_Clicked)
                D33.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!D33_Clicked)
                D33.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        Add(D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30, D31, D32, D33);
        #endregion 

        /////////
        //Row C//
        /////////

        #region Row C
        bool C1_Clicked = false; 
        Label C1 = new Label()
        {
            Text = "C1",
            Y = Pos.Bottom(D2) + 1,
            X = 37,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C1.Clicked += () =>
        {
            C1_Clicked = !C1_Clicked;
            if (C1_Clicked)
                C1.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C1_Clicked)
                C1.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C2_Clicked = false; 
        Label C2 = new Label()
        {
            Text = "C2",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C1) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C2.Clicked += () =>
        {
            C2_Clicked = !C2_Clicked;
            if (C2_Clicked)
                C2.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C2_Clicked)
                C2.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C3_Clicked = false; 
        Label C3 = new Label()
        {
            Text = "C3",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C2) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C3.Clicked += () =>
        {
            C3_Clicked = !C3_Clicked;

            if (C3_Clicked)
                C3.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C3_Clicked)
                C3.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C4_Clicked = false; 
        Label C4 = new Label()
        {
            Text = "C4",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C3) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C4.Clicked += () =>
        {
            C4_Clicked = !C4_Clicked;

            if (C4_Clicked)
                C4.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C4_Clicked)
                C4.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C5_Clicked = false; 
        Label C5 = new Label()
        {
            Text = "C5",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C4) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C5.Clicked += () =>
        {
            C5_Clicked = !C5_Clicked;

            if (C5_Clicked)
                C5.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C5_Clicked)
                C5.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C6_Clicked = false; 
        Label C6 = new Label()
        {
            Text = "C6",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C5) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C6.Clicked += () =>
        {
            C6_Clicked = !C6_Clicked;

            if (C6_Clicked)
                C6.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C6_Clicked)
                C6.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C7_Clicked = false; 
        Label C7 = new Label()
        {
            Text = "C7",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C6) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C7.Clicked += () =>
        {
            C7_Clicked = !C7_Clicked;

            if (C7_Clicked)
                C7.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C7_Clicked)
                C7.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C8_Clicked = false; 
        Label C8 = new Label()
        {
            Text = "C8",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C7) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C8.Clicked += () =>
        {
            C8_Clicked = !C8_Clicked;

            if (C8_Clicked)
                C8.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C8_Clicked)
                C8.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C9_Clicked = false; 
        Label C9 = new Label()
        {
            Text = "C9",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C8) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C9.Clicked += () =>
        {
            C9_Clicked = !C9_Clicked;

            if (C9_Clicked)
                C9.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C9_Clicked)
                C9.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C10_Clicked = false; 
        Label C10 = new Label()
        {
            Text = "C10",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C9) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C10.Clicked += () =>
        {
            C10_Clicked = !C10_Clicked;

            if (C10_Clicked)
                C10.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C10_Clicked)
                C10.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C11_Clicked = false; 
        Label C11 = new Label()
        {
            Text = "C11",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C10) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C11.Clicked += () =>
        {
            C11_Clicked = !C11_Clicked;

            if (C11_Clicked)
                C11.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C11_Clicked)
                C11.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C12_Clicked = false; 
        Label C12 = new Label()
        {
            Text = "C12",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C11) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C12.Clicked += () =>
        {
            C12_Clicked = !C12_Clicked;

            if (C12_Clicked)
                C12.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C12_Clicked)
                C12.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C13_Clicked = false; 
        Label C13 = new Label()
        {
            Text = "C13",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C12) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C13.Clicked += () =>
        {
            C13_Clicked = !C13_Clicked;

            if (C13_Clicked)
                C13.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C13_Clicked)
                C13.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C14_Clicked = false; 
        Label C14 = new Label()
        {
            Text = "C14",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C13) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C14.Clicked += () =>
        {
            C14_Clicked = !C14_Clicked;

            if (C14_Clicked)
                C14.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C14_Clicked)
                C14.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C15_Clicked = false; 
        Label C15 = new Label()
        {
            Text = "C15",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C14) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C15.Clicked += () =>
        {
            C15_Clicked = !E15_Clicked;

            if (C15_Clicked)
                C15.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C15_Clicked)
                C15.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C16_Clicked = false; 
        Label C16 = new Label()
        {
            Text = "C16",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C15) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C16.Clicked += () =>
        {
            C16_Clicked = !C16_Clicked;

            if (C16_Clicked)
                C16.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C16_Clicked)
                C16.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C17_Clicked = false; 
        Label C17 = new Label()
        {
            Text = "C17",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C16) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C17.Clicked += () =>
        {
            C17_Clicked = !C17_Clicked;

            if (C17_Clicked)
                C17.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C17_Clicked)
                C17.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C18_Clicked = false; 
        Label C18 = new Label()
        {
            Text = "C18",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C17) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C18.Clicked += () =>
        {
            C18_Clicked = !C18_Clicked;

            if (C18_Clicked)
                C18.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C13_Clicked)
                C18.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C19_Clicked = false; 
        Label C19 = new Label()
        {
            Text = "C19",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C18) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C19.Clicked += () =>
        {
            C19_Clicked = !C19_Clicked;

            if (C19_Clicked)
                C19.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C19_Clicked)
                C19.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C20_Clicked = false; 
        Label C20 = new Label()
        {
            Text = "C20",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C19) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C20.Clicked += () =>
        {
            C20_Clicked = !C20_Clicked;

            if (C20_Clicked)
                C20.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C20_Clicked)
                C20.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C21_Clicked = false; 
        Label C21 = new Label()
        {
            Text = "C21",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C20) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C21.Clicked += () =>
        {
            C21_Clicked = !C21_Clicked;

            if (C21_Clicked)
                C21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C21_Clicked)
                C21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C22_Clicked = false; 
        Label C22 = new Label()
        {
            Text = "C22",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C21) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C22.Clicked += () =>
        {
            C22_Clicked = !C22_Clicked;

            if (C22_Clicked)
                C22.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C22_Clicked)
                C22.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C23_Clicked = false; 
        Label C23 = new Label()
        {
            Text = "C23",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C22) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C23.Clicked += () =>
        {
            C23_Clicked = !C21_Clicked;

            if (C23_Clicked)
                C21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C23_Clicked)
                C21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C24_Clicked = false; 
        Label C24 = new Label()
        {
            Text = "C24",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C23) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C24.Clicked += () =>
        {
            C24_Clicked = !C24_Clicked;

            if (C24_Clicked)
                C24.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C24_Clicked)
                C24.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C25_Clicked = false; 
        Label C25 = new Label()
        {
            Text = "C25",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C24) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C25.Clicked += () =>
        {
            C25_Clicked = !C25_Clicked;

            if (C25_Clicked)
                C25.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C25_Clicked)
                C25.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C26_Clicked = false; 
        Label C26 = new Label()
        {
            Text = "C26",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C25) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C26.Clicked += () =>
        {
            C26_Clicked = !C26_Clicked;

            if (C26_Clicked)
                C26.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C26_Clicked)
                C26.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C27_Clicked = false; 
        Label C27 = new Label()
        {
            Text = "C27",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C26) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C27.Clicked += () =>
        {
            C27_Clicked = !C27_Clicked;

            if (C27_Clicked)
                C27.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C27_Clicked)
                C27.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C28_Clicked = false; 
        Label C28 = new Label()
        {
            Text = "C28",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C27) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C28.Clicked += () =>
        {
            C28_Clicked = !C28_Clicked;

            if (C28_Clicked)
                C28.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C28_Clicked)
                C28.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C29_Clicked = false; 
        Label C29 = new Label()
        {
            Text = "C29",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C28) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C29.Clicked += () =>
        {
            C29_Clicked = !C29_Clicked;

            if (C29_Clicked)
                C29.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C29_Clicked)
                C29.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C30_Clicked = false; 
        Label C30 = new Label()
        {
            Text = "C30",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C29) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C30.Clicked += () =>
        {
            C30_Clicked = !C30_Clicked;

            if (C30_Clicked)
                C30.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C30_Clicked)
                C30.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C31_Clicked = false; 
        Label C31 = new Label()
        {
            Text = "C31",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C30) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C31.Clicked += () =>
        {
            C31_Clicked = !C31_Clicked;

            if (C31_Clicked)
                C31.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C31_Clicked)
                C31.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C32_Clicked = false; 
        Label C32 = new Label()
        {
            Text = "C32",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C31) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C32.Clicked += () =>
        {
            C32_Clicked = !C32_Clicked;

            if (C32_Clicked)
                C32.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C32_Clicked)
                C32.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool C33_Clicked = false; 
        Label C33 = new Label()
        {
            Text = "C33",
            Y = Pos.Bottom(D2) + 1,
            X = Pos.Right(C32) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        C33.Clicked += () =>
        {
            C33_Clicked = !C33_Clicked;

            if (C33_Clicked)
                C33.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!C33_Clicked)
                C33.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        Add(C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, C15, C16, C17, C18, C19, C20, C21, C22, C23, C24, C25, C26, C27, C28, C29, C30, C31, C32, C33);
        #endregion
    
        /////////
        //Row B//
        /////////

        #region Row B
        bool B1_Clicked = false; 
        Label B1 = new Label()
        {
            Text = "B1",
            Y = Pos.Bottom(C2),
            X = 37,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B1.Clicked += () =>
        {
            B1_Clicked = !B1_Clicked;
            if (B1_Clicked)
                B1.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B1_Clicked)
                B1.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B2_Clicked = false; 
        Label B2 = new Label()
        {
            Text = "B2",
            Y = Pos.Bottom(C2),
            X = 40,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B2.Clicked += () =>
        {
            B2_Clicked = !B2_Clicked;
            if (B2_Clicked)
                B2.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B2_Clicked)
                B2.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B3_Clicked = false; 
        Label B3 = new Label()
        {
            Text = "B3",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B2) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B3.Clicked += () =>
        {
            B3_Clicked = !B3_Clicked;

            if (B3_Clicked)
                B3.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B3_Clicked)
                B3.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B4_Clicked = false; 
        Label B4 = new Label()
        {
            Text = "B4",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B3) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B4.Clicked += () =>
        {
            B4_Clicked = !B4_Clicked;

            if (B4_Clicked)
                B4.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B4_Clicked)
                B4.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B5_Clicked = false; 
        Label B5 = new Label()
        {
            Text = "B5",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B4) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B5.Clicked += () =>
        {
            B5_Clicked = !B5_Clicked;

            if (B5_Clicked)
                B5.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B5_Clicked)
                B5.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B6_Clicked = false; 
        Label B6 = new Label()
        {
            Text = "B6",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B5) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B6.Clicked += () =>
        {
            B6_Clicked = !B6_Clicked;

            if (B6_Clicked)
                B6.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B6_Clicked)
                B6.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B7_Clicked = false; 
        Label B7 = new Label()
        {
            Text = "B7",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B6) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B7.Clicked += () =>
        {
            B7_Clicked = !B7_Clicked;

            if (B7_Clicked)
                B7.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B7_Clicked)
                B7.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B8_Clicked = false; 
        Label B8 = new Label()
        {
            Text = "B8",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B7) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B8.Clicked += () =>
        {
            B8_Clicked = !B8_Clicked;

            if (B8_Clicked)
                B8.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B8_Clicked)
                B8.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B9_Clicked = false; 
        Label B9 = new Label()
        {
            Text = "B9",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B8) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B9.Clicked += () =>
        {
            B9_Clicked = !B9_Clicked;

            if (B9_Clicked)
                B9.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B9_Clicked)
                B9.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B10_Clicked = false; 
        Label B10 = new Label()
        {
            Text = "B10",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B9) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B10.Clicked += () =>
        {
            B10_Clicked = !B10_Clicked;

            if (B10_Clicked)
                B10.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B10_Clicked)
                B10.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B11_Clicked = false; 
        Label B11 = new Label()
        {
            Text = "B11",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B10) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B11.Clicked += () =>
        {
            B11_Clicked = !B11_Clicked;

            if (B11_Clicked)
                B11.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B11_Clicked)
                B11.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B12_Clicked = false; 
        Label B12 = new Label()
        {
            Text = "B12",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B11) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B12.Clicked += () =>
        {
            B12_Clicked = !B12_Clicked;

            if (B12_Clicked)
                B12.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B12_Clicked)
                B12.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B13_Clicked = false; 
        Label B13 = new Label()
        {
            Text = "B13",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B12) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B13.Clicked += () =>
        {
            B13_Clicked = !B13_Clicked;

            if (B13_Clicked)
                B13.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B13_Clicked)
                B13.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B14_Clicked = false; 
        Label B14 = new Label()
        {
            Text = "B14",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B13) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B14.Clicked += () =>
        {
            B14_Clicked = !B14_Clicked;

            if (B14_Clicked)
                B14.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B14_Clicked)
                B14.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B15_Clicked = false; 
        Label B15 = new Label()
        {
            Text = "B15",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B14) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B15.Clicked += () =>
        {
            B15_Clicked = !E15_Clicked;

            if (B15_Clicked)
                B15.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B15_Clicked)
                B15.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B16_Clicked = false; 
        Label B16 = new Label()
        {
            Text = "B16",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B15) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B16.Clicked += () =>
        {
            B16_Clicked = !B16_Clicked;

            if (B16_Clicked)
                B16.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B16_Clicked)
                B16.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B17_Clicked = false; 
        Label B17 = new Label()
        {
            Text = "B17",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B16) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B17.Clicked += () =>
        {
            B17_Clicked = !B17_Clicked;

            if (B17_Clicked)
                B17.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B17_Clicked)
                B17.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B18_Clicked = false; 
        Label B18 = new Label()
        {
            Text = "B18",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B17) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B18.Clicked += () =>
        {
            B18_Clicked = !B18_Clicked;

            if (B18_Clicked)
                B18.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B13_Clicked)
                B18.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B19_Clicked = false; 
        Label B19 = new Label()
        {
            Text = "B19",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B18) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B19.Clicked += () =>
        {
            B19_Clicked = !B19_Clicked;

            if (B19_Clicked)
                B19.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B19_Clicked)
                B19.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B20_Clicked = false; 
        Label B20 = new Label()
        {
            Text = "B20",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B19) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B20.Clicked += () =>
        {
            B20_Clicked = !B20_Clicked;

            if (B20_Clicked)
                B20.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B20_Clicked)
                B20.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B21_Clicked = false; 
        Label B21 = new Label()
        {
            Text = "B21",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B20) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B21.Clicked += () =>
        {
            B21_Clicked = !B21_Clicked;

            if (B21_Clicked)
                B21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B21_Clicked)
                B21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B22_Clicked = false; 
        Label B22 = new Label()
        {
            Text = "B22",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B21) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B22.Clicked += () =>
        {
            B22_Clicked = !B22_Clicked;

            if (B22_Clicked)
                B22.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B22_Clicked)
                B22.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B23_Clicked = false; 
        Label B23 = new Label()
        {
            Text = "B23",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B22) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B23.Clicked += () =>
        {
            B23_Clicked = !B21_Clicked;

            if (B23_Clicked)
                B21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B23_Clicked)
                B21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B24_Clicked = false; 
        Label B24 = new Label()
        {
            Text = "B24",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B23) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B24.Clicked += () =>
        {
            B24_Clicked = !B24_Clicked;

            if (B24_Clicked)
                B24.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B24_Clicked)
                B24.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B25_Clicked = false; 
        Label B25 = new Label()
        {
            Text = "B25",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B24) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B25.Clicked += () =>
        {
            B25_Clicked = !B25_Clicked;

            if (B25_Clicked)
                B25.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B25_Clicked)
                B25.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B26_Clicked = false; 
        Label B26 = new Label()
        {
            Text = "B26",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B25) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B26.Clicked += () =>
        {
            B26_Clicked = !B26_Clicked;

            if (B26_Clicked)
                B26.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B26_Clicked)
                B26.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B27_Clicked = false; 
        Label B27 = new Label()
        {
            Text = "B27",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B26) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B27.Clicked += () =>
        {
            B27_Clicked = !B27_Clicked;

            if (B27_Clicked)
                B27.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B27_Clicked)
                B27.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B28_Clicked = false; 
        Label B28 = new Label()
        {
            Text = "B28",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B27) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B28.Clicked += () =>
        {
            B28_Clicked = !B28_Clicked;

            if (B28_Clicked)
                B28.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B28_Clicked)
                B28.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B29_Clicked = false; 
        Label B29 = new Label()
        {
            Text = "B29",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B28) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B29.Clicked += () =>
        {
            B29_Clicked = !B29_Clicked;

            if (B29_Clicked)
                B29.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B29_Clicked)
                B29.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B30_Clicked = false; 
        Label B30 = new Label()
        {
            Text = "B30",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B29) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B30.Clicked += () =>
        {
            B30_Clicked = !B30_Clicked;

            if (B30_Clicked)
                B30.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B30_Clicked)
                B30.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B31_Clicked = false; 
        Label B31 = new Label()
        {
            Text = "B31",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B30) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B31.Clicked += () =>
        {
            B31_Clicked = !B31_Clicked;

            if (B31_Clicked)
                B31.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B31_Clicked)
                B31.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B32_Clicked = false; 
        Label B32 = new Label()
        {
            Text = "B32",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B31) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B32.Clicked += () =>
        {
            B32_Clicked = !B32_Clicked;

            if (B32_Clicked)
                B32.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B32_Clicked)
                B32.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool B33_Clicked = false; 
        Label B33 = new Label()
        {
            Text = "B33",
            Y = Pos.Bottom(C2),
            X = Pos.Right(B32) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        B33.Clicked += () =>
        {
            B33_Clicked = !B33_Clicked;

            if (B33_Clicked)
                B33.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!B33_Clicked)
                B33.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        Add(B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11, B12, B13, B14, B15, B16, B17, B18, B19, B20, B21, B22, B23, B24, B25, B26, B27, B28, B29, B30, B31, B32, B33);
        #endregion
    
        /////////
        //Row A//
        /////////

        #region Row A
        bool A1_Clicked = false; 
        Label A1 = new Label()
        {
            Text = "A1",
            Y = Pos.Bottom(B2),
            X = 37,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A1.Clicked += () =>
        {
            A1_Clicked = !A1_Clicked;
            if (A1_Clicked)
                A1.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A1_Clicked)
                A1.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A2_Clicked = false; 
        Label A2 = new Label()
        {
            Text = "A2",
            Y = Pos.Bottom(B2),
            X = 40,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A2.Clicked += () =>
        {
            A2_Clicked = !A2_Clicked;
            if (A2_Clicked)
                A2.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A2_Clicked)
                A2.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A3_Clicked = false; 
        Label A3 = new Label()
        {
            Text = "A3",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A2) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A3.Clicked += () =>
        {
            A3_Clicked = !A3_Clicked;

            if (A3_Clicked)
                A3.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A3_Clicked)
                A3.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A4_Clicked = false; 
        Label A4 = new Label()
        {
            Text = "A4",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A3) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A4.Clicked += () =>
        {
            A4_Clicked = !A4_Clicked;

            if (A4_Clicked)
                A4.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A4_Clicked)
                A4.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A5_Clicked = false; 
        Label A5 = new Label()
        {
            Text = "A5",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A4) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A5.Clicked += () =>
        {
            A5_Clicked = !A5_Clicked;

            if (A5_Clicked)
                A5.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A5_Clicked)
                A5.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A6_Clicked = false; 
        Label A6 = new Label()
        {
            Text = "A6",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A5) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A6.Clicked += () =>
        {
            A6_Clicked = !A6_Clicked;

            if (A6_Clicked)
                A6.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A6_Clicked)
                A6.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A7_Clicked = false; 
        Label A7 = new Label()
        {
            Text = "A7",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A6) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A7.Clicked += () =>
        {
            A7_Clicked = !A7_Clicked;

            if (A7_Clicked)
                A7.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A7_Clicked)
                A7.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A8_Clicked = false; 
        Label A8 = new Label()
        {
            Text = "A8",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A7) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A8.Clicked += () =>
        {
            A8_Clicked = !A8_Clicked;

            if (A8_Clicked)
                A8.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A8_Clicked)
                A8.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A9_Clicked = false; 
        Label A9 = new Label()
        {
            Text = "A9",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A8) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A9.Clicked += () =>
        {
            A9_Clicked = !A9_Clicked;

            if (A9_Clicked)
                A9.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A9_Clicked)
                A9.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A10_Clicked = false; 
        Label A10 = new Label()
        {
            Text = "A10",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A9) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A10.Clicked += () =>
        {
            A10_Clicked = !A10_Clicked;

            if (A10_Clicked)
                A10.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A10_Clicked)
                A10.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A11_Clicked = false; 
        Label A11 = new Label()
        {
            Text = "A11",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A10) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A11.Clicked += () =>
        {
            A11_Clicked = !A11_Clicked;

            if (A11_Clicked)
                A11.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A11_Clicked)
                A11.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A12_Clicked = false; 
        Label A12 = new Label()
        {
            Text = "A12",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A11) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A12.Clicked += () =>
        {
            A12_Clicked = !A12_Clicked;

            if (A12_Clicked)
                A12.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A12_Clicked)
                A12.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A13_Clicked = false; 
        Label A13 = new Label()
        {
            Text = "A13",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A12) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A13.Clicked += () =>
        {
            A13_Clicked = !A13_Clicked;

            if (A13_Clicked)
                A13.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A13_Clicked)
                A13.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A14_Clicked = false; 
        Label A14 = new Label()
        {
            Text = "A14",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A13) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A14.Clicked += () =>
        {
            A14_Clicked = !A14_Clicked;

            if (A14_Clicked)
                A14.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A14_Clicked)
                A14.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A15_Clicked = false; 
        Label A15 = new Label()
        {
            Text = "A15",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A14) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A15.Clicked += () =>
        {
            A15_Clicked = !E15_Clicked;

            if (A15_Clicked)
                A15.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A15_Clicked)
                A15.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A16_Clicked = false; 
        Label A16 = new Label()
        {
            Text = "A16",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A15) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A16.Clicked += () =>
        {
            A16_Clicked = !A16_Clicked;

            if (A16_Clicked)
                A16.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A16_Clicked)
                A16.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A17_Clicked = false; 
        Label A17 = new Label()
        {
            Text = "A17",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A16) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A17.Clicked += () =>
        {
            A17_Clicked = !A17_Clicked;

            if (A17_Clicked)
                A17.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A17_Clicked)
                A17.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A18_Clicked = false; 
        Label A18 = new Label()
        {
            Text = "A18",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A17) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A18.Clicked += () =>
        {
            A18_Clicked = !A18_Clicked;

            if (A18_Clicked)
                A18.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A13_Clicked)
                A18.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A19_Clicked = false; 
        Label A19 = new Label()
        {
            Text = "A19",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A18) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A19.Clicked += () =>
        {
            A19_Clicked = !A19_Clicked;

            if (A19_Clicked)
                A19.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A19_Clicked)
                A19.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A20_Clicked = false; 
        Label A20 = new Label()
        {
            Text = "A20",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A19) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A20.Clicked += () =>
        {
            A20_Clicked = !A20_Clicked;

            if (A20_Clicked)
                A20.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A20_Clicked)
                A20.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A21_Clicked = false; 
        Label A21 = new Label()
        {
            Text = "A21",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A20) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A21.Clicked += () =>
        {
            A21_Clicked = !A21_Clicked;

            if (A21_Clicked)
                A21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A21_Clicked)
                A21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A22_Clicked = false; 
        Label A22 = new Label()
        {
            Text = "A22",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A21) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A22.Clicked += () =>
        {
            A22_Clicked = !A22_Clicked;

            if (A22_Clicked)
                A22.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A22_Clicked)
                A22.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A23_Clicked = false; 
        Label A23 = new Label()
        {
            Text = "A23",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A22) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A23.Clicked += () =>
        {
            A23_Clicked = !A21_Clicked;

            if (A23_Clicked)
                A21.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A23_Clicked)
                A21.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A24_Clicked = false; 
        Label A24 = new Label()
        {
            Text = "A24",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A23) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A24.Clicked += () =>
        {
            A24_Clicked = !A24_Clicked;

            if (A24_Clicked)
                A24.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A24_Clicked)
                A24.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A25_Clicked = false; 
        Label A25 = new Label()
        {
            Text = "A25",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A24) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A25.Clicked += () =>
        {
            A25_Clicked = !A25_Clicked;

            if (A25_Clicked)
                A25.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A25_Clicked)
                A25.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A26_Clicked = false; 
        Label A26 = new Label()
        {
            Text = "A26",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A25) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A26.Clicked += () =>
        {
            A26_Clicked = !A26_Clicked;

            if (A26_Clicked)
                A26.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A26_Clicked)
                A26.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A27_Clicked = false; 
        Label A27 = new Label()
        {
            Text = "A27",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A26) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A27.Clicked += () =>
        {
            A27_Clicked = !A27_Clicked;

            if (A27_Clicked)
                A27.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A27_Clicked)
                A27.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A28_Clicked = false; 
        Label A28 = new Label()
        {
            Text = "A28",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A27) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A28.Clicked += () =>
        {
            A28_Clicked = !A28_Clicked;

            if (A28_Clicked)
                A28.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A28_Clicked)
                A28.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A29_Clicked = false; 
        Label A29 = new Label()
        {
            Text = "A29",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A28) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A29.Clicked += () =>
        {
            A29_Clicked = !A29_Clicked;

            if (A29_Clicked)
                A29.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A29_Clicked)
                A29.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A30_Clicked = false; 
        Label A30 = new Label()
        {
            Text = "A30",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A29) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A30.Clicked += () =>
        {
            A30_Clicked = !A30_Clicked;

            if (A30_Clicked)
                A30.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A30_Clicked)
                A30.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A31_Clicked = false; 
        Label A31 = new Label()
        {
            Text = "A31",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A30) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A31.Clicked += () =>
        {
            A31_Clicked = !A31_Clicked;

            if (A31_Clicked)
                A31.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A31_Clicked)
                A31.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A32_Clicked = false; 
        Label A32 = new Label()
        {
            Text = "A32",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A31) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A32.Clicked += () =>
        {
            A32_Clicked = !A32_Clicked;

            if (A32_Clicked)
                A32.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A32_Clicked)
                A32.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        bool A33_Clicked = false; 
        Label A33 = new Label()
        {
            Text = "A33",
            Y = Pos.Bottom(B2),
            X = Pos.Right(A32) + 1,
            ColorScheme = Colors.ColorSchemes["SeatOpen"]
        };

        A33.Clicked += () =>
        {
            A33_Clicked = !A33_Clicked;

            if (A33_Clicked)
                A33.ColorScheme = Colors.ColorSchemes["SeatSelected"];
            else if (!A33_Clicked)
                A33.ColorScheme = Colors.ColorSchemes["SeatOpen"];
        };

        Add(A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12, A13, A14, A15, A16, A17, A18, A19, A20, A21, A22, A23, A24, A25, A26, A27, A28, A29, A30, A31, A32, A33);
        #endregion
    
        /////////////////
        //Plane drawing//
        /////////////////

        #region Plane drawing
        Label rightWall = new Label()
        {
            Text = "--------------------------------------------------------------------------------------------------------------------------------",
            Y = Pos.Top(F2) - 1,
            X = 34,
        };

        Label leftWall = new Label()
        {
            Text = "--------------------------------------------------------------------------------------------------------------------------------",
            Y = Pos.Bottom(A1),
            X = 34,
        };

        Add(rightWall, leftWall);
        #endregion
    }
}