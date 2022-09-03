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
    public partial class studentMini : UserControl
    {
        public studentMini()
        {
            InitializeComponent();
        }
        public void SetID(string id)
        {
            m_lbID.Text = id;
        }
        public void SetName(string name)
        {
            m_lbName.Text = name;
        }
    }
}
