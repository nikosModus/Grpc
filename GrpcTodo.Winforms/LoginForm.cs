using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using People.Service.Models;

namespace People.Winforms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter username and password.");
                return;
            }

            var result = await TryLoginAsync(username, password);

            if (result.Success)
            {
                MessageBox.Show($"Login successful! Role: {result.Role}");
                // Open main form
                var mainForm = new Form1(result.Role);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private async Task<(bool Success, string Role)> TryLoginAsync(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            var options = new DbContextOptionsBuilder<PeopleContext>()
                .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PeopleDb;Integrated Security=True;")
                .Options;

            using var db = new PeopleContext(options);

            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
                return (true, user.Role);

            return (false, null);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password.Trim()));
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using var registerForm = new RegisterForm();
            registerForm.ShowDialog(); // Open as modal dialog
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
