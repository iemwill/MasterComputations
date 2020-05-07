using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MasterComputations.Data;


namespace MasterComputations
{
    public partial class BaseForm : Form
    {
        public List<Currency> currencies;
        public List<Instrument> optionsBTC;
        public List<Instrument> inactive;

        public BaseForm()
        {
            InitializeComponent();
            currencies = API.Deribit.getCurrencies();
            optionsBTC = API.Deribit.getInstruments();
            inactive = new List<Instrument>();
            foreach (var x in optionsBTC)
                if (x.is_active == false)
                    inactive.Add(x);
            Debug.WriteLine(true);
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
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
//connection Form to deribit private API
//var form = new connectDeribitForm();
//        if (form.ShowDialog() == DialogResult.OK)
//        {
//            name = form.name;
//            passW = form.passW;
//            form.Close();
//        }
