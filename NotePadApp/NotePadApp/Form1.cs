//Joseph Cunningham - 11511536
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SaveText()
        {

        }

        private void LoadText(TextReader sr )
        {

        }
    }

    public class FibonacciTextReader : TextReader
    {
        private int max = 0;

        public FibonacciTextReader(int val)
        {
            max = val;
        }
        
        private string Readline()
        {

            return "";
        }
    }
}
