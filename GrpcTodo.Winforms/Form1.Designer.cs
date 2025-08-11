namespace People.Winforms
{
    partial class Form1
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
            numAge = new ComboBox();
            listViewPeople = new ListView();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(63, 24);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(118, 21);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 23);
            txtName.TabIndex = 1;
            // 
            // lblSurname
            // 
            lblSurname.AutoSize = true;
            lblSurname.Location = new Point(63, 70);
            lblSurname.Name = "lblSurname";
            lblSurname.Size = new Size(54, 15);
            lblSurname.TabIndex = 2;
            lblSurname.Text = "Surname";
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(118, 67);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(100, 23);
            txtSurname.TabIndex = 3;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Location = new Point(63, 126);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(28, 15);
            lblAge.TabIndex = 4;
            lblAge.Text = "Age";
            // 
            // lblDOB
            // 
            lblDOB.AutoSize = true;
            lblDOB.Location = new Point(27, 183);
            lblDOB.Name = "lblDOB";
            lblDOB.Size = new Size(75, 15);
            lblDOB.TabIndex = 6;
            lblDOB.Text = "Date Of BIrth";
            // 
            // dtpDOB
            // 
            dtpDOB.AllowDrop = true;
            dtpDOB.Location = new Point(118, 177);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(210, 23);
            dtpDOB.TabIndex = 7;
            dtpDOB.ValueChanged += dtpDOB_ValueChanged;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(6, 247);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(96, 23);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "Create Person";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtSearchSurname
            // 
            txtSearchSurname.Location = new Point(429, 29);
            txtSearchSurname.Name = "txtSearchSurname";
            txtSearchSurname.Size = new Size(283, 23);
            txtSearchSurname.TabIndex = 10;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(108, 247);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(96, 23);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "Update Person";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(210, 247);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(96, 23);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Delete Person";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnList
            // 
            btnList.Location = new Point(523, 288);
            btnList.Name = "btnList";
            btnList.Size = new Size(96, 23);
            btnList.TabIndex = 14;
            btnList.Text = "Load List";
            btnList.UseVisualStyleBackColor = true;
            btnList.Visible = false;
            btnList.Click += btnList_Click;
            // 
            // numAge
            // 
            numAge.FormattingEnabled = true;
            numAge.Location = new Point(118, 126);
            numAge.Name = "numAge";
            numAge.Size = new Size(100, 23);
            numAge.TabIndex = 15;
            // 
            // listViewPeople
            // 
            listViewPeople.FullRowSelect = true;
            listViewPeople.Location = new Point(334, 58);
            listViewPeople.MultiSelect = false;
            listViewPeople.Name = "listViewPeople";
            listViewPeople.Size = new Size(464, 224);
            listViewPeople.TabIndex = 0;
            listViewPeople.UseCompatibleStateImageBehavior = false;
            listViewPeople.View = View.Details;
            listViewPeople.SelectedIndexChanged += listViewPeople_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listViewPeople);
            Controls.Add(numAge);
            Controls.Add(btnList);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(txtSearchSurname);
            Controls.Add(btnCreate);
            Controls.Add(dtpDOB);
            Controls.Add(lblDOB);
            Controls.Add(lblAge);
            Controls.Add(txtSurname);
            Controls.Add(lblSurname);
            Controls.Add(txtName);
            Controls.Add(lblName);
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
        private Label lblDOB;
        private DateTimePicker dtpDOB;
        private Button btnCreate;
        private TextBox txtSearchSurname;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnList;
        private ComboBox numAge;
        private ListView listViewPeople;
    }
}