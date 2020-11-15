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
using System.Windows.Forms;

namespace GameVoiceControl
{
    public static class GVCommand
    {
        public const int TESTDATA_CONFIG_GEN_FORNITE = 1;
        public const int TESTDATA_CONFIG_GEN_STARCRAFT = 2;
        public static string groupid_navigation = "NAVIGATION";
        public static string groupid_combat = "COMBAT";
        public static string groupid_weapon = "WEAPON";
        public static string groupid_build = "BUILD";
        public static string groupid_DROP = "DROP";
        public static string groupid_special = "SPECIAL";
        public static string groupid_group = "GROUP";
        public static string groupid_action = "ACTION";
        public static string groupid_custom = "CUSTOM"; // USER DEFIND

        public class CommandItem {

            public string action = "";
            public string word = "";
            public float confidence = 9.0f;
            public List<string> key = new List<string>();
            public List<string> keyBehaviour = new List<string>(); // KEYDOWN, PRESS(UP/DOWN), KEYUP
            public string groupid;
        }

        public class GVSetup
        {
            public string gameName = "";
            public List<CommandItem> commands = null;
        }

        public static GVSetup gvSetup = null;

        //public static string gameName ="";
        public static List<CommandItem> commands = new List<CommandItem>();

        // Fast lookups
        public static Dictionary<string, string> WordToAction = new Dictionary<string, string>();
        public static Dictionary<string, string> ActionToWord = new Dictionary<string, string>();
        public static Dictionary<string, string> ActionToGroupID = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> ActionToKeys = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> ActionkeyBehaviours = new Dictionary<string, List<string>>();
        public static Dictionary<string, float> WordConfidenceMin = new Dictionary<string, float>();

        public static int Count()
        {
            return commands.Count();
        }

        public static CommandItem NewItem(string action, string word, float confidence, List<string> key, List<string> keyBehaviour, string id)
        {
            CommandItem c = new CommandItem
            {
                action = action,
                word = word,
                confidence = confidence,
                key = key,
                keyBehaviour = keyBehaviour,
                groupid = id
            };
            return c;
        }

        //The opposing keys that must be release e.g LEFT - RIGHT key are mutually exclusive.
        // this should be XML
        public static Keyboard.DirectXKeyStrokes KeySwitchPairing(Keyboard.DirectXKeyStrokes  key)
        {
            Keyboard.DirectXKeyStrokes okey = Keyboard.DirectXKeyStrokes.DIK_NULL;
            string c = key.ToString();
            string o = "";
            c = c.Replace("DIK_", "");
            
            switch (c)
            {
                case "A": o= "D"; break;
                case "D": o= "A"; break;
                case "S": o ="W"; break;
            }

            if( GameInput.Inputs.ContainsKey(o)) {
                okey = (Keyboard.DirectXKeyStrokes)GameInput.Inputs[o];
            }

            return okey;
        }


