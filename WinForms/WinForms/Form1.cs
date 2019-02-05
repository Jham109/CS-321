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
        /// Function for the load event of the form, executed after the form loads and displays the outputs of the
        /// 3 processing tasks in the textbox
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
            this.textBox1.Text ="(1) HashSet method: " + output1 + Environment.NewLine;
            this.textBox1.AppendText("The time complexity of this method is just O(n) because all the program does is run through each item in the input list and add them to the Hash set, which naturally removes duplicates by looking up if each item exists in O(1) time" + Environment.NewLine);

            string output2 = Task2(integers);
            this.textBox1.AppendText("(2) O(1) storage method: " + output2 + Environment.NewLine);

            string output3 = Task3(integers);
            this.textBox1.AppendText("(3) sorted method: " + output3 + Environment.NewLine);
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
        /// Loads every item from the input list into a hashset which automatically removes the duplicates as each item is added
        /// </summary>
        /// <returns> String of the count of the number of items in the HashSet</returns>
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
        /// Counts the number of distinct items in a list 
        /// </summary>
        /// <returns> the count of the distinct items in the list</returns>
        public String Task2(List<int> input)
        {
            int distinctCount = 1;

            for (int i = 1; i < input.Count; i++)
            {
               if(!input.GetRange(0, i-1).Contains(input[i])) // if the item at the index of the list is not contained in the previous part of the list
                {
                    distinctCount++;
                }
            }

            return distinctCount.ToString();
        }

        /// <summary>
        /// Sorts the input list and counts the number of distinct items in the list
        /// </summary>
        /// <returns> the count of the disting items in the list </returns>
        public String Task3(List<int> input)
        {
            int distinctCount = 1;
            input.Sort();

            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] != input[i-1]) // since the list is sorted you can just check to see when the next new number is and count it
                {
                    distinctCount++;
                }
            }

            return distinctCount.ToString();
        }
    }
}
