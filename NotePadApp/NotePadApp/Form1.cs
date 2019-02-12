//Joseph Cunningham - 11511536
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePadApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //the following functions handle the event of each menu button if clicked
        //****************************************************************************************************************
        private void loadFibonacciNumbers50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FibonacciTextReader fib = new FibonacciTextReader(50))
            {
                textBox1.Clear();

                LoadText(fib);
            }
        }

        private void loadFibonacciNumbers100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FibonacciTextReader fib = new FibonacciTextReader(100))
            {
                textBox1.Clear();

                LoadText(fib);
            }
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            DialogResult result = dialog.ShowDialog();
            
            if (result.ToString() == "OK")
            {
                string docPath = dialog.InitialDirectory;
                SaveText(docPath, dialog.FileName);
                
            }
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result.ToString() == "OK")
            {
                using (StreamReader sr = new StreamReader(dialog.FileName))
                {
                    textBox1.Clear();

                    LoadText(sr);
                }
            }
        }
        //****************************************************************************************************************

        /// <summary>
        /// function saves the text in the textbox to a file 
        /// </summary>
        /// <param name="docPath"></param>
        /// <param name="fileName"></param>
        private void SaveText(string docPath, string fileName)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName + ".txt"))) // saves the file as a txt file
            {
                outputFile.WriteLine(textBox1.Text);
            }
        }

        /// <summary>
        /// The generic load function that loads text from a file into the textbox
        /// </summary>
        /// <param name="sr"></param>
        private void LoadText(TextReader sr )
        {
            string line = sr.ReadLine();
            while (line != null)
            {
                textBox1.AppendText(line + Environment.NewLine);
                line = sr.ReadLine();
            }
        }

        
    }

    /// <summary>
    /// fibtextreader class which inherits from textreader outputs different number of sequences depending on what you pass in during initialization
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        private int max = 0;
        private int sequenceNum = -1;
        private BigInteger a = 0, b = 1;

        public FibonacciTextReader(int val)
        {
            max = val;
        }
        
        /// <summary>
        /// Function overrides TextReader's Readline() function to output fiboonacci numbers in sequence
        /// </summary>
        /// <returns> a string containing the sequence number and the fibonacci number if the sequence number is less than the max else returns null</returns>
        public override string ReadLine()
        {
            sequenceNum++;
            if (sequenceNum <= max)
            {
                // the logic to handle the first two numbers in the fibonacci sequence which is just 0 and 1
                if (sequenceNum == 0)
                {
                    return sequenceNum.ToString() + ": 0";
                }
                else if (sequenceNum == 1)
                {
                    return sequenceNum.ToString() + ": 1";
                }
                else
                {
                    BigInteger temp = a;
                    a = b;
                    b = temp + b;
                }

                return sequenceNum.ToString() + ": " + a.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
