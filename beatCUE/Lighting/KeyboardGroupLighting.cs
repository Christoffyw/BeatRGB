﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Mouse;
using CUE.NET.Devices.Headset;
using CUE.NET.Groups;

namespace beatCUE.Lighting
{
    static class KeyboardGroupLighting
    {
        internal static RectangleLedGroup FunctionRow = new RectangleLedGroup(Testing.LightingTest.keyboard, CorsairLedId.Escape, CorsairLedId.ScanNextTrack);
        internal static RectangleLedGroup Numpad = new RectangleLedGroup(Testing.LightingTest.keyboard, CorsairLedId.NumLock, CorsairLedId.KeypadEnter, 0.9f);
        internal static RectangleLedGroup AlphaNumeric = new RectangleLedGroup(Testing.LightingTest.keyboard, CorsairLedId.LeftCtrl, CorsairLedId.Backspace);
        internal static RectangleLedGroup InBetween = new RectangleLedGroup(Testing.LightingTest.keyboard, CorsairLedId.Insert, CorsairLedId.RightArrow);

        public static void KeyboardFunctionRow(this CorsairKeyboard keyboard, UnityEngine.Color color)
        {
            color = color.Ify();
            CorsairColor cc = color.ToCorsair();
            keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
            if (keyboard != null)
            {
                FunctionRow.Brush = new SolidColorBrush(cc);
                keyboard.Update();
            }
        }
        public static void KeyboardLogoRow(this CorsairKeyboard keyboard, UnityEngine.Color color)
        {
            color = color.Ify();
            CorsairColor cc = color.ToCorsair();
            keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
            if (keyboard != null)
            {
                keyboard[CorsairLedId.Mute].Color = cc;
                keyboard.Update();
            }
        }
        public static void KeyboardNumpad(this CorsairKeyboard keyboard, UnityEngine.Color color)
        {
            color = color.Ify();
            CorsairColor cc = color.ToCorsair();
            keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
            if (keyboard != null)
            {
                var b = new SolidColorBrush(cc);
                keyboard[CorsairLedId.Keypad0].Color = cc;
                keyboard[CorsairLedId.KeypadPeriodAndDelete].Color = cc;
                Numpad.Brush = b;
                keyboard.Update();
            }
        }
        public static void KeyboardInbetween(this CorsairKeyboard keyboard, UnityEngine.Color color)
        {
            color = color.Ify();
            CorsairColor cc = color.ToCorsair();
            keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
            if (keyboard != null)
            {
                var b = new SolidColorBrush(cc);
                InBetween.Brush = b;
                keyboard.Update();
            }
        }
        public static void KeyboardAlphanumeric(this CorsairKeyboard keyboard, UnityEngine.Color color)
        {
            color = color.Ify();
            CorsairColor cc = color.ToCorsair();
            keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;
            if (keyboard != null)
            {
                var b = new SolidColorBrush(cc);
                AlphaNumeric.Brush = b;
                keyboard.Update();
            }
        }
    }
}
