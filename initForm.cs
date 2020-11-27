using BOPcomputations;
using System;
using System.Windows.Forms;

namespace MasterComputations
{
    public partial class initForm : Form
    {
        public initForm()
        {
            InitializeComponent();
        }

        private void pingData_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form pingDataForm = new pingOBForm();
            pingDataForm.ShowDialog();
            this.Close();
        }

        private void dataVs_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form dataForm = new dataForm();
            dataForm.ShowDialog();
            this.Close();
        }

        private void computations_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form computationForm = new computationForm();
            computationForm.ShowDialog();
            this.Close();
        }

        private void apiKEY_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form connectForm = new connectDeribitForm();
            connectForm.ShowDialog();
            this.Close();
        }

        private void updateHistoricalData_Click(object sender, EventArgs e)
        {

        }
    }
}
