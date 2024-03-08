namespace AutoApplyTool
{
    partial class AutoApplyForm
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label2 = new Label();
            loginCustomerIdTextBox = new TextBox();
            button2 = new Button();
            button1 = new Button();
            groupBox2 = new GroupBox();
            cidInputTextBox = new TextBox();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            checkedListBox1 = new CheckedListBox();
            label1 = new Label();
            tabPage2 = new TabPage();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(884, 453);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(loginCustomerIdTextBox);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(876, 420);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Kontenauswahl";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 15);
            label2.Name = "label2";
            label2.Size = new Size(132, 20);
            label2.TabIndex = 6;
            label2.Text = "Login Customer ID";
            // 
            // loginCustomerIdTextBox
            // 
            loginCustomerIdTextBox.Location = new Point(157, 12);
            loginCustomerIdTextBox.Name = "loginCustomerIdTextBox";
            loginCustomerIdTextBox.Size = new Size(168, 27);
            loginCustomerIdTextBox.TabIndex = 2;
            // 
            // button2
            // 
            button2.Location = new Point(6, 379);
            button2.Name = "button2";
            button2.Size = new Size(319, 29);
            button2.TabIndex = 5;
            button2.Text = "Liste laden";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(331, 379);
            button1.Name = "button1";
            button1.Size = new Size(539, 29);
            button1.TabIndex = 4;
            button1.Text = "Starten";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cidInputTextBox);
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Location = new Point(12, 41);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(313, 329);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "CIDs der Accounts";
            // 
            // cidInputTextBox
            // 
            cidInputTextBox.Location = new Point(7, 39);
            cidInputTextBox.Multiline = true;
            cidInputTextBox.Name = "cidInputTextBox";
            cidInputTextBox.Size = new Size(300, 273);
            cidInputTextBox.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.Enabled = false;
            dataGridView1.Location = new Point(7, 26);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(300, 286);
            dataGridView1.TabIndex = 0;
            dataGridView1.Visible = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkedListBox1);
            groupBox1.Location = new Point(331, 15);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(539, 355);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Auto Apply Settings";
            // 
            // checkedListBox1
            // 
            checkedListBox1.Enabled = false;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "DISPLAY_EXPANSION_OPT_IN", "KEYWORD", "USE_BROAD_MATCH_KEYWORD", "MAXIMIZE_CLICKS_OPT_IN", "MAXIMIZE_CONVERSIONS_OPT_IN", "MAXIMIZE_CONVERSION_VALUE_OPT_IN", "OPTIMIZE_AD_ROTATION", "RAISE_TARGET_CPA", "LOWER_TARGET_ROAS", "SET_TARGET_CPA", "SET_TARGET_ROAS", "SEARCH_PARTNERS_OPT_IN", "TARGET_CPA_OPT_IN", "TARGET_ROAS_OPT_IN", "RESPONSIVE_SEARCH_AD", "RESPONSIVE_SEARCH_AD_IMPROVE_AD_STRENGTH" });
            checkedListBox1.Location = new Point(6, 26);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(527, 312);
            checkedListBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 15);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 1;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(876, 420);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Status";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // AutoApplyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(908, 477);
            Controls.Add(tabControl1);
            Name = "AutoApplyForm";
            Text = "AutoApplyTool";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label1;
        private Button button1;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private CheckedListBox checkedListBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button button2;
        private DataGridView dataGridView1;
        private TextBox cidInputTextBox;
        private Label label2;
        private TextBox loginCustomerIdTextBox;
    }
}
