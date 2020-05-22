using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MasterComputations.Classes;
using MasterComputations.Data;
using MasterComputations.Visualization;
using OxyPlot;

namespace MasterComputations
{
    public partial class DataForm : Form
    {
        public List<Currency> currencies;
        public List<Option> activeOptionsBTC;
        public List<Option> inactiveOptionsBTC;
        public List<Option> btcOptions;
        public List<Option> mostTraded;

        public DataForm()
        {
            InitializeComponent();
            btcOptions = new List<Option>(); activeOptionsBTC = new List<Option>(); inactiveOptionsBTC = new List<Option>();
            try
            {
                //var data = Data.Load.onlinePublicAPI();
                var data = Data.Load.localPublicAPI();
                btcOptions = data.Item1; activeOptionsBTC = data.Item2; inactiveOptionsBTC = data.Item3; currencies = data.Item4;
                loadData();
            }
            catch (Exception err)
            {
                MessageBox.Show("Get new online data failed caused by no internetconnection, gonna load data from directory. \n",
                    err.Message);
                var data = Data.Load.localPublicAPI();
                btcOptions = data.Item1; activeOptionsBTC = data.Item2; inactiveOptionsBTC = data.Item3; currencies = data.Item4;
            }
            //var startTime = Helper.dateTimeToUnix(new DateTime(2018, 05, 16, 11, 0, 0, DateTimeKind.Utc).ToUniversalTime());
            //var endTime = Helper.dateTimeToUnix(new DateTime(2018, 07, 15, 11, 0, 0, DateTimeKind.Utc).ToUniversalTime());
            //var check = API.Deribit.getTradesByInstrumentWA("BTC-29JUN18-9500-C", startTime, endTime, true, 1000);
        }

        private void loadData()
        {
            try
            {
                //                plotView1.Model = Plot.option(inactiveOptionsBTC);
                var checkk = new List<Option>();
                foreach (var x in activeOptionsBTC)
                    if (x.trades.Count == 1000)
                        checkk.Add(x);

                plotView1.Model = Plot.option(checkk);
                dataGridView1.Rows.Clear();
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
                var check = Grid.options(activeOptionsBTC);
                foreach (var x in check)
                    this.dataGridView1.Rows.Add(x);
                dataGridView2.Rows.Clear();
                dataGridView2.ColumnCount = 4;
                dataGridView2.Columns[0].Name = "direcetion";
                dataGridView2.Columns[1].Name = "$(price*amount*index_price)";
                dataGridView2.Columns[2].Name = "timestamp";
                dataGridView2.Columns[3].Name = "implied_volatility time";
                var check2 = Grid.trades(checkk[0]);
                foreach (var x in check2)
                    this.dataGridView2.Rows.Add(x);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void useactive_Click(object sender, EventArgs e)
        {

        }
        private void drawSelected_Click(object sender, EventArgs e)
        {
            try
            {
                //var check = dataGridView1.SelectedRows;
                //if (check.Count == 0)
                //{
                //    List<Instrument> plotta = new List<Instrument>();
                //    foreach (DataGridViewRow x in check)
                //    {
                //        Instrument toAdd = new Instrument();
                //        toAdd.option_type = x.Cells[0].FormattedValue.ToString();
                //        toAdd.settlement_period = x.Cells[1].FormattedValue.ToString();
                //        toAdd.strike = Convert.ToDouble(x.Cells[2].FormattedValue);
                //        toAdd.instrument_name = x.Cells[3].FormattedValue.ToString();
                //        toAdd.creation_timestamp = Helper.dateTimeToUnix(Convert.ToDateTime(x.Cells[4].FormattedValue));
                //        toAdd.expiration_timestamp = Helper.dateTimeToUnix(Convert.ToDateTime(x.Cells[5].FormattedValue));
                //        toAdd.base_currency = x.Cells[6].FormattedValue.ToString();
                //        toAdd.quote_currency = x.Cells[7].FormattedValue.ToString();
                //        //toAdd.maker_commission = Convert.ToInt64(x.Cells[8].FormattedValue);
                //        //toAdd.taker_commission = Convert.ToInt64(x.Cells[9].FormattedValue);
                //        plotta.Add(toAdd);
                //    }
                //    paintDotsWithDuration(plotta);
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void useInactive_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void drawInactiveND_Click(object sender, EventArgs e)
        {
            //try
            //{

            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}
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
