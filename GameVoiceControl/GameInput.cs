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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVoiceControl
{
    public static class GameInput
    {
        public static Dictionary<int, Keyboard.DirectXKeyStrokes> Windows2DirectXInputs = new Dictionary<int, Keyboard.DirectXKeyStrokes>();

        // Map a game action to another action 
        static GameInput()
        {
            Windows2DirectXInputs[27] = Keyboard.DirectXKeyStrokes.DIK_ESCAPE;
            Windows2DirectXInputs[48] = Keyboard.DirectXKeyStrokes.DIK_0;
            Windows2DirectXInputs[49] = Keyboard.DirectXKeyStrokes.DIK_1;
            Windows2DirectXInputs[50] = Keyboard.DirectXKeyStrokes.DIK_2;
            Windows2DirectXInputs[51] = Keyboard.DirectXKeyStrokes.DIK_3;
            Windows2DirectXInputs[52] = Keyboard.DirectXKeyStrokes.DIK_4;
            Windows2DirectXInputs[53] = Keyboard.DirectXKeyStrokes.DIK_5;
            Windows2DirectXInputs[54] = Keyboard.DirectXKeyStrokes.DIK_6;
            Windows2DirectXInputs[55] = Keyboard.DirectXKeyStrokes.DIK_7;
            Windows2DirectXInputs[56] = Keyboard.DirectXKeyStrokes.DIK_8;
            Windows2DirectXInputs[57] = Keyboard.DirectXKeyStrokes.DIK_9;


            Windows2DirectXInputs[65] = Keyboard.DirectXKeyStrokes.DIK_A;
            Windows2DirectXInputs[66] = Keyboard.DirectXKeyStrokes.DIK_B;
            Windows2DirectXInputs[67] = Keyboard.DirectXKeyStrokes.DIK_C;
            Windows2DirectXInputs[68] = Keyboard.DirectXKeyStrokes.DIK_D;
            Windows2DirectXInputs[69] = Keyboard.DirectXKeyStrokes.DIK_E;
            Windows2DirectXInputs[70] = Keyboard.DirectXKeyStrokes.DIK_F;
            Windows2DirectXInputs[71] = Keyboard.DirectXKeyStrokes.DIK_G;
            Windows2DirectXInputs[72] = Keyboard.DirectXKeyStrokes.DIK_H;
            Windows2DirectXInputs[73] = Keyboard.DirectXKeyStrokes.DIK_I;
            Windows2DirectXInputs[74] = Keyboard.DirectXKeyStrokes.DIK_J;
            Windows2DirectXInputs[75] = Keyboard.DirectXKeyStrokes.DIK_K;
            Windows2DirectXInputs[76] = Keyboard.DirectXKeyStrokes.DIK_L;
            Windows2DirectXInputs[77] = Keyboard.DirectXKeyStrokes.DIK_M;
            Windows2DirectXInputs[78] = Keyboard.DirectXKeyStrokes.DIK_N;
            Windows2DirectXInputs[79] = Keyboard.DirectXKeyStrokes.DIK_O;
            Windows2DirectXInputs[80] = Keyboard.DirectXKeyStrokes.DIK_P;
            Windows2DirectXInputs[81] = Keyboard.DirectXKeyStrokes.DIK_Q;
            Windows2DirectXInputs[82] = Keyboard.DirectXKeyStrokes.DIK_R;
            Windows2DirectXInputs[83] = Keyboard.DirectXKeyStrokes.DIK_S;
            Windows2DirectXInputs[84] = Keyboard.DirectXKeyStrokes.DIK_T;
            Windows2DirectXInputs[85] = Keyboard.DirectXKeyStrokes.DIK_U;
            Windows2DirectXInputs[86] = Keyboard.DirectXKeyStrokes.DIK_V;
            Windows2DirectXInputs[87] = Keyboard.DirectXKeyStrokes.DIK_W;
            Windows2DirectXInputs[88] = Keyboard.DirectXKeyStrokes.DIK_X;
            Windows2DirectXInputs[89] = Keyboard.DirectXKeyStrokes.DIK_Y;
            Windows2DirectXInputs[90] = Keyboard.DirectXKeyStrokes.DIK_Z;
        }

        public enum ActionsInput
        {
            DoAction = 10001
        }

        public static Dictionary<string, object> Inputs = new Dictionary<string, object>() {
            {"NULL",Keyboard.DirectXKeyStrokes.DIK_NULL },
            {"ESCAPE",  Keyboard.DirectXKeyStrokes.DIK_ESCAPE},
            {"1",    Keyboard.DirectXKeyStrokes.DIK_1},
            {"2",    Keyboard.DirectXKeyStrokes.DIK_2},
            {"3",    Keyboard.DirectXKeyStrokes.DIK_3},
            {"4",    Keyboard.DirectXKeyStrokes.DIK_4},
            {"5",    Keyboard.DirectXKeyStrokes.DIK_5},
            {"6",    Keyboard.DirectXKeyStrokes.DIK_6},
            {"7",    Keyboard.DirectXKeyStrokes.DIK_7},
            {"8",    Keyboard.DirectXKeyStrokes.DIK_8},
            {"9",    Keyboard.DirectXKeyStrokes.DIK_9},
            {"0",    Keyboard.DirectXKeyStrokes.DIK_0},
            {"-",    Keyboard.DirectXKeyStrokes.DIK_MINUS},
            {"=",    Keyboard.DirectXKeyStrokes.DIK_EQUALS},
            {"BACK", Keyboard.DirectXKeyStrokes.DIK_BACK},
            {"TAB",  Keyboard.DirectXKeyStrokes.DIK_TAB},
            {"Q",    Keyboard.DirectXKeyStrokes.DIK_Q},
            {"W",    Keyboard.DirectXKeyStrokes.DIK_W},
            {"E",    Keyboard.DirectXKeyStrokes.DIK_E},
            {"R",    Keyboard.DirectXKeyStrokes.DIK_R},
            {"T",    Keyboard.DirectXKeyStrokes.DIK_T},
            {"Y",    Keyboard.DirectXKeyStrokes.DIK_Y},
            {"U",    Keyboard.DirectXKeyStrokes.DIK_U},
            {"I",    Keyboard.DirectXKeyStrokes.DIK_I},
            {"O",    Keyboard.DirectXKeyStrokes.DIK_O},
            {"P",    Keyboard.DirectXKeyStrokes.DIK_P},
            {"LBRACKET",     Keyboard.DirectXKeyStrokes.DIK_LBRACKET},
            {"RBRACKET",     Keyboard.DirectXKeyStrokes.DIK_RBRACKET},
            {"RETURN",   Keyboard.DirectXKeyStrokes.DIK_RETURN},
            {"LCONTROL",     Keyboard.DirectXKeyStrokes.DIK_LCONTROL},
            {"A",    Keyboard.DirectXKeyStrokes.DIK_A},
            {"S",    Keyboard.DirectXKeyStrokes.DIK_S},
            {"D",    Keyboard.DirectXKeyStrokes.DIK_D},
            {"F",    Keyboard.DirectXKeyStrokes.DIK_F},
            {"G",    Keyboard.DirectXKeyStrokes.DIK_G},
            {"H",    Keyboard.DirectXKeyStrokes.DIK_H},
            {"J",    Keyboard.DirectXKeyStrokes.DIK_J},
            {"K",    Keyboard.DirectXKeyStrokes.DIK_K},
            {"L",    Keyboard.DirectXKeyStrokes.DIK_L},
            {";",    Keyboard.DirectXKeyStrokes.DIK_SEMICOLON},
            {"APOSTROPHE",   Keyboard.DirectXKeyStrokes.DIK_APOSTROPHE},
            {"GRAVE",    Keyboard.DirectXKeyStrokes.DIK_GRAVE},
            {"LSHIFT",   Keyboard.DirectXKeyStrokes.DIK_LSHIFT},
            {"BACKSLASH",    Keyboard.DirectXKeyStrokes.DIK_BACKSLASH},
            {"Z",    Keyboard.DirectXKeyStrokes.DIK_Z},
            {"X",    Keyboard.DirectXKeyStrokes.DIK_X},
            {"C",    Keyboard.DirectXKeyStrokes.DIK_C},
            {"V",    Keyboard.DirectXKeyStrokes.DIK_V},
            {"B",    Keyboard.DirectXKeyStrokes.DIK_B},
            {"N",    Keyboard.DirectXKeyStrokes.DIK_N},
            {"M",    Keyboard.DirectXKeyStrokes.DIK_M},
            {",",    Keyboard.DirectXKeyStrokes.DIK_COMMA},
            {".",   Keyboard.DirectXKeyStrokes.DIK_PERIOD},
            {"SLASH",    Keyboard.DirectXKeyStrokes.DIK_SLASH},
            {"RSHIFT",   Keyboard.DirectXKeyStrokes.DIK_RSHIFT},
            {"*",     Keyboard.DirectXKeyStrokes.DIK_MULTIPLY},
            {"LMENU",    Keyboard.DirectXKeyStrokes.DIK_LMENU},
            {"SPACE",    Keyboard.DirectXKeyStrokes.DIK_SPACE},
            {"CAPITAL",  Keyboard.DirectXKeyStrokes.DIK_CAPITAL},
            {"F1",   Keyboard.DirectXKeyStrokes.DIK_F1},
            {"F2",   Keyboard.DirectXKeyStrokes.DIK_F2},
            {"F3",   Keyboard.DirectXKeyStrokes.DIK_F3},
            {"F4",   Keyboard.DirectXKeyStrokes.DIK_F4},
            {"F5",   Keyboard.DirectXKeyStrokes.DIK_F5},
            {"F6",   Keyboard.DirectXKeyStrokes.DIK_F6},
            {"F7",   Keyboard.DirectXKeyStrokes.DIK_F7},
            {"F8",   Keyboard.DirectXKeyStrokes.DIK_F8},
            {"F9",   Keyboard.DirectXKeyStrokes.DIK_F9},
            {"F10",  Keyboard.DirectXKeyStrokes.DIK_F10},
            {"NUMLOCK",  Keyboard.DirectXKeyStrokes.DIK_NUMLOCK},
            {"SCROLL",   Keyboard.DirectXKeyStrokes.DIK_SCROLL},
            {"NUMPAD7",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD7},
            {"NUMPAD8",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD8},
            {"NUMPAD9",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD9},
            {"SUBTRACT",     Keyboard.DirectXKeyStrokes.DIK_SUBTRACT},
            {"NUMPAD4",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD4},
            {"NUMPAD5",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD5},
            {"NUMPAD6",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD6},
            {"ADD",  Keyboard.DirectXKeyStrokes.DIK_ADD},
            {"NUMPAD1",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD1},
            {"NUMPAD2",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD2},
            {"NUMPAD3",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD3},
            {"NUMPAD0",  Keyboard.DirectXKeyStrokes.DIK_NUMPAD0},
            {"DECIMAL",  Keyboard.DirectXKeyStrokes.DIK_DECIMAL},
            {"F11",  Keyboard.DirectXKeyStrokes.DIK_F11},
            {"F12",  Keyboard.DirectXKeyStrokes.DIK_F12},
            {"F13",  Keyboard.DirectXKeyStrokes.DIK_F13},
            {"F14",  Keyboard.DirectXKeyStrokes.DIK_F14},
            {"F15",  Keyboard.DirectXKeyStrokes.DIK_F15},
            {"KANA",     Keyboard.DirectXKeyStrokes.DIK_KANA},
            {"CONVERT",  Keyboard.DirectXKeyStrokes.DIK_CONVERT},
            {"NOCONVERT",    Keyboard.DirectXKeyStrokes.DIK_NOCONVERT},
            {"YEN",  Keyboard.DirectXKeyStrokes.DIK_YEN},
            {"NUMPADEQUALS",     Keyboard.DirectXKeyStrokes.DIK_NUMPADEQUALS},
            {"CIRCUMFLEX",   Keyboard.DirectXKeyStrokes.DIK_CIRCUMFLEX},
            {"AT",   Keyboard.DirectXKeyStrokes.DIK_AT},
            {"COLON",    Keyboard.DirectXKeyStrokes.DIK_COLON},
            {"UNDERLINE",    Keyboard.DirectXKeyStrokes.DIK_UNDERLINE},
            {"KANJI",    Keyboard.DirectXKeyStrokes.DIK_KANJI},
            {"STOP",     Keyboard.DirectXKeyStrokes.DIK_STOP},
            {"AX",   Keyboard.DirectXKeyStrokes.DIK_AX},
            {"UNLABELED",    Keyboard.DirectXKeyStrokes.DIK_UNLABELED},
            {"NUMPADENTER",  Keyboard.DirectXKeyStrokes.DIK_NUMPADENTER},
            {"RCONTROL",     Keyboard.DirectXKeyStrokes.DIK_RCONTROL},
            {"NUMPADCOMMA",  Keyboard.DirectXKeyStrokes.DIK_NUMPADCOMMA},
            {"DIVIDE",   Keyboard.DirectXKeyStrokes.DIK_DIVIDE},
            {"SYSRQ",    Keyboard.DirectXKeyStrokes.DIK_SYSRQ},
            {"RMENU",    Keyboard.DirectXKeyStrokes.DIK_RMENU},
            {"HOME",     Keyboard.DirectXKeyStrokes.DIK_HOME},
            {"UP",   Keyboard.DirectXKeyStrokes.DIK_UP},
            {"PRIOR",    Keyboard.DirectXKeyStrokes.DIK_PRIOR},
            {"LEFT",     Keyboard.DirectXKeyStrokes.DIK_LEFT},
            {"RIGHT",    Keyboard.DirectXKeyStrokes.DIK_RIGHT},
            {"END",  Keyboard.DirectXKeyStrokes.DIK_END},
            {"DOWN",     Keyboard.DirectXKeyStrokes.DIK_DOWN},
            {"NEXT",     Keyboard.DirectXKeyStrokes.DIK_NEXT},
            {"INSERT",   Keyboard.DirectXKeyStrokes.DIK_INSERT},
            {"DELETE",   Keyboard.DirectXKeyStrokes.DIK_DELETE},
            {"LWIN",     Keyboard.DirectXKeyStrokes.DIK_LWIN},
            {"RWIN",     Keyboard.DirectXKeyStrokes.DIK_RWIN},
            {"APPS",     Keyboard.DirectXKeyStrokes.DIK_APPS},
            {"BACKSPACE",    Keyboard.DirectXKeyStrokes.DIK_BACKSPACE},
            {"NUMPADSTAR",   Keyboard.DirectXKeyStrokes.DIK_NUMPADSTAR},
            {"LALT",     Keyboard.DirectXKeyStrokes.DIK_LALT},
            {"CAPSLOCK",     Keyboard.DirectXKeyStrokes.DIK_CAPSLOCK},
            {"NUMPADMINUS",  Keyboard.DirectXKeyStrokes.DIK_NUMPADMINUS},
            {"NUMPADPLUS",   Keyboard.DirectXKeyStrokes.DIK_NUMPADPLUS},
            {"NUMPADPERIOD",     Keyboard.DirectXKeyStrokes.DIK_NUMPADPERIOD},
            {"NUMPADSLASH",  Keyboard.DirectXKeyStrokes.DIK_NUMPADSLASH},
            {"RALT",     Keyboard.DirectXKeyStrokes.DIK_RALT},
            {"UPARROW",  Keyboard.DirectXKeyStrokes.DIK_UPARROW},
            {"PGUP",     Keyboard.DirectXKeyStrokes.DIK_PGUP},
            {"LEFTARROW",    Keyboard.DirectXKeyStrokes.DIK_LEFTARROW},
            {"RIGHTARROW",   Keyboard.DirectXKeyStrokes.DIK_RIGHTARROW},
            {"DOWNARROW",    Keyboard.DirectXKeyStrokes.DIK_DOWNARROW},
            {"PGDN",     Keyboard.DirectXKeyStrokes.DIK_PGDN},

            {"LEFTMOUSEBUTTON", Mouse.Button.LEFT},
            {"RIGHTMOUSEBUTTON", Mouse.Button.RIGHT},
            {"MIDDLEWHEELBUTTON", Mouse.Button.MIDDLE},
            {"LASTWEAPON", ActionsInput.DoAction }
        };
    }
}
