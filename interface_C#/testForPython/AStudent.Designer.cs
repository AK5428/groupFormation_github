namespace testForPython
{
    partial class AStudent
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_lbNum = new System.Windows.Forms.Label();
            this.m_lbName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::testForPython.Properties.Resources.list_stu;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(297, 30);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // m_lbNum
            // 
            this.m_lbNum.AutoSize = true;
            this.m_lbNum.BackColor = System.Drawing.Color.White;
            this.m_lbNum.Font = new System.Drawing.Font("苹方-简", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.m_lbNum.ForeColor = System.Drawing.Color.Black;
            this.m_lbNum.Location = new System.Drawing.Point(40, 4);
            this.m_lbNum.Name = "m_lbNum";
            this.m_lbNum.Size = new System.Drawing.Size(45, 20);
            this.m_lbNum.TabIndex = 1;
            this.m_lbNum.Text = "label1";
            // 
            // m_lbName
            // 
            this.m_lbName.AutoSize = true;
            this.m_lbName.BackColor = System.Drawing.Color.White;
            this.m_lbName.Font = new System.Drawing.Font("苹方-简", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.m_lbName.ForeColor = System.Drawing.Color.Black;
            this.m_lbName.Location = new System.Drawing.Point(180, 4);
            this.m_lbName.Name = "m_lbName";
            this.m_lbName.Size = new System.Drawing.Size(47, 20);
            this.m_lbName.TabIndex = 2;
            this.m_lbName.Text = "label2";
            // 
            // AStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_lbName);
            this.Controls.Add(this.m_lbNum);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AStudent";
            this.Size = new System.Drawing.Size(297, 30);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label m_lbNum;
        private System.Windows.Forms.Label m_lbName;
    }
}
