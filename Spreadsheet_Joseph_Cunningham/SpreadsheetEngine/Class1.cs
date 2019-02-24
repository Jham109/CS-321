using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public abstract class Cell
    {
        private readonly int row = 0, col = 0;
        protected string cellText, cellValue;

        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        private int RowIndex
        {
            get {return row; }
        }

        private int ColumnIndex
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
        }

        //OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class Spreadsheet
    {
        private int rows, cols;

          public Spreadsheet(int rowSize, int colSize)
        {

        }
    }
}
