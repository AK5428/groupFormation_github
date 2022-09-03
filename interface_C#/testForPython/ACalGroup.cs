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
    public partial class ACalGroup : UserControl
    {
        public Form1 fo = new Form1("empty");
        public double homoNum = 0D;
        public double heterNum = 0D;
        public ACalGroup(string res)
        {
            InitializeComponent();
            string[] aGroup = res.Split(',');
            for (int j = 0; j < aGroup.Length; j++)
            {
                if (j == 0)
                {
                    homoNum = double.Parse(aGroup[j].Trim());
                    m_lbGroupHomo.Text = fo.GetSubNum(homoNum, 2);
                }
                else if (j == 1)
                {
                    heterNum = double.Parse(aGroup[j].Trim());
                    m_lbGroupHeter.Text = fo.GetSubNum(heterNum, 2);
                }
                else
                {
                    string[] student = aGroup[j].Split('&');
                    AStudent aStudent = new AStudent();
                    aStudent.SetNum(student[0].Trim());
                    aStudent.SetName(student[1].Trim());
                    m_flPanel.Controls.Add(aStudent);
                }
            }
        }

        public double GetHomoNum()
        {
            return homoNum;
        }
        public double GetHeterNum()
        {
            return heterNum;
        }
    }
}
