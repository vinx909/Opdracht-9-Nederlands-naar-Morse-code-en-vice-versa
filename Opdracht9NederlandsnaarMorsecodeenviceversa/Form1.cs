using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opdracht9NederlandsnaarMorsecodeenviceversa
{
    public partial class Form1 : Form
    {
        private const double persentageTotal = 100;

        private const string buttonTranslateToMorseString = "convert to morse code";
        private const string buttonTranslateToLetterString = "convert morse code to letter";

        private const int buttonWidthPersentage = 15;

        private int textBoxWidth;
        private int buttonWidth;

        private RichTextBox textBoxToTranslate;
        private RichTextBox textBoxTranslated;
        private Button buttonTranslateToMorse;
        private Button buttonTranslateToLetter;

        public Form1()
        {
            InitializeComponent();
            SetupSizes();
            SetupTextBoxes();
            SetupButtons();
        }
        private void SetupSizes()
        {
            buttonWidth = (int)(ClientRectangle.Width / persentageTotal * buttonWidthPersentage);
            textBoxWidth = (ClientRectangle.Width - buttonWidth) / 2;
        }
        private void SetupTextBoxes()
        {
            textBoxToTranslate = new RichTextBox();
            textBoxTranslated = new RichTextBox();
            textBoxToTranslate.Width = textBoxWidth;
            textBoxTranslated.Width = textBoxWidth;
            textBoxToTranslate.Height = ClientRectangle.Height;
            textBoxTranslated.Height = ClientRectangle.Height;
            textBoxToTranslate.Location = new Point(0, 0);
            textBoxTranslated.Location = new Point(textBoxWidth + buttonWidth, 0);
            Controls.Add(textBoxToTranslate);
            Controls.Add(textBoxTranslated);
        }
        private void SetupButtons()
        {
            buttonTranslateToMorse = new Button();
            buttonTranslateToLetter = new Button();
            buttonTranslateToMorse.Width = buttonWidth;
            buttonTranslateToLetter.Width = buttonWidth;
            buttonTranslateToMorse.Location = new Point(textBoxWidth, 0);
            buttonTranslateToLetter.Location = new Point(textBoxWidth, buttonTranslateToMorse.Height);
            buttonTranslateToMorse.Text = buttonTranslateToMorseString;
            buttonTranslateToLetter.Text = buttonTranslateToLetterString;
            buttonTranslateToMorse.Click += new EventHandler(ButtonTranslateToMorseFunction);
            buttonTranslateToLetter.Click += new EventHandler(ButtonTranslateToLetterFunction);
            Controls.Add(buttonTranslateToMorse);
            Controls.Add(buttonTranslateToLetter);
        }

        private void ConvertToMorseCode()
        {
            textBoxTranslated.Text = MorseCodeConverter.ConvertToMorseCode(textBoxToTranslate.Text);
        }
        private void ConvertToLetters()
        {
            textBoxTranslated.Text = MorseCodeConverter.ConvertToLetter(textBoxToTranslate.Text);
        }

        private void ButtonTranslateToMorseFunction(object sender, EventArgs e)
        {
            ConvertToMorseCode();
        }
        private void ButtonTranslateToLetterFunction(object sender, EventArgs e)
        {
            ConvertToLetters();
        }
    }
}
