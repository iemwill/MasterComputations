using System.Windows.Forms;

namespace MasterComputations
{
    public partial class tradingForm : Form
    {
        private string name = "";
        private string passw = "";
        public tradingForm(string _name, string _passw)
        {
            InitializeComponent();
            name = _name;
            passw = _passw;
        }
    }
}
