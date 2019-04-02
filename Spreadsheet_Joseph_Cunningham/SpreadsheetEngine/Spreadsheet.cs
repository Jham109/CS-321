using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using ExpressionEvaluator;

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
                if (((Cell)sender).Text != "")
                {
                    if (((Cell)sender).Text[0] == '=')
                    {
                        string algorithm = ((Cell)sender).Text.TrimStart('=');
                        ((Cell)sender).variables = new List<Cell>();
                        Tree = new ExpressionTree(algorithm);

                        //get list of variables and get values from the corresponding cells
                        List<string> variables = Tree.GetVariables();
                        foreach (string cell in variables)
                        {
                            int column = Convert.ToInt16(cell[0]) - 'A';
                            int row = Convert.ToInt16(cell.Substring(1)) - 1;
                            double value;

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

                        //evaluate the tree and set it as the cell's value
                        ((Cell)sender).Value = Tree.Evaluate().ToString();
                    }
                }
            }
          if(e.PropertyName == "Color")
            {
                PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Color"));
            }
           PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
        }

        public Cell GetCell(int row, int col)
        {
            if( row < 0 || row > rows && col < 0 || col > cols)
            {
                return null;
            }

            return sheet[row, col];
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
