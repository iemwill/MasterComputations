using MasterComputations.Classes;
using MasterComputations.Computations;
using MasterComputations.Data;
using MasterComputations.Visualization;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;

namespace MasterComputations
{
    public partial class pingDataForm : Form
    {
        public Dictionary<string, Option> btcOptions;
        public static int minute = 60000;
        public int intervall = 15 * minute;
        private static System.Timers.Timer aTimer;
        public pingDataForm()
        {
            InitializeComponent();
            //get init Data
            init();
            //Set the timer
            aTimer = new System.Timers.Timer(intervall);
            aTimer.Elapsed += new ElapsedEventHandler(update);
            aTimer.Enabled = true;
        }
        private void init()
        {
            btcOptions = new Dictionary<string, Option>();
            //get all active traded options on BTC
            var activeOptions = API.Deribit.getInstrumentsWA();
            //fill data with raw calldata
            foreach (var x in activeOptions)
            {
                Option add = new Option();
                add.raw = x;
                add.name = x.instrument_name;
                add.start = Helper.unixToDateTime(x.creation_timestamp / 1000);
                add.end = Helper.unixToDateTime(x.expiration_timestamp / 1000);
                add.active = true;
                add.orderBook.Add(API.Deribit.getBook(x.instrument_name));
                btcOptions.Add(x.instrument_name, add);
            }
            Save.options(btcOptions);
        }
        private void update(object source, ElapsedEventArgs e)
        {
            try
            {
                var newActive = API.Deribit.getInstrumentsWA();
                //get Order Book or add new option and add first book entry.
                foreach (var x in newActive)
                {
                    if (btcOptions.ContainsKey(x.instrument_name))
                        btcOptions[x.instrument_name].orderBook.Add(API.Deribit.getBook(x.instrument_name));
                    else
                    {
                        Option add = new Option();
                        add.raw = x;
                        add.name = x.instrument_name;
                        add.start = Helper.unixToDateTime(x.creation_timestamp / 1000);
                        add.end = Helper.unixToDateTime(x.expiration_timestamp / 1000);
                        add.active = true;
                        add.orderBook.Add(API.Deribit.getBook(x.instrument_name));
                        btcOptions.Add(x.instrument_name, add);
                    }
                }
                // check if some are inactive now
                foreach (var x in btcOptions.Values)
                {
                    var check = false;
                    foreach (var y in newActive)
                        if (y.instrument_name == x.name)
                            check = true;
                    if (!check)
                        btcOptions[x.name].active = false;
                }
                Save.options(btcOptions);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in update from pingDataForm. \n " + ex.Message);
            }
        }
        private void updateGridButton_Click(object sender, EventArgs e)
        {
            //update dataGrid
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "isActive";
            dataGridView1.Columns[2].Name = "updates";
            dataGridView1.Rows.Clear();
            var check = Grid.updates(btcOptions);
            foreach (var x in check)
                dataGridView1.Rows.Add(x);
        }
    }
}
