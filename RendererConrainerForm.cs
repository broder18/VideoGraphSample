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
        private int _oldLen;

        public RendererConrainerForm(ushort name)
        {
            InitializeComponent(name);
            _oldLen = this.Size.Width;
        }

        private void RendererContainerForm_Resize(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;

                if (control.Size.Height != control.Size.Width)
                {
                    this.Size = new System.Drawing.Size(control.Size.Width, control.Size.Width);
                }

                _oldLen = this.Size.Height;
                Dll.Resize(Handle);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
