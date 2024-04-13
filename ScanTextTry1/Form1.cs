using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using AForge;

namespace ScanTextTry1
{
    public partial class Form1 : Form
    {
        public string openedText = ""; //string that is text, converted from image
        public int num = 0; //int to show later that all words are not restricted
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {

                ImageBox.Image = new Bitmap(open.FileName);

                PathBox.Text = open.FileName;
            }
        }

        public void ConvBTN_Click(object sender, EventArgs e)
        {
            var ocrengine = new TesseractEngine(@".\tessdata", "rus+eng", EngineMode.Default);
            var img = Pix.LoadFromFile(PathBox.Text);
            var res = ocrengine.Process(img);
            ConvTextBox.Text = res.GetText();
            openedText = res.GetText();
        }

        public void CheckTheText(string text) //Checks the text for "restricted" words
        {
            string[] RestrictedText = { "Sugar", "sugar" };
            List<string> words = new List<string>(text.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries));

            foreach (string word in words)
            {
                string trimmedWord = word.Trim();
                if (RestrictedText.Any(restrictedWord => string.Equals(restrictedWord, trimmedWord, StringComparison.OrdinalIgnoreCase)))
                {
                    listBox1.Items.Add(trimmedWord);
                }
                else
                {
                    num++;
                }
            }
            if (num == words.Count) listBox1.Items.Add("No restricted words");
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            if(!(openedText is null)) CheckTheText(openedText);
        }
    }
}
