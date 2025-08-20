namespace People.Winforms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                searchDebounceTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblName = new Label();
            txtName = new TextBox();
            lblSurname = new Label();
            txtSurname = new TextBox();
            lblAge = new Label();
            lblDOB = new Label();
            dtpDOB = new DateTimePicker();
            btnCreate = new Button();
            txtSearchSurname = new TextBox();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnList = new Button();
            listViewPeople = new ListView();
            label1 = new Label();
            clearFields = new Button();
            btnUndo = new Button();
            btnRedo = new Button();
            btnToggleTheme = new Button();
            rbSearchByName = new RadioButton();
            rbSearchBySurname = new RadioButton();
            lblAgeValue = new Label();
            searchDebounceTimer = new System.Windows.Forms.Timer(components);
            btnExport = new Button();
            btnImport = new Button();
            btnLanguage = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(50, 38);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(150, 33);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 2;
            txtName.KeyPress += txtName_KeyPress;
            // 
            // lblSurname
            // 
            lblSurname.AutoSize = true;
            lblSurname.Location = new Point(50, 83);
            lblSurname.Name = "lblSurname";
            lblSurname.Size = new Size(67, 20);
            lblSurname.TabIndex = 3;
            lblSurname.Text = "Surname";
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(150, 80);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(200, 27);
            txtSurname.TabIndex = 4;
            txtSurname.KeyPress += txtSurname_KeyPress;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Location = new Point(50, 128);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(36, 20);
            lblAge.TabIndex = 5;
            lblAge.Text = "Age";
            // 
            // lblDOB
            // 
            lblDOB.AutoSize = true;
            lblDOB.Location = new Point(50, 173);
            lblDOB.Name = "lblDOB";
            lblDOB.Size = new Size(96, 20);
            lblDOB.TabIndex = 7;
            lblDOB.Text = "Date Of Birth";
            // 
            // dtpDOB
            // 
            dtpDOB.Location = new Point(150, 168);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(200, 27);
            dtpDOB.TabIndex = 8;
            dtpDOB.ValueChanged += dtpDOB_ValueChanged;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.MediumSeaGreen;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(9, 218);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(120, 35);
            btnCreate.TabIndex = 9;
            btnCreate.Text = "Create Person";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtSearchSurname
            // 
            txtSearchSurname.Location = new Point(579, 68);
            txtSearchSurname.Name = "txtSearchSurname";
            txtSearchSurname.Size = new Size(350, 27);
            txtSearchSurname.TabIndex = 20;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.DodgerBlue;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(135, 218);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(120, 35);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Update Person";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.IndianRed;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(261, 218);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 35);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Delete Person";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnList
            // 
            btnList.Location = new Point(893, 396);
            btnList.Name = "btnList";
            btnList.Size = new Size(75, 23);
            btnList.TabIndex = 24;
            btnList.Visible = false;
            // 
            // listViewPeople
            // 
            listViewPeople.FullRowSelect = true;
            listViewPeople.Location = new Point(399, 112);
            listViewPeople.MultiSelect = false;
            listViewPeople.Name = "listViewPeople";
            listViewPeople.Size = new Size(530, 300);
            listViewPeople.TabIndex = 18;
            listViewPeople.UseCompatibleStateImageBehavior = false;
            listViewPeople.View = View.Details;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(399, 71);
            label1.Name = "label1";
            label1.Size = new Size(135, 20);
            label1.TabIndex = 19;
            label1.Text = "Search By Surname";
            // 
            // clearFields
            // 
            clearFields.BackColor = Color.Orange;
            clearFields.FlatStyle = FlatStyle.Flat;
            clearFields.ForeColor = Color.White;
            clearFields.Location = new Point(135, 268);
            clearFields.Name = "clearFields";
            clearFields.Size = new Size(120, 35);
            clearFields.TabIndex = 12;
            clearFields.Text = "Clear All Fields";
            clearFields.UseVisualStyleBackColor = false;
            clearFields.Click += clearFields_Click;
            // 
            // btnUndo
            // 
            btnUndo.Location = new Point(0, 0);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(75, 23);
            btnUndo.TabIndex = 25;
            // 
            // btnRedo
            // 
            btnRedo.Location = new Point(0, 0);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(75, 23);
            btnRedo.TabIndex = 26;
            // 
            // btnToggleTheme
            // 
            btnToggleTheme.BackColor = Color.Gray;
            btnToggleTheme.FlatStyle = FlatStyle.Flat;
            btnToggleTheme.ForeColor = Color.White;
            btnToggleTheme.Location = new Point(836, 583);
            btnToggleTheme.Name = "btnToggleTheme";
            btnToggleTheme.Size = new Size(141, 35);
            btnToggleTheme.TabIndex = 15;
            btnToggleTheme.Text = "Dark Mode";
            btnToggleTheme.UseVisualStyleBackColor = false;
            btnToggleTheme.Click += btnToggleTheme_Click;
            // 
            // rbSearchByName
            // 
            rbSearchByName.Location = new Point(579, 34);
            rbSearchByName.Name = "rbSearchByName";
            rbSearchByName.Size = new Size(164, 24);
            rbSearchByName.TabIndex = 16;
            rbSearchByName.Text = "Search by Name";
            // 
            // rbSearchBySurname
            // 
            rbSearchBySurname.Checked = true;
            rbSearchBySurname.Location = new Point(765, 34);
            rbSearchBySurname.Name = "rbSearchBySurname";
            rbSearchBySurname.Size = new Size(164, 24);
            rbSearchBySurname.TabIndex = 17;
            rbSearchBySurname.TabStop = true;
            rbSearchBySurname.Text = "Search by Surname";
            // 
            // lblAgeValue
            // 
            lblAgeValue.BorderStyle = BorderStyle.Fixed3D;
            lblAgeValue.Location = new Point(150, 125);
            lblAgeValue.Name = "lblAgeValue";
            lblAgeValue.Size = new Size(200, 27);
            lblAgeValue.TabIndex = 6;
            lblAgeValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(0, 0);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(75, 23);
            btnExport.TabIndex = 2;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(0, 0);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(75, 23);
            btnImport.TabIndex = 1;
            // 
            // btnLanguage
            // 
            btnLanguage.BackColor = Color.DarkSlateBlue;
            btnLanguage.FlatStyle = FlatStyle.Flat;
            btnLanguage.ForeColor = Color.White;
            btnLanguage.Location = new Point(689, 583);
            btnLanguage.Name = "btnLanguage";
            btnLanguage.Size = new Size(141, 35);
            btnLanguage.TabIndex = 23;
            btnLanguage.Text = "Greek";
            btnLanguage.UseVisualStyleBackColor = false;
            btnLanguage.Click += btnLanguage_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(980, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, undoToolStripMenuItem, redoToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(137, 26);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += btnImport_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(137, 26);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += btnExport_Click;

            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(137, 26);
            undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(137, 26);
            redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 630);
            Controls.Add(menuStrip1);
            Controls.Add(btnImport);
            Controls.Add(btnExport);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblSurname);
            Controls.Add(txtSurname);
            Controls.Add(lblAge);
            Controls.Add(lblAgeValue);
            Controls.Add(lblDOB);
            Controls.Add(dtpDOB);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(clearFields);
            Controls.Add(btnToggleTheme);
            Controls.Add(btnList);
            Controls.Add(label1);
            Controls.Add(txtSearchSurname);
            Controls.Add(rbSearchByName);
            Controls.Add(rbSearchBySurname);
            Controls.Add(listViewPeople);
            Controls.Add(btnLanguage);
            Controls.Add(btnUndo);
            Controls.Add(btnRedo);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // --- ΌΛΑ τα controls σου ---
        private Label lblName;
        private TextBox txtName;
        private Label lblSurname;
        private TextBox txtSurname;
        private Label lblAge;
        private Label lblAgeValue;
        private Label lblDOB;
        private DateTimePicker dtpDOB;
        private Button btnCreate;
        private Button btnUpdate;
        private Button btnDelete;
        private Button clearFields;
        private Button btnToggleTheme;
        private Button btnList;
        private Button btnExport;
        private Button btnImport;
        private TextBox txtSearchSurname;
        private Label label1;
        private RadioButton rbSearchByName;
        private RadioButton rbSearchBySurname;
        private ListView listViewPeople;
        private System.Windows.Forms.Timer searchDebounceTimer;
        private Button btnLanguage;
        private Button btnUndo;
        private Button btnRedo;

        // --- Νέα MenuStrip ---
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
    }
}
