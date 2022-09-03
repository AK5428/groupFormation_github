namespace testForPython
{
    partial class studentMini
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
            this.m_lbName = new System.Windows.Forms.Label();
            this.m_lbID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::testForPython.Properties.Resources.list;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // m_lbName
            // 
            this.m_lbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.m_lbName.Font = new System.Drawing.Font("苹方-简", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.m_lbName.ForeColor = System.Drawing.Color.Black;
            this.m_lbName.Location = new System.Drawing.Point(91, 2);
            this.m_lbName.Name = "m_lbName";
            this.m_lbName.Size = new System.Drawing.Size(75, 23);
            this.m_lbName.TabIndex = 1;
            this.m_lbName.Text = "label1";
            this.m_lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lbID
            // 
            this.m_lbID.BackColor = System.Drawing.Color.White;
            this.m_lbID.Font = new System.Drawing.Font("苹方-简", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.m_lbID.ForeColor = System.Drawing.Color.Black;
            this.m_lbID.Location = new System.Drawing.Point(3, 4);
            this.m_lbID.Name = "m_lbID";
            this.m_lbID.Size = new System.Drawing.Size(69, 20);
            this.m_lbID.TabIndex = 2;
            this.m_lbID.Text = "label2";
            this.m_lbID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // studentMini
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_lbID);
            this.Controls.Add(this.m_lbName);
            this.Controls.Add(this.pictureBox1);
            this.Name = "studentMini";
            this.Size = new System.Drawing.Size(172, 27);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label m_lbName;
        private System.Windows.Forms.Label m_lbID;
    }
}
