using Grpc.Net.Client;
using System;
using System.Linq;
using System.Windows.Forms;
using People.Protos;



namespace People.Winforms
{
    public partial class Form1 : Form
    {
        private PersonService.PersonServiceClient _client;

        public Form1()
        {
            InitializeComponent();

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            _client = new PersonService.PersonServiceClient(channel);
        }

        private async void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                var id = long.Parse(txtId.Text);
                var person = await _client.ReadAsync(new Id { Id_ = id });  // use Id_ as in your proto generated class
                txtName.Text = person.Name;
                txtSurname.Text = person.Surname;
                numAge.Text = person.Age.ToString();
                dtpDOB.Text = person.DateOfBirth;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Read error: " + ex.Message);
            }
        }


        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var person = new Person
                {
                    Name = txtName.Text,
                    Surname = txtSurname.Text,
                    Age = int.Parse(numAge.Text),
                    DateOfBirth = dtpDOB.Text
                };
                var created = await _client.CreateAsync(person);
                MessageBox.Show($"Created person with ID: {created.Id}");
                btnList.PerformClick();
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
                var person = new Person
                {
                    Id = long.Parse(txtId.Text),
                    Name = txtName.Text,
                    Surname = txtSurname.Text,
                    Age = int.Parse(numAge.Text),
                    DateOfBirth = dtpDOB.Text
                };
                var result = await _client.UpdateAsync(person);
                MessageBox.Show(result.Message);
                btnList.PerformClick();
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
                var id = long.Parse(txtId.Text);
                var result = await _client.DeleteAsync(new Id { Id_ = id }); // use Id_ 
                MessageBox.Show(result.Message);
                btnList.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete error: " + ex.Message);
            }
        }

        private async void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                var list = await _client.ListAsync(new People.Protos.Empty());
                listBoxPeople.Items.Clear();
                foreach (var p in list.People)
                {
                    listBoxPeople.Items.Add($"{p.Id}: {p.Name} {p.Surname}, Age: {p.Age}, DOB: {p.DateOfBirth}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("List error: " + ex.Message);
            }
        }
    }
}
