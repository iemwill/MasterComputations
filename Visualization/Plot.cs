using OptionPricing.Classes;
using OptionPricing.Computations;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OptionPricing.Visualization
{
    public class Plot
    {
        public static PlotModel option(List<Option> input)
        {
            try
            {
                var minmax = Option.getDateEx(input);
                PlotModel model = new PlotModel
                {
                    LegendSymbolLength = 20,
                    LegendTitle = "Legend",
                    LegendPosition = LegendPosition.LeftTop,
                    LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                    LegendBorder = OxyColors.Black
                };
                model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Strike Price USD + Traded USD amount" });
                model.Axes.Add(new DateTimeAxis()
                {
                    Position = AxisPosition.Bottom,
                    Title = "Time",
                    IntervalType = DateTimeIntervalType.Days,
                    Minimum = DateTimeAxis.ToDouble(Helper.unixToDateTime(minmax.Item1 / 1000)),//DateTimeAxis.ToDouble((new DateTime(2018, 05, 16, 11, 0, 0, DateTimeKind.Utc))),
                    Maximum = DateTimeAxis.ToDouble(Helper.unixToDateTime(minmax.Item2 / 1000))//DateTimeAxis.ToDouble((new DateTime(2018, 07, 15, 11, 0, 0, DateTimeKind.Utc)))
                });
                foreach (var x in input)
                    if (x.raw.option_type == "call")
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
                        lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.raw.creation_timestamp / 1000)), x.raw.strike));
                        lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.raw.expiration_timestamp / 1000)), x.raw.strike));
                        model.Series.Add(lineserieCall);

                        LineSeries lineserieCallTrades = new LineSeries
                        {
                            DataFieldX = "x",
                            DataFieldY = "Y",
                            StrokeThickness = 2,
                            MarkerStrokeThickness = 2,
                            MarkerSize = 4,
                            MarkerStroke = OxyColors.Gold,
                            LineStyle = LineStyle.Solid,
                            Color = OxyColors.Black,
                            MarkerType = MarkerType.Plus,
                        };
                        foreach (var y in x.trades)
                            lineserieCallTrades.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(y.timestamp / 1000)), x.raw.strike + y.amount * y.index_price * y.price));
                        model.Series.Add(lineserieCallTrades);
                    }
                    else
                    {
                        LineSeries lineseriePut = new LineSeries
                        {
                            DataFieldX = "x",
                            DataFieldY = "Y",
                            StrokeThickness = 2,
                            MarkerSize = 2,
                            MarkerStroke = OxyColors.Red,
                            LineStyle = LineStyle.DashDot,
                            Color = OxyColors.Red,
                            MarkerType = MarkerType.Circle,
                        };
                        lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.raw.creation_timestamp / 1000)), x.raw.strike));
                        lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.raw.expiration_timestamp / 1000)), x.raw.strike));
                        model.Series.Add(lineseriePut);
                        LineSeries lineseriePutTrades = new LineSeries
                        {
                            DataFieldX = "x",
                            DataFieldY = "Y",
                            StrokeThickness = 2,
                            MarkerStrokeThickness = 2,
                            MarkerSize = 4,
                            MarkerStroke = OxyColors.Gold,
                            LineStyle = LineStyle.Solid,
                            Color = OxyColors.Red,
                            MarkerType = MarkerType.Plus,
                        };
                        foreach (var y in x.trades)
                        {
                            lineseriePutTrades.Points.Add(
                                new DataPoint(
                                    DateTimeAxis.ToDouble(
                                        Helper.unixToDateTime(y.timestamp / 1000)),
                                        x.raw.strike + y.amount * y.index_price * y.price
                                    )
                                );
                        }
                        model.Series.Add(lineseriePutTrades);
                    }
                // Add empty lineseries for Legend.
                LineSeries trade = new LineSeries
                {
                    Title = "Trade",
                    DataFieldX = "x",
                    DataFieldY = "Y",
                    StrokeThickness = 2,
                    MarkerSize = 2,
                    MarkerStroke = OxyColors.Gold,
                    LineStyle = LineStyle.None,
                    MarkerType = MarkerType.Plus,
                };
                LineSeries lineseriePut2 = new LineSeries
                {
                    Title = "Put",
                    DataFieldX = "x",
                    DataFieldY = "Y",
                    StrokeThickness = 2,
                    MarkerSize = 2,
                    MarkerStroke = OxyColors.Red,
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
                model.Series.Add(trade);
                model.Series.Add(lineseriePut2);
                model.Series.Add(lineserieCall2);
                return model;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                throw;
            }
        }
        public void model()
        {

        }
    }
}
