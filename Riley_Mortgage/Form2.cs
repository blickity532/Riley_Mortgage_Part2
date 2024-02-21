using System;
using System.Windows.Forms;

namespace Riley_Mortgage
{
    public partial class frm_Mortgage_Calculator : Form
    {
        // Variables for the formula
        private double principal;
        private double numberOfPayments;
        private double monthlyInterestRate;

        public frm_Mortgage_Calculator()
        {
            InitializeComponent();
        }

        private void frm_Mortgage_Calculator_Load(object sender, EventArgs e)
        {
            this.Text = "Blake Riley : Mortgage Calculator";
            BTN_Reset_Click(null, null);
            BTN_Reset.Enabled = false;
        }
        private void TermChanged(object sender, EventArgs e)
        {
            if (RBO_15Year.Checked)
            {
                numberOfPayments = 15 * 12;
                TXT_Other.Visible = false;
            }
            else if (RBO_30Year.Checked)
            {
                numberOfPayments = 30 * 12;
                TXT_Other.Visible = false;
            }
            else if (RBO_Other.Checked)
            {
                TXT_Other.Visible = true;
            }
        }

        private void BTN_Calculate_Click(object sender, EventArgs e)
        {
            bool hasErrors = false;
            LBL_ERROR.Text = "";
            if (RBO_Other.Checked)
            {
                if(double.TryParse(TXT_Other.Text, out numberOfPayments) == false)
                {
                    //Display an error message
                    LBL_ERROR.Text = "Please enter a valid number of years.\n";
                    hasErrors = true;
                }
            }

            if (double.TryParse(TXT_Principal.Text, out principal)==false)
            {
                LBL_ERROR.Text += "Please enter a valid principal amount.\n";
                hasErrors = true;
            }
            
            if (CBX_Interest.SelectedIndex == -1)
            {
                LBL_ERROR.Text += "Please select an interest rate.\n";
                hasErrors = true;
            }
            else
            {
                monthlyInterestRate = (double.Parse(CBX_Interest.Text) / 10) + 3;
            }

            monthlyInterestRate = monthlyInterestRate / 100 / 12;
            double monthlyPayment = principal * ((monthlyInterestRate * (Math.Pow(1 + monthlyInterestRate,numberOfPayments))) / ((Math.Pow(1 + monthlyInterestRate, numberOfPayments)) - 1));
            LBL_MonthlyPayment.Text = monthlyPayment.ToString("c");
            BTN_Reset.Enabled = !hasErrors;

        }

        private void BTN_Reset_Click(object sender, EventArgs e)
        {
            TXT_Principal.Clear();
            TXT_Principal.Select();
            TXT_Other.Clear();
            RBO_15Year.Checked = false;
            RBO_Other.Checked = false;
            TXT_Other.Visible = false;
            CBX_Interest.SelectedIndex = -1;
            LBL_MonthlyPayment.Text = "";
            BTN_Reset.Enabled = false;
        }

        private void BTN_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TXT_Principal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
