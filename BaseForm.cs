using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MasterComputations.Data;
using Newtonsoft.Json;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;

namespace MasterComputations
{
    public partial class BaseForm : Form
    {
        public List<Currency> currencies;
        public BaseForm()
        {
            InitializeComponent();
            currencies = API.Deribit.getCurrencies();
            Debug.WriteLine(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///Parse data
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OptionData data = new OptionData();
                    data.rawData = Parse.csv(openFileDialog1.FileName, ';');


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
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                // Configure HTTP basic authorization: bearerAuth
                //Configuration.Default.Username = textBox1.Text;
                //Configuration.Default.Password = textBox2.Text;
                var apiInstance = new PublicApi(Configuration.Default);
                // Change the user name for a subaccount
                Object result = apiInstance.PublicGetTimeGet();
                Debug.WriteLine(result);
            }
            catch (Exception)
            {
                throw;
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
    }
}
