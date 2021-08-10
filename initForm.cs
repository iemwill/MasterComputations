using System;
using System.Windows.Forms;

namespace OptionPricing
{
    public partial class initForm : Form
    {
        public initForm()
        {
            InitializeComponent();
        }

        private void pingOB_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form pingDataForm = new pingOBForm();
            pingDataForm.ShowDialog();
            this.Close();
        }

        private void dataV_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form viewDataForm = new dataForm();
            viewDataForm.ShowDialog();
            this.Close();
        }
    }
}
