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
        public List<Tuple<long, double>> historicalVolatilityBTC;
        public BaseForm()
        {
            InitializeComponent();
            //inactive = API.Deribit.getChartData();//TODO
            historicalVolatilityBTC = API.Deribit.getHistVol();
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
                    MessageBox.Show("Data was load successfully. Now filling Table 1...");
                    fillGrid();
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
        private void load()
        {
            foreach(var x in currencies)
            {

            }
        }
        private void fillGrid()
        {
            try
            {
                dataGridView1.ColumnCount = 10;
                dataGridView1.Columns[0].Name = "option type";
                dataGridView1.Columns[1].Name = "settlement period";
                dataGridView1.Columns[2].Name = "strike";
                dataGridView1.Columns[3].Name = "instrument name";
                dataGridView1.Columns[4].Name = "creation time";
                dataGridView1.Columns[5].Name = "expiration time";
                dataGridView1.Columns[6].Name = "base currency";
                dataGridView1.Columns[7].Name = "quote currency";
                dataGridView1.Columns[8].Name = "maker commission";
                dataGridView1.Columns[9].Name = "taker comission";
                foreach (var x in optionsBTC)
                {//add code here for adding rows to dataGridviewFiles
                    DataGridViewRow tempRow = new DataGridViewRow();

                    DataGridViewCell cellFileName = new DataGridViewTextBoxCell();
                    cellFileName.Value = x.option_type;
                    tempRow.Cells.Add(cellFileName);
                    DataGridViewCell cellDocCount = new DataGridViewTextBoxCell();
                    cellDocCount.Value = x.settlement_period;
                    tempRow.Cells.Add(cellDocCount);
                    DataGridViewCell cellPageCount = new DataGridViewTextBoxCell();
                    cellPageCount.Value = x.strike;
                    tempRow.Cells.Add(cellPageCount);
                    DataGridViewCell one = new DataGridViewTextBoxCell();
                    one.Value = x.instrument_name;
                    tempRow.Cells.Add(one);
                    DataGridViewCell two = new DataGridViewTextBoxCell();
                    two.Value = x.creation_timestamp;
                    tempRow.Cells.Add(two);
                    DataGridViewCell three = new DataGridViewTextBoxCell();
                    three.Value = x.expiration_timestamp;
                    tempRow.Cells.Add(three);
                    DataGridViewCell four = new DataGridViewTextBoxCell();
                    four.Value = x.base_currency;
                    tempRow.Cells.Add(four);
                    DataGridViewCell fiv = new DataGridViewTextBoxCell();
                    fiv.Value = x.quote_currency;
                    tempRow.Cells.Add(fiv);
                    DataGridViewCell ser = new DataGridViewTextBoxCell();
                    ser.Value = x.maker_commission;
                    tempRow.Cells.Add(ser);
                    DataGridViewCell dd = new DataGridViewTextBoxCell();
                    dd.Value = x.taker_commission;
                    tempRow.Cells.Add(dd);

                    tempRow.Tag = x.is_active;
                    dataGridView1.Rows.Add(tempRow);
                }
            } catch(Exception)
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
