namespace testForPython
{
    partial class DimensionTongzhiItem
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
            this.m_lbDimensionName = new System.Windows.Forms.Label();
            this.m_btTongzhi = new System.Windows.Forms.Button();
            this.m_pbBack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbBack)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lbDimensionName
            // 
            this.m_lbDimensionName.BackColor = System.Drawing.Color.White;
            this.m_lbDimensionName.Font = new System.Drawing.Font("苹方-简", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.m_lbDimensionName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(38)))), ((int)(((byte)(64)))));
            this.m_lbDimensionName.Location = new System.Drawing.Point(12, 12);
            this.m_lbDimensionName.Name = "m_lbDimensionName";
            this.m_lbDimensionName.Size = new System.Drawing.Size(406, 22);
            this.m_lbDimensionName.TabIndex = 1;
            this.m_lbDimensionName.Text = "亲和度";
            this.m_lbDimensionName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_btTongzhi
            // 
            this.m_btTongzhi.BackColor = System.Drawing.Color.White;
            this.m_btTongzhi.FlatAppearance.BorderSize = 0;
            this.m_btTongzhi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btTongzhi.Image = global::testForPython.Properties.Resources.tongzhiItem3;
            this.m_btTongzhi.Location = new System.Drawing.Point(618, 7);
            this.m_btTongzhi.Name = "m_btTongzhi";
            this.m_btTongzhi.Size = new System.Drawing.Size(64, 33);
            this.m_btTongzhi.TabIndex = 2;
            this.m_btTongzhi.UseVisualStyleBackColor = false;
            this.m_btTongzhi.Click += new System.EventHandler(this.m_btTongzhi_Click);
            // 
            // m_pbBack
            // 
            this.m_pbBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pbBack.Image = global::testForPython.Properties.Resources.tongzhiItem11;
            this.m_pbBack.Location = new System.Drawing.Point(0, 0);
            this.m_pbBack.Name = "m_pbBack";
            this.m_pbBack.Size = new System.Drawing.Size(865, 46);
            this.m_pbBack.TabIndex = 0;
            this.m_pbBack.TabStop = false;
            // 
            // DimensionTongzhiItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_btTongzhi);
            this.Controls.Add(this.m_lbDimensionName);
            this.Controls.Add(this.m_pbBack);
            this.Name = "DimensionTongzhiItem";
            this.Size = new System.Drawing.Size(865, 46);
            ((System.ComponentModel.ISupportInitialize)(this.m_pbBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_pbBack;
        private System.Windows.Forms.Label m_lbDimensionName;
        private System.Windows.Forms.Button m_btTongzhi;
    }
}
