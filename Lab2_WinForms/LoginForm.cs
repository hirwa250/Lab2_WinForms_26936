using System;
using System.Windows.Forms;

namespace Lab2_WinForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "")
            {
                MessageBox.Show("Username cannot be empty.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (password == "")
            {
                MessageBox.Show("Password cannot be empty.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            ChooseForm choose = new ChooseForm(this, username);
            choose.Show();
            this.Hide();
        }

        public void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }
    }
}
