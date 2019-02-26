using SpreadsheetEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet_Joseph_Cunningham
{
    public partial class Form1 : Form
    {
        Spreadsheet Sheet; 

        public Form1()
        {
            InitializeComponent();
            Sheet = new Spreadsheet(50, 26);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            Sheet.PropertyChanged += new PropertyChangedEventHandler(CellPropertyChanged);

            // create columns A-Z
            dataGridView1.ColumnCount = 26;
            for (int i = 0; i < 26; i++)
            {
                char asci = (char)(65 + i);
                dataGridView1.Columns[i].HeaderText = asci.ToString();
            }

            //create 50 rows
            dataGridView1.RowCount = 50;
            dataGridView1.RowHeadersVisible = true;
            for (int i = 0; i < 50; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (cell != null && e.PropertyName == "Value")
            {
                dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            for (int i = 0; i < 50; i++)
            {
                Sheet.sheet[i, 1].Text = "This is cell B" + (i + 1).ToString();
            }
            for (int i = 0; i < 50; i++)
            {
                Sheet.sheet[i, 0].Text = "=B" + (i + 1).ToString();
            }

            for (int i = 0; i < 50; i++)
            {
                int randomCol = rand.Next(0, 25);
                int randomRow = rand.Next(0, 49);

                Cell randomCell = Sheet.GetCell(randomRow, randomCol);
                randomCell.Text = "Hello";
                Sheet.sheet[randomRow, randomCol] = randomCell;
            }
        }
    }
}
