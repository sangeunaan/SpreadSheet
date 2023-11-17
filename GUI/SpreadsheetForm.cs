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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class SpreadsheetForm : Form
    {
        //spreadsheet object
        private Spreadsheet spreadsheet;

        //the name of the file that will be saved
        private String fileName;
        private String cellName;

        private Boolean IsValidName(String name)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z]+[\d]+$"))
                return true;
            else return false;
        }



        public SpreadsheetForm()
        {
            InitializeComponent();
            spreadsheet = new Spreadsheet(IsValidName, s => s.ToUpper(), "SpreadSheet");
        }

        private void dataGridViewNormal_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All);
            e.PaintHeader(DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.Focus | DataGridViewPaintParts.SelectionBackground);
            e.Handled = true;

            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
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
            cellName = CellNameTextbox.Text;
        }

        private void CellContentsTextbox_TextChanged(object sender, EventArgs e)
        {
            //spreadsheet.GetCellContents(cellName);
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            CellValueTextbox.Text = Convert.ToString(spreadsheet.GetCellValue(CellNameTextbox.Text));
        }

        private void CellValueTextbox_TextChanged(object sender, EventArgs e)
        {

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //CellNameTextbox.Text = 
            CellContentsTextbox.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }
    }
}