using System;
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
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
        }

        bool enableBtn = true;
        private void button1_Click(object sender, EventArgs e)
        {
            if (enableBtn == true)
            {
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
                btn1.AccessibleName = "Disable Voice Control";
                btn1.Text = "Disable Voice Control";
                enableBtn = false;
            }
            else if (enableBtn == false)
            {
                recEngine.RecognizeAsyncStop();
                btn1.AccessibleName = "Enable Voice Control";
                btn1.Text = "Enable Voice Control";
                enableBtn = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "Next", "Previous", "Prev" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
        }

        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Previous":
                case "Prev":

                    richTextBox1.Text += "\nprevious";
                    break;
                case "Next":
                    richTextBox1.Text += "\nNext";
                    break;
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
