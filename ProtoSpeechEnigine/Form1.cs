using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace ProtoSpeechEnigine
{
    public partial class Form1 : Form
    {
        SpeechRecognizer recEngine = new SpeechRecognizer();
        bool enableBtn = true;
        Grammar movementGrammar;
        public Form1()
        {
            InitializeComponent();
        }

        public void SendCommand(String command)
        {
            Process.Start("msr_util.exe", command);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (enableBtn == true)
            {
                recEngine.LoadGrammarAsync(movementGrammar);
                btn1.AccessibleName = "Voice Control Enabled";
                btn1.Text = "Voice Control Enabled";
                enableBtn = false;
            }
            else if (enableBtn == false)
            {
                recEngine.UnloadGrammar(movementGrammar);
                btn1.AccessibleName = "Voice Control Disabled";
                btn1.Text = "Voice Control Disabled";
                enableBtn = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices movementCommands = new Choices();
            movementCommands.Add(new string[] { "Next", "Previous", "Prev", "Root" });
            GrammarBuilder movementGrammarBuilder = new GrammarBuilder();
            movementGrammarBuilder.Append(movementCommands);
            movementGrammar = new Grammar(movementGrammarBuilder);
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Previous":
                case "Prev":
                    this.SendCommand("review_previousWord");
                    richTextBox1.Text += "\nprevious";
                    break;
                case "Next":
                    this.SendCommand("review_nextWord");
                    richTextBox1.Text += "\nNext";
                    break;
                case "Root":
                    this.SendCommand("navigatorObject_moveFocus|2");
                    richTextBox1.Text += "\nRoot";
                    break;
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
