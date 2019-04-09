using SpreadsheetEngine;
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
            dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);

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
            else if (e.PropertyName == "Color")
            {
                dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb((int)cell.BGColor);
            }
        }

        //Enters this when a cell begins to edit
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Cell cell = Sheet.GetCell(e.RowIndex, e.ColumnIndex);
            dataGridView1[e.ColumnIndex, e.RowIndex].Value = cell.Text;
        }

        //Enters this when hitting enter after editting or leaving the cell
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;
            string text = "";
            Cell cell = Sheet.GetCell(row, column);
            List<UndoRedoCmd> undos = new List<UndoRedoCmd>();

            try
            {
                text = dataGridView1.Rows[row].Cells[column].Value.ToString();
            }
            catch (NullReferenceException)
            {
                text = "";
            }

            string oldText = cell.Text;
            cell.Text = text;

            undos.Add(new UndoText(cell.Text, oldText, cell.RowIndex, cell.ColumnIndex));
            Sheet.AddUndo(new UndoRedoCollection(undos.ToArray(), "Text Change"));
            
            dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;

            UpdateDOMenu();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // enters this when the Demp button is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            for (int i = 0; i < 50; i++)
            {
                Sheet.sheet[i, 1].Text = "This is cell B" + (i + 1).ToString();
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

        // Enters this when User wants clicks the "Change Background Color" menu option
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            List<UndoRedoCmd> undos = new List<UndoRedoCmd>();

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                //...Get the chosen color as an int.
                int chosenColor = dialog.Color.ToArgb();

                // For each cell selected...
                foreach (DataGridViewCell dgCell in dataGridView1.SelectedCells)
                {
                    //get the cell from the spreadsheet
                    Cell cell = Sheet.GetCell(dgCell.RowIndex, dgCell.ColumnIndex);

                    //save the old color
                    uint oldColor = cell.BGColor;

                    //set the color in the spreadsheet
                    cell.BGColor = (uint)chosenColor;

                    undos.Add(new UndoBGColor(cell.BGColor, oldColor, cell.RowIndex, cell.ColumnIndex));
                }

                Sheet.AddUndo(new UndoRedoCollection(undos.ToArray(), "Background Color Change"));
                UpdateDOMenu();
            }
        }

        // the Undo button
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sheet.Undo(Sheet);

            UpdateDOMenu();
        }

        // the Redo button
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sheet.Redo(Sheet);
            UpdateDOMenu();
        }

        /// <summary>
        /// updates the menu with the new descriptions and also will disable/renable them
        /// </summary>
        private void UpdateDOMenu()
        {
            ToolStripMenuItem group = menuStrip1.Items[1] as ToolStripMenuItem;

            foreach (ToolStripItem item in group.DropDownItems)
            {
                if (item.Text.Contains("Undo"))
                {
                    if (Sheet.UndoDescription == "")
                    {
                        item.Enabled = false;
                    }
                    else
                    {
                        item.Enabled = true;
                    }

                    item.Text = "Undo " + Sheet.UndoDescription;
                }
                else if (item.Text.Contains("Redo"))
                {
                    if (Sheet.RedoDescription == "")
                    {
                        item.Enabled = false;
                    }
                    else
                    {
                        item.Enabled = true;
                    }
                    
                    item.Text = "Redo " + Sheet.RedoDescription;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                Sheet.Save(stream);
                stream.Dispose();
            }

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                Sheet.Load(stream);
                stream.Dispose();
            }

            UpdateDOMenu();
        }
    }
}
