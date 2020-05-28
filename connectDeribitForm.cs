using System;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using System.Windows.Forms;
using BOPcomputations;

namespace MasterComputations
{
    public partial class connectDeribitForm : Form
    {
        public string name;
        public string passW;
        public connectDeribitForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO still not checking if connection established!!!
            try
            {
                Configuration.Default.BasePath = "https://www.deribit.com/api/v2";
                // Configure HTTP basic authorization: bearerAuth
                Configuration.Default.Username = textBox1.Text;
                Configuration.Default.Password = textBox2.Text;
                var apiInstance = new PublicApi(Configuration.Default);
                // Change the user name for a subaccount
                Object result = apiInstance.PublicGetTimeGet();
                Debug.WriteLine(result);
                name = textBox1.Text;
                passW = textBox2.Text;
                this.Hide();
                Form tradingForm = new tradingForm(name, passW);
                tradingForm.ShowDialog();
                this.Close();
            }
            catch (ApiException err)
            {
                Debug.Print("Exception when calling AccountManagementApi.PrivateChangeSubaccountNameGet: " + err.Message);
                Debug.Print("Status Code: " + err.ErrorCode);
                Debug.Print(err.StackTrace);
            }
        }
    }
}
