using System;
using System.Windows.Forms;

namespace Lab2_WinForms
{
    public partial class MushroomForm : Form
    {
        private const string AskRing = "Does your mushroom have a ring?";
        private const string AskForest = "Does your mushroom grow in a forest?";
        private const string AskConvex = "Does your mushroom have a convex cup?";
        private const string AskGills = "Does your mushroom have gills?";

        private ChooseForm chooseForm;
        private bool goingToAnotherForm = false;

        private int questionNumber;
        private bool hasRing;

        public MushroomForm(ChooseForm choose)
        {
            InitializeComponent();
            chooseForm = choose;
            StartAgain();
        }

        private void StartAgain()
        {
            questionNumber = 1;
            hasRing = false;

            ShowQuestion(AskRing);
            lblResult.Text = "";
            txtAnswer.Enabled = true;
            btnAnswer.Enabled = true;
            txtAnswer.Focus();
        }

        private void ShowQuestion(string question)
        {
            lblQuestionNumber.Text = "Question " + questionNumber + " of 3 (maximum)";
            lblQuestion.Text = question;
            txtAnswer.Clear();
            txtAnswer.Focus();
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            string text = txtAnswer.Text.Trim().ToLower();
            bool yes;

            if (text == "yes" || text == "y")
            {
                yes = true;
            }
            else if (text == "no" || text == "n")
            {
                yes = false;
            }
            else if (text == "")
            {
                MessageBox.Show("Please type your answer first (yes or no).", "Mushroom",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnswer.Focus();
                return;
            }
            else
            {
                MessageBox.Show("\"" + txtAnswer.Text.Trim() + "\" is not a valid answer.\nPlease type yes or no.",
                    "Mushroom", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnswer.Focus();
                txtAnswer.SelectAll();
                return;
            }

            if (questionNumber == 1)
            {
                hasRing = yes;
                questionNumber = 2;

                if (hasRing)
                    ShowQuestion(AskForest);
                else
                    ShowQuestion(AskConvex);
            }
            else if (questionNumber == 2)
            {
                if (hasRing && yes)
                {
                    ShowResult("Amanite tue-mouche");
                }
                else if (hasRing)
                {
                    questionNumber = 3;
                    ShowQuestion(AskConvex);
                }
                else if (yes)
                {
                    ShowResult("Pied bleu");
                }
                else
                {
                    questionNumber = 3;
                    ShowQuestion(AskGills);
                }
            }
            else
            {
                if (hasRing)
                {
                    if (yes)
                        ShowResult("Agaric jaunissant");
                    else
                        ShowResult("Coprin chevelu");
                }
                else
                {
                    if (yes)
                        ShowResult("Girolle");
                    else
                        ShowResult("Cepe de Bordeaux");
                }
            }
        }

        private void ShowResult(string mushroom)
        {
            txtAnswer.Clear();
            txtAnswer.Enabled = false;
            btnAnswer.Enabled = false;

            lblQuestion.Text = "Done! Press \"Start again\" to try another mushroom.";
            lblResult.Text = "Your mushroom: " + mushroom;

            MessageBox.Show("Your mushroom is the " + mushroom + ".\n\nFound with " + questionNumber + " question(s).",
                "Mushroom", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartAgain();
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

        private void MushroomForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!goingToAnotherForm)
            {
                Application.Exit();
            }
        }
    }
}
