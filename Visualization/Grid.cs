using MasterComputations.Classes;
using MasterComputations.Computations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterComputations.Visualization
{
    public class Grid
    {
        public static List<DataGridViewRow> options(List<Option> input)
        {
            try
            {
                var check = new List<DataGridViewRow>();
                foreach (var x in input)
                {//add code here for adding rows to dataGridviewFiles
                    DataGridViewRow tempRow = new DataGridViewRow();

                    DataGridViewCell cellFileName = new DataGridViewTextBoxCell();
                    cellFileName.Value = x.raw.option_type;
                    tempRow.Cells.Add(cellFileName);
                    DataGridViewCell cellDocCount = new DataGridViewTextBoxCell();
                    cellDocCount.Value = x.raw.settlement_period;
                    tempRow.Cells.Add(cellDocCount);
                    DataGridViewCell cellPageCount = new DataGridViewTextBoxCell();
                    cellPageCount.Value = x.raw.strike;
                    tempRow.Cells.Add(cellPageCount);
                    DataGridViewCell one = new DataGridViewTextBoxCell();
                    one.Value = x.raw.instrument_name;
                    tempRow.Cells.Add(one);
                    DataGridViewCell two = new DataGridViewTextBoxCell();
                    two.Value = Helper.unixToDateTime(x.raw.creation_timestamp / 1000).ToShortDateString();
                    tempRow.Cells.Add(two);
                    DataGridViewCell three = new DataGridViewTextBoxCell();
                    three.Value = Helper.unixToDateTime(x.raw.expiration_timestamp / 1000).ToShortDateString();
                    tempRow.Cells.Add(three);
                    DataGridViewCell four = new DataGridViewTextBoxCell();
                    four.Value = x.raw.base_currency;
                    tempRow.Cells.Add(four);
                    DataGridViewCell fiv = new DataGridViewTextBoxCell();
                    fiv.Value = x.raw.quote_currency;
                    tempRow.Cells.Add(fiv);
                    DataGridViewCell ser = new DataGridViewTextBoxCell();
                    ser.Value = x.raw.maker_commission;
                    tempRow.Cells.Add(ser);
                    DataGridViewCell dd = new DataGridViewTextBoxCell();
                    dd.Value = x.raw.taker_commission;
                    tempRow.Cells.Add(dd);

                    tempRow.Tag = x.raw.is_active;
                    check.Add(tempRow);
                }
                return check;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in Plot.grid(List<Option>). \n", e.Message);
                throw;
            }
        }

        public static List<DataGridViewRow> trades(Option input)
        {
            try
            {
                var check = new List<DataGridViewRow>();
                foreach (var x in input.trades)
                {//add code here for adding rows to dataGridviewFiles
                    DataGridViewRow tempRow = new DataGridViewRow();

                    DataGridViewCell cellFileName = new DataGridViewTextBoxCell();
                    cellFileName.Value = x.direction;
                    tempRow.Cells.Add(cellFileName);
                    DataGridViewCell cellFileName2 = new DataGridViewTextBoxCell();
                    cellFileName2.Value = x.amount * x.index_price * x.price;
                    tempRow.Cells.Add(cellFileName2);
                    DataGridViewCell cellFileName3 = new DataGridViewTextBoxCell();
                    cellFileName3.Value = Helper.unixToDateTime(x.timestamp / 1000).ToShortDateString();
                    tempRow.Cells.Add(cellFileName3);
                    DataGridViewCell cellFileName4 = new DataGridViewTextBoxCell();
                    cellFileName4.Value = x.implied_volatility;
                    tempRow.Cells.Add(cellFileName4);
                    tempRow.Tag = input.active;

                    check.Add(tempRow);
                }
                return check;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in Plot.grid(Option). \n", e.Message);
                throw;
            }
        }
    }
}
