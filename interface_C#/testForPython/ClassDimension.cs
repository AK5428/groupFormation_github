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
    public partial class ClassDimension : UserControl
    {
        
        public ClassDimension()
        {
            InitializeComponent();
        }

        public void SetLength(int w)
        {
            this.Width = w;
        }

        public void SetImage(Image im)
        {
            m_pbDim.Image = im;
        }
        public void SetName(string name)
        {
            m_lbName.Text = name;
        }

    }
}
