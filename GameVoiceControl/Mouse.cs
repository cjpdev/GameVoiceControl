/**
*
	Copyright (c) 2020 Chay Palton

	Permission is hereby granted, free of charge, to any person obtaining
	a copy of this software and associated documentation files (the "Software"),
	to deal in the Software without restriction, including without limitation
	the rights to use, copy, modify, merge, publish, distribute, sublicense,
	and/or sell copies of the Software, and to permit persons to whom the Software
	is furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in
	all copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
	OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
	IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
	CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
	TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
	OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;

namespace GameVoiceControl
{
    public static class Mouse
    {
        public struct POINT
        {
            public Int32 X;
            public Int32 Y;
        }

        /// <summary>
        /// Mouse Button type
        /// </summary>
        public enum Button
        {
            LEFT = 1,
            RIGHT = 2,
            MIDDLE = 3
        }

        private struct State
        {
            public Int32 Down;
            public Int32 Up;

            public State(Int32 Down,Int32 Up)
            {
                this.Down = Down;
                this.Up = Up;
            }
        }

        public const Int32 MOUSEEVGetCursorPosENTF_MOVE = 0x0001;
        public const Int32 MOUSEEVENTF_ABSOLUTE = 0x8000;
        public const Int32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const Int32 MOUSEEVENTF_LEFTUP = 0x0004;
        public const Int32 MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const Int32 MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const Int32 MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const Int32 MOUSEEVENTF_RIGHTUP = 0x0010;

        private static readonly Dictionary<Button, State> Map = new Dictionary<Button, State>()
        {
            {Button.LEFT,  new State(MOUSEEVENTF_LEFTDOWN, MOUSEEVENTF_LEFTUP ) },
            {Button.RIGHT, new State(MOUSEEVENTF_RIGHTDOWN, MOUSEEVENTF_RIGHTUP ) },
            {Button.MIDDLE, new State(MOUSEEVENTF_MIDDLEDOWN, MOUSEEVENTF_MIDDLEUP ) }
        };

        /**
        * Mouse functions
        */
        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true)]
         public static extern long mouse_event(Int32 dwFlags, Int32 dx, Int32 dy, Int32 cButtons, Int32 dwExtraInfo);

        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true)]
        public static extern void SetCursorPos(Int32 x, Int32 y);

        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true)]
        static extern bool GetCursorPos(out POINT lpPoint);

        public static void GetPos(POINT lpPoint)
        {
           
        }

        public static void Move(int dx, int dy)
        {
            POINT lpPoint;
            POINT glpPoint;

            GetCursorPos(out lpPoint);

            lpPoint.X += (Int32)dx;
            lpPoint.Y += (Int32)dy;
 
            //Console.Beep();

            GetCursorPos(out glpPoint);
            mouse_event(MOUSEEVGetCursorPosENTF_MOVE, (Int32)dx, (Int32)dy, 0, 0);
        }

        public static void Click(Button button)
        {
            Int32 Down = Map[button].Down;
            Int32 Up = Map[button].Up;
            mouse_event(MOUSEEVENTF_ABSOLUTE | Down, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(Properties.Settings.Default.MouseClickSpeed);
            mouse_event(MOUSEEVENTF_ABSOLUTE | Up, 0, 0, 0, 0);
        }
    }
}


