using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceRecognition
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginForm LoginForm = new LoginForm();
            LoginForm.Show();
            this.Hide(); // Hide the start form, or you can close it if not needed anymore.
        }

        private void SignupButton_Click(object sender, EventArgs e)
        {
            SignupForm SignupForm = new SignupForm();
            SignupForm.Show();
            this.Hide(); // Hide the start form, or you can close it if not needed anymore.
        }
    }
}
