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

/**
 * NOTTES:
 * 
 * As of 03/02/202 this software support Fornite with some support for Starcraft. 
 * However, I don't really play these games much, and this software was mainly created
 * as a bit of fun while in Covid-19 lockdown. Thus, why it is now open
 * source.
 * 
 * Inital was a C++ implementation, but to save time ended up using C#.
 * 
 * Mainly tested with Fornite. It is possible to play game with minimal keyboard 
 * or mouse input. However, there is a fraction of a second delay between issuing a
 * voice command and the action being performed. This is result of the processing need to
 * recongize the voice command, a faster computer should have better results.
 * 
 * Voice commands functioning with no peformance lose to the game.
 *  
 * Test Computer:
 *    Intel i7, 8G RAM, SSD HD, Graphics NVidia 1050
 *    Fornite running at high quality settings
 *    resolution 3000x2000.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Text;
using System.Windows.Forms;

namespace GameVoiceControl
{
    public partial class FormMain : Form
    {
        bool isGame = false;
        //bool isTestModeOn = true;// All action to run even if fornite is not the active window
        static long TIMER_MI = 10000;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        bool stopKeyInput = false;
    
        // Track state of key presses
        public Dictionary<Keyboard.DirectXKeyStrokes, int> KeyDownLog = new Dictionary<Keyboard.DirectXKeyStrokes, int>();

        string autoDodgeAction = "";

        //UI Compatible timer.
        DispatchTimerGo timerWindowsChecker = new DispatchTimerGo();
        DispatchTimerGo timerDodge = new DispatchTimerGo();
        DispatchTimerGo timerJump = new DispatchTimerGo();
        DispatchTimerGo timerBuild = new DispatchTimerGo();
        DispatchTimerGo timerFire = new DispatchTimerGo();
        DispatchTimerGo timerWordOkay = new DispatchTimerGo();

        bool buidFireFlipflop = false;
        string wordLastWeapon = "";
        string wordLastBuild = "";
        Hookkeys hookkeys = new Hookkeys();
    
        //Voice recognition.

        Choices choices = null;
        GrammarBuilder grammarBuilder = null;
        Grammar grammer = null;
     
        private SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine(new CultureInfo("en-US")); // en-US
        private DictationGrammar dictationGrammar = new DictationGrammar("grammar:dictation");

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // GVCommand.InitData(true, GVCommand.TESTDATA_CONFIG_GEN_FORNITE);
            GVCommand.InitData(false);

            //GVCommand.LoadGVSetup();

            Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs["ESCAPE"];
#if DEBUG
            //   isDebug = true;
            Console.WriteLine("Debug version");
#endif

            Hookkeys.handlerKeyDown += HandleKeyDown;
            Hookkeys.handlerKeyUp += HandleKeyUp;

            this.TopMost = true; // checkBoxTopmost.Checked = Properties.Settings.Default.TopMost;

            if(GVCommand.gvSetup == null || GVCommand.gvSetup.gameName == null)
            {
                MessageBox.Show("GVC configuration was not loaded....", "Error");
                Application.Exit();
                Application.ExitThread();
                return;
                //Environment.Exit();
            }

            if (GVCommand.gvSetup.gameName == "FORNITE")
            {
                wordLastBuild = GVCommand.ActionToWord["WALL"];
                wordLastWeapon = GVCommand.ActionToWord["HARVESTING TOOL"];
            }

            BuildSpeechRecognized();

            timerJump.Interval = new TimeSpan(TIMER_MI * Properties.Settings.Default.AutoJumpSpeed);
            timerJump.Tick = new EventHandler(TimerJump_Elapsed);
            timerJump.Stop();

            timerBuild.Interval = new TimeSpan(TIMER_MI * Properties.Settings.Default.AutoBuildSpeed);
            timerBuild.Tick = new EventHandler(TimerBuild_Elapsed);
            timerBuild.Stop();

            timerDodge.Interval = new TimeSpan(TIMER_MI * Properties.Settings.Default.AutoDodgeSpeed);
            timerDodge.Tick = new EventHandler(TimerDodge_Elapsed);
            timerDodge.Stop();

            timerFire.Interval = new TimeSpan(TIMER_MI * Properties.Settings.Default.AutoFireSpeed);
            timerFire.Tick = new EventHandler(TimerFire_Elapsed);
            timerFire.Stop();

            timerWindowsChecker.Interval = new TimeSpan(TIMER_MI * 300);
            timerWindowsChecker.Tick = new EventHandler(CheckActiveWindowd);
            timerWindowsChecker.Start();

            timerWordOkay.Interval = new TimeSpan(TIMER_MI * 100);
            timerWordOkay.Tick = new EventHandler(TimerWordOkay_Elapsed);
            timerWordOkay.Stop();

            labelAction.BackColor = Color.FromArgb(0, 200, 0);
            labelAction.Text = "";
        }

        private void BuildSpeechRecognized()
        {
            int numberOfWords = 0;

            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;

            // IF WE RECONFIGURE AT RUNTIME.
            //  if (grammer != null)
            // {
            //      recEngine.UnloadGrammar(grammer);
            //      recEngine.SetInputToNull();
            //  }

            choices = new Choices();

            numberOfWords = GVCommand.commands.Count;
 
            for (var n = 0; n < numberOfWords; n++)
            {
                choices.Add(GVCommand.commands[n].word);
            }
          
            grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(choices);
            grammer = new Grammar(grammarBuilder);
            grammer.Name = GVCommand.gvSetup.gameName;
         
            recEngine.LoadGrammar(grammer);
            
            recEngine.EndSilenceTimeout = new TimeSpan(110);
            recEngine.EndSilenceTimeoutAmbiguous = new TimeSpan(110);
            recEngine.BabbleTimeout = new TimeSpan(1000);
            recEngine.InitialSilenceTimeout = new TimeSpan(110);
            recEngine.MaxAlternates = 3;

            try
            {
                recEngine.SetInputToDefaultAudioDevice();
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
            } catch (Exception ex)
            {
                MessageBox.Show("Need mic input, none found.", "Speech Recognition");
                System.Console.WriteLine(ex);
                Application.Exit();
            } 
        }

        private void DictationGrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            System.Console.WriteLine("DictationGrammar_SpeechRecognized Confidence for word '{0}' is %{1}. Min is %{2}", e.Result.Text, 100 * e.Result.Confidence, 100 * Properties.Settings.Default.WordConfidenceAdjust);
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null && e.Result.Text != "" && e.Result.Grammar != null && e.Result.Grammar.Name == GVCommand.gvSetup.gameName)
            {
                // Properties.Settings.Default.WordConfidenceAdjust] + 

                float confidence = GVCommand.WordConfidenceMin[e.Result.Text] / 10;
                confidence += Properties.Settings.Default.WordConfidenceAdjust/10;
                
                if(confidence < Properties.Settings.Default.WordConfidenceAdjustMin / 10)
                {
                    confidence = Properties.Settings.Default.WordConfidenceAdjustMin / 10;
                } 

                if(confidence > Properties.Settings.Default.WordConfidenceAdjustMax / 10)
                {
                    confidence = Properties.Settings.Default.WordConfidenceAdjustMax / 10;
                }


                if (GVCommand.WordConfidenceMin.ContainsKey(e.Result.Text) && e.Result.Confidence >= confidence)
                {
                    // Do the action on a new thread so the app can continue ASAP..
                    // Should make the FormMain functions that handle work recognition Thread safe (or move them so there own class).... instead of using Invoke
                    System.Threading.Tasks.Task.Factory.StartNew(() =>
                        // Make scope  this Form.
                        this.Invoke(new Action(() =>
                        {
                            WordToAction(e.Result.Text);
                        })));

                } else {

                    System.Console.WriteLine("Confidence for word '{0}' is %{1}. Min is %{2}", e.Result.Text, 100 * e.Result.Confidence, 100 * Properties.Settings.Default.WordConfidenceAdjust);
                
                }
#if DEBUG
            System.Console.WriteLine(e.Result.Text);
#endif
            }
        }


        private void ActionExec(string action)
        {
            if (!GVCommand.ActionToKeys.ContainsKey(action))
                return;

            List<string> keys = GVCommand.ActionToKeys[action];
            List<string> keyBehaviours = GVCommand.ActionkeyBehaviours[action];

            for (var k = 0; k < keys.Count; k++)
            {
                String stype = GameInput.Inputs[keys[k]].GetType().ToString();

                if (stype == "GameVoiceControl.Mouse+Button")
                {
                    Mouse.Button button = (Mouse.Button)GameInput.Inputs[keys[k]];

#if DEBUG
                    System.Console.WriteLine("MOUSE=================");
#endif

                    if (keyBehaviours[k].ToUpper() == "DOWN")
                    {

                    }
                    if (keyBehaviours[k].ToUpper() == "PRESS" || keyBehaviours[k].ToUpper() == "CLICK")
                    {
                        Mouse.Click(button);
                    }

                    if (keyBehaviours[k].ToUpper() == "UP")
                    {

                    }
                }
                else if (stype == "GameVoiceControl.Keyboard+DirectXKeyStrokes")
                {
                    Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[k]];

                    if (keyBehaviours[k].ToUpper() == "DOWN")
                    {
                        SimulateKeyDown(key);
                    }
                    if (keyBehaviours[k].ToUpper() == "PRESS")
                    {
                        SimulateKeyPress(key);
                    }

                    if (keyBehaviours[k].ToUpper() == "UP")
                    {
                        SimulateKeyUp(key);
                    }
                }
                else if (stype == "GameVoiceControl.GameInput+ActionsInput")
                {
                    string nextAction = keys[k];

#if DEBUG
                    System.Console.WriteLine("Action next = {0}.", nextAction);
#endif
                    Action(nextAction);
                }
            }
        }

        private void WordToAction(string word)
        {
            string action = GVCommand.WordToAction[word].ToUpper();
            Action(action);

            if (GVCommand.ActionToGroupID[action] == GVCommand.groupid_weapon || action == "HARVESTING TOOL")
            {
                wordLastWeapon = word;
            }

            if (GVCommand.ActionToGroupID[action] == GVCommand.groupid_build)
            {

                wordLastBuild = word;
            }
        }

        private void StopAllAction()
        {
            timerJump.Stop();
            timerDodge.Stop();
            timerFire.Stop();
            timerBuild.Stop();
            timerWordOkay.Stop();
      
            SimulateKeyAllup();

            if (wordLastWeapon != "")
                WordToAction(wordLastWeapon);
            else
            {
                labelAction.BackColor = Color.Yellow;
                labelAction.Text = " ";
            }
        }

        private void Action(string action)
        {

            if (action == "")
                return;

            //if(/  if (isFornite == false && action != "STOP")
            //  {
            //      System.Console.WriteLine("TEST MODE: Action requested = " + action);
            //      return;
            //k  }

            switch (action)
            {
                case "STOP":
                    StopAllAction();
                    return;
                case "MOVE LEFT":
                    {
                        List<string> keys = GVCommand.ActionToKeys["MOVE RIGHT"];
                        Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                        SimulateKeyUp(key);
                    }
                    break;

                case "MOVE RIGHT":
                    {
                        List<string> keys = GVCommand.ActionToKeys["MOVE LEFT"];
                        Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                        SimulateKeyUp(key);
                    }
                    break;

                case "MOVE FORWARD":
                    {
                        List<string> keys = GVCommand.ActionToKeys["MOVE BACKWARD"];
                        Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                        SimulateKeyUp(key);
                    }
                    break;

                case "MOVE BACKWARD":
                    {
                        List<string> keysOdd = GVCommand.ActionToKeys["MOVE BACKWARD"];
                        Keyboard.DirectXKeyStrokes keyOdd = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keysOdd[0]];

                        if (!KeyDownLog.ContainsKey(keyOdd) || KeyDownLog[keyOdd] <= 0)
                        {
                            List<string> keys = GVCommand.ActionToKeys["MOVE FORWARD"];
                            Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                            SimulateKeyUp(key);
                       
                        } else
                        {
                            return;
                        }
                    }
                    break;

                case "AUTO FIRE ON":
                    //timerFire.Enabled = true;
                    timerFire.Start();
                    buidFireFlipflop = true;
                    break;

                case "AUTO FIRE OFF":
                    //timerFire.Enabled = false;
                    timerFire.Stop();
                    break;

                case "AUTO JUMP ON":
                    {
                       // timerJump.Enabled = true;
                        timerJump.Start();
                        // List<string> keys = GVCommand.ActionToKeys["MOVE FORWARD"];
                        //Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                        //SimulateKeyUp(key);
                    }
                    break;

                case "JUMP":
                    {
                        if (timerJump.IsEnabled == true)
                        {
                            //timerJump.Enabled = false;
                            timerJump.Stop();
                            List<string> keys = GVCommand.ActionToKeys["MOVE FORWARD"];
                            Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                            SimulateKeyUp(key);
                            SimulateKeyPress(key);
                        }
                    }
                    break;

                case "AUTO JUMP OFF":
                    {
                        //timerJump.Enabled = false;
                        timerJump.Stop();
                    }
                    break;


                case "AUTO BUILD ON":
                    {
                        buidFireFlipflop = true;
                       // timerBuild.Enabled = true;
                        timerBuild.Start();


                    }
                    break;
                case "AUTO BUILD OFF":
                    {
                        //timerBuild.Enabled = false;
                        timerBuild.Stop();
                        //if(timerBuild.IsEnabled == false)
                            if (wordLastWeapon != "")
                                WordToAction(wordLastWeapon);
                    }
                    break;


                case "GHOST ON":
                {
                        timerDodge.Interval = new TimeSpan(TIMER_MI/2);

                        if (timerDodge.IsEnabled == false)
                        {

                            autoDodgeAction = "LEFT";
                            // timerDodge.Enabled = true;
                            timerDodge.Start();
                        }
                    }
                break;
                
                case "AUTO DODGE ON":
                { 
                        timerDodge.Interval = new TimeSpan(TIMER_MI * Properties.Settings.Default.AutoDodgeSpeed);

                        if (timerDodge.IsEnabled == false)
                        {

                            autoDodgeAction = "LEFT";
                           // timerDodge.Enabled = true;
                            timerDodge.Start();
                        }
                    }
                    break;

                case "GHOST OFF":
                case "AUTO DODGE OFF":
                    {
                        if (timerDodge.IsEnabled == true)
                        {
                            //timerDodge.Enabled = false;
                            timerDodge.Stop();

                            List<string> keys = GVCommand.ActionToKeys["MOVE LEFT"];
                            Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                            SimulateKeyUp(key);

                            keys = GVCommand.ActionToKeys["MOVE RIGHT"];
                            key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                            SimulateKeyUp(key);
                        }
                    }
                    break;

                case "LASTWEAPON":
                    if(timerBuild.IsEnabled == false)
                        if (wordLastWeapon != "")
                          WordToAction(wordLastWeapon);
                    break;

                case "BUILD FORT":
                    {/*
                        Mouse.Move(0, 600);
                        Action("WALL");
                        List<string> keys = GVCommand.ActionToKeys["MOVE FORWARD"];
                        Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                        SimulateKeyDown(key);
                        System.Threading.Thread.Sleep(Properties.Settings.Default.KeyPressSpeed + 1000);
                        SimulateKeyUp(key);
                        Mouse.Move(200, 0);
                        Action("WALL");
                        // Mouse.Move(100, 500);
                        // Action("WALL");
                        //Mouse.Move(1, 0);
                        // Action("WALL");*/
                        break;
                    }
            }

           labelAction.BackColor =  Color.Red;
           labelAction.Text = action;
            //timerWordOkay.Enabled = true;
            timerWordOkay.Start();       

             ActionExec(action);

            /*
                switch (action)
                {
                case "WALL":
                case "FLOOR":
                case "STAIRS":
                case "ROOF":
                    break;
                }
            */
            }



        private void TimerBuild_Elapsed(object sender, EventArgs e)
        {
            if (stopKeyInput == true)
                return;

            if (wordLastBuild != "")
            {
                if (wordLastWeapon != "" && timerFire.IsEnabled == true)
                {
                    //share build and fireing 
                    if (buidFireFlipflop == true)
                    {
                        WordToAction(wordLastBuild);
                        buidFireFlipflop = false;
                    } else {
                        WordToAction(wordLastWeapon);
                        ActionExec("FIRE");
                        // List<string> keys = GVCommand.ActionToKeys["FIRE"];
                        // Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                        // SimulateKeyPress(key);
                        buidFireFlipflop = true;
                    }

                } else
                {
                    WordToAction(wordLastBuild);
                }
            }
        }

        private void TimerFire_Elapsed(object sender, EventArgs e)
        {
            if (stopKeyInput == true)
                return;

            if (wordLastWeapon != "" && timerBuild.IsEnabled == false)
            {
                WordToAction(wordLastWeapon);
                ActionExec("FIRE");
                // List<string> keys = GVCommand.ActionToKeys["FIRE"];
                // Keyboard.DirectXKeyStrokes key = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[keys[0]];
                // SimulateKeyPress(key);
            }
        }

        private void TimerWordOkay_Elapsed(object sender, EventArgs e)
        {
            // timerWordOkay.IsEnabled = false;
            timerWordOkay.Stop();

            labelAction.BackColor = Color.Green;
            labelAction.Text = "+" + labelAction.Text +"+";
        }

        private void TimerDodge_Elapsed(object sender, EventArgs e)
        {
            if (stopKeyInput == true)
                return;

            if (autoDodgeAction == "LEFT")
            {
                System.Console.WriteLine("Auto dodge Right...");
                Action("MOVE RIGHT");
                autoDodgeAction = "RIGHT";
            }
            else
            {
                System.Console.WriteLine("Auto dodge Right...");
                Action("MOVE LEFT");
                autoDodgeAction = "LEFT";
            }
        }

        private void TimerJump_Elapsed(object sender, EventArgs e)
        {
            if (stopKeyInput == true)
                return;
                  
            // timerJump.Enabled = false;
            // Add some ramdom time to stop the jump from getting locked in a spot when running.
            // timerJump.Interval = Properties.Settings.Default.AutoJumpSpeed + rand.Next(4)*60;
            // timerJump.Elapsed += TimerJump_Elapsed;
            // timerJump.AutoReset = false;
            // timerJump.Enabled = true;

            ActionExec("JUMP");
        }

        private void HandleKeyDown(string message, int keyCode)
        {
           // if (stopKeyInput == true)
            //   return;

            if (GameInput.Windows2DirectXInputs.ContainsKey(keyCode))
            {
                Keyboard.DirectXKeyStrokes key = GameInput.Windows2DirectXInputs[keyCode];
               
                if (!KeyDownLog.ContainsKey(key) || KeyDownLog[key] <= 0)
                {
                    KeyDownLog[key] = 1;

                    if (GVCommand.gvSetup.gameName == "FORNITE")
                    {
                        Keyboard.DirectXKeyStrokes okey = GVCommand.KeySwitchPairing(key);
                        if (okey != Keyboard.DirectXKeyStrokes.DIK_NULL)
                            SimulateKeyUp(okey);
                    }

                    //  System.Console.Beep();
#if DEBUG
                    Console.WriteLine("----- External key down Pressed--------" + key.ToString());
#endif
                    //  Keyboard.SendKey(key, Keyboard.KEY_DOWN, Keyboard.InputType.Keyboard);
                }
            }
        }

        private void HandleKeyUp(string message, int keyCode)
        {
            // if (stopKeyInput == true)
            //     return;
            if (keyCode == 20)
            {
                this.Invoke(new Action(() =>
                {
                    StopAllAction();
                }));
            } else  if (GameInput.Windows2DirectXInputs.ContainsKey(keyCode))
            {
                Keyboard.DirectXKeyStrokes key = GameInput.Windows2DirectXInputs[keyCode];
                // CAP LOCK PRESS
           
               if (KeyDownLog.ContainsKey(key) && KeyDownLog[key] > 0)
                {
                    // KeyDownLog.Remove(key) ;
                    KeyDownLog[key] = 0;

                    // System.Console.Beep();
#if DEBUG
                    Console.WriteLine("----- External key up Pressed--------" + key.ToString());
#endif
                    //Keyboard.SendKey(key, Keyboard.KEY_UP, Keyboard.InputType.Keyboard);

                }
            }
        }

        private void SimulateKeyUp(Keyboard.DirectXKeyStrokes key)
        {
            if (stopKeyInput == true)
                return;

            if (KeyDownLog.ContainsKey(key) && KeyDownLog[key] > 0)
            {

#if DEBUG
                Console.WriteLine("----- SimulateKeyUp--------" + key.ToString());
#endif
                Keyboard.SendKey(key, Keyboard.KEY_UP);
                //KeyDownLog.Remove(key);
            }
        }

        private void SimulateKeyDown(Keyboard.DirectXKeyStrokes key)
        {
            if (stopKeyInput == true)
                return;

            if (!KeyDownLog.ContainsKey(key) || KeyDownLog[key] <= 0)
            {

#if DEBUG
                Console.WriteLine("----- SimulateKeyDown--------" + key.ToString());
#endif
                Keyboard.SendKey(key, Keyboard.KEY_DOWN);
                //KeyDownLog[key] = 1;
            }
        }

        private void SimulateKeyPress(Keyboard.DirectXKeyStrokes key)
        {
            if (stopKeyInput == true)
               return;

            if (!KeyDownLog.ContainsKey(key) || KeyDownLog[key] == 0)
            {
#if DEBUG
                Console.WriteLine("----- SimulateKeyPress--------" + key.ToString());
#endif
                Keyboard.SendKey(key, Keyboard.KEY_DOWN);
                System.Threading.Thread.Sleep(Properties.Settings.Default.KeyPressSpeed);
                Keyboard.SendKey(key, Keyboard.KEY_UP);
            }
        }


        private void SimulateKeyAllup()
        {
           

            if (stopKeyInput == true)
                return;

            List<Keyboard.DirectXKeyStrokes> keys = new List<Keyboard.DirectXKeyStrokes>(KeyDownLog.Keys);

            stopKeyInput = true;

            try
            {
                if (KeyDownLog.Count > 0 && KeyDownLog != null)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        Keyboard.DirectXKeyStrokes key = keys[i];
                         
                        if (KeyDownLog[key] > 0)
                        {
                            KeyDownLog[key] = 0;
#if DEBUG
                            Console.WriteLine("SimulateKeyAllup, {0} count = {1}", key, KeyDownLog[key]);
#endif
                            Keyboard.SendKey(key, Keyboard.KEY_UP);
                        }
                    }
                    //KeyDownLog.Clear();
                }
            } catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            stopKeyInput = false;
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString().Replace(" ","");
            }
            return null;
        }

        /// <summary>
        /// TODO: This should be another windows Hook.
        ///         But for now just pool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckActiveWindowd(object sender, EventArgs e)
        {
            try
            {
                string title = GetActiveWindowTitle();

                if (title != null)
                {
                    title = title.ToUpper();
                    if (title.Contains(GVCommand.gvSetup.gameName))
                    {
                        if (isGame == false)
                        {
                            System.Console.Beep();
                            // Form1.Hide();
                            isGame = true;
                        }

                    }
                    else
                    {
                        if (isGame == true)
                        {
                          //  Action("STOP");
                            isGame = false;
                        }

                        //   Show();
                    }

                    System.Console.WriteLine("Active windows [{0}]", title);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
#if !DEBUG
            if( isTestModeOn == true )// All action to run even if fornite is not the active window
            {
                isGame = true;
            }
#endif
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            var form = new FormConfigure();
            form.LoadData();
            form.ShowDialog(this);
        }

        private void FormMain_VisibleChanged(object sender, EventArgs e)
        {
            //GetActiveWindowTitle("")
            //System.Console.WriteLine(e);
        }

        private void checkBoxTopmost_CheckedChanged(object sender, EventArgs e)
        {
           //this.TopMost = checkBoxTopmost.Checked;
           //Properties.Settings.Default.SettingsKey["TopMost"] = this.TopMost;
        }
    }
}
