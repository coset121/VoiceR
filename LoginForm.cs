using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace VoiceRecognition
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

       
        private void UserLogin()
        {
            string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [userName],[passWord] FROM Users WHERE userName = @userName AND passWord = @passWord", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@userName", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@passWord", txtPassword.Text.Trim());
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        // Successful login
                        MessageBox.Show("Login successful.");

                        // Update last login timestamp
                        dr.Close(); // Close the previous reader
                        cmd.CommandText = "UPDATE Users SET lastLogin = @lastLogin WHERE userName = @userName";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@lastLogin", DateTime.Now);
                        cmd.Parameters.AddWithValue("@userName", txtUsername.Text.Trim());
                        cmd.ExecuteNonQuery();

                        // Navigate to Dashboard.aspx (Windows Form)
                        var dashboardForm = new DashboardForm();
                        dashboardForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid UserID or password!");
                    }
                }
            }
        }

        private void SignupButton_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm();
            signupForm.Show();
            this.Hide(); // Hide the login form, or you can close it if not needed anymore.
        }

        private void BtnLogon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Please type your UserID!");
                txtUsername.Focus();
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please type your password!");
                txtPassword.Focus();
            }
            else
            {
                UserLogin();
            }
        }
    }
}
