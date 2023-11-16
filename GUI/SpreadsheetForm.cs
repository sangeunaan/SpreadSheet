using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SpreadsheetUtilities;
using static FormulaEvaluator.Evaluator;

namespace GUI
{
    public partial class SpreadsheetForm : Form
    {
        //spreadsheet object
        private Spreadsheet spreadsheet;

        //the name of the file that will be saved
        private String fileName;

        public Func<string, bool> IsValid { get; protected set; }

        private Boolean IsValidName(String name)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z]+[\d]+$") && IsValid(name))
                return true;
            else return false;
        }

        public void SelectedCell(out int column, out int row)
        {
            column = 
            row = 
        }

        private String GetCellName()
        {
            //  column: 
            int column, row;

            dataGridView1.SelectedCell(out column, out row);
            // for rows we want to start a 1 instead of 0 so add 1 to each term
            int cellRow = ++row;
            // since 'A' = 01000001 in binary, and col counts from 0 (0000), 1 (0001), 2 (0010)...
            // then if we keep adding the next number to A the binary addition takes us from A-Z
            char cellCol = (char)('A' + column);
            return "" + cellCol + cellRow;
        }

        public SpreadsheetForm()
        {
            InitializeComponent();
            spreadsheet = new Spreadsheet(IsValidName, s => s.ToUpper(), "SpreadSheet");
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to exit the program?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        //  If you click the Save button after when the spreadsheet is changed,
        //  save the changed version
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (spreadsheet.Changed)
            {
                spreadsheet.Save(fileName);
            }
        }

        private void CellNameTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CellContentsTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            CellValueTextbox.Text = Convert.ToString(Formula.Evaluate(CellContentsTextbox.Text()));
        }

        private void CellValueTextbox_TextChanged(object sender, EventArgs e)
        {
            CellValueTextbox.Text = spreadsheet.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}