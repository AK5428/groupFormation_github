using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testForPython
{
    public partial class ProcessBarForm : Form
    {
        public ProcessBarForm()
        {
            InitializeComponent();
            Innitial();
        }
        public void Innitial()
        {
            m_lbpresent.Text = "0 %";
            m_pbProcessBar.Width = 8;
            m_pbBack.Image = Properties.Resources.Frame_81;
            m_lbpresent.Visible = true;
            m_pbProcessBar.Visible = true;
            m_pbConform.Visible = false;
        }

        public void ClaFinished()
        {
            m_pbBack.Image = Properties.Resources.Frame_82;
            m_lbpresent.Visible = false;
            m_pbProcessBar.Visible = false;
            m_pbConform.Visible = true ;
        }

        public void SetLable(string st)
        {
            m_lbpresent.Text = st;
        }
        //进度条长度8 =1%，433=100%
        public void SetWidth(double num)
        {
            double w1 = 425 * num;
            int w = (int)w1;
            w += 8;
            m_pbProcessBar.Width = w;
        }
        #region 窗体圆角的实现
        private void m_pbBack_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                SetWindowRegion();
            }
            else
            {
                this.Region = null;
            }
        }

        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 70);
            this.Region = new Region(FormPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">窗体大小</param>
        /// <param name="radius">圆角大小</param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);//左上角

            arcRect.X = rect.Right - diameter;//右上角
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;// 右下角
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;// 左下角
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion
        private void m_pbConform_Click(object sender, EventArgs e)
        {
            Form1 fo = (Form1)this.Owner;
            fo.GetCalDetailRes(-1);
            fo.ReDrawResItems();
            fo.curNum = -1;

            fo.public_m_btClubResult_Click();
            this.Dispose();
        }

       
    }
}
