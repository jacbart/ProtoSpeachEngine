using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
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
                btn1.AccessibleName = "Voice Control Enabled";
                btn1.Text = "Voice Control Enabled";
                richTextBox1.Text += "Enabling Command Logging\n";
                enableBtn = false;
            }
            else if (enableBtn == false)
            {
                recEngine.UnloadGrammar(movementGrammar);
                btn1.AccessibleName = "Voice Control Disabled";
                btn1.Text = "Voice Control Disabled";
                richTextBox1.Text += "Disabling Command Logging\n";
                enableBtn = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GrammarBuilder movementGrammarBuilder = new GrammarBuilder();

            // Defining the Grammar XML file
            string fi = Directory.GetCurrentDirectory() + @"\vocab.grxml";
            // Checks if the vocab file exist and loads it in if it does, otherwise I displays a error message
            if (File.Exists(fi))
            {
                movementGrammarBuilder.AppendRuleReference(fi);
                richTextBox1.Text += "Grammar File Loaded (" + fi + ")\n";
            }
            else
            {
                richTextBox1.Text += "Unable to load grammar file\n";
            }

            movementGrammar = new Grammar(movementGrammarBuilder);

            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(RecEngine_SpeechRecognized);
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.Text += e.Result.Text + "\n";
            switch (e.Result.Text)
            {
                case "Previous":
                    this.SendCommand("review_previousWord");
                    break;
                case "Next":
                    this.SendCommand("review_nextWord");
                    break;
                case "Root":
                    this.SendCommand("navigatorObject_moveFocus|2");
                    break;
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}