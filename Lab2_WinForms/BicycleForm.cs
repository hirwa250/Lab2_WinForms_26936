using System;
using System.Windows.Forms;

namespace Lab2_WinForms
{
    public partial class BicycleForm : Form
    {
        private ChooseForm chooseForm;
        private bool goingToAnotherForm = false;

        public BicycleForm(ChooseForm choose)
        {
            InitializeComponent();
            chooseForm = choose;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            txtTotal.Clear();
            lblDetails.Text = "";

            int start;
            int end;

            if (!ReadHour(txtStart, "Start hour", out start))
                return;

            if (!ReadHour(txtEnd, "End hour", out end))
                return;

            if (start < 0 || start > 23)
            {
                ShowError("Start hour must be between 0 and 23.", txtStart);
                return;
            }

            if (end < 1 || end > 24)
            {
                ShowError("End hour must be between 1 and 24.", txtEnd);
                return;
            }

            if (start >= end)
            {
                ShowError("Start hour must be less than the end hour.\n" +
                          "A rental cannot go past midnight.", txtEnd);
                return;
            }

            int total = RentalCalculator.GetTotal(start, end);
            txtTotal.Text = total.ToString("N0");

            string details = "";
            details = AddLine(details, start, end, RentalCalculator.NightRate);
            details = AddLine(details, start, end, RentalCalculator.NormalRate);
            details = AddLine(details, start, end, RentalCalculator.PeakRate);
            lblDetails.Text = details;
        }

        private bool ReadHour(TextBox box, string name, out int hour)
        {
            hour = 0;
            string text = box.Text.Trim();

            if (text == "")
            {
                ShowError(name + " is required.", box);
                return false;
            }

            if (text.Contains(".") || text.Contains(","))
            {
                ShowError(name + ": decimal numbers are not accepted, use full hours only.", box);
                return false;
            }

            if (!int.TryParse(text, out hour))
            {
                ShowError(name + " must be a number, for example 8 or 17.", box);
                return false;
            }

            return true;
        }

        private void ShowError(string message, TextBox box)
        {
            MessageBox.Show(message, "Bicycle renting",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            box.Focus();
            box.SelectAll();
        }

        private string AddLine(string details, int start, int end, int rate)
        {
            int hours = RentalCalculator.HoursAtRate(start, end, rate);

            if (hours == 0)
                return details;

            if (details != "")
                details += "\n";

            return details + hours + " h x " + rate + " RWF = " + (hours * rate).ToString("N0") + " RWF";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStart.Clear();
            txtEnd.Clear();
            txtTotal.Clear();
            lblDetails.Text = "";
            txtStart.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            goingToAnotherForm = true;
            chooseForm.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            goingToAnotherForm = true;
            chooseForm.Logout();
            this.Close();
        }

        private void BicycleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!goingToAnotherForm)
            {
                Application.Exit();
            }
        }
    }
}
