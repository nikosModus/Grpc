using Grpc.Net.Client;
using People.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace People.Winforms
{
    public partial class Form1 : Form
    {
        private Dictionary<long, Person> _personMap = new Dictionary<long, Person>();
        private PersonService.PersonServiceClient _client;

        // To keep track of the currently selected person's Id
        private long? _currentSelectedPersonId = null;

        public Form1()
        {
            InitializeComponent();

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            _client = new PersonService.PersonServiceClient(channel);

            listViewPeople.Columns.Clear();
            listViewPeople.Columns.Add("Id", 50, HorizontalAlignment.Left);
            listViewPeople.Columns.Add("Name", 120, HorizontalAlignment.Left);
            listViewPeople.Columns.Add("Surname", 120, HorizontalAlignment.Left);
            listViewPeople.Columns.Add("Age", 50, HorizontalAlignment.Center);
            listViewPeople.Columns.Add("Date of Birth", 100, HorizontalAlignment.Left);

            // Attach event handler to the existing TextBox from Designer
            txtSearchSurname.TextChanged += txtSearchSurname_TextChanged;
            listViewPeople.SelectedIndexChanged += listViewPeople_SelectedIndexChanged;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            numAge.Items.AddRange(Enumerable.Range(1, 100).Cast<object>().ToArray());
            numAge.SelectedIndex = 0;

            await LoadPeopleAsync(); // Load all people initially
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtSurname.Clear();
            numAge.SelectedIndex = 0;
            dtpDOB.Value = DateTime.Today;
            // Don't clear txtSearchSurname here as user might want to keep search text
            listViewPeople.SelectedItems.Clear();
            _currentSelectedPersonId = null;
        }

        private async Task<List<Person>> LoadPeopleAsync(string? surnameFilter = null)
        {
            try
            {
                var list = await _client.ListAsync(new People.Protos.Empty());

                listViewPeople.Items.Clear();

                var filteredPeople = string.IsNullOrWhiteSpace(surnameFilter)
                    ? list.People.ToList()
                    : list.People.Where(p => p.Surname.StartsWith(surnameFilter, StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (var p in filteredPeople)
                {
                    var item = new ListViewItem(p.Id.ToString());
                    item.SubItems.Add(p.Name);
                    item.SubItems.Add(p.Surname);
                    item.SubItems.Add(p.Age.ToString());
                    item.SubItems.Add(p.DateOfBirth);

                    listViewPeople.Items.Add(item);
                }

                return filteredPeople;
            }
            catch (Exception ex)
            {
                MessageBox.Show("List error: " + ex.Message);
                return new List<Person>();
            }
        }

        private async void btnList_Click(object sender, EventArgs e)
        {
            await LoadPeopleAsync();
        }

        private async void txtSearchSurname_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearchSurname.Text.Trim();

            var filteredPeople = await LoadPeopleAsync(searchText);

            if (_currentSelectedPersonId.HasValue)
            {
                bool isStillPresent = filteredPeople.Any(p => p.Id == _currentSelectedPersonId.Value);

                if (isStillPresent)
                {
                    SelectPersonInListView(_currentSelectedPersonId.Value);
                }
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

            var selectedIdText = listViewPeople.SelectedItems[0].Text;
            if (!long.TryParse(selectedIdText, out long selectedId))
                return;

            try
            {
                var person = await _client.ReadAsync(new Id { Id_ = selectedId });

                _currentSelectedPersonId = selectedId; // Remember current selection

                // Don't modify txtSearchSurname to preserve user search input

                txtName.Text = person.Name;
                txtSurname.Text = person.Surname;
                numAge.Text = person.Age.ToString();
                dtpDOB.Text = person.DateOfBirth;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading person: " + ex.Message);
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var list = await _client.ListAsync(new People.Protos.Empty());

                bool duplicateExists = list.People.Any(p =>
                    p.Name.Equals(txtName.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    p.Surname.Equals(txtSurname.Text.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    p.Age == int.Parse(numAge.Text) &&
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
                    Age = int.Parse(numAge.Text),
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
            try
            {
                var id = _currentSelectedPersonId;
                if (!id.HasValue)
                {
                    MessageBox.Show("No person selected.");
                    return;
                }

                var name = txtName.Text.Trim();
                var surname = txtSurname.Text.Trim();
                var age = int.Parse(numAge.Text);
                var dob = dtpDOB.Value.ToString("yyyy-MM-dd");

                var list = await _client.ListAsync(new People.Protos.Empty());

                var originalPerson = list.People.FirstOrDefault(p => p.Id == id.Value);
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
                    p.Id != id.Value &&
                    p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                    p.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase) &&
                    p.Age == age &&
                    p.DateOfBirth == dob);

                if (duplicateExists)
                {
                    MessageBox.Show("Person with the same details already exists!", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearInputs();
                    return;
                }

                var person = new Person
                {
                    Id = id.Value,
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
                if (_currentSelectedPersonId == null)
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

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            var dob = dtpDOB.Value;

            int age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;

            // Check if age is within valid range for numAge dropdown (1 to 100)
            if (age < 1)
                age = 1;
            else if (age > 100)
                age = 100;

            numAge.SelectedIndex = age - 1;  // since SelectedIndex is zero-based and age is 1-based
        }

    }
}
