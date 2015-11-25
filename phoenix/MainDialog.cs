using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phoenix
{
    public partial class MainDialog : Form
    {
        public MainDialog()
        {
            InitializeComponent();
        }

        private void apps_MouseEnter(object sender, EventArgs e)
        {
            status_strip_text.Text = "Drag and Drop and .exe or a .bat file to watch it.";
        }

        private void remote_Click(object sender, EventArgs e)
        {

        }
    }
}
