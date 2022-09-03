using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace testForPython
{
    
    public partial class Form1 : Form
    {
        public class dimesionSet
        {
            public string name = "";
            public string name_Ch = "";
            public bool homoDimension = false;
            public bool is_checked = false;
        }
        public dimesionSet[] m_dimensions = new dimesionSet[15];
        public int classMemberCount = 0;
        public bool needSearchProcess = true;
        public string[] m_Oridimension = new string[30];
        int dimensionCount = 0;
        public int[] m_tongzhi_checked = new int[15];
        public bool m_hopeBalance = false;
        public string urlTital = "http://127.0.0.1:8000/";
        public int curNum = -1;
        
        public Dictionary<int, Tuple<int,Image>> dic_forClass = new Dictionary<int, Tuple<int, Image>>();
       
        public bool has_preRes = false;


        
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            Tuple<int, Image> t1 = new Tuple<int, Image>(15, Properties.Resources.classDimension1);
            dic_forClass[1] = t1;
            Tuple<int, Image> t2 = new Tuple<int, Image>(49, Properties.Resources.classDimension2);
            dic_forClass[2] = t2;
            Tuple<int, Image> t3 = new Tuple<int, Image>(62, Properties.Resources.classDimension3);
            dic_forClass[3] = t3;
            Tuple<int, Image> t4 = new Tuple<int, Image>(76, Properties.Resources.classDimension4);
            dic_forClass[4] = t4;
            Tuple<int, Image> t5 = new Tuple<int, Image>(90, Properties.Resources.classDimension5);
            dic_forClass[5] = t5; 
            Tuple<int, Image> t6 = new Tuple<int, Image>(104, Properties.Resources.classDimension6);
            dic_forClass[6] = t6;
            Tuple<int, Image> t7 = new Tuple<int, Image>(118, Properties.Resources.classDimension7);
            dic_forClass[7] = t7;
            Tuple<int, Image> t8 = new Tuple<int, Image>(132, Properties.Resources.classDimension8);
            dic_forClass[8] = t8;
            Tuple<int, Image> t9 = new Tuple<int, Image>(146, Properties.Resources.classDimension9);
            dic_forClass[9] = t9;
            Tuple<int, Image> t10 = new Tuple<int, Image>(160, Properties.Resources.classDimension10);
            dic_forClass[10] = t10;

            GetClassInfo();
            string res = GetGroupRes();
            if (res == "")
            {
                //没东西，给新的界面
                m_btResultList_Click(null, null);
                tabControl1.SelectedTab = tabPage1;
            }
            else if (res == "Failed")
            {
                MessageBox.Show("服务器连接失败，请重新启动");
            }
            else
            {
                //展示之前数据
                has_preRes = true;
                string[] firstDeCode = res.Split('#');
                int num = 0;
                flowLayoutPanel5.Controls.Clear();
                foreach (var secondDecode in firstDeCode)
                {
                    SingleResult sr = new SingleResult(secondDecode);
                    sr.myEvent += new SingleResult.MyDelegate(userControl1_myEvent);
                    sr.SetId(num);
                    flowLayoutPanel5.Controls.Add(sr);
                    num++;
                }
                m_lbResList.Text = "共计结果: " + num.ToString() + "条";
                m_btResultList_Click(null, null);
                tabControl1.SelectedTab = tabPage5;
            }
            GetDimensionInfo();
            flowLayoutPanel3.Controls.Clear();
            for (int i=0;i< dimensionCount; i++)
            {
                ClassDimension cd = new ClassDimension();
                int length = SimuLength(m_dimensions[i].name_Ch);
                Tuple<int, Image> ab = dic_forClass[length];
                cd.SetLength(ab.Item1);
                cd.SetImage(ab.Item2);
                cd.SetName(m_dimensions[i].name_Ch);
                flowLayoutPanel3.Controls.Add(cd);
            }
        }

        public Form1(string time)
        {
            InitializeComponent();
        }
        public void ReDrawResItems()
        {
            string res = GetGroupRes();
            if (res == "Failed")
            {
                MessageBox.Show("服务器连接失败，请重新启动");
            }
            else
            {
                //展示之前数据
                has_preRes = true;
                string[] firstDeCode = res.Split('#');
                int num = 0;
                flowLayoutPanel5.Controls.Clear();
                foreach (var secondDecode in firstDeCode)
                {
                    SingleResult sr = new SingleResult(secondDecode);
                    sr.myEvent += new SingleResult.MyDelegate(userControl1_myEvent);
                    sr.SetId(num);
                    flowLayoutPanel5.Controls.Add(sr);
                    num++;
                }
                m_lbResList.Text = "共计结果: " + num.ToString() + "条";
                m_btResultList_Click(null, null);
                tabControl1.SelectedTab = tabPage5;
            }
        }
        public void GetClassInfo()
        {
            //获取班级信息;
            string url = urlTital + "getClass/";
            string log = "";//错误信息

            string jsonParams = "";
            string result = RequestsPost(url, jsonParams);//C:/Users/DELL/source/repos/testForPython/testForPython/bin/Debug/groupFormation_genetic//mindData/originalInput/class_multi/classNameList.csv
            if (result == null)
            {
                log = "Failed to Connect Flask Server!";
            }
            else
            {
                if (result.Contains("default"))
                {
                    log = "There is an error running the algorithm." + "\r\n" + result;
                }
                else
                {
                    flowLayoutPanel2.Controls.Clear();
                    FileStream fs = new FileStream(result, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    //记录每次读取的一行记录
                    string strLine = "";
                    //记录每行记录中的各字段内容
                    string[] aryLine;
                    //标示列数
                    int columnCount = 0;
                    //标示是否是读取的第一行
                    bool IsFirst = true;

                    //逐行读取CSV中的数据
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        aryLine = strLine.Split(',');
                        if (IsFirst == true)
                        {
                            IsFirst = false;
                            columnCount = aryLine.Length;
                        }
                        else
                        {
                            string ID = "";
                            string name = "";
                            for (int j = 0; j < columnCount; j++)
                            {
                                if (j == 1)
                                {
                                    ID = aryLine[j];
                                }
                                else if (j == 2)
                                {
                                    name = aryLine[j];
                                }
                            }
                            classMemberCount++;
                            AStudent aStudent = new AStudent();
                            aStudent.SetNum(ID);
                            aStudent.SetName(name);
                            flowLayoutPanel2.Controls.Add(aStudent);
                        }
                    }
                    sr.Close();
                    fs.Close();
                    m_lbClassMemberCount.Text = "总人数：" + classMemberCount.ToString();
                }
            }
        }
        public int SimuLength(string st)
        {
            int res = 0;
            int count_ch = 0;
            int count_en = 0;
            for(int i = 0; i < st.Length; i++)
            {
                if ((int)st[i] > 127)
                {
                    count_ch ++;
                }
                else
                {
                    count_en ++;
                }
            }
            res = count_ch + count_en / 2;
            return res;
        }
        #region 窗体圆角的实现
        private void Form1_Resize(object sender, EventArgs e)
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
            FormPath = GetRoundedRectPath(rect, 80);
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
        public void DeleteItem(string id)
        {
            //获取维度信息;
            string url = urlTital + "deleteAResult/";
            string jsonParams = id;
            string result = RequestsPost(url, jsonParams);
        }
        /// <summary>
        /// 通过网络地址和端口访问数据
        /// </summary>
        /// <param name="Url">网络地址</param>
        /// <param name="jsonParas">json参数</param>
        /// <returns></returns>
        public string RequestsPost(string Url, string jsonParas)
        {
            string postContent = "";
            string strURL = Url;
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/json";
            //设置参数，并进行URL编码 

            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);   

            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength 
            request.ContentLength = payload.Length;

            //发送请求，获得请求流 
            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                MessageBox.Show("连接服务器失败!");
                return null;
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
                postContent = "default: The response is null." + "\r\n" + "Exception: " + ex.Message;
            }
            if (response != null)
            {
                try
                {
                    Stream s = response.GetResponseStream();
                    StreamReader sRead = new StreamReader(s);
                    postContent = sRead.ReadToEnd();
                    sRead.Close();
                }
                catch (Exception e)
                {
                    postContent = "default: The data stream is not readable." + "\r\n" + e.Message;
                }
            }
            return postContent;//返回Json数据
        }

        private Point mouseLocation;//表示鼠标对于窗口左上角的坐标的负数
        private bool isDragging;//标识鼠标是否按下

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLocation = new Point(-e.X, -e.Y);
                //表示鼠标当前位置相对于窗口左上角的坐标，
                //并取负数,这里的e是参数，
                //可以获取鼠标位置
                isDragging = true;//标识鼠标已经按下
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newMouseLocation = MousePosition;
                //获取鼠标当前位置
                newMouseLocation.Offset(mouseLocation.X, mouseLocation.Y);
                //用鼠标当前位置加上鼠标相较于窗体左上角的
                //坐标的负数，也就获取到了新的窗体左上角位置
                Location = newMouseLocation;//设置新的窗体左上角位置
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
             if (isDragging)
            {
                isDragging = false;//鼠标已抬起，标识为false
            }
        }

        private void m_btResultList_Click(object sender, EventArgs e)
        {
            m_btResultList.Image = Properties.Resources.but1_1;
            m_btDimensionChoose.Image = Properties.Resources.but2;
            m_btDistribution.Image = Properties.Resources.but3;
            m_btClubResult.Image = Properties.Resources.but4;
            if (flowLayoutPanel5.Controls.Count > 0)
            {
                has_preRes = true;
            }
            else
            {
                has_preRes = false;
            }
            if (has_preRes)
            {
                tabControl1.SelectedTab = tabPage5;
            }
            else
            {
                tabControl1.SelectedTab = tabPage1;
            }
        }

        private void m_btDimensionChoose_Click(object sender, EventArgs e)
        {
            m_btResultList.Image = Properties.Resources.but1;
            m_btDimensionChoose.Image = Properties.Resources.but2_1;
            m_btDistribution.Image = Properties.Resources.but3;
            m_btClubResult.Image = Properties.Resources.but4;
            tabControl1.SelectedTab = tabPage2;

        }

        private void m_btDistribution_Click(object sender, EventArgs e)
        {
            m_btResultList.Image = Properties.Resources.but1;
            m_btDimensionChoose.Image = Properties.Resources.but2;
            m_btDistribution.Image = Properties.Resources.but3_1;
            m_btClubResult.Image = Properties.Resources.but4;
            tabControl1.SelectedTab = tabPage3;
        }
        public void public_m_btClubResult_Click()
        {
            m_btResultList.Image = Properties.Resources.but1;
            m_btDimensionChoose.Image = Properties.Resources.but2;
            m_btDistribution.Image = Properties.Resources.but3;
            m_btClubResult.Image = Properties.Resources.but4_1;
            tabControl1.SelectedTab = tabPage4;
        }
        private void m_btClubResult_Click(object sender, EventArgs e)
        {
            m_btResultList.Image = Properties.Resources.but1;
            m_btDimensionChoose.Image = Properties.Resources.but2;
            m_btDistribution.Image = Properties.Resources.but3;
            m_btClubResult.Image = Properties.Resources.but4_1;
            tabControl1.SelectedTab = tabPage4;

        }

        private void m_btPage2NextStep_Click(object sender, EventArgs e)
        {
            //判断是否没选择维度
            int count = 0;
            for (int i = 0; i < dimensionCount; i++)
            {
                if (m_dimensions[i].is_checked)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                MessageBox.Show("请至少选择一个维度");
            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(m_pbtongzhiTitle);
                //ScrollBar sb = new ScrollBar();
                ////...
                ////设置滚动条的Dock Width 之类的属性
                ////...
                //sb.Minimum = 0;
                //sb.Maximum = 100;
                //sb.LargeChange = 1;
                //sb.Value = 2;
                //flowLayoutPanel1.Controls.Add(sb);

                for (int i = 0; i < dimensionCount; i++)
                {
                    if (m_dimensions[i].is_checked)
                    {
                        DimensionTongzhiItem item = new DimensionTongzhiItem();
                        item.SetName(m_dimensions[i].name_Ch);
                        item.SetNum(i);
                        flowLayoutPanel1.Controls.Add(item);
                        item.Show();
                    }
                }
                m_lbSelectedDimension.Text = "您选择的维度: " + count.ToString() + "个";
                m_btDistribution_Click(null, null);
            }
        }

        private void m_btTongzhihuaLastStep_Click(object sender, EventArgs e)
        {
            m_btDimensionChoose_Click(null, null);
        }
        public void SetDimensionButtonAndText1()
        {
            //配置按钮和文字
            m_btDimension1.Visible = true;
            m_btDimension1.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension1.Visible = true;
            m_lbDimension1.Text = m_dimensions[0].name_Ch;
        }
        public void SetDimensionButtonAndText2()
        {
            SetDimensionButtonAndText1();
            m_btDimension2.Visible = true;
            m_btDimension2.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension2.Visible = true;
            m_lbDimension2.Text = m_dimensions[1].name_Ch;
        }
        public void SetDimensionButtonAndText3()
        {
            SetDimensionButtonAndText2();
            m_btDimension3.Visible = true;
            m_btDimension3.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension3.Visible = true;
            m_lbDimension3.Text = m_dimensions[2].name_Ch;
        }
        public void SetDimensionButtonAndText4()
        {
            SetDimensionButtonAndText3();
            m_btDimension4.Visible = true;
            m_btDimension4.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension4.Visible = true;
            m_lbDimension4.Text = m_dimensions[3].name_Ch;
        }
        public void SetDimensionButtonAndText5()
        {
            SetDimensionButtonAndText4();
            m_btDimension5.Visible = true;
            m_btDimension5.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension5.Visible = true;
            m_lbDimension5.Text = m_dimensions[4].name_Ch;
        }
        public void SetDimensionButtonAndText6()
        {
            SetDimensionButtonAndText5();
            m_btDimension6.Visible = true;
            m_btDimension6.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension6.Visible = true;
            m_lbDimension6.Text = m_dimensions[5].name_Ch;
        }
        public void SetDimensionButtonAndText7()
        {
            SetDimensionButtonAndText6();
            m_btDimension7.Visible = true;
            m_btDimension7.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension7.Visible = true;
            m_lbDimension7.Text = m_dimensions[6].name_Ch;
        }
        public void SetDimensionButtonAndText8()
        {
            SetDimensionButtonAndText7();
            m_btDimension8.Visible = true;
            m_btDimension8.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension8.Visible = true;
            m_lbDimension8.Text = m_dimensions[7].name_Ch;
        }
        public void SetDimensionButtonAndText9()
        {
            SetDimensionButtonAndText8();
            m_btDimension9.Visible = true;
            m_btDimension9.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension9.Visible = true;
            m_lbDimension9.Text = m_dimensions[8].name_Ch;
        }
        public void SetDimensionButtonAndText10()
        {
            SetDimensionButtonAndText9();
            m_btDimension10.Visible = true;
            m_btDimension10.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension10.Visible = true;
            m_lbDimension10.Text = m_dimensions[9].name_Ch;
        }
        public void SetDimensionButtonAndText11()
        {
            SetDimensionButtonAndText10();
            m_btDimension11.Visible = true;
            m_btDimension11.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension11.Visible = true;
            m_lbDimension11.Text = m_dimensions[10].name_Ch;
        }
        public void SetDimensionButtonAndText12()
        {
            SetDimensionButtonAndText11();
            m_btDimension12.Visible = true;
            m_btDimension12.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension12.Visible = true;
            m_lbDimension12.Text = m_dimensions[11].name_Ch;
        }
        public void SetDimensionButtonAndText13()
        {
            SetDimensionButtonAndText12();
            m_btDimension13.Visible = true;
            m_btDimension13.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension13.Visible = true;
            m_lbDimension13.Text = m_dimensions[12].name_Ch;
        }
        public void SetDimensionButtonAndText14()
        {
            SetDimensionButtonAndText13();
            m_btDimension14.Visible = true;
            m_btDimension14.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension14.Visible = true;
            m_lbDimension14.Text = m_dimensions[13].name_Ch;
        }
        public void ResetAllDimensionButton()
        {
            for(int i=0; i < dimensionCount; i++)
            {
                m_dimensions[i].is_checked = false;
            }

            m_btDimension1.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension1.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension1.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension2.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension2.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension2.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension3.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension3.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension3.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension4.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension4.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension4.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension5.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension5.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension5.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension6.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension6.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension6.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension7.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension7.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension7.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension8.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension8.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension8.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension9.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension9.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension9.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension10.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension10.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension10.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension11.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension11.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension11.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension12.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension12.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension12.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension13.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension13.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension13.BackColor = Color.FromArgb(255, 255, 255);

            m_btDimension14.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension14.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension14.BackColor = Color.FromArgb(255, 255, 255); 

            m_btDimension15.Image = Properties.Resources.dimension_button_blank;
            m_lbDimension15.ForeColor = Color.FromArgb(152, 153, 204);
            m_lbDimension15.BackColor = Color.FromArgb(255, 255, 255);
        }
        public void SetDimensionButtonAndText15()
        {
            SetDimensionButtonAndText14();
            m_btDimension15.Visible = true;
            m_btDimension15.Image = Properties.Resources.dimension_button_blank;

            m_lbDimension15.Visible = true;
            m_lbDimension15.Text = m_dimensions[14].name_Ch;
        }
        public void Setpbdimension1Image()
        {
            switch (dimensionCount)
            {
                case 1:
                    m_pbdimension1.Image = Properties.Resources.dimension1_1;
                    m_pbdimension2.Image = Properties.Resources.dimension2_1;
                    //配置按钮和文字
                    SetDimensionButtonAndText1();
                    break;
                case 2:
                    m_pbdimension1.Image = Properties.Resources.dimension1_2;
                    m_pbdimension2.Image = Properties.Resources.dimension2_2;
                    SetDimensionButtonAndText2();
                    break;
                case 3:
                    m_pbdimension1.Image = Properties.Resources.dimension1_3;
                    m_pbdimension2.Image = Properties.Resources.dimension2_3;
                    SetDimensionButtonAndText3();
                    break;
                case 4:
                    m_pbdimension1.Image = Properties.Resources.dimension1_4;
                    m_pbdimension2.Image = Properties.Resources.dimension2_4;
                    SetDimensionButtonAndText4();
                    break;
                case 5:
                    m_pbdimension1.Image = Properties.Resources.dimension1_5;
                    m_pbdimension2.Image = Properties.Resources.dimension2_5;
                    SetDimensionButtonAndText5();
                    break;
                case 6:
                    m_pbdimension1.Image = Properties.Resources.dimension1_6;
                    m_pbdimension2.Image = Properties.Resources.dimension2_6;
                    SetDimensionButtonAndText6();
                    break;
                case 7:
                    m_pbdimension1.Image = Properties.Resources.dimension1_7;
                    m_pbdimension2.Image = Properties.Resources.dimension2_7;
                    SetDimensionButtonAndText7();
                    break;
                case 8:
                    m_pbdimension1.Image = Properties.Resources.dimension1_8;
                    m_pbdimension2.Image = Properties.Resources.dimension2_8;
                    SetDimensionButtonAndText8();
                    break;
                case 9:
                    m_pbdimension1.Image = Properties.Resources.dimension1_9;
                    m_pbdimension2.Image = Properties.Resources.dimension2_9;
                    SetDimensionButtonAndText9();
                    break;
                case 10:
                    m_pbdimension1.Image = Properties.Resources.dimension1_10;
                    m_pbdimension2.Image = Properties.Resources.dimension2_10;
                    SetDimensionButtonAndText10();
                    break;
                case 11:
                    m_pbdimension1.Image = Properties.Resources.dimension1_11;
                    m_pbdimension2.Image = Properties.Resources.dimension2_11;
                    SetDimensionButtonAndText11();
                    break;
                case 12:
                    m_pbdimension1.Image = Properties.Resources.dimension1_12;
                    m_pbdimension2.Image = Properties.Resources.dimension2_12;
                    SetDimensionButtonAndText12();
                    break;
                case 13:
                    m_pbdimension1.Image = Properties.Resources.dimension1_13;
                    m_pbdimension2.Image = Properties.Resources.dimension2_13;
                    SetDimensionButtonAndText13();
                    break;
                case 14:
                    m_pbdimension1.Image = Properties.Resources.dimension1_14;
                    m_pbdimension2.Image = Properties.Resources.dimension2_14;
                    SetDimensionButtonAndText14();
                    break;
                case 15:
                    m_pbdimension1.Image = Properties.Resources.dimension1_15;
                    m_pbdimension2.Image = Properties.Resources.dimension2_15;
                    SetDimensionButtonAndText15();
                    break;


            }
        }
        public string SubString_name(string str)
        {
            return str.Substring(2, str.Length - 3);
        }
        public string SubString_Ch(string str)
        {
            return str.Substring(2, str.Length - 3);
        }
        public void GetDimensionNameAndCh()
        {
            Array.Clear(m_dimensions, 0, m_dimensions.Length);
            for (int i = 0; i < dimensionCount; i++)
            {
                dimesionSet tem = new dimesionSet();
                tem.name = SubString_name(m_Oridimension[i]);
                tem.name_Ch = SubString_Ch(m_Oridimension[i+ dimensionCount]);
                tem.is_checked = false;
                m_dimensions[i] = tem;
            }
        }

        public void GetDimensionInfo()
        {
            //获取维度信息;
            string url = urlTital + "getDimension/";
            string log = "";//错误信息

            string jsonParams = "";
            string result = RequestsPost(url, jsonParams);
            if (result == null)
            {
                log = "Failed to Connect Flask Server!";
            }
            else
            {
                if (result.Contains("default"))
                {
                    log = "There is an error running the algorithm." + "\r\n" + result;
                }
                else
                {
                    //['age', 'field', 'intimate', 'major', 'mbti', '年龄', '擅长知识点', '亲密度', '专业', 'MBTI']
                    string result1 = result.Substring(1, result.Length - 2);
                    Array.Clear(m_Oridimension, 0, m_Oridimension.Length);
                    m_Oridimension = result1.Split(',');
                    string mid1 = m_Oridimension[0];
                    m_Oridimension[0] = " " + mid1;
                    dimensionCount = m_Oridimension.Length / 2;
                    GetDimensionNameAndCh();
                }
            }
        }
        private void m_btNewClass_Click(object sender, EventArgs e)
        {

            Setpbdimension1Image();
            m_btDimensionChoose_Click(null, null);
        }

        private void m_btDimension1_Click(object sender, EventArgs e)
        {
            if (m_dimensions[0].is_checked)
            {
                m_btDimension1.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension1.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension1.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[0].is_checked = false;
            }
            else
            {
                m_btDimension1.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension1.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension1.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[0].is_checked = true;
            }
        }

        private void m_btDimension2_Click(object sender, EventArgs e)
        {
            if (m_dimensions[1].is_checked)
            {
                m_btDimension2.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension2.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension2.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[1].is_checked = false;
            }
            else
            {
                m_btDimension2.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension2.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension2.BackColor = Color.FromArgb(217,216,245);
                m_dimensions[1].is_checked = true;
            }
        }

        private void m_btDimension3_Click(object sender, EventArgs e)
        {
            if (m_dimensions[2].is_checked)
            {
                m_btDimension3.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension3.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension3.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[2].is_checked = false;
            }
            else
            {
                m_btDimension3.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension3.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension3.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[2].is_checked = true;
            }
        }

        private void m_btDimension4_Click(object sender, EventArgs e)
        {
            if (m_dimensions[3].is_checked)
            {
                m_btDimension4.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension4.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension4.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[3].is_checked = false;
            }
            else
            {
                m_btDimension4.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension4.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension4.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[3].is_checked = true;
            }
        }

        private void m_btDimension5_Click(object sender, EventArgs e)
        {
            if (m_dimensions[4].is_checked)
            {
                m_btDimension5.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension5.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension5.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[4].is_checked = false;
            }
            else
            {
                m_btDimension5.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension5.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension5.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[4].is_checked = true;
            }
        }

        private void m_btDimension6_Click(object sender, EventArgs e)
        {
            if (m_dimensions[5].is_checked)
            {
                m_btDimension6.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension6.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension6.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[5].is_checked = false;
            }
            else
            {
                m_btDimension6.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension6.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension6.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[5].is_checked = true;
            }
        }

        private void m_btDimension7_Click(object sender, EventArgs e)
        {
            if (m_dimensions[6].is_checked)
            {
                m_btDimension7.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension7.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension7.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[6].is_checked = false;
            }
            else
            {
                m_btDimension7.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension7.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension7.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[6].is_checked = true;
            }
        }

        private void m_btDimension8_Click(object sender, EventArgs e)
        {
            if (m_dimensions[7].is_checked)
            {
                m_btDimension8.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension8.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension8.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[7].is_checked = false;
            }
            else
            {
                m_btDimension8.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension8.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension8.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[7].is_checked = true;
            }
        }

        private void m_btDimension9_Click(object sender, EventArgs e)
        {
            if (m_dimensions[8].is_checked)
            {
                m_btDimension9.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension9.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension9.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[8].is_checked = false;
            }
            else
            {
                m_btDimension9.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension9.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension9.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[8].is_checked = true;
            }
        }

        private void m_btDimension10_Click(object sender, EventArgs e)
        {
            if (m_dimensions[9].is_checked)
            {
                m_btDimension10.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension10.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension10.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[9].is_checked = false;
            }
            else
            {
                m_btDimension10.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension10.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension10.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[9].is_checked = true;
            }
        }

        private void m_btDimension11_Click(object sender, EventArgs e)
        {
            if (m_dimensions[10].is_checked)
            {
                m_btDimension11.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension11.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension11.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[10].is_checked = false;
            }
            else
            {
                m_btDimension11.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension11.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension11.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[10].is_checked = true;
            }
        }

        private void m_btDimension12_Click(object sender, EventArgs e)
        {
            if (m_dimensions[11].is_checked)
            {
                m_btDimension12.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension12.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension12.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[11].is_checked = false;
            }
            else
            {
                m_btDimension12.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension12.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension12.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[11].is_checked = true;
            }
        }

        private void m_btDimension13_Click(object sender, EventArgs e)
        {
            if (m_dimensions[12].is_checked)
            {
                m_btDimension13.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension13.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension13.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[12].is_checked = false;
            }
            else
            {
                m_btDimension13.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension13.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension13.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[12].is_checked = true;
            }
        }

        private void m_btDimension14_Click(object sender, EventArgs e)
        {
            if (m_dimensions[13].is_checked)
            {
                m_btDimension14.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension14.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension14.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[13].is_checked = false;
            }
            else
            {
                m_btDimension14.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension14.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension14.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[13].is_checked = true;
            }
        }

        private void m_btDimension15_Click(object sender, EventArgs e)
        {
            if (m_dimensions[14].is_checked)
            {
                m_btDimension15.Image = Properties.Resources.dimension_button_blank;
                m_lbDimension15.ForeColor = Color.FromArgb(152, 153, 204);
                m_lbDimension15.BackColor = Color.FromArgb(255, 255, 255);
                m_dimensions[14].is_checked = false;
            }
            else
            {
                m_btDimension15.Image = Properties.Resources.dimension_button_checked;
                m_lbDimension15.ForeColor = Color.FromArgb(33, 38, 64);
                m_lbDimension15.BackColor = Color.FromArgb(217, 216, 245);
                m_dimensions[14].is_checked = true;
            }
        }

        private void m_lbDimension1_Click(object sender, EventArgs e)
        {
            m_btDimension1_Click(null, null);
        }

        private void m_lbDimension2_Click(object sender, EventArgs e)
        {
            m_btDimension2_Click(null, null);
        }

        private void m_lbDimension3_Click(object sender, EventArgs e)
        {
            m_btDimension3_Click(null, null);
        }

        private void m_lbDimension4_Click(object sender, EventArgs e)
        {
            m_btDimension4_Click(null, null);
        }

        private void m_lbDimension5_Click(object sender, EventArgs e)
        {
            m_btDimension5_Click(null, null);
        }

        private void m_lbDimension6_Click(object sender, EventArgs e)
        {
            m_btDimension6_Click(null, null);
        }

        private void m_lbDimension7_Click(object sender, EventArgs e)
        {
            m_btDimension7_Click(null, null);
        }

        private void m_lbDimension8_Click(object sender, EventArgs e)
        {
            m_btDimension8_Click(null, null);
        }

        private void m_lbDimension9_Click(object sender, EventArgs e)
        {
            m_btDimension9_Click(null, null);
        }

        private void m_lbDimension10_Click(object sender, EventArgs e)
        {
            m_btDimension10_Click(null, null);
        }

        private void m_lbDimension11_Click(object sender, EventArgs e)
        {
            m_btDimension11_Click(null, null);
        }

        private void m_lbDimension12_Click(object sender, EventArgs e)
        {
            m_btDimension12_Click(null, null);
        }

        private void m_lbDimension13_Click(object sender, EventArgs e)
        {
            m_btDimension13_Click(null, null);
        }

        private void m_lbDimension14_Click(object sender, EventArgs e)
        {
            m_btDimension14_Click(null, null);
        }

        private void m_lbDimension15_Click(object sender, EventArgs e)
        {
            m_btDimension15_Click(null, null);
        }

        private void m_btHopeBalance_Click(object sender, EventArgs e)
        {
            if (m_hopeBalance)
            {
                m_hopeBalance = false;
                m_btHopeBalance.Image = Properties.Resources.tongzhiItem6;
            }
            else
            {
                m_hopeBalance = true;
                m_btHopeBalance.Image = Properties.Resources.tongzhiItem5;
            }
        }
        public string HeterString()
        {
            string res = "";
            string tem1 = "";
            string tem2 = "";

            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if(c is DimensionTongzhiItem)
                {
                    DimensionTongzhiItem ctrl = c as DimensionTongzhiItem;
                    int num = ctrl.GetNum();
                    if (ctrl.GetHomoDimension())
                    {
                        tem1 += m_dimensions[num].name + "-";
                    }
                    else
                    {
                        tem2 += m_dimensions[num].name + "-";
                    }
                }               
            }
            if(tem1 == "")
            {
                res = "#" + "#" + tem2.Substring(0, tem2.Length - 1)
                + "#" + m_hopeBalance.ToString();

            }
            else if(tem2 == "")
            {
                res = "#" + tem1.Substring(0, tem1.Length - 1) + "#" + "#" + m_hopeBalance.ToString();
            }
            else
            {
                res = "#" + tem1.Substring(0, tem1.Length - 1) + "#" + tem2.Substring(0, tem2.Length - 1)
                + "#" + m_hopeBalance.ToString();
            }
            

            return res;
        }
 
        public string SetHeterData()
        {
            //首先传入同质和异质数据以及是否均衡
            string url = urlTital + "setHeterData/";
            string log = "";//错误信息

            string jsonParams = HeterString();
            string result = RequestsPost(url, jsonParams);
            if (result == null)
            {
                log = "Failed to Connect Flask Server!";
            }
            else
            {
                if (result.Contains("default"))
                {
                    log = "There is an error running the algorithm." + "\r\n" + result;
                }
                else
                {
                    log = "success";
                }
            }
            return log;
        }
        public string GetProcess()
        {
            //获取计算进度
            string url = urlTital + "getProcess/";
            string log = "";//错误信息

            string jsonParams = "";
            string result = RequestsPost(url, jsonParams);
            if (result == null)
            {
                log = "Failed to Connect Flask Server!";
                MessageBox.Show(log);
            }
            else
            {
                if (result.Contains("default"))
                {
                    log = "There is an error running the algorithm." + "\r\n" + result;
                }
                else
                {
                    return result;
                }
            }
            return "";
        }
        private void m_btStartCal_Click(object sender, EventArgs e)
        {
            has_preRes = true;
            ResetProcess();
            needSearchProcess = true;
            BackgroundWorker worker1 = new BackgroundWorker();
            worker1.DoWork += SetData;
            worker1.RunWorkerAsync();

            BackgroundWorker worker2 = new BackgroundWorker();
            worker2.DoWork += GetCalculationProcess;
            worker2.RunWorkerAsync();
        }
        private void SetData(object sender, DoWorkEventArgs e)
        {
            string res = SetHeterData();
        }
        /// <summary>
        /// 读取txt文件，并返回文件中的内容
        /// </summary>
        /// <returns>txt文件内容</returns>
        private string ReadTxTContent(string path)
        {
            try
            {
                string s_con = string.Empty;
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        s_con += line;
                    }
                }
                return s_con;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        private void GetCalculationProcess(object sender, DoWorkEventArgs e)
        {
            string str = GetProcess();//C:/Users/DELL/source/repos/testForPython/testForPython/bin/Debug/groupFormation_genetic//mindData/currentProgress/multi.txt
            ProcessBarForm processBar = new ProcessBarForm();
            processBar.Owner = this;
            this.Invoke(new Action(() =>
            {
                processBar.Show();
            }));
            while (needSearchProcess)
            {
                string processNum_str = ReadTxTContent(str);
                if (double.TryParse(processNum_str, out double processNum_dou))
                {
                    int processNum_int = (int)processNum_dou;
                    string lable = processNum_int.ToString() + "%";
                    this.Invoke(new Action(() =>
                    {
                        processBar.SetLable(lable);
                        processBar.SetWidth(processNum_dou / 100);
                    }));
                    if (processNum_int == 100)
                    {
                        needSearchProcess = false;
                        this.Invoke(new Action(() =>
                        {
                            processBar.ClaFinished();
                        }));
                        break;
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        void userControl1_myEvent(object sender, EventArgs e)
        {
            foreach(SingleResult sr in flowLayoutPanel5.Controls)
            {
                if (sr.is_thisOne)
                {
                    GetCalDetailRes(sr.singleResult_ID);
                    curNum = sr.singleResult_ID;
                    sr.is_thisOne = false;
                    break;
                }
            }
            m_btClubResult_Click(null, null);
        }

        public void GetCalDetailRes(int num)
        {
            flowLayoutPanel4.Controls.Clear();
            double minx = 999999;
            double miny = 999999;
            double maxx = -1;
            double maxy = -1;

            m_chart.Series[0].Points.Clear();
            // 设置曲线的样式
            Series series = m_chart.Series[0];
            // 画点图Point
            series.ChartType = SeriesChartType.Point;
            series.MarkerStyle = MarkerStyle.Circle;
            // 线宽2个像素
            series.BorderWidth = 2;
            // 线的颜色：红色
            series.Color = Color.FromArgb(95,55,255);
            

            string res =GetCalDetailFromPython(num);
            try
            {
                string[] first_split = res.Split('|');
                for (int i = 0; i < first_split.Length; i++)
                {
                    if (i == 0)
                    {
                        //班级信息
                        string[] paraNumbs = first_split[0].Split(',');
                        double Num_HomoDis = double.Parse(paraNumbs[0].Trim());
                        double Num_HeterDis = double.Parse(paraNumbs[1].Trim());
                        double Num_groupVariance = double.Parse(paraNumbs[2].Trim());
                        m_lbClassHomoDis.Text = GetSubNum(Num_HomoDis, 2);
                        m_lbClassHeterDis.Text = GetSubNum(Num_HeterDis, 2);
                        m_lbClassVariance.Text = GetSubNum(Num_groupVariance, 4);
                    }
                    else
                    {
                        //组内信息
                        ACalGroup temGroup = new ACalGroup(first_split[i]);
                        //收集每个组的同异质距离用于绘图
                        double x = double.Parse(GetSubNum(temGroup.GetHomoNum(), 2));
                        double y = double.Parse(GetSubNum(temGroup.GetHeterNum(), 2));
                        if (x < minx)
                        {
                            minx = x;
                        }
                        if (y < miny)
                        {
                            miny = y;
                        }
                        if (x > maxx)
                        {
                            maxx = x;
                        }
                        if (y > maxy)
                        {
                            maxy = y;
                        }
                        if(x == 0)
                        {
                            x = 0.00001;
                        }
                        if (y == 0)
                        {
                            y = 0.00001;
                        }
                        series.Points.AddXY(x, y);
                        flowLayoutPanel4.Controls.Add(temGroup);
                    }
                }
                //画图
                // 设置显示范围
                ChartArea chartArea = m_chart.ChartAreas[0];
                chartArea.AxisX.Minimum = minx-1;
                chartArea.AxisX.Maximum = maxx+1;
                chartArea.AxisY.Minimum = miny-1;
                chartArea.AxisY.Maximum = maxy+1;
                chartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            }
            catch
            {
                MessageBox.Show("Failed to Connect Flask Server!");
            }
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
        public string GetCalDetailFromPython(int num)
        {
            //首先传入同质和异质数据以及是否均衡
            string url = urlTital + "GetCalRes/";
            string log = "";//错误信息

            string jsonParams = num.ToString();
            string result = RequestsPost(url, jsonParams);
            if (result == null)
            {
                log = "Failed";
            }
            else
            {
                if (result.Contains("default"))
                {
                    log = "Failed";
                }
                else
                {
                    log = result;
                }
            }
            return log;
        }
        public string ResetProcess()
        {
            //首先传入同质和异质数据以及是否均衡
            string url = urlTital + "ResetProcess/";
            string log = "";//错误信息

            string jsonParams = "";
            string result = RequestsPost(url, jsonParams);
            if (result == null)
            {
                log = "Failed to Connect Flask Server!";
            }
            else
            {
                if (result.Contains("default"))
                {
                    log = "There is an error running the algorithm." + "\r\n" + result;
                }
                else
                {
                    log = "success";
                }
            }
            return log;
        }
        public string SendSavePath(string path)
        {
            //首先传入同质和异质数据以及是否均衡
            string url = urlTital + "SendSavePath/";
            string log = "";//错误信息

            string result = RequestsPost(url, path);
            if (result == null)
            {
                log = "None";
            }
            else
            {
                if (result.Contains("default"))
                {
                    log = "Failed";
                }
                else
                {
                    log = "OK";
                }
            }
            return log;
        }
        public string GetGroupRes()
        {
            //首先传入同质和异质数据以及是否均衡
            string url = urlTital + "getGroupData/";
            string log = "";//错误信息

            string jsonParams ="";
            string result = RequestsPost(url, jsonParams);
            if (result == null)
            {
                log = "None";
            }
            else
            {
                if (result.Contains("default"))
                {
                    log = "Failed";
                }
                else
                {
                    //亲密度,MBTI || 擅长知识点,专业,年龄 || 6.636656211439511,8.59748028776696,
                    //0.008465198285452958 || 1203465 & wengxiaomin,1203422 & linjunting,
                    //1203344 & yangyuke,1203351 & huzhouning#亲密度,MBTI
                    //||擅长知识点,专业,年龄||6.612950002811782,8.453608276411329,
                    //0.012785874512143566||1203452&weiyuqing,1203420&ousuen,1203368&wuxingyi,
                    //1203383&wangjiaqi#亲密度,MBTI||擅长知识点,专业,年龄||
                    //6.455274336905658,8.597319158362756,0.013874958733382393||
                    //1203417&lixinyi,1203373&suxin,1203369&luoxinran,1203365&wangxianzhi
                    log = result;
                }
            }
            return log;
        }
        private void m_pbClassPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                m_pbClassPic.Image =Image.FromFile(dlg.FileName);
            }
        }

        private void m_pbANewGroup_Click(object sender, EventArgs e)
        {
            Setpbdimension1Image();
            ResetAllDimensionButton();
            m_btDimensionChoose_Click(null, null);
        }
        public void ClearItems()
        {
            //首先传入同质和异质数据以及是否均衡
            string url = urlTital + "clearAllRes/";
            string jsonParams = "";
            string result = RequestsPost(url, jsonParams);
        }
        private void m_pbClearItems_Click(object sender, EventArgs e)
        {
            ClearItems();
            has_preRes = false;
            flowLayoutPanel5.Controls.Clear();
        }

        private void m_pbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void m_pbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void m_pbMinimize_MouseEnter(object sender, EventArgs e)
        {
            m_pbClose.Image = Properties.Resources.Group_9;
            m_pbMinimize.Image = Properties.Resources.Group_10;
            m_pbpanel.Image = Properties.Resources.Group_113;
            m_pbClose.BackColor = Color.FromArgb(95, 55, 255);
            m_pbMinimize.BackColor = Color.FromArgb(95, 55, 255);
        }

        private void m_pbMinimize_MouseLeave(object sender, EventArgs e)
        {
            m_pbClose.Image = null;
            m_pbMinimize.Image = null;
            m_pbpanel.Image = null;
            m_pbClose.BackColor = Color.White;
            m_pbMinimize.BackColor = Color.White;
        }

        private void m_pbClose_MouseEnter(object sender, EventArgs e)
        {
            m_pbClose.Image = Properties.Resources.Group_9;
            m_pbMinimize.Image = Properties.Resources.Group_10;
            m_pbpanel.Image = Properties.Resources.Group_113;
            m_pbClose.BackColor = Color.FromArgb(95, 55, 255);
            m_pbMinimize.BackColor = Color.FromArgb(95, 55, 255);
        }

        private void m_pbClose_MouseLeave(object sender, EventArgs e)
        {
            m_pbClose.Image = null;
            m_pbMinimize.Image = null;
            m_pbpanel.Image = null;
            m_pbClose.BackColor = Color.White;
            m_pbMinimize.BackColor = Color.White;
        }

        private void m_pbOutputGroup_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "导出文件 (*.csv)|*.csv";
            dlg.Title = "导出文件保存路径";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string res = dlg.FileName;
                string para = curNum.ToString() + "#" + res;
                string backres = SendSavePath(para);
                if (backres != "OK")
                {
                    MessageBox.Show(backres);
                }
            }
        }
    }

}
