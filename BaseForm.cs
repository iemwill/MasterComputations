using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MasterComputations.Data;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace MasterComputations
{
    public partial class BaseForm : Form
    {
        public List<Currency> currencies;
        public List<Instrument> optionsBTC;
        public long minDateTime;
        public long maxDateTime;
        public List<Instrument> inactive;
        public List<Tuple<long, double>> historicalVolatilityBTC;
        public Dictionary<string, List<Book>> orderBook;
        public BaseForm()
        {
            InitializeComponent();
            //inactive = API.Deribit.getChartData(
            //    "BTC-15MAY20-10000-C",
            //    Convert.ToInt32(dateTimeToUnix(new DateTime(2020,05,01, 0, 0, 0).ToUniversalTime())),
            //    Convert.ToInt32(dateTimeToUnix(DateTime.UtcNow)),
            //    ""
            //);//TODO
        }
        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    loadAPI();
                    MessageBox.Show("Data was load successfully. Now filling Table 1 and Graphic 1.");
                    fillGrid();
                    paintDotsWithDuration();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                currencies = Data.Load.currencies();
                optionsBTC = Data.Load.optionsBTC();
                orderBook = Data.Load.book();
                MessageBox.Show("Data was load successfully. Now filling Table 1 and Graphic 1.");
                var minmax = Instrument.getMinMax(optionsBTC);
                minDateTime = minmax.Item1;
                maxDateTime = minmax.Item1;
                fillGrid();
                paintDots();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void loadAPI()
        {
            //Get supported currencies and options for BTC
            currencies = API.Deribit.getCurrencies();
            optionsBTC = API.Deribit.getInstruments();            
            //fill current Orderbook.
            orderBook = new Dictionary<string, List<Book>>();
            foreach (var x in optionsBTC)
                orderBook.Add(x.instrument_name, API.Deribit.getBook(x.instrument_name));

            Save.currencies(currencies);
            Save.optionsBTC(optionsBTC);
            Save.book(orderBook);
            var minmax = Instrument.getMinMax(optionsBTC);
            minDateTime = minmax.Item1;
            maxDateTime = minmax.Item1;

            historicalVolatilityBTC = API.Deribit.getHistVol();
            foreach (var x in optionsBTC)
            {
                orderBook[x.instrument_name].Add(API.Deribit.getTicker(x.instrument_name));
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
                    two.Value = unixToDateTime(x.creation_timestamp / 1000).ToShortDateString();
                    tempRow.Cells.Add(two);
                    DataGridViewCell three = new DataGridViewTextBoxCell();
                    three.Value = unixToDateTime(x.expiration_timestamp / 1000).ToShortDateString();
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
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void paintDots()
        {
            PlotModel model = new PlotModel { LegendSymbolLength = 24 };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Strike Price USD" });
            model.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "Date",
                IntervalType = DateTimeIntervalType.Days,
                Minimum = DateTimeAxis.ToDouble(unixToDateTime(minDateTime / 1000)),
                Maximum = DateTimeAxis.ToDouble(new DateTime(2021, 06, 01,0,0,0)),
            });
            LineSeries lineserieCall = new LineSeries
            {
                DataFieldX = "x",
                DataFieldY = "Y",
                StrokeThickness = 2,
                MarkerSize = 5,
                LineStyle = LineStyle.None,
                Color = OxyColors.Red,
                MarkerType = MarkerType.Triangle,
            };
            LineSeries lineseriePut = new LineSeries
            {
                DataFieldX = "x",
                DataFieldY = "Y",
                StrokeThickness = 2,
                MarkerSize = 2,
                LineStyle = LineStyle.None,
                Color = OxyColors.Black,
                MarkerType = MarkerType.Circle,
            };
            foreach (var x in optionsBTC)
                if (x.option_type == "call")
                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(unixToDateTime(x.creation_timestamp / 1000)), x.strike));
                else
                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(unixToDateTime(x.creation_timestamp / 1000)), x.strike));

            model.Series.Add(lineserieCall);
            model.Series.Add(lineseriePut);
            this.plotView1.Model = model;
        }
        private void paintDotsWithDuration()
        {
            PlotModel model = new PlotModel { LegendSymbolLength = 24 };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Strike Price USD" });
            model.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "Date",
                IntervalType = DateTimeIntervalType.Days,
                Minimum = DateTimeAxis.ToDouble(unixToDateTime(minDateTime / 1000)),
                Maximum = DateTimeAxis.ToDouble(new DateTime(2021, 06, 01, 0, 0, 0)),
            });
            foreach (var x in optionsBTC)
                if (x.option_type == "call")
                {
                    LineSeries lineserieCall = new LineSeries
                    {
                        DataFieldX = "x",
                        DataFieldY = "Y",
                        StrokeThickness = 2,
                        MarkerSize = 5,
                        LineStyle = LineStyle.Dot,
                        Color = OxyColors.Red,
                        MarkerType = MarkerType.Triangle,
                    };
                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(unixToDateTime(x.creation_timestamp / 1000)), x.strike));
                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(unixToDateTime(x.expiration_timestamp / 1000)), x.strike));
                    model.Series.Add(lineserieCall);
                }
                else
                {
                    LineSeries lineseriePut = new LineSeries
                    {
                        DataFieldX = "x",
                        DataFieldY = "Y",
                        StrokeThickness = 2,
                        MarkerSize = 2,
                        LineStyle = LineStyle.Solid,
                        Color = OxyColors.Black,
                        MarkerType = MarkerType.Circle,
                    };
                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(unixToDateTime(x.creation_timestamp / 1000)), x.strike));
                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(unixToDateTime(x.expiration_timestamp / 1000)), x.strike));
                    model.Series.Add(lineseriePut);
                }

            this.plotView1.Model = model;
        }
        public DateTime unixToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public double dateTimeToUnix(DateTime dt)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unix = (dt.ToUniversalTime() - dtDateTime);
            return unix.TotalSeconds;
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
