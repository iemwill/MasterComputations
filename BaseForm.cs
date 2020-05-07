using System;
using System.Collections.Generic;
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
        }
        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    currencies = API.Deribit.getCurrencies();
                    optionsBTC = API.Deribit.getInstruments();
                    inactive = new List<Instrument>();
                    foreach (var x in optionsBTC)
                        if (x.is_active == false)
                            inactive.Add(x);
                    Save.currencies(currencies);
                    Save.optionsBTC(optionsBTC);
                    MessageBox.Show("Data was load successfully.");
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
                currencies = Data.Load.currencies();
                optionsBTC = Data.Load.optionsBTC();
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
