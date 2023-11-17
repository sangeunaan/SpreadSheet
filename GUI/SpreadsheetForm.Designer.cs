using System;
using System.Data;


namespace GUI
{
    partial class SpreadsheetForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            NewButton = new Button();
            SaveButton = new Button();
            OpenButton = new Button();
            CalculateButton = new Button();
            CloseButton = new Button();
            CellNameTextbox = new TextBox();
            CellContentsTextbox = new TextBox();
            CellValueTextbox = new TextBox();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column13 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column16 = new DataGridViewTextBoxColumn();
            Column17 = new DataGridViewTextBoxColumn();
            Column18 = new DataGridViewTextBoxColumn();
            Column19 = new DataGridViewTextBoxColumn();
            Column20 = new DataGridViewTextBoxColumn();
            Column21 = new DataGridViewTextBoxColumn();
            Column22 = new DataGridViewTextBoxColumn();
            Column23 = new DataGridViewTextBoxColumn();
            Column24 = new DataGridViewTextBoxColumn();
            Column25 = new DataGridViewTextBoxColumn();
            Column26 = new DataGridViewTextBoxColumn();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // NewButton
            // 
            NewButton.Location = new Point(12, 12);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(112, 34);
            NewButton.TabIndex = 0;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;
            NewButton.Click += NewButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(130, 12);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(112, 34);
            SaveButton.TabIndex = 1;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // OpenButton
            // 
            OpenButton.Location = new Point(248, 12);
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(112, 34);
            OpenButton.TabIndex = 2;
            OpenButton.Text = "Open";
            OpenButton.UseVisualStyleBackColor = true;
            OpenButton.Click += OpenButton_Click;
            // 
            // CalculateButton
            // 
            CalculateButton.Location = new Point(12, 104);
            CalculateButton.Name = "CalculateButton";
            CalculateButton.Size = new Size(150, 34);
            CalculateButton.TabIndex = 3;
            CalculateButton.Text = "Calculate";
            CalculateButton.UseVisualStyleBackColor = true;
            CalculateButton.Click += CalculateButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.BackColor = Color.IndianRed;
            CloseButton.ForeColor = SystemColors.ButtonFace;
            CloseButton.Location = new Point(1068, 12);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(50, 34);
            CloseButton.TabIndex = 4;
            CloseButton.Text = "X";
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            // 
            // CellNameTextbox
            // 
            CellNameTextbox.Location = new Point(12, 67);
            CellNameTextbox.Name = "CellNameTextbox";
            CellNameTextbox.Size = new Size(150, 31);
            CellNameTextbox.TabIndex = 5;
            CellNameTextbox.TextChanged += CellNameTextbox_TextChanged;
            // 
            // CellContentsTextbox
            // 
            CellContentsTextbox.Location = new Point(168, 67);
            CellContentsTextbox.Name = "CellContentsTextbox";
            CellContentsTextbox.Size = new Size(511, 31);
            CellContentsTextbox.TabIndex = 6;
            CellContentsTextbox.TextChanged += CellContentsTextbox_TextChanged;
            // 
            // CellValueTextbox
            // 
            CellValueTextbox.Location = new Point(168, 107);
            CellValueTextbox.Name = "CellValueTextbox";
            CellValueTextbox.Size = new Size(150, 31);
            CellValueTextbox.TabIndex = 7;
            CellValueTextbox.TextChanged += CellValueTextbox_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9, Column10, Column11, Column12, Column13, Column14, Column15, Column16, Column17, Column18, Column19, Column20, Column21, Column22, Column23, Column24, Column25, Column26 });
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 33;
            dataGridView1.Size = new Size(1106, 537);
            dataGridView1.TabIndex = 8;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "A";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.Width = 150;
            // 
            // Column2
            // 
            Column2.HeaderText = "B";
            Column2.MinimumWidth = 8;
            Column2.Name = "Column2";
            Column2.Width = 150;
            // 
            // Column3
            // 
            Column3.HeaderText = "C";
            Column3.MinimumWidth = 8;
            Column3.Name = "Column3";
            Column3.Width = 150;
            // 
            // Column4
            // 
            Column4.HeaderText = "D";
            Column4.MinimumWidth = 8;
            Column4.Name = "Column4";
            Column4.Width = 150;
            // 
            // Column5
            // 
            Column5.HeaderText = "E";
            Column5.MinimumWidth = 8;
            Column5.Name = "Column5";
            Column5.Width = 150;
            // 
            // Column6
            // 
            Column6.HeaderText = "F";
            Column6.MinimumWidth = 8;
            Column6.Name = "Column6";
            Column6.Width = 150;
            // 
            // Column7
            // 
            Column7.HeaderText = "G";
            Column7.MinimumWidth = 8;
            Column7.Name = "Column7";
            Column7.Width = 150;
            // 
            // Column8
            // 
            Column8.HeaderText = "H";
            Column8.MinimumWidth = 8;
            Column8.Name = "Column8";
            Column8.Width = 150;
            // 
            // Column9
            // 
            Column9.HeaderText = "I";
            Column9.MinimumWidth = 8;
            Column9.Name = "Column9";
            Column9.Width = 150;
            // 
            // Column10
            // 
            Column10.HeaderText = "J";
            Column10.MinimumWidth = 8;
            Column10.Name = "Column10";
            Column10.Width = 150;
            // 
            // Column11
            // 
            Column11.HeaderText = "K";
            Column11.MinimumWidth = 8;
            Column11.Name = "Column11";
            Column11.Width = 150;
            // 
            // Column12
            // 
            Column12.HeaderText = "L";
            Column12.MinimumWidth = 8;
            Column12.Name = "Column12";
            Column12.Width = 150;
            // 
            // Column13
            // 
            Column13.HeaderText = "M";
            Column13.MinimumWidth = 8;
            Column13.Name = "Column13";
            Column13.Width = 150;
            // 
            // Column14
            // 
            Column14.HeaderText = "N";
            Column14.MinimumWidth = 8;
            Column14.Name = "Column14";
            Column14.Width = 150;
            // 
            // Column15
            // 
            Column15.HeaderText = "O";
            Column15.MinimumWidth = 8;
            Column15.Name = "Column15";
            Column15.Width = 150;
            // 
            // Column16
            // 
            Column16.HeaderText = "P";
            Column16.MinimumWidth = 8;
            Column16.Name = "Column16";
            Column16.Width = 150;
            // 
            // Column17
            // 
            Column17.HeaderText = "Q";
            Column17.MinimumWidth = 8;
            Column17.Name = "Column17";
            Column17.Width = 150;
            // 
            // Column18
            // 
            Column18.HeaderText = "R";
            Column18.MinimumWidth = 8;
            Column18.Name = "Column18";
            Column18.Width = 150;
            // 
            // Column19
            // 
            Column19.HeaderText = "S";
            Column19.MinimumWidth = 8;
            Column19.Name = "Column19";
            Column19.Width = 150;
            // 
            // Column20
            // 
            Column20.HeaderText = "T";
            Column20.MinimumWidth = 8;
            Column20.Name = "Column20";
            Column20.Width = 150;
            // 
            // Column21
            // 
            Column21.HeaderText = "U";
            Column21.MinimumWidth = 8;
            Column21.Name = "Column21";
            Column21.Width = 150;
            // 
            // Column22
            // 
            Column22.HeaderText = "V";
            Column22.MinimumWidth = 8;
            Column22.Name = "Column22";
            Column22.Width = 150;
            // 
            // Column23
            // 
            Column23.HeaderText = "W";
            Column23.MinimumWidth = 8;
            Column23.Name = "Column23";
            Column23.Width = 150;
            // 
            // Column24
            // 
            Column24.HeaderText = "X";
            Column24.MinimumWidth = 8;
            Column24.Name = "Column24";
            Column24.Width = 150;
            // 
            // Column25
            // 
            Column25.HeaderText = "Y";
            Column25.MinimumWidth = 8;
            Column25.Name = "Column25";
            Column25.Width = 150;
            // 
            // Column26
            // 
            Column26.HeaderText = "Z";
            Column26.MinimumWidth = 8;
            Column26.Name = "Column26";
            Column26.Width = 150;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(0, 161);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1134, 428);
            tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1126, 390);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1126, 390);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // SpreadsheetForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1130, 713);
            Controls.Add(tabControl1);
            Controls.Add(CellValueTextbox);
            Controls.Add(CellContentsTextbox);
            Controls.Add(CellNameTextbox);
            Controls.Add(CloseButton);
            Controls.Add(CalculateButton);
            Controls.Add(OpenButton);
            Controls.Add(SaveButton);
            Controls.Add(NewButton);
            Name = "SpreadsheetForm";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();




        }

        #endregion

        private Button NewButton;
        private Button SaveButton;
        private Button OpenButton;
        private Button CalculateButton;
        private Button CloseButton;
        private TextBox CellNameTextbox;
        private TextBox CellContentsTextbox;
        private TextBox CellValueTextbox;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column19;
        private DataGridViewTextBoxColumn Column20;
        private DataGridViewTextBoxColumn Column21;
        private DataGridViewTextBoxColumn Column22;
        private DataGridViewTextBoxColumn Column23;
        private DataGridViewTextBoxColumn Column24;
        private DataGridViewTextBoxColumn Column25;
        private DataGridViewTextBoxColumn Column26;

        DataTable dataTable1 = new DataTable();
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}