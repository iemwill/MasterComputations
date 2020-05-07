using System;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using System.Windows.Forms;

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
                this.DialogResult = DialogResult.OK;
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
