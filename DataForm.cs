using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MasterComputations.Computations;
using MasterComputations.Data;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace MasterComputations
{
    public partial class DataForm : Form
    {
        public List<Currency> currencies;
        public List<Instrument> activeOptionsBTC;
        public List<Instrument> inactiveOptionsBTC;
        public DataForm()
        {
            InitializeComponent();
            var startTime = Helper.dateTimeToUnix(new DateTime(2018, 05, 16, 11, 0, 0, DateTimeKind.Utc).ToUniversalTime());
            var endTime = Helper.dateTimeToUnix(new DateTime(2018, 07, 15, 11, 0, 0, DateTimeKind.Utc).ToUniversalTime());
            var check = API.Deribit.getTradesByInstrumentWA("BTC-29JUN18-9500-C", startTime, endTime, true, 1000);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    loadAPI();
                    MessageBox.Show("Data was load successfully. Now filling Table 1 and Graphic 1.");
                    fillGrid(activeOptionsBTC);
                    paintDotsWithDuration(activeOptionsBTC);
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
                var check = dataGridView1.SelectedRows;
                if (check.Count == 0)
                {
                    currencies = Data.Load.currencies();
                    activeOptionsBTC = Data.Load.activeOptionsBTC();
                    inactiveOptionsBTC = Data.Load.inactiveOptionsBTC();
                    //chartData = Data.Load.chartData();
                    //orderBook = Data.Load.book();
                    fillGrid(activeOptionsBTC);
                    paintDots(activeOptionsBTC);
                }
                else
                {
                    List<Instrument> plotta = new List<Instrument>();
                    foreach (DataGridViewRow x in check)
                    {
                        Instrument toAdd = new Instrument();
                        toAdd.option_type = x.Cells[0].FormattedValue.ToString();
                        toAdd.settlement_period = x.Cells[1].FormattedValue.ToString();
                        toAdd.strike = Convert.ToDouble(x.Cells[2].FormattedValue);
                        toAdd.instrument_name = x.Cells[3].FormattedValue.ToString();
                        toAdd.creation_timestamp = Helper.dateTimeToUnix(Convert.ToDateTime(x.Cells[4].FormattedValue));
                        toAdd.expiration_timestamp = Helper.dateTimeToUnix(Convert.ToDateTime(x.Cells[5].FormattedValue));
                        toAdd.base_currency = x.Cells[6].FormattedValue.ToString();
                        toAdd.quote_currency = x.Cells[7].FormattedValue.ToString();
                        //toAdd.maker_commission = Convert.ToInt64(x.Cells[8].FormattedValue);
                        //toAdd.taker_commission = Convert.ToInt64(x.Cells[9].FormattedValue);
                        plotta.Add(toAdd);
                    }
                    paintDotsWithDuration(plotta);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void drawInactive_Click(object sender, EventArgs e)
        {
            try
            {
                fillGrid(inactiveOptionsBTC);
                paintDotsWithDuration(inactiveOptionsBTC);
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
                fillGrid(inactiveOptionsBTC);
                paintDots(inactiveOptionsBTC);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void loadAPI()
        {
            //Get supported currencies and options for BTC
            currencies = API.Deribit.getCurrencies();
            activeOptionsBTC = API.Deribit.getInstrumentsWA("BTC", "option", false);
            inactiveOptionsBTC = API.Deribit.getInstrumentsWA("BTC", "option", true);

            Save.currencies(currencies);
            Save.activeOptionsBTC(activeOptionsBTC);
            Save.inactiveOptionsBTC(inactiveOptionsBTC);
        }
        private void fillGrid(List<Instrument> input)
        {
            try
            {
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
                foreach (var x in input)
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
                    two.Value = Helper.unixToDateTime(x.creation_timestamp / 1000).ToShortDateString();
                    tempRow.Cells.Add(two);
                    DataGridViewCell three = new DataGridViewTextBoxCell();
                    three.Value = Helper.unixToDateTime(x.expiration_timestamp / 1000).ToShortDateString();
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
        private void paintDots(List<Instrument> input)
        {
            var minmax = Instrument.getMinMax(input);
            var minDateTime = minmax.Item1;
            var maxDateTime = minmax.Item2;
            PlotModel model = new PlotModel
            {
                LegendSymbolLength = 20,
                LegendTitle = "Legend",
                LegendPosition = LegendPosition.LeftTop,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendBorder = OxyColors.Black
            };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Strike Price USD" });
            model.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "Time",
                IntervalType = DateTimeIntervalType.Days,
                Minimum = DateTimeAxis.ToDouble(Helper.unixToDateTime(minDateTime / 1000)),
                Maximum = DateTimeAxis.ToDouble(Helper.unixToDateTime(maxDateTime / 1000)),
            });
            LineSeries lineserieCall = new LineSeries
            {
                Title = "Call",
                DataFieldX = "x",
                DataFieldY = "Y",
                StrokeThickness = 2,
                MarkerStrokeThickness = 2,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Black,
                LineStyle = LineStyle.None,
                Color = OxyColors.Black,
                MarkerType = MarkerType.Cross,
            };
            LineSeries lineseriePut = new LineSeries
            {
                Title = "Put",
                DataFieldX = "x",
                DataFieldY = "Y",
                StrokeThickness = 2,
                MarkerSize = 2,
                LineStyle = LineStyle.None,
                Color = OxyColors.Red,
                MarkerType = MarkerType.Circle,
            };
            foreach (var x in input)
                if (x.option_type == "call")
                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.creation_timestamp / 1000)), x.strike));
                else
                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.creation_timestamp / 1000)), x.strike));

            model.Series.Add(lineserieCall);
            model.Series.Add(lineseriePut);
            this.plotView1.Model = model;
        }
        private void paintDotsWithDuration(List<Instrument> input)
        {
            var minmax = Instrument.getMinMax(input);
            var minDateTime = minmax.Item1;
            var maxDateTime = minmax.Item2;
            PlotModel model = new PlotModel
            {
                LegendSymbolLength = 20,
                LegendTitle = "Legend",
                LegendPosition = LegendPosition.LeftTop,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendBorder = OxyColors.Black
            };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Strike Price USD" });
            model.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "Time",
                IntervalType = DateTimeIntervalType.Days,
                Minimum = DateTimeAxis.ToDouble((new DateTime(2018, 05, 16, 11, 0, 0, DateTimeKind.Utc))),
                Maximum = DateTimeAxis.ToDouble((new DateTime(2018, 07, 15, 11, 0, 0, DateTimeKind.Utc)))
            });
            foreach (var x in input)
                if (x.option_type == "call")
                {
                    LineSeries lineserieCall = new LineSeries
                    {
                        DataFieldX = "x",
                        DataFieldY = "Y",
                        StrokeThickness = 2,
                        MarkerStrokeThickness = 2,
                        MarkerSize = 4,
                        MarkerStroke = OxyColors.Black,
                        LineStyle = LineStyle.Solid,
                        Color = OxyColors.Black,
                        MarkerType = MarkerType.Cross,
                    };
                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.creation_timestamp / 1000)), x.strike));
                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.expiration_timestamp / 1000)), x.strike));
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
                        LineStyle = LineStyle.DashDot,
                        Color = OxyColors.Red,
                        MarkerType = MarkerType.Circle,
                    };
                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.creation_timestamp / 1000)), x.strike));
                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.expiration_timestamp / 1000)), x.strike));
                    model.Series.Add(lineseriePut);
                }
            // Add empty lineseries for Legend.
            LineSeries lineseriePut2 = new LineSeries
            {
                Title = "Put",
                DataFieldX = "x",
                DataFieldY = "Y",
                StrokeThickness = 2,
                MarkerSize = 2,
                LineStyle = LineStyle.DashDot,
                Color = OxyColors.Red,
                MarkerType = MarkerType.Circle,
            };
            LineSeries lineserieCall2 = new LineSeries
            {
                Title = "Call",
                DataFieldX = "x",
                DataFieldY = "Y",
                StrokeThickness = 2,
                MarkerStrokeThickness = 2,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Black,
                LineStyle = LineStyle.Solid,
                Color = OxyColors.Black,
                MarkerType = MarkerType.Cross,
            };
            model.Series.Add(lineseriePut2);
            model.Series.Add(lineserieCall2);
            this.plotView1.Model = model;
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
