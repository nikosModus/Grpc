using Grpc.Net.Client;
using People.Protos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace People.Winforms
{
    public partial class Form1 : Form
    {
        private Dictionary<long, Person> _personMap = new Dictionary<long, Person>();
        private PersonService.PersonServiceClient _client;

        private long? _currentSelectedPersonId = null;
        private bool darkMode = false;

        //for user management
        private string _role;


        // Store original colors for restore
        private Dictionary<Control, (Color BackColor, Color ForeColor)> originalColors
            = new Dictionary<Control, (Color, Color)>();

        private const int DebounceDelayMs = 300;
        //greek-english toggle
        private bool isGreek = false; // default Αγγλικά


        public Form1()
        {
            InitializeComponent();
            ApplyButtonStyles();
            SaveOriginalColors(this);

            // Subscribe to form events
            Load += Form1_Load;


            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            _client = new PersonService.PersonServiceClient(channel);

            listViewPeople.Columns.Clear();
            listViewPeople.Columns.Add("Id", 50, HorizontalAlignment.Left);
            listViewPeople.Columns.Add("Name", 120, HorizontalAlignment.Left);
            listViewPeople.Columns.Add("Surname", 120, HorizontalAlignment.Left);
            listViewPeople.Columns.Add("Age", 50, HorizontalAlignment.Center);
            listViewPeople.Columns.Add("Date of Birth", 100, HorizontalAlignment.Left);

            // Setup debounce timer for search
            searchDebounceTimer.Interval = DebounceDelayMs;
            searchDebounceTimer.Tick += SearchDebounceTimer_Tick;

            // Hook search TextChanged to debounced handler
            txtSearchSurname.TextChanged += TxtSearchSurname_TextChanged_Debounced;

            listViewPeople.SelectedIndexChanged += listViewPeople_SelectedIndexChanged;

            // Setup DateTimePicker
            dtpDOB.MinDate = new DateTime(1900, 1, 1);
            dtpDOB.MaxDate = DateTime.Today;
            dtpDOB.ValueChanged += dtpDOB_ValueChanged;

        }

        public Form1(string role) : this()  // Calls default constructor to keep all existing initialization
        {
            _role = role;

            // Set visibility of buttons based on role
            bool isAdmin = _role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
            btnCreate.Visible = isAdmin;
            btnUpdate.Visible = isAdmin;
            btnDelete.Visible = isAdmin;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Load your people list after form is ready
                await LoadPeopleAsync();

                // Set default DateTimePicker
                dtpDOB.Value = DateTime.Today;
                lblAgeValue.Text = "";

                ApplyTheme();  // Apply dark/light theme safely at runtime
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dispose of any timers or resources safely
            searchDebounceTimer?.Stop();
            searchDebounceTimer?.Dispose();
        }

        private void SaveOriginalColors(Control parent)
        {
            originalColors[parent] = (parent.BackColor, parent.ForeColor);

            foreach (Control child in parent.Controls)
            {
                SaveOriginalColors(child);
            }
        }

        private void ApplyTheme()
        {
            if (darkMode)
            {
                this.BackColor = Color.FromArgb(45, 45, 48);
                ForeColor = Color.White;
                SetControlDark(this);
                btnToggleTheme.Text = "Light Mode";
            }
            else
            {
                // Restore original colors
                foreach (var kvp in originalColors)
                {
                    kvp.Key.BackColor = kvp.Value.BackColor;
                    kvp.Key.ForeColor = kvp.Value.ForeColor;
                }
                btnToggleTheme.Text = "Dark Mode";
            }
        }

        private void SetControlDark(Control ctrl)
        {
            ctrl.BackColor = Color.FromArgb(45, 45, 48);
            ctrl.ForeColor = Color.White;

            if (ctrl is ListView lv)
            {
                lv.BackColor = Color.FromArgb(30, 30, 30);
                lv.ForeColor = Color.White;
            }

            foreach (Control child in ctrl.Controls)
            {
                SetControlDark(child);
            }
        }

        private void btnToggleTheme_Click(object sender, EventArgs e)
        {
            darkMode = !darkMode;
            ApplyTheme();
        }

        private async Task<List<Person>> LoadPeopleAsync(string? filterText = null)
        {
            try
            {
                var list = await _client.ListAsync(new People.Protos.Empty());
                listViewPeople.Items.Clear();

                IEnumerable<Person> filteredPeople = list.People;

                if (!string.IsNullOrWhiteSpace(filterText))
                {
                    filteredPeople = rbSearchByName.Checked
                        ? list.People.Where(p => p.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))
                        : list.People.Where(p => p.Surname.StartsWith(filterText, StringComparison.OrdinalIgnoreCase));
                }

                foreach (var p in filteredPeople)
                {
                    var item = new ListViewItem(p.Id.ToString());
                    item.SubItems.Add(p.Name);
                    item.SubItems.Add(p.Surname);
                    item.SubItems.Add(p.Age.ToString());
                    item.SubItems.Add(p.DateOfBirth);

                    listViewPeople.Items.Add(item);
                }

                return filteredPeople.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("List error: " + ex.Message);
                return new List<Person>();
            }
        }

        private void TxtSearchSurname_TextChanged_Debounced(object sender, EventArgs e)
        {
            searchDebounceTimer.Stop();
            searchDebounceTimer.Start();
        }

        private async void SearchDebounceTimer_Tick(object? sender, EventArgs e)
        {
            searchDebounceTimer.Stop();

            string searchText = txtSearchSurname.Text.Trim();
            var filteredPeople = await LoadPeopleAsync(searchText);

            if (_currentSelectedPersonId.HasValue)
            {
                bool isStillPresent = filteredPeople.Any(p => p.Id == _currentSelectedPersonId.Value);

                if (isStillPresent)
                    SelectPersonInListView(_currentSelectedPersonId.Value);
                else
                {
                    listViewPeople.SelectedItems.Clear();
                    ClearInputs();
                }
            }
        }

        private void SelectPersonInListView(long id)
        {
            foreach (ListViewItem item in listViewPeople.Items)
            {
                if (long.TryParse(item.Text, out long itemId) && itemId == id)
                {
                    item.Selected = true;
                    item.Focused = true;
                    item.EnsureVisible();
                    break;
                }
            }
        }

        private async void listViewPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPeople.SelectedItems.Count == 0)
                return;

            if (!long.TryParse(listViewPeople.SelectedItems[0].Text, out long selectedId))
                return;

            try
            {
                var person = await _client.ReadAsync(new Id { Id_ = selectedId });
                _currentSelectedPersonId = selectedId;

                txtName.Text = person.Name;
                txtSurname.Text = person.Surname;

                if (DateTime.TryParse(person.DateOfBirth, out DateTime dob))
                {
                    dtpDOB.Value = dob;
                    UpdateAgeLabel(dob);
                }
                else
                {
                    dtpDOB.Value = DateTime.Today;
                    lblAgeValue.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading person: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtSurname.Clear();
            dtpDOB.Value = DateTime.Today;
            lblAgeValue.Text = "";

            listViewPeople.SelectedItems.Clear();
            _currentSelectedPersonId = null;

            txtName.BackColor = SystemColors.Window;
            txtSurname.BackColor = SystemColors.Window;
            dtpDOB.CalendarMonthBackground = SystemColors.Window;
        }

        private bool ValidateInputs()
        {
            bool isValid = true;
            txtName.BackColor = SystemColors.Window;
            txtSurname.BackColor = SystemColors.Window;
            dtpDOB.CalendarMonthBackground = SystemColors.Window;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.BackColor = Color.LightCoral;
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                txtSurname.BackColor = Color.LightCoral;
                MessageBox.Show("Surname cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSurname.Focus();
                return false;
            }

            if (dtpDOB.Value.Date > DateTime.Today)
            {
                dtpDOB.CalendarMonthBackground = Color.LightCoral;
                MessageBox.Show("Date of Birth cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDOB.Focus();
                return false;
            }

            return isValid;
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            UpdateAgeLabel(dtpDOB.Value.Date);
        }

        private void UpdateAgeLabel(DateTime dob)
        {
            if (dob > DateTime.Today)
            {
                lblAgeValue.Text = "";
                return;
            }

            int age = DateTime.Today.Year - dob.Year;
            if (dob > DateTime.Today.AddYears(-age)) age--;
            lblAgeValue.Text = age < 0 ? "" : age.ToString();
        }

        // Create, Update, Delete unchanged except using lblAgeValue.Text for age
        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var list = await _client.ListAsync(new People.Protos.Empty());

                bool duplicateExists = list.People.Any(p =>
                    p.Name.Equals(txtName.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    p.Surname.Equals(txtSurname.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    p.Age.ToString() == lblAgeValue.Text &&
                    p.DateOfBirth == dtpDOB.Value.ToString("yyyy-MM-dd")
                );

                if (duplicateExists)
                {
                    MessageBox.Show("Person with the same details already exists!", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearInputs();
                    return;
                }

                var person = new Person
                {
                    Name = txtName.Text.Trim(),
                    Surname = txtSurname.Text.Trim(),
                    Age = int.Parse(lblAgeValue.Text),
                    DateOfBirth = dtpDOB.Value.ToString("yyyy-MM-dd")
                };

                var created = await _client.CreateAsync(person);
                MessageBox.Show($"Created person with ID: {created.Id}");
                await LoadPeopleAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Create error: " + ex.Message);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                if (!_currentSelectedPersonId.HasValue)
                {
                    MessageBox.Show("No person selected.");
                    return;
                }

                var id = _currentSelectedPersonId.Value;
                var name = txtName.Text.Trim();
                var surname = txtSurname.Text.Trim();
                var age = int.Parse(lblAgeValue.Text);
                var dob = dtpDOB.Value.ToString("yyyy-MM-dd");

                var list = await _client.ListAsync(new People.Protos.Empty());
                var originalPerson = list.People.FirstOrDefault(p => p.Id == id);

                if (originalPerson == null)
                {
                    MessageBox.Show("Person not found.");
                    return;
                }

                if (originalPerson.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                    originalPerson.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase) &&
                    originalPerson.Age == age &&
                    originalPerson.DateOfBirth == dob)
                {
                    MessageBox.Show("No changes were made.");
                    return;
                }

                bool duplicateExists = list.People.Any(p =>
                    p.Id != id &&
                    p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                    p.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase) &&
                    p.Age == age &&
                    p.DateOfBirth == dob
                );

                if (duplicateExists)
                {
                    MessageBox.Show("Person with the same details already exists!", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearInputs();
                    return;
                }

                var person = new Person
                {
                    Id = id,
                    Name = name,
                    Surname = surname,
                    Age = age,
                    DateOfBirth = dob
                };

                var result = await _client.UpdateAsync(person);
                MessageBox.Show(result.Message);
                await LoadPeopleAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error: " + ex.Message);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_currentSelectedPersonId.HasValue)
                {
                    MessageBox.Show("No person selected.");
                    return;
                }

                if (MessageBox.Show("Are you sure you want to delete this person?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                var id = _currentSelectedPersonId.Value;
                var result = await _client.DeleteAsync(new Id { Id_ = id });
                MessageBox.Show(result.Message);
                await LoadPeopleAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete error: " + ex.Message);
            }
        }

        private void clearFields_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            listViewPeople.Size = new Size(ClientSize.Width - 350, ClientSize.Height - 80);
            txtSearchSurname.Size = new Size(ClientSize.Width - 470, 23);

            btnCreate.Location = new Point(6, ClientSize.Height - 40);
            btnUpdate.Location = new Point(122, ClientSize.Height - 40);
            btnDelete.Location = new Point(232, ClientSize.Height - 40);
            clearFields.Location = new Point(100, ClientSize.Height - 40);
            btnToggleTheme.Location = new Point(ClientSize.Width - 87, ClientSize.Height - 40);
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys like backspace
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys like backspace
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; // Block the input
            }
        }
        private void StyleButton(Button btn, Color normalBack, Color hoverBack)
        {
            btn.BackColor = normalBack;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;

            btn.MouseEnter += (s, e) => { btn.BackColor = hoverBack; };
            btn.MouseLeave += (s, e) => { btn.BackColor = normalBack; };
        }
        private void ApplyButtonStyles()
        {
            StyleButton(btnCreate, Color.MediumSeaGreen, Color.SeaGreen);
            StyleButton(btnUpdate, Color.DodgerBlue, Color.RoyalBlue);
            StyleButton(btnDelete, Color.IndianRed, Color.Firebrick);
            StyleButton(clearFields, Color.Orange, Color.DarkOrange);
            StyleButton(btnToggleTheme, Color.Gray, Color.DimGray);
            StyleButton(btnExport, Color.MediumPurple, Color.DarkMagenta);
            StyleButton(btnImport, Color.MediumSlateBlue, Color.SlateBlue);
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (listViewPeople.Items.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv|Excel files (*.xlsx)|*.xlsx";
                sfd.Title = "Export People";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    if (sfd.FileName.EndsWith(".csv"))
                    {
                        await ExportToCsvAsync(sfd.FileName);
                    }
                    else if (sfd.FileName.EndsWith(".xlsx"))
                    {
                        ExportToExcel(sfd.FileName);
                    }

                    MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task ExportToCsvAsync(string filePath)
        {
            var lines = new List<string>();
            // Header
            lines.Add("Id,Name,Surname,Age,DateOfBirth");

            // Rows
            foreach (ListViewItem item in listViewPeople.Items)
            {
                string[] values = item.SubItems.Cast<ListViewItem.ListViewSubItem>()
                                    .Select(s => s.Text.Contains(",") ? $"\"{s.Text}\"" : s.Text)
                                    .ToArray();
                lines.Add(string.Join(",", values));
            }

            await File.WriteAllLinesAsync(filePath, lines);
        }

        private void ExportToExcel(string filePath)
        {
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("People");

                // Header
                for (int c = 0; c < listViewPeople.Columns.Count; c++)
                {
                    ws.Cell(1, c + 1).Value = listViewPeople.Columns[c].Text;
                }

                // Rows
                for (int r = 0; r < listViewPeople.Items.Count; r++)
                {
                    var item = listViewPeople.Items[r];
                    for (int c = 0; c < item.SubItems.Count; c++)
                    {
                        ws.Cell(r + 2, c + 1).Value = item.SubItems[c].Text;
                    }
                }

                workbook.SaveAs(filePath);
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV files (*.csv)|*.csv|Excel files (*.xlsx)|*.xlsx";
                ofd.Title = "Import People";

                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    List<Person> peopleToImport = new List<Person>();

                    if (ofd.FileName.EndsWith(".csv"))
                        peopleToImport = await ReadCsvAsync(ofd.FileName);
                    else if (ofd.FileName.EndsWith(".xlsx"))
                        peopleToImport = ReadExcel(ofd.FileName);

                    var existingPeople = await _client.ListAsync(new People.Protos.Empty());

                    var duplicates = peopleToImport.Where(p =>
                        existingPeople.People.Any(x =>
                            x.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase) &&
                            x.Surname.Equals(p.Surname, StringComparison.OrdinalIgnoreCase) &&
                            x.Age == p.Age &&
                            x.DateOfBirth == p.DateOfBirth)).ToList();

                    var toAdd = peopleToImport.Except(duplicates).ToList();

                    if (toAdd.Count == 0)
                    {
                        MessageBox.Show("No new people to add. All rows are duplicates.");
                        return;
                    }

                    var preview = new ImportPreviewForm(toAdd, duplicates);
                    if (preview.ShowDialog() != DialogResult.OK) return;

                    int importedCount = 0;
                    foreach (var p in toAdd)
                    {
                        await _client.CreateAsync(p);
                        importedCount++;
                    }

                    await LoadPeopleAsync();
                    MessageBox.Show($"Import finished. Imported: {importedCount}, Duplicates skipped: {duplicates.Count}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import failed: " + ex.Message);
                }
            }
        }

        private async Task<List<Person>> ReadCsvAsync(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            var people = new List<Person>();

            for (int i = 1; i < lines.Length; i++) // skip header
            {
                var columns = lines[i].Split(',');

                if (columns.Length < 5) continue; // invalid row

                if (!int.TryParse(columns[3], out int age)) continue;
                if (!DateTime.TryParse(columns[4], out DateTime dob)) continue;

                people.Add(new Person
                {
                    Name = columns[1].Trim(),
                    Surname = columns[2].Trim(),
                    Age = age,
                    DateOfBirth = dob.ToString("yyyy-MM-dd")
                });
            }

            return people;
        }
        private List<Person> ReadExcel(string filePath)
        {
            var people = new List<Person>();
            using (var workbook = new ClosedXML.Excel.XLWorkbook(filePath))
            {
                var ws = workbook.Worksheets.First();

                foreach (var row in ws.RowsUsed().Skip(1)) // skip header
                {
                    string name = row.Cell(2).GetString().Trim();
                    string surname = row.Cell(3).GetString().Trim();

                    if (!int.TryParse(row.Cell(4).GetString(), out int age)) continue;
                    if (!DateTime.TryParse(row.Cell(5).GetString(), out DateTime dob)) continue;

                    people.Add(new Person
                    {
                        Name = name,
                        Surname = surname,
                        Age = age,
                        DateOfBirth = dob.ToString("yyyy-MM-dd")
                    });
                }
            }
            return people;
        }

        private void btnLanguage_Click(object sender, EventArgs e)
        {
            isGreek = !isGreek;
            var t = isGreek ? Translations.Greek : Translations.English;

            lblName.Text = t["Name"];
            lblSurname.Text = t["Surname"];
            lblAge.Text = t["Age"];
            lblDOB.Text = t["DOB"];
            btnCreate.Text = t["Create"];
            btnUpdate.Text = t["Update"];
            btnDelete.Text = t["Delete"];
            clearFields.Text = t["Clear"];
            btnToggleTheme.Text = t["DarkMode"];
            btnLanguage.Text = t["SwitchLanguage"];
            label1.Text = t["SearchLabel"];
            rbSearchByName.Text = t["SearchByName"];
            rbSearchBySurname.Text = t["SearchBySurname"];
            btnExport.Text = t["Export"];
            btnImport.Text = t["Import"];
        }
    }
}
