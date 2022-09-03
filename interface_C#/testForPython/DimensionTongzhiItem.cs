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
    public partial class DimensionTongzhiItem : UserControl
    {
        public bool isChecked = false;
        public int dimension_Num = 0;
        public DimensionTongzhiItem()
        {
            InitializeComponent();
        }

        public void SetName(string name)
        {
            m_lbDimensionName.Text = name;
        }

        public void SetNum(int num)
        {
            dimension_Num = num;
        }
        public int GetNum()
        {
            return dimension_Num;
        }
        public bool GetHomoDimension()
        {
            return isChecked;
        }
        private void m_btTongzhi_Click(object sender, EventArgs e)
        {
            if (isChecked)
            {
                isChecked = false;
                m_pbBack.Image = Properties.Resources.tongzhiItem1;
                m_btTongzhi.Image = Properties.Resources.tongzhiItem3;
            }
            else
            {
                isChecked = true;
                m_pbBack.Image = Properties.Resources.tongzhiItem2;
                m_btTongzhi.Image = Properties.Resources.tongzhiItem4;
            }
        }
    }
}
