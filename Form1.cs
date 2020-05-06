using System;
using System.Windows.Forms;
using MasterComputations.Data;

namespace MasterComputations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///Parse data
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OptionData data = new OptionData();
                    data.rawData = Parse.csv(openFileDialog1.FileName, ';');
                }
                catch
                {
                }
            }
        }
    }
}
