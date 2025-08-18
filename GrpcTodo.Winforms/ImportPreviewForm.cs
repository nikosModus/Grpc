using People.Protos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace People.Winforms
{
    public partial class ImportPreviewForm : Form
    {
        public bool Confirmed { get; private set; } = false;

        public ImportPreviewForm(List<Person> toAdd, List<Person> duplicates)
        {
            InitializeComponent();

            Text = "Import Preview";
            Size = new Size(600, 400);

            var lv = new ListView();
            lv.Dock = DockStyle.Fill;
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.Columns.Add("Name", 150);
            lv.Columns.Add("Surname", 150);
            lv.Columns.Add("Age", 50);
            lv.Columns.Add("DOB", 100);

            foreach (var p in toAdd)
            {
                var item = new ListViewItem(p.Name);
                item.SubItems.Add(p.Surname);
                item.SubItems.Add(p.Age.ToString());
                item.SubItems.Add(p.DateOfBirth);
                lv.Items.Add(item);
            }

            Controls.Add(lv);

            var lbl = new Label();
            lbl.Text = $"Rows to add: {toAdd.Count}, Duplicates skipped: {duplicates.Count}";
            lbl.Dock = DockStyle.Top;
            lbl.Height = 30;
            Controls.Add(lbl);

            var btnPanel = new Panel();
            btnPanel.Dock = DockStyle.Bottom;
            btnPanel.Height = 40;

            var btnConfirm = new Button();
            btnConfirm.Text = "Confirm";
            btnConfirm.DialogResult = DialogResult.OK;
            btnConfirm.Dock = DockStyle.Left;
            btnPanel.Controls.Add(btnConfirm);

            var btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Dock = DockStyle.Right;
            btnPanel.Controls.Add(btnCancel);

            Controls.Add(btnPanel);
        }
    }

}
