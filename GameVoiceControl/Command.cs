using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameVoiceControl
{
    public class GVCommand
    {
        public class CommandItem {

            public string commandName = "";
            public string key = "";
            public string word = "";
        }

        private List<CommandItem> commands = new List<CommandItem>();
      
        public GVCommand()
        {
        }

        public int test()
        {
            return 1;
        }

        public CommandItem NewItem(string commandName, string key, string word)
        {
            CommandItem c = new CommandItem
            {
                commandName = commandName,
                key = key,
                word = word
            };

            return c;
        }

        public void SaveCommands()
        { 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GameVoiceControl|*.gvc";
            saveFileDialog.Title = "Save game voice control file";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();

                System.Xml.Serialization.XmlSerializer serialization = new System.Xml.Serialization.XmlSerializer(commands.GetType());
                serialization.Serialize(fs, commands);
     
                fs.Close();
            }
        }

        public void LoadCommands()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GameVoiceControl|*.gvc";
            openFileDialog.Title = "Load game voice control file";
            openFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (openFileDialog.FileName != "")
            {
              
                System.IO.FileStream fs = (System.IO.FileStream)openFileDialog.OpenFile();
           
                System.Xml.Serialization.XmlSerializer serialization = new System.Xml.Serialization.XmlSerializer(commands.GetType());
                commands = new List<CommandItem>();
                commands = (List<CommandItem>) serialization.Deserialize(fs);

                fs.Close();
            }
        }
    }
}
