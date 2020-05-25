using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using MasterComputations.Classes;
using MasterComputations.Computations;
using MasterComputations.Data;
using MasterComputations.Visualization;

namespace MasterComputations
{
    public partial class DataForm : Form
    {
        public List<Data.Currency> currencies;
        public Dictionary<string, Option> btcOptions;

        public List<Option> activeOptionsBTC;
        public List<Option> inactiveOptionsBTC;
        public List<Option> options2019;
        public int options2019TradesCount;
        public List<Option> mostTraded;
        //"BTC-27DEC19-7750-C"
        public DataForm()
        {
            InitializeComponent();
            btcOptions = new Dictionary<string, Option>(); activeOptionsBTC = new List<Option>(); inactiveOptionsBTC = new List<Option>();
            var data = Data.Load.localPublicAPI();
            btcOptions = data.Item1; activeOptionsBTC = data.Item2; inactiveOptionsBTC = data.Item3; currencies = data.Item4;
            initData();
            updateData();
            showData();
        }

        private void initData()
        {
            try
            {
                options2019 = new List<Option>();
                options2019TradesCount = 0;
                //fill options2019
                foreach (var x in inactiveOptionsBTC)
                {
                    if (x.raw.creation_timestamp / 1000 >= Helper.dateTimeToUnix(new DateTime(2019, 01, 01, 0, 0, 0, DateTimeKind.Utc)) &&
                        x.raw.expiration_timestamp / 1000 <= Helper.dateTimeToUnix(new DateTime(2019, 12, 31, 23, 59, 59, DateTimeKind.Utc)))
                    {
                        options2019.Add(x);
                        options2019TradesCount += x.trades.Count;
                        if (x.trades.Count == 1000)
                        {
                            var period = x.raw.expiration_timestamp - x.raw.creation_timestamp;
                            var intervalDays = Convert.ToInt32(period / (60 * 60 * 24 * 1000));
                            List<Option> moreData = new List<Option>();
                            var newDataCount = 0;
                            for (var i = 0; i < intervalDays; i++)
                            {
                                var moreDoption = new Option();
                                long start = x.raw.creation_timestamp;
                                long end = x.raw.creation_timestamp + i * (60 * 60 * 24 * 1000);//(x.raw.creation_timestamp / 1000) + (x.raw.expiration_timestamp / 1000 - x.raw.creation_timestamp / 1000) / 2;
                                var trad = API.Deribit.getTradesByInstrumentWA(x.raw.instrument_name, start, end, true, 1000);
                                foreach (var t in trad)
                                    if (!moreDoption.trades.Contains(t))
                                        moreDoption.trades.Add(t);
                                moreDoption.start = Helper.unixToDateTime(x.raw.creation_timestamp / 1000);
                                moreDoption.end = Helper.unixToDateTime(x.raw.expiration_timestamp / 1000);
                                newDataCount += moreDoption.trades.Count;
                                moreData.Add(moreDoption); ;
                            }
                            var filteredTrades = new List<Trade>();
                            foreach (var yes in x.trades)
                                filteredTrades.Add(yes);
                            foreach (var opt in moreData)
                                foreach (var tr in opt.trades)
                                    if (!filteredTrades.Contains(tr))
                                        filteredTrades.Add(tr);
                            btcOptions[x.name].trades = filteredTrades;
                        }
                    }
                }
                Save.options(btcOptions);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void updateData()
        {
            try
            {
                activeOptionsBTC = new List<Option>();
                inactiveOptionsBTC = new List<Option>();
                options2019 = new List<Option>();
                options2019TradesCount = 0;
                mostTraded = new List<Option>();
                //fill options2019
                foreach (var x in btcOptions.Values)
                {
                    if (x.active)
                        activeOptionsBTC.Add(x);
                    else
                        inactiveOptionsBTC.Add(x);
                    if (x.raw.creation_timestamp / 1000 >= Helper.dateTimeToUnix(new DateTime(2019, 01, 01, 0, 0, 0, DateTimeKind.Utc)) &&
                                 x.raw.expiration_timestamp / 1000 <= Helper.dateTimeToUnix(new DateTime(2019, 12, 31, 23, 59, 59, DateTimeKind.Utc)))
                    {
                        options2019.Add(x);
                        options2019TradesCount += x.trades.Count;
                    }
                    if (x.trades.Count >= 5000)
                        mostTraded.Add(x);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void showData()
        {
            try
            {
                //plotList
                var plotta = new List<Option>();
                foreach (var x in options2019)
                    if (x.trades.Count >= 5000 && plotta.Count <= 5)
                        plotta.Add(x);
                plotView1.Model = Plot.option(plotta);

                dataGridView1.ColumnCount = 11;
                dataGridView1.Columns[0].Name = "option type";
                dataGridView1.Columns[1].Name = "trades count";
                dataGridView1.Columns[2].Name = "settlement period";
                dataGridView1.Columns[3].Name = "strike";
                dataGridView1.Columns[4].Name = "instrument name";
                dataGridView1.Columns[5].Name = "creation time";
                dataGridView1.Columns[6].Name = "expiration time";
                dataGridView1.Columns[7].Name = "base currency";
                dataGridView1.Columns[8].Name = "quote currency";
                dataGridView1.Columns[9].Name = "maker commission";
                dataGridView1.Columns[10].Name = "taker comission";

                dataGridView1.Rows.Clear();
                var check = Grid.options(options2019);
                foreach (var x in check)
                    this.dataGridView1.Rows.Add(x);

                dataGridView2.ColumnCount = 6;
                dataGridView2.Columns[0].Name = "direcetion";
                dataGridView2.Columns[1].Name = "$(price*index_price)";
                dataGridView2.Columns[2].Name = "amount";
                dataGridView2.Columns[3].Name = "timestamp";
                dataGridView2.Columns[4].Name = "implied_volatility time";
                dataGridView2.Columns[5].Name = "period";

                dataGridView2.Rows.Clear();
                var check2 = Grid.trades(options2019[0]);
                foreach (var x in check2)
                    this.dataGridView2.Rows.Add(x);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void useActive_Click(object sender, EventArgs e)
        {
            //Plot option
            var checkk = new List<Option>();
            foreach (var x in activeOptionsBTC)
                if (x.trades.Count == 1000 && checkk.Count == 0)
                    checkk.Add(x);
            plotView1.Model = Plot.option(checkk);

            //fill grid
            dataGridView1.Rows.Clear();
            var check = Grid.options(activeOptionsBTC);
            foreach (var x in check)
                this.dataGridView1.Rows.Add(x);

            dataGridView2.Rows.Clear();
            var check2 = Grid.trades(checkk[0]);
            foreach (var x in check2)
                this.dataGridView2.Rows.Add(x);
        }
        private void useInactive_Click(object sender, EventArgs e)
        {
            //plot option
            var checkk = new List<Option>();
            foreach (var x in inactiveOptionsBTC)
                if (x.trades.Count == 1000 && checkk.Count == 0)
                    checkk.Add(x);
            plotView1.Model = Plot.option(checkk);

            //fill grids
            dataGridView1.Rows.Clear();
            var check = Grid.options(inactiveOptionsBTC);
            foreach (var x in check)
                this.dataGridView1.Rows.Add(x);

            dataGridView2.Rows.Clear();
            var check2 = Grid.trades(checkk[0]);
            foreach (var x in check2)
                this.dataGridView2.Rows.Add(x);
        }
        private void plotSelected_Click(object sender, EventArgs e)
        {
            try
            {
                var check = dataGridView1.SelectedRows;
                if (check.Count != 0)
                {
                    var check2 = new List<string>();
                    var check3 = "";
                    foreach (DataGridViewRow x in check)
                        check2.Add(x.Cells[4].FormattedValue.ToString());
                    check3 = check[0].Cells[4].FormattedValue.ToString();
                    var plot = new List<Option>();
                    foreach (var x in check2)
                        plot.Add(btcOptions[x]);
                    this.plotView1.Model = Plot.option(plot);

                    dataGridView2.Rows.Clear();
                    var check4 = Grid.trades(btcOptions[check3]);
                    foreach (var x in check4)
                        this.dataGridView2.Rows.Add(x);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void newData_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Since we gonna get trades for over 10 options traded from 2017 until today this step will take +- 10 min.");
                var data = Data.Load.onlinePublicAPI();
                btcOptions = data.Item1; activeOptionsBTC = data.Item2; inactiveOptionsBTC = data.Item3; currencies = data.Item4;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
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
