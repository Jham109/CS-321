using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private uint cellBGcolor, priorColor;
        private int cellRow, cellColumn;


        public UndoBGColor(uint color, uint lastColor, int row, int column)
        {
            cellBGcolor = color;
            priorColor = lastColor;
            cellRow = row;
            cellColumn = column;
        }

        public new UndoRedoCmd Execute(Spreadsheet sheet)
        {
            Cell cell = sheet.GetCell(cellRow, cellColumn);
            uint oldColor = cell.BGColor;
            cell.BGColor = priorColor;

            return new UndoBGColor(oldColor, cellBGcolor, cellRow, cellColumn);
        }
    }

    /// <summary>
    /// represents the undo command of a cell's text
    /// </summary>
    public class UndoText : UndoRedoCmd
    {
        private string cellText, priorText;
        private int cellRow, cellColumn;

        public UndoText(string text, string lastText, int row, int column)
        {
            cellText = text;
            priorText = lastText;
            cellRow = row;
            cellColumn = column;
        }

        public new UndoRedoCmd Execute(Spreadsheet sheet)
        {
            Cell cell = sheet.GetCell(cellRow, cellColumn);
            string oldText = cell.Text;
            cell.Text = priorText;

            return new UndoText(oldText, cellText, cellRow, cellColumn);
        }
    }
}
