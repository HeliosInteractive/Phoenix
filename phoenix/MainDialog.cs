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
        private IniSettings m_AppSettings;
        public MainDialog()
        {
            m_AppSettings = new IniSettings("phoenix.ini");
            InitializeComponent();
        }
    }
}
