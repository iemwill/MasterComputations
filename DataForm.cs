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
        public List<Option> options2019;
        public Dictionary<string, Option> btcOptions;
        public List<Option> mostTraded;

        public DataForm()
        {
            InitializeComponent();
            btcOptions = new Dictionary<string, Option>(); activeOptionsBTC = new List<Option>(); inactiveOptionsBTC = new List<Option>();
            try
            {
                var data = Data.Load.localPublicAPI();
                btcOptions = data.Item1; activeOptionsBTC = data.Item2; inactiveOptionsBTC = data.Item3; currencies = data.Item4;
                loadGridAndPlotData();
            }
            catch (Exception err)
            {
                MessageBox.Show("Get new online data failed caused by no internetconnection, gonna load data from directory. \n" +
                    err.Message);
                var data = Data.Load.localPublicAPI();
                btcOptions = data.Item1; activeOptionsBTC = data.Item2; inactiveOptionsBTC = data.Item3; currencies = data.Item4;
                loadGridAndPlotData();
            }
        }

        private void loadGridAndPlotData()
        {
            try
            {
                var checkk = new List<Option>();
                //active
                foreach (var x in activeOptionsBTC)
                    if (x.trades.Count == 1000 && checkk.Count <= 5)
                        checkk.Add(x);
                //inactive
                //foreach (var x in inactiveOptionsBTC)
                //    if (x.trades.Count == 1000 && checkk.Count <= 5)
                //        checkk.Add(x);

                plotView1.Model = Plot.option(checkk);

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
                var check = Grid.options(activeOptionsBTC);
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
                var check2 = Grid.trades(checkk[0]);
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
        private void drawInactiveND_Click(object sender, EventArgs e)
        {
            try
            {

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
