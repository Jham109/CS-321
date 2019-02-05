using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        internal static void Form1_load()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize the list of integers 
            List<int> integers = new List<int>();
            //initialize the random number generator
            Random rand = new Random();

            //load the list with random integers
            integers = LoadList(integers, rand);

            string output1 = Task1(integers);
            this.textBox1.Text = output1;
        }

        /// <summary>
        /// Loads the input list with 10,000 random integers in the range [0, 20,000]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="rand"></param>
        /// <returns name="input"></returns>
        public List<int> LoadList(List<int> input, Random rand)
        {
            for (int i = 0; i < 10000; i++)
            {
                input.Add(rand.Next(20000));
            }
            return input;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String Task1(List<int> input)
        {
            HashSet<int> Distinct = new HashSet<int>();

            for (int i = 0; i < input.Count; i++)
            {
                Distinct.Add(input[i]);
            }


            return (Distinct.Count().ToString());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String Task2(List<int> input)
        {
           
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String Task3(List<int> input)
        {
           
            return "";
        }
    }
}
