using MasterComputations.Classes;
using MasterComputations.Computations;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Windows.Forms;

namespace MasterComputations.Visualization
{
    public class Plot
    {
        public static PlotModel option(List<Option> _input)
        {
            try
            {
                var input = new List<Option>();
                foreach (var x in _input)
                    if (!(x.trades.Count < 555))
                        input.Add(x);
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
                            MarkerStroke = OxyColors.Black,
                            LineStyle = LineStyle.None,
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
                            MarkerStroke = OxyColors.Black,
                            LineStyle = LineStyle.None,
                            Color = OxyColors.Black,
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


        //        private void paintDots(List<Instrument> input)
        //        {
        //            var minmax = Instrument.getMinMax(input);
        //            var minDateTime = minmax.Item1;
        //            var maxDateTime = minmax.Item2;
        //            PlotModel model = new PlotModel
        //            {
        //                LegendSymbolLength = 20,
        //                LegendTitle = "Legend",
        //                LegendPosition = LegendPosition.LeftTop,
        //                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
        //                LegendBorder = OxyColors.Black
        //            };
        //            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Strike Price USD" });
        //            model.Axes.Add(new DateTimeAxis()
        //            {
        //                Position = AxisPosition.Bottom,
        //                Title = "Time",
        //                IntervalType = DateTimeIntervalType.Days,
        //                Minimum = DateTimeAxis.ToDouble(Helper.unixToDateTime(minDateTime / 1000)),
        //                Maximum = DateTimeAxis.ToDouble(Helper.unixToDateTime(maxDateTime / 1000)),
        //            });
        //            LineSeries lineserieCall = new LineSeries
        //            {
        //                Title = "Call",
        //                DataFieldX = "x",
        //                DataFieldY = "Y",
        //                StrokeThickness = 2,
        //                MarkerStrokeThickness = 2,
        //                MarkerSize = 4,
        //                MarkerStroke = OxyColors.Black,
        //                LineStyle = LineStyle.None,
        //                Color = OxyColors.Black,
        //                MarkerType = MarkerType.Cross,
        //            };
        //            LineSeries lineseriePut = new LineSeries
        //            {
        //                Title = "Put",
        //                DataFieldX = "x",
        //                DataFieldY = "Y",
        //                StrokeThickness = 2,
        //                MarkerSize = 2,
        //                LineStyle = LineStyle.None,
        //                Color = OxyColors.Red,
        //                MarkerType = MarkerType.Circle,
        //            };
        //            foreach (var x in input)
        //                if (x.option_type == "call")
        //                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.creation_timestamp / 1000)), x.strike));
        //                else
        //                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.creation_timestamp / 1000)), x.strike));

        //            model.Series.Add(lineserieCall);
        //            model.Series.Add(lineseriePut);
        //            this.plotView1.Model = model;
        //        }
        //        private void paintDotsWithDuration(List<Instrument> input)
        //        {
        //            var minmax = Instrument.getMinMax(input);
        //            var minDateTime = minmax.Item1;
        //            var maxDateTime = minmax.Item2;
        //            PlotModel model = new PlotModel
        //            {
        //                LegendSymbolLength = 20,
        //                LegendTitle = "Legend",
        //                LegendPosition = LegendPosition.LeftTop,
        //                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
        //                LegendBorder = OxyColors.Black
        //            };
        //            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Strike Price USD" });
        //            model.Axes.Add(new DateTimeAxis()
        //            {
        //                Position = AxisPosition.Bottom,
        //                Title = "Time",
        //                IntervalType = DateTimeIntervalType.Days,
        //                Minimum = DateTimeAxis.ToDouble((new DateTime(2018, 05, 16, 11, 0, 0, DateTimeKind.Utc))),
        //                Maximum = DateTimeAxis.ToDouble((new DateTime(2018, 07, 15, 11, 0, 0, DateTimeKind.Utc)))
        //            });
        //            foreach (var x in input)
        //                if (x.option_type == "call")
        //                {
        //                    LineSeries lineserieCall = new LineSeries
        //                    {
        //                        DataFieldX = "x",
        //                        DataFieldY = "Y",
        //                        StrokeThickness = 2,
        //                        MarkerStrokeThickness = 2,
        //                        MarkerSize = 4,
        //                        MarkerStroke = OxyColors.Black,
        //                        LineStyle = LineStyle.Solid,
        //                        Color = OxyColors.Black,
        //                        MarkerType = MarkerType.Cross,
        //                    };
        //                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.creation_timestamp / 1000)), x.strike));
        //                    lineserieCall.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.expiration_timestamp / 1000)), x.strike));
        //                    model.Series.Add(lineserieCall);
        //                }
        //                else
        //                {
        //                    LineSeries lineseriePut = new LineSeries
        //                    {
        //                        DataFieldX = "x",
        //                        DataFieldY = "Y",
        //                        StrokeThickness = 2,
        //                        MarkerSize = 2,
        //                        LineStyle = LineStyle.DashDot,
        //                        Color = OxyColors.Red,
        //                        MarkerType = MarkerType.Circle,
        //                    };
        //                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.creation_timestamp / 1000)), x.strike));
        //                    lineseriePut.Points.Add(new DataPoint(DateTimeAxis.ToDouble(Helper.unixToDateTime(x.expiration_timestamp / 1000)), x.strike));
        //                    model.Series.Add(lineseriePut);
        //                }
        //            // Add empty lineseries for Legend.
        //            LineSeries lineseriePut2 = new LineSeries
        //            {
        //                Title = "Put",
        //                DataFieldX = "x",
        //                DataFieldY = "Y",
        //                StrokeThickness = 2,
        //                MarkerSize = 2,
        //                LineStyle = LineStyle.DashDot,
        //                Color = OxyColors.Red,
        //                MarkerType = MarkerType.Circle,
        //            };
        //            LineSeries lineserieCall2 = new LineSeries
        //            {
        //                Title = "Call",
        //                DataFieldX = "x",
        //                DataFieldY = "Y",
        //                StrokeThickness = 2,
        //                MarkerStrokeThickness = 2,
        //                MarkerSize = 4,
        //                MarkerStroke = OxyColors.Black,
        //                LineStyle = LineStyle.Solid,
        //                Color = OxyColors.Black,
        //                MarkerType = MarkerType.Cross,
        //            };
        //            model.Series.Add(lineseriePut2);
        //            model.Series.Add(lineserieCall2);
        //            this.plotView1.Model = model;
        //        }
        //    }
        //}
        public void model()
        {

        }
    }
}
