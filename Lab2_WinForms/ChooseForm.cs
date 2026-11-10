using System;
using System.Windows.Forms;

namespace Lab2_WinForms
{
    public partial class ChooseForm : Form
    {
        private LoginForm loginForm;

        private bool goingToAnotherForm = false;

        public ChooseForm(LoginForm login, string username)
        {
            InitializeComponent();

            loginForm = login;
            lblWelcome.Text = "Welcome, " + username + ". What would you like to do?";
        }

        private void btnBicycle_Click(object sender, EventArgs e)
        {
            BicycleForm bicycle = new BicycleForm(this);
            bicycle.Show();
            this.Hide();
        }

        private void btnMushroom_Click(object sender, EventArgs e)
        {
            MushroomForm mushroom = new MushroomForm(this);
            mushroom.Show();
            this.Hide();
        }

        public void Logout()
        {
            goingToAnotherForm = true;
            loginForm.ClearFields();
            loginForm.Show();
            this.Close();
        }

        private void ChooseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!goingToAnotherForm)
            {
                Application.Exit();
            }
        }
    }
}
