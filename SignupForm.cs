using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceRecognition
{
    public partial class SignupForm : Form
    {
        public SignupForm()
        {
            InitializeComponent();
        }

       
        private bool IsUsernameTaken(string username)
        {
            string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE userName = @userName", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@userName", username);
                    cn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        private void BtnSignup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Username and password are required.");
                return;
            }

            // Check if the username is already taken
            if (IsUsernameTaken(txtUsername.Text))
            {
                MessageBox.Show("Username is already in use. Please choose another.");
                return;
            }

            string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (userName, passWord, createDate) VALUES (@userName, @passWord, @createDate)", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@userName", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@passWord", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                    // cmd.Parameters.AddWithValue("@otherColumns", txtOtherColumns.Text.Trim());

                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful.");
                        // Optionally, you can redirect to the login page or perform other actions.
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.");
                    }
                }
            }
        }
        private void VoiceLoginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide(); // Hide the Signup form, or you can close it if not needed anymore.
        }

        
    }
}
