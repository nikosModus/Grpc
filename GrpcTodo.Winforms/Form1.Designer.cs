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
            btnToggleTheme = new Button();
            rbSearchByName = new RadioButton();
            rbSearchBySurname = new RadioButton();
            lblAgeValue = new Label();
            searchDebounceTimer = new System.Windows.Forms.Timer(components);
            btnExport = new Button();
            btnImport = new Button();
            btnLanguage = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(50, 30);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(150, 25);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 1;
            txtName.KeyPress += txtName_KeyPress;
            // 
            // lblSurname
            // 
            lblSurname.AutoSize = true;
            lblSurname.Location = new Point(50, 75);
            lblSurname.Name = "lblSurname";
            lblSurname.Size = new Size(67, 20);
            lblSurname.TabIndex = 2;
            lblSurname.Text = "Surname";
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(150, 72);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(200, 27);
            txtSurname.TabIndex = 3;
            txtSurname.KeyPress += txtSurname_KeyPress;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Location = new Point(50, 120);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(36, 20);
            lblAge.TabIndex = 4;
            lblAge.Text = "Age";
            // 
            // lblDOB
            // 
            lblDOB.AutoSize = true;
            lblDOB.Location = new Point(50, 165);
            lblDOB.Name = "lblDOB";
            lblDOB.Size = new Size(96, 20);
            lblDOB.TabIndex = 6;
            lblDOB.Text = "Date Of Birth";
            // 
            // dtpDOB
            // 
            dtpDOB.Location = new Point(150, 160);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(200, 27);
            dtpDOB.TabIndex = 7;
            dtpDOB.ValueChanged += dtpDOB_ValueChanged;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.MediumSeaGreen;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(9, 210);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(120, 35);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "Create Person";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtSearchSurname
            // 
            txtSearchSurname.Location = new Point(580, 46);
            txtSearchSurname.Name = "txtSearchSurname";
            txtSearchSurname.Size = new Size(350, 27);
            txtSearchSurname.TabIndex = 15;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.DodgerBlue;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(135, 210);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(120, 35);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Update Person";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.IndianRed;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(261, 210);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 35);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Delete Person";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnList
            // 
            btnList.Location = new Point(893, 396);
            btnList.Name = "btnList";
            btnList.Size = new Size(75, 23);
            btnList.TabIndex = 13;
            btnList.Visible = false;
            // 
            // listViewPeople
            // 
            listViewPeople.FullRowSelect = true;
            listViewPeople.Location = new Point(400, 90);
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
            label1.Location = new Point(400, 49);
            label1.Name = "label1";
            label1.Size = new Size(135, 20);
            label1.TabIndex = 14;
            label1.Text = "Search By Surname";
            // 
            // clearFields
            // 
            clearFields.BackColor = Color.Orange;
            clearFields.FlatStyle = FlatStyle.Flat;
            clearFields.ForeColor = Color.White;
            clearFields.Location = new Point(135, 260);
            clearFields.Name = "clearFields";
            clearFields.Size = new Size(120, 35);
            clearFields.TabIndex = 11;
            clearFields.Text = "Clear All Fields";
            clearFields.UseVisualStyleBackColor = false;
            clearFields.Click += clearFields_Click;
            // 
            // btnToggleTheme
            // 
            btnToggleTheme.BackColor = Color.Gray;
            btnToggleTheme.FlatStyle = FlatStyle.Flat;
            btnToggleTheme.ForeColor = Color.White;
            btnToggleTheme.Location = new Point(836, 583);
            btnToggleTheme.Name = "btnToggleTheme";
            btnToggleTheme.Size = new Size(141, 35);
            btnToggleTheme.TabIndex = 12;
            btnToggleTheme.Text = "Dark Mode";
            btnToggleTheme.UseVisualStyleBackColor = false;
            btnToggleTheme.Click += btnToggleTheme_Click;
            // 
            // rbSearchByName
            // 
            rbSearchByName.Location = new Point(580, 12);
            rbSearchByName.Name = "rbSearchByName";
            rbSearchByName.Size = new Size(164, 24);
            rbSearchByName.TabIndex = 16;
            rbSearchByName.Text = "Search by Name";
            // 
            // rbSearchBySurname
            // 
            rbSearchBySurname.Checked = true;
            rbSearchBySurname.Location = new Point(766, 12);
            rbSearchBySurname.Name = "rbSearchBySurname";
            rbSearchBySurname.Size = new Size(164, 24);
            rbSearchBySurname.TabIndex = 17;
            rbSearchBySurname.TabStop = true;
            rbSearchBySurname.Text = "Search by Surname";
            // 
            // lblAgeValue
            // 
            lblAgeValue.BorderStyle = BorderStyle.Fixed3D;
            lblAgeValue.Location = new Point(150, 117);
            lblAgeValue.Name = "lblAgeValue";
            lblAgeValue.Size = new Size(200, 27);
            lblAgeValue.TabIndex = 5;
            lblAgeValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(9, 580);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(90, 27);
            btnExport.TabIndex = 19;
            btnExport.Text = "Export As...";
            btnExport.Click += btnExport_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(125, 580);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(90, 27);
            btnImport.TabIndex = 20;
            btnImport.Text = "Import...";
            btnImport.Click += btnImport_Click;
            // 
            // btnLanguage
            // 
            btnLanguage.BackColor = Color.DarkSlateBlue;
            btnLanguage.FlatStyle = FlatStyle.Flat;
            btnLanguage.ForeColor = Color.White;
            btnLanguage.Location = new Point(689, 583);
            btnLanguage.Name = "btnLanguage";
            btnLanguage.Size = new Size(141, 35);
            btnLanguage.TabIndex = 21;
            btnLanguage.Text = "Greek";
            btnLanguage.UseVisualStyleBackColor = false;
            btnLanguage.Click += btnLanguage_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 630);
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
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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

    }
}
