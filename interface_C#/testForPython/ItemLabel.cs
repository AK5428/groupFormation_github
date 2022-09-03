using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testForPython
{
    public partial class ItemLabel : UserControl
    {
        public ItemLabel()
        {
            InitializeComponent();
        }
        public void SetText(string text)
        {
            m_lb.Text = text;
        }
        public void SetBackColor(Color co)
        {
            this.BackColor = co;
        }
    }
}
