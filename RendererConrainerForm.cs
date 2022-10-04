using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoGraphSample
{
    public partial class RendererConrainerForm : Form
    {

        public RendererConrainerForm(ushort name)
        {
            InitializeComponent(name);
        }
    }
}