        public static void InitData(bool bSave, int nTest = -1)
        {
            // USE TEST DATA.
            if (bSave == true && nTest > -1)
            {
                switch (nTest)
                {
                    case TESTDATA_CONFIG_GEN_STARCRAFT:
                        StarCraftTestData();
                        break;
                    case TESTDATA_CONFIG_GEN_FORNITE:
                        ForniteTestData();
                        break;
                }

                SaveGVSetup() ;
            } else
            {
                LoadGVSetup();
            }
        }
        /// <summary>
        /// StarDcraft
        /// </summary>
          private static void StarCraftTestData()
          { 
        
            //  commands.Add(NewItem("BUILD", new List<string>() { "B" }, new List<string>() { "PRESS" }, groupid_weapon));
            commands.Add(NewItem("BUILD COMMAND CENTER", "Build command center", 7.5f, new List<string>() { "B", "C" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));

            commands.Add(NewItem("BUILD REFINERY", "Build refinery", 7.5f, new List<string>() { "B", "R" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD SUPPLY"  , "Build supply", 5.5f,  new List<string>() { "B" , "S"}, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD BARACKS" , "Build barrracks", 5.5f, new List<string>() { "B", "B" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD BAY"     , "Build bay", 5.5f, new List<string>() { "B", "E" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD BUNKER"  , "Build bunker", 5.5f, new List<string>() { "B", "U" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD TURRET"  , "Build turret", 7.5f, new List<string>() { "B", "T" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD TOWER", "Build tower", 7.5f, new List<string>() { "B", "N" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));

            commands.Add(NewItem("BUILD FACTORY", "Build factory", 7.5f, new List<string>() { "V", "F" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD FACTORY", "Build factory", 7.5f, new List<string>() { "V", "F" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD ARMORY", "Build armory", 6.5f, new List<string>() { "V", "S" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("BUILD STAR PORT", "Build star port", 6.5f, new List<string>() { "V", "S" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));

            commands.Add(NewItem("REPAIR", "Repair", 7.5f, new List<string>() { "R"}, new List<string>() { "PRESS"}, groupid_action));
            commands.Add(NewItem("SPRAY", "Spray", 7.5f, new List<string>() { "Y" }, new List<string>() { "PRESS" }, groupid_action));


            commands.Add(NewItem("GROUP 1", "Group 1", 7.5f, new List<string>() { "1" }, new List<string>() { "PRESS" }, groupid_group));
            commands.Add(NewItem("GROUP 2", "Group 2", 7.5f, new List<string>() { "2" }, new List<string>() { "PRESS" }, groupid_group));
            commands.Add(NewItem("GROUP 3", "Group 3", 7.5f, new List<string>() { "3" }, new List<string>() { "PRESS" }, groupid_group));
            commands.Add(NewItem("GROUP 4", "Group 4", 7.5f, new List<string>() { "4" }, new List<string>() { "PRESS" }, groupid_group));
            commands.Add(NewItem("GROUP 5", "Group 5", 7.5f, new List<string>() { "5" }, new List<string>() { "PRESS" }, groupid_group));
            commands.Add(NewItem("GROUP 6", "Group 6", 7.5f, new List<string>() { "6" }, new List<string>() { "PRESS" }, groupid_group));
            commands.Add(NewItem("GROUP 7", "Group 7", 7.5f, new List<string>() { "7" }, new List<string>() { "PRESS" }, groupid_group));
            commands.Add(NewItem("GROUP 8", "Group 8", 7.5f, new List<string>() { "8" }, new List<string>() { "PRESS" }, groupid_group));
            commands.Add(NewItem("GROUP 9", "Group 9", 7.5f, new List<string>() { "9" }, new List<string>() { "PRESS" }, groupid_group));

            commands.Add(NewItem("MAKE GROUP 1", "Make group 1", 8.5f, new List<string>() { "LCONTROL", "1" , "LCONTROL" }, new List<string>() { "DOWN" , "PRESS", "UP"}, groupid_group));
            commands.Add(NewItem("MAKE GROUP 2", "Make group 2", 8.5f, new List<string>() { "LCONTROL", "2", "LCONTROL" }, new List<string>() { "DOWN", "PRESS", "UP" }, groupid_group));
            commands.Add(NewItem("MAKE GROUP 3", "Make group 3", 8.5f, new List<string>() { "LCONTROL", "3", "LCONTROL" }, new List<string>() { "DOWN", "PRESS", "UP" }, groupid_group));
            commands.Add(NewItem("MAKE GROUP 4", "Make group 4", 8.5f, new List<string>() { "LCONTROL", "4", "LCONTROL" }, new List<string>() { "DOWN", "PRESS", "UP" }, groupid_group));
            commands.Add(NewItem("MAKE GROUP 5", "Make group 5", 8.5f, new List<string>() { "LCONTROL", "5", "LCONTROL" }, new List<string>() { "DOWN", "PRESS", "UP" }, groupid_group));
            commands.Add(NewItem("MAKE GROUP 6", "Make group 6", 8.5f, new List<string>() { "LCONTROL", "6", "LCONTROL" }, new List<string>() { "DOWN", "PRESS", "UP" }, groupid_group));
            commands.Add(NewItem("MAKE GROUP 7", "Make group 7", 8.5f, new List<string>() { "LCONTROL", "7", "LCONTROL" }, new List<string>() { "DOWN", "PRESS", "UP" }, groupid_group));
            commands.Add(NewItem("MAKE GROUP 8", "Make group 8", 8.5f, new List<string>() { "LCONTROL", "8", "LCONTROL" }, new List<string>() { "DOWN", "PRESS", "UP" }, groupid_group));
            commands.Add(NewItem("MAKE GROUP 9", "Make group 9", 8.5f, new List<string>() { "LCONTROL", "9", "LCONTROL" }, new List<string>() { "DOWN", "PRESS", "UP" }, groupid_group));


            commands.Add(NewItem("CREATE MARINE", "marine", 6.5f, new List<string>() { "A" }, new List<string>() { "PRESS"}, groupid_build));
            commands.Add(NewItem("CREATE REAPER", "reaper", 6.5f, new List<string>() { "S" }, new List<string>() { "PRESS" }, groupid_build));

            commands.Add(NewItem("CREATE 2 MARINES", "2 marines", 6.5f, new List<string>() { "A" , "A" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("CREATE 2 REAPERS", "2 reapers", 6.5f, new List<string>() { "S", "S" }, new List<string>() { "PRESS", "PRESS" }, groupid_build));

            commands.Add(NewItem("CREATE 4 MARINES", "4 marines", 6.5f, new List<string>() { "A", "A", "A", "A" }, new List<string>() { "PRESS", "PRESS", "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("CREATE 4 REAPERS", "4 reapers", 6.5f, new List<string>() { "S", "S", "S", "S" }, new List<string>() { "PRESS", "PRESS", "PRESS", "PRESS" }, groupid_build));

            gvSetup = new GVSetup();
            gvSetup.gameName = "STARCRAFT II";
            gvSetup.commands = commands;

            BuildActionLookup();
         }

        /// Fornite
        public static void ForniteTestData()
        {
            commands.Add(NewItem("MOVE FORWARD", "RUN",  9.0f,   new List<string>() { "W" },             new List<string>() { "DOWN" }, groupid_navigation));
            commands.Add(NewItem("MOVE LEFT",    "LEFT", 9.0f,   new List<string>() { "A" } ,            new List<string>() { "DOWN" }, groupid_navigation));
            commands.Add(NewItem("MOVE BACKWARD","BACK", 9.8f,   new List<string>() { "S" },             new List<string>() { "DOWN" }, groupid_navigation));
            commands.Add(NewItem("MOVE RIGHT",   "RIGHT",9.0f,   new List<string>() { "D" },             new List<string>() { "DOWN" }, groupid_navigation));
            commands.Add(NewItem("JUMP",         "JUMP", 9.0f,   new List<string>() { "SPACE" },         new List<string>() { "PRESS" }, groupid_navigation));
            commands.Add(NewItem("SPRINT",       "SPRINT", 9.0f, new List<string>() { "LSHIFT" },         new List<string>() { "PRESS" }, groupid_navigation));
            commands.Add(NewItem("CROUCH",       "CROUCH", 9.0f, new List<string>() { "LCONTROL" },      new List<string>() { "PRESS" }, groupid_navigation));
          
            commands.Add(NewItem("FIRE", "FIRE", 9.0f,new List<string>() { "LEFTMOUSEBUTTON" }, new List<string>() { "PRESS" }, groupid_navigation));

            commands.Add(NewItem("TARGET OF",   "TARGET ON", 9.0f, new List<string>() { "T" }, new List<string>() { "DOWN" }, groupid_combat));
            commands.Add(NewItem("TARGET ON",   "TARGET OFF",9.0f, new List<string>() { "T" }, new List<string>() { "UP" }, groupid_combat));
            commands.Add(NewItem("RELOAD",      "RELOAD",9.0f, new List<string>() { "R" }, new List<string>() { "PRESS" }, groupid_combat));
            commands.Add(NewItem("USE", "E", 9.0f,new List<string>() { "E" }, new List<string>() { "DOWN" }, groupid_combat));
            commands.Add(NewItem("HARVESTING TOOL", "AXE",9.0f, new List<string>() { "F" }, new List<string>() { "PRESS" }, groupid_combat));
            
          
            commands.Add(NewItem("WEAPON SLOT 1 TOOL", "WEAPON 1", 9.0f,new List<string>() { "1" }, new List<string>() { "PRESS" }, groupid_weapon));
            commands.Add(NewItem("WEAPON SLOT 2 TOOL", "WEAPON 2", 9.0f,new List<string>() { "2" }, new List<string>() { "PRESS" }, groupid_weapon));
            commands.Add(NewItem("WEAPON SLOT 3 TOOL", "WEAPON 3",9.0f, new List<string>() { "3" }, new List<string>() { "PRESS" }, groupid_weapon));
            commands.Add(NewItem("WEAPON SLOT 4 TOOL", "WEAPON 4", 9.0f,new List<string>() { "4" }, new List<string>() { "PRESS" }, groupid_weapon));
            commands.Add(NewItem("WEAPON SLOT 5 TOOL", "WEAPON 5", 9.0f,new List<string>() { "5" }, new List<string>() { "PRESS" }, groupid_weapon));

            commands.Add(NewItem("WALL","WALL", 9.0f,        new List<string>() { "Z", "LEFTMOUSEBUTTON", "LASTWEAPON"}, new List<string>() { "PRESS", "PRESS", "ACTION" }, groupid_build));
            commands.Add(NewItem("FLOOR", "FLOOR",  9.0f,    new List<string>() { "X", "LEFTMOUSEBUTTON", "LASTWEAPON" }, new List<string>() { "PRESS", "PRESS", "ACTION" }, groupid_build));
            commands.Add(NewItem("STAIRS", "STAIRS", 9.0f,   new List<string>() {  "C", "LEFTMOUSEBUTTON", "LASTWEAPON" }, new List<string>() { "PRESS",  "PRESS" ,"ACTION"}, groupid_build));
            commands.Add(NewItem("ROOF", "ROOF",    9.0f,    new List<string>() { "V", "LEFTMOUSEBUTTON", "LASTWEAPON" }, new List<string>() { "PRESS", "PRESS", "ACTION"}, groupid_build));
            commands.Add(NewItem("TRAP", "TRAP",   9.0f,     new List<string>() { "Y", "LEFTMOUSEBUTTON",  }, new List<string>() { "PRESS", "PRESS" }, groupid_build));
            commands.Add(NewItem("PLACE BUILDING", "BUILD",9.0f, new List<string>() { "LEFTMOUSEBUTTON" }, new List<string>() { "PRESS" }, groupid_build));
            commands.Add(NewItem("TRAP EQUIP", "TRAP", 9.0f,new List<string>() { "F3", }, new List<string>() { "PRESS" }, groupid_special));
            commands.Add(NewItem("EMOTE", "DANCE",9.0f, new List<string>() { "B", }, new List<string>() { "PRESS" }, groupid_special));

            // commands.Add(NewItem("QICKBAR", "QICKBAR", new List<string>() { "Q" }, new List<string>() { "PRESS" }, groupid_build));

            commands.Add(NewItem("WEAPON SLOT 1 DROP", "DROP 1", 9.0f,new List<string>() { "TAB","X", "TAB" }, new List<string>() { "PRESS", "PRESS", "PRESS" }, groupid_special));
            commands.Add(NewItem("WEAPON SLOT 2 DROP", "DROP 2", 9.0f,new List<string>() { "TAB", "RIGHTARROW","X", "TAB" }, new List<string>() { "PRESS", "PRESS", "PRESS", "PRESS" }, groupid_special));
            commands.Add(NewItem("WEAPON SLOT 3 DROP", "DROP 3", 9.0f,new List<string>() { "TAB", "RIGHTARROW", "RIGHTARROW","X", "TAB" }, new List<string>() { "PRESS","PRESS", "PRESS", "PRESS", "PRESS" }, groupid_special));
            commands.Add(NewItem("WEAPON SLOT 4 DROP", "DROP 4", 9.0f,new List<string>() { "TAB", "RIGHTARROW", "RIGHTARROW","RIGHTARROW", "X", "TAB" }, new List<string>() { "PRESS","PRESS", "PRESS", "PRESS", "PRESS", "PRESS" }, groupid_special));
            commands.Add(NewItem("WEAPON SLOT 5 DROP", "DROP 5", 9.0f,new List<string>() { "TAB", "RIGHTARROW", "RIGHTARROW", "RIGHTARROW","RIGHTARROW", "X", "TAB" }, new List<string>() { "PRESS", "PRESS", "PRESS", "PRESS", "PRESS", "PRESS", "PRESS" }, groupid_special));

            // commands.Add(NewItem("WEAPON SLOT 2 DROP", "DROP 1",9.0, new List<string>() { "TAB", "X" }, new List<string>() { "PRESS" }, ,));

            commands.Add(NewItem("STOP", "STOP",9.0f, new List<string>(), new List<string>(), groupid_special));
            commands.Add(NewItem("AUTO FIRE ON", "FIRE ON", 9.0f,new List<string>(), new List<string>(), groupid_special));
            commands.Add(NewItem("AUTO FIRE OFF", "FIRE OFF", 9.0f,new List<string>(), new List<string>(), groupid_special));

            commands.Add(NewItem("MAP", "MAP", 9.0f,new List<string>() { "M" }, new List<string>() { "PRESS" }, groupid_special));

            commands.Add(NewItem("EXIT BUS",     "DIVE",9.0f,new List<string>() { "SPACE", "W" },    new List<string>() { "PRESS", "DOWN" }, groupid_special));
            commands.Add(NewItem("AUTO DODGE ON", "DODGE ON", 9.0f,new List<string>(), new List<string>(), groupid_special));
            commands.Add(NewItem("AUTO DODGE OFF", "DODGE OFF", 9.0f,new List<string>(), new List<string>(), groupid_special));
            commands.Add(NewItem("GHOST ON", "GHOST ON", 9.0f,new List<string>(), new List<string>(), groupid_special));
            commands.Add(NewItem("GHOST OFF", "GHOST OFF", 9.0f,new List<string>(), new List<string>(), groupid_special));
            commands.Add(NewItem("AUTO JUMP ON", "JUMP ON",9.0f, new List<string>() { "SPACE" },         new List<string>() { "DOWN"}, groupid_special));
            commands.Add(NewItem("AUTO JUMP OFF", "JUMP OFF", 9.0f,new List<string>() { "SPACE" },       new List<string>() { "UP" }, groupid_special));
           
            commands.Add(NewItem("AUTO BUILD ON", "BUILD ON", 9.0f,new List<string>(), new List<string>(), groupid_special));
            commands.Add(NewItem("AUTO BUILD OFF", "BUILD OFF", 9.0f, new List<string>(), new List<string>(), groupid_special));

            gvSetup = new GVSetup();
            gvSetup.gameName = "FORNITE";
            gvSetup.commands = commands;

            BuildActionLookup();
        }

        public static void BuildActionLookup()
        {
            WordToAction = new Dictionary<string, string>();
            ActionToWord = new Dictionary<string, string>();
            ActionToGroupID = new Dictionary<string, string>();
            ActionToKeys = new Dictionary<string, List<string>>();
            ActionkeyBehaviours = new Dictionary<string, List<string>>();
            WordConfidenceMin = new Dictionary<string, float>();
            int len = commands.Count();

            for (var n = 0; n < len; n++)
            {
                WordToAction[commands[n].word] = commands[n].action;
                WordConfidenceMin[commands[n].word] = commands[n].confidence;
                ActionToWord[commands[n].action] = commands[n].word;
                ActionToGroupID[commands[n].action] = commands[n].groupid;
                
                for (var k = 0; k < commands[n].key.Count(); k++)
                {
                    if (commands[n].key.Count != commands[n].keyBehaviour.Count)
                    {
                        string message = "Number of keys mismatch number of key behaviours.";
                        string title = "Error (" + commands[n].action + ")";
                        MessageBox.Show(message, title);
                    }
                    else
                    {
                        // Check Key Names
                        for (int ni = 0; ni < commands[n].key.Count(); ni++)
                        {
                            if(!GameInput.Inputs.ContainsKey(commands[n].key[ni])) //&& commands[n].keyBehaviour[ni] != "ACTION")
                            { 
                                string message = "Invalid Key name: " + commands[n].key[ni];
                                  string title = "Error (" + commands[n].action + ")";
                                MessageBox.Show(message, title);
                            }
                        }
                        ActionToKeys[commands[n].action.ToUpper()] = new List<string>(commands[n].key);
                        ActionkeyBehaviours[commands[n].action.ToUpper()] = new List<string>(commands[n].keyBehaviour);
                    }
                }
            }
        }

        public static void SaveGVSetup()
        { 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GameVoiceControl|*.gvc";
            saveFileDialog.Title = "Save game voice control file";
            saveFileDialog.ShowDialog();

           // gvSetup.gameName = gameName;
           // gvSetup.commands = commands;

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();

                System.Xml.Serialization.XmlSerializer serialization = new System.Xml.Serialization.XmlSerializer(gvSetup.GetType());
                serialization.Serialize(fs, (object)gvSetup);
     
                fs.Close();
            }

            saveFileDialog.Dispose();
           // BuildActionLookup();
        }

        public static void LoadGVSetup()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GameVoiceControl|*.gvc";
            openFileDialog.Title = "Load game voice control file";
            openFileDialog.ShowDialog();
          
            // If the file name is not an empty string open it for saving.
            if (openFileDialog.FileName != "")
            {
                try
                {
                    gvSetup = new GVSetup();

                    System.IO.FileStream fs = (System.IO.FileStream)openFileDialog.OpenFile();
                    System.Xml.Serialization.XmlSerializer serialization = new System.Xml.Serialization.XmlSerializer(gvSetup.GetType());

                    gvSetup = (GVSetup)serialization.Deserialize(fs);
                    fs.Close();

                    commands = gvSetup.commands;
                    BuildActionLookup();
                }
                catch
                {
                    gvSetup = null;
                }
            }

            openFileDialog.Dispose();
        }
    }
}
