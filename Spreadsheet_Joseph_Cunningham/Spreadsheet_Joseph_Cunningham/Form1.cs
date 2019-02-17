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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

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
    }
}
