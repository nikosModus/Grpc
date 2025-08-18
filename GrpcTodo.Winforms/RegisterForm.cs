using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using People.Service.Models;

namespace People.Winforms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password are required.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Hash the password
            string hashedPassword = ComputeSha256Hash(password);

            var options = new DbContextOptionsBuilder<PeopleContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PeopleDb;Integrated Security=True;")
                .Options;

            using var db = new PeopleContext(options);

            // Check if username already exists
            var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (existingUser != null)
            {
                MessageBox.Show("Username already exists.");
                return;
            }

            // Create new user
            var user = new UserEntity
            {
                Username = txtUsername.Text,
                PasswordHash = hashedPassword,
                Role = "User",
                CreatedAt = DateTime.Now // Not DateTime.MinValue!
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            MessageBox.Show("Registration successful!");
            this.Close(); // close register form
        }

        private string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
