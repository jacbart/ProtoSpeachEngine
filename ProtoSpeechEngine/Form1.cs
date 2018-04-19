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
using NVAccess;

namespace protoSpeechEngine
{
    public partial class Form1 : Form
    {
        SpeechRecognizer recEngine = new SpeechRecognizer();
        bool enableBtn = true;
        Grammar movementGrammar;
        Grammar modeGrammar;
        NVDA NVDAApi = new NVDA();
        String[] modes = new String[] { "Edit", "Review"};

        public Form1()
        {
            InitializeComponent();
        }

        public void SendCommand(String command)
        {
            Process.Start("msr_util.exe", command);
        }

        public void speak(String text)
        {
            NVDA.Say(text, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (enableBtn == true)
            {
                recEngine.LoadGrammarAsync(movementGrammar);
                recEngine.LoadGrammarAsync(modeGrammar);
                btn1.AccessibleName = "Voice Control Enabled";
                btn1.Text = "Voice Control Enabled";
                richTextBox1.Text += "Enabling Command Logging\n";
                enableBtn = false;
            }
            else if (enableBtn == false)
            {
                recEngine.UnloadGrammar(movementGrammar);
                recEngine.UnloadGrammar(modeGrammar);
                btn1.AccessibleName = "Voice Control Disabled";
                btn1.Text = "Voice Control Disabled";
                richTextBox1.Text += "Disabling Command Logging\n";
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
            Choices modeCommands= new Choices();
            modeCommands.Add(modes);
            GrammarBuilder modeBuilder = new GrammarBuilder();
            modeBuilder.Append(modeCommands);
            modeBuilder.Append("Mode");
            modeGrammar = new Grammar(modeBuilder);
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Previous":
                case "Prev":
                    this.SendCommand("review_previousWord");
                    richTextBox1.Text += "Previous\n";
                    break;
                case "Next":
                    this.SendCommand("review_nextWord");
                    richTextBox1.Text += "Next\n";
                    break;
                case "Root":
                    this.SendCommand("navigatorObject_moveFocus|2");
                    richTextBox1.Text += "Root\n";
                    break;
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
