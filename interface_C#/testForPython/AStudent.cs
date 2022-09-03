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
    public partial class AStudent : UserControl
    {
        public AStudent()
        {
            InitializeComponent();
        }

        public void SetNum(string num)
        {
            m_lbNum.Text = num;
        }

        public void SetName(string name)
        {
            m_lbName.Text = name;
        }
    }
}
