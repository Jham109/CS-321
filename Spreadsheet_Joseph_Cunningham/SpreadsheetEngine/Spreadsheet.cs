using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using ExpressionEvaluator;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace SpreadsheetEngine
{
    public abstract class Cell
    {
        protected readonly int row = 0, col = 0;
        protected string cellText, cellValue;
        private HashSet<Cell> dependents = new HashSet<Cell>();
        public List<Cell> variables = new List<Cell>();
        private uint color = 0xFFFFFFFF;
        public uint BGColor
        {
            get
            {
                return color;
            }
            set {
                if (value != color)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }            
            }
        }  

        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        public int RowIndex
        {
            get {return row; }
        }

        public int ColumnIndex
        {
            get { return col; }
        }


        public Cell(int rowNum, int colNum)
        {
            row = rowNum;
            col = colNum;
        }

        public string Text
        {
            get {return cellText;}
            set
            {
                if(value != cellText)
                {
                    cellText = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public string Value
        {
            get { return cellValue; }

            internal set
            {
                if (value != cellValue)
                {
                    cellValue = value;

                    //update each cell that is dependent on this cell
                    foreach(Cell cell in dependents)
                    {
                        if(cell.variables.Contains(this))
                        {
                            cell.OnPropertyChanged("Value");
                        }
                        //if no longer a variable in the cell's expression
                        else
                        {
                            dependents.Remove(cell);
                        }
                    }
                    OnPropertyChanged("Value");
                }
            }
        }

        public void AddDependent(Cell cell)
        {
            dependents.Add(cell);
        }

        //OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

    public class Unit : Cell
    {
        public Unit(int rowIndex, int colIndex) :base(rowIndex, colIndex)
        {      
        }


    }


    /// <summary>
    /// This class represents a 2d array of cells and handles all of the actions of the cells
    /// </summary>
    public class Spreadsheet
    {
        private int rows, cols;
        private Stack<UndoRedoCollection> undos = new Stack<UndoRedoCollection>();
        private Stack<UndoRedoCollection> redos = new Stack<UndoRedoCollection>();
        public ExpressionTree Tree; 
        public Cell[,] sheet;
        
        

        public event PropertyChangedEventHandler PropertyChanged;

        public Spreadsheet(int rowSize, int colSize)
        {
            rows = rowSize;
            cols = colSize;

            sheet = new Cell[rows, cols];

            for(int r = 0; r < rows; r++)
            {
                for(int c = 0; c < cols; c++)
                {
                    sheet[r, c] = new Unit(r, c);
                    sheet[r, c].PropertyChanged += new PropertyChangedEventHandler(CellPropertyChanged);
                }
            }
        }

        public int ColumnCount
        {
            get { return cols; }
        }

        public int RowCount
        {
            get { return rows; }
        }

        //OnPropertyChanged method to raise the event
        public void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
          if (e.PropertyName == "Text")
            {
                ((Cell)sender).Value = ((Cell)sender).Text;

            }
          if(e.PropertyName == "Value")
            {
                if (((Cell)sender).Text != "" && ((Cell)sender).Text != null)
                {
                    if (((Cell)sender).Text[0] == '=')
                    {
                        string algorithm = ((Cell)sender).Text.TrimStart('=');
                        ((Cell)sender).variables = new List<Cell>();
                        Tree = new ExpressionTree(algorithm);
                        string badRef = null;

                        //get list of variables and get values from the corresponding cells
                        List<string> variables = Tree.GetVariables();
                        foreach (string cell in variables)
                        {
                            int column = Convert.ToInt16(cell[0]) - 'A';
                            int row = Convert.ToInt16(cell.Substring(1)) - 1;
                            double value;

                            // if the cell exists in the spreadsheet...
                            if ((column < 26 && column >= 0) && (row < 50 && row >= 0))
                            {
                                //add itself to the dependent list in the cells in it's expression
                                GetCell(row, column).AddDependent(((Cell)sender));

                                //add the cells in it's expression to it's variable list
                                ((Cell)sender).variables.Add(GetCell(row, column));

                                if (Double.TryParse(GetCell(row, column).Value, out value))
                                {
                                    Tree.SetVariable(cell, value);
                                }
                                else
                                {
                                    Tree.SetVariable(cell, 0);
                                }
                            }
                            else
                            {
                                //output bad reference string if there is there is no corresponding cell
                                badRef = "!(bad reference)";
                            }
                        }

                        // if the bad reference wasnt set
                        if (badRef == null)
                        {
                            //evaluate the tree and set it as the cell's value
                            ((Cell)sender).Value = Tree.Evaluate().ToString();
                        }
                        else //set the value as a bad refence
                        {
                            ((Cell)sender).Value = badRef;
                        }
                    }
                }
            }
          if(e.PropertyName == "Color")
            {
                PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Color"));
            }
           PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
        }

        /// <summary>
        /// Returns a cell given the row index and the column index
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Cell GetCell(int row, int col)
        {
            if( row < 0 || row > rows && col < 0 || col > cols)
            {
                return null;
            }

            return sheet[row, col];
        }

        // returns the corresponding cell in the spreadsheet given the name of the cell
        public Cell GetCell(string name)
        {
            Int16 number;
            Cell cell;

            // Return null if the name doesnt start with a letter
            if (!Char.IsLetter(name[0]))
            {  
                return null;
            }

            //Return null if the the rest of the name isnt a number
            if (!Int16.TryParse(name.Substring(1), out number))
            {
                return null;
            }


            try
            {
                cell = GetCell(number - 1, name[0] - 'A');
            }
            // If the given location does not exist in the spreadsheet, return null.
            catch (Exception)
            {
                
                return null;
            }

            return cell;
        }

        //clears all of the data in the Cells array
        private void clear()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    sheet[r, c].Text = null;
                }
            }

            //clear out the undos and redos stack
            undos.Clear();
            redos.Clear();
        }

        // Load a XML file into the spreadsheet
        public bool Load(Stream stream)
        {
            XDocument file = null;

            try
            {
                file = XDocument.Load(stream);
            }
            catch (Exception)
            {
                return false;
            }

            if (file == null)
            {
                return false;
            }

            // Clear the sheet
            this.clear();
            
            XElement root = file.Root;

           
                if ("Spreadsheet" != root.Name)
                {
                    return false;
                }

                foreach (XElement child in root.Elements("Cell"))
                {
                    Cell cell = GetCell(child.Attribute("Name").Value);
                    var textElement = child.Element("Text");
                    var bgElement = child.Element("BGColor");

                    // Only edit existing cells.
                    if (cell == null) { continue; }

                    // Load and set text.
                    if (textElement != null)
                    {
                        cell.Text = textElement.Value;
                    }

                    //Load and set background color.        
                    if (bgElement != null)
                    {
                        cell.BGColor = uint.Parse(bgElement.Value);
                    }
                }
            
            return true;
        }

        //Saves spreadsheet data to an xml file
        public bool Save(Stream stream)
        {
            List<Cell> cellsToWrite = new List<Cell>();
            Cell cell1;

            // Create the XmlWriter to write to the stream
            XmlWriter writer = XmlWriter.Create(stream);
            if (writer == null)
            {
                return false;
            }

            writer.WriteStartElement("Spreadsheet");

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    cell1 = GetCell(r, c);

                    if (cell1.Text != null || cell1.BGColor != 0xFFFFFFFF)
                    {
                        cellsToWrite.Add(cell1);
                    }
                }
            }

            foreach (Cell cell in cellsToWrite)
            {
                String Name = null;
                Name += Convert.ToChar('A' + cell.ColumnIndex);
                Name += (cell.RowIndex + 1).ToString();

                writer.WriteStartElement("Cell");
                writer.WriteAttributeString("Name", Name);

                writer.WriteElementString("Text", cell.Text);
                writer.WriteElementString("BGColor", cell.BGColor.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Close();

            return true;
        }

        // next region deals with undo/redo stack actions
        #region 

        //The description of the next available undo.
        public string UndoDescription
        {
            get
            {
                if (undos.Count != 0)
                {
                    return undos.Peek().GetDescription;
                }
                return "";
            }
        }

        //The description of the next available redo.
        public string RedoDescription
        {
            get
            {
                if (redos.Count != 0)
                {
                    return redos.Peek().GetDescription;
                }
                return "";
            }
        }

        //pushes a new undo command on to the undo stack
        public void AddUndo(UndoRedoCollection newUndo)
        {
            undos.Push(newUndo);
            redos.Clear();
        }

        //executes an undo command
        public void Undo(Spreadsheet sheet)
        {
            
            UndoRedoCollection undo = undos.Pop();
            redos.Push(undo.Execute(sheet));
            //refresh(sheet);
        }

        //executes a redo command
        public void Redo(Spreadsheet sheet)
        {
            UndoRedoCollection redo = redos.Pop();
            undos.Push(redo.Execute(sheet));
        }
        #endregion
    }
}
