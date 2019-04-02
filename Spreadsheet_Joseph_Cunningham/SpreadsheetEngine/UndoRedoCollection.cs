using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// a collection of undo and redo commands
    /// </summary>
    public class UndoRedoCollection
    {
        private string description;
        private UndoRedoCmd[] cmds;

        public UndoRedoCollection()
        {
        }

        //overloaded constructor
        public UndoRedoCollection(UndoRedoCmd[] newCmds, string newDescription)
        {
            cmds = newCmds;
            description = newDescription;
        }

        //allows user to get the description but not set it
        public string GetDescription
        {
            get { return description; }
        }

        //executes each command in the collection
        public UndoRedoCollection Execute(Spreadsheet sheet)
        {
            List<UndoRedoCmd> cmdList = new List<UndoRedoCmd>();

            foreach (UndoRedoCmd cmd in cmds)
            {
                cmdList.Add(cmd.Execute(sheet));
            }

            return new UndoRedoCollection(cmdList.ToArray(), this.description);
        }
    }

    /// <summary>
    /// base class for the undo/redo commands
    /// </summary>
    public interface UndoRedoCmd
    {
        UndoRedoCmd Execute(Spreadsheet sheet);
       
    }

    /// <summary>
    /// represent the undo command of a cell's background color
    /// </summary>
    public class UndoBGColor : UndoRedoCmd
    {
        private uint cellBGcolor;
        private int cellRow, cellColumn;

        public UndoBGColor(uint color, int row, int column)
        {
            cellBGcolor = color;
            cellRow = row;
            cellColumn = column;
        }

        public new UndoRedoCmd Execute(Spreadsheet sheet)
        {
            Cell cell = sheet.GetCell(cellRow, cellColumn);
            uint oldColor = cell.BGColor;
            cell.BGColor = cellBGcolor;

            return new UndoBGColor(oldColor, cellRow, cellColumn);
        }
    }

    /// <summary>
    /// represents the undo command of a cell's text
    /// </summary>
    public class UndoText : UndoRedoCmd
    {
        private string cellText;
        private int cellRow, cellColumn;

        public UndoText(string text, int row, int column)
        {
            cellText = text;
            cellRow = row;
            cellColumn = column;
        }

        public new UndoRedoCmd Execute(Spreadsheet sheet)
        {
            Cell cell = sheet.GetCell(cellRow, cellColumn);
            string oldText = cell.Text;
            cell.Text = cellText;

            return new UndoText(oldText, cellRow, cellColumn);
        }
    }
        

}
