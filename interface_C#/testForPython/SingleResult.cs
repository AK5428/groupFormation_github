using System;
using System.Drawing;
using System.Windows.Forms;

namespace testForPython
{
    public partial class SingleResult : UserControl
    {
        public int singleResult_ID = 0;
        public bool is_thisOne = false;
        public Form1 fo = new Form1("empty");
        public delegate void MyDelegate(object sender, EventArgs e);
        public event MyDelegate myEvent;
        //亲密度,MBTI | 擅长知识点,专业,年龄 | 6.636656211439511,8.59748028776696,
        //0.008465198285452958 | 1203465 & wengxiaomin,1203422 & linjunting,
        //1203344 & yangyuke,1203351 & huzhouning
        public SingleResult(string para)
        {
            InitializeComponent();
            string[] paras = para.Split('|');
            SetHomos(paras[0].Split(','));
            SetHeters(paras[1].Split(','));
            SetParaNums(paras[2].Split(','));
            SetStudents(paras[3].Split(','));

            m_pbDetails.Click += new EventHandler(btnTest_Click);

        }
        void btnTest_Click(object sender, EventArgs e)
        {
            //将自定义事件绑定到控件事件上
            if (myEvent != null)
            {
                myEvent(sender, e);
            }
        }

        public void SetHomos(string[] homos)
        {
            m_flHomo.Controls.Clear();
            foreach (var aHomo in homos)
            {
                if (aHomo.Trim() != "")
                {
                    ItemLabel lb = new ItemLabel();
                    lb.SetBackColor(Color.FromArgb(236, 252, 241));
                    lb.SetText(aHomo.Trim());
                    m_flHomo.Controls.Add(lb);
                }
            }
        }
        public void SetHeters(string[] heters)
        {
            m_flHeter.Controls.Clear();
            foreach (var aHeter in heters)
            {
                if (aHeter.Trim() != "")
                {
                    ItemLabel lb = new ItemLabel();
                    lb.SetBackColor(Color.FromArgb(255, 235, 244));
                    lb.SetText(aHeter.Trim());
                    m_flHeter.Controls.Add(lb);
                }
            }
        }
        public void SetParaNums(string[] paraNumbs)
        {
            double Num_HomoDis = double.Parse(paraNumbs[0].Trim());
            double Num_HeterDis = double.Parse(paraNumbs[1].Trim());
            double Num_groupVariance = double.Parse(paraNumbs[2].Trim());
            m_lbHomoDis.Text = GetSubNum(Num_HomoDis, 2);
            m_lbHeterDis.Text = GetSubNum(Num_HeterDis, 2);
            m_lbgroupVariance.Text = GetSubNum(Num_groupVariance, 4);
        }

        public void SetStudents(string[] group)
        {
            m_flStudents.Controls.Clear();
            foreach (var person in group)
            {
                if (person.Trim() != "")
                {
                    string[] aGuy = person.Split('&');
                    studentMini sm = new studentMini();
                    sm.SetID(aGuy[0].Trim());
                    sm.SetName(aGuy[1].Trim());
                    m_flStudents.Controls.Add(sm);
                }
            }
        }
        private void m_pbDetails_Click(object sender, EventArgs e)
        {
            is_thisOne = true;
        }

        private void m_pbDeleteItem_Click(object sender, EventArgs e)
        {
            fo.DeleteItem(singleResult_ID.ToString());
            this.Dispose();
        }
        public string GetSubNum(double num, int count)
        {
            string ori = num.ToString();
            int counts = ori.Length;
            int tem = ori.IndexOf(".");
            if (tem > 0)
            {
                if (counts - tem - 1 > count)
                {
                    string res = ori.Substring(0, tem + count + 1);
                    return res;
                }
                else
                {
                    return ori;
                }
            }
            else
            {
                return ori;
            }
        }
        public void SetId(int count)
        {
            singleResult_ID = count;
            if (count >= 10)
            {
                m_lbCount.Location = new Point(31, 68);
            }
            else
            {
                m_lbCount.Location = new Point(38, 68);
            }
            int theCount = count+1;
            m_lbCount.Text = theCount.ToString();
            m_lbCount.Parent = this.pictureBox1;
            m_lbCount.BackColor = Color.Transparent;
        }
    }
}
