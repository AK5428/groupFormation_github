namespace testForPython
{
    partial class ItemLabel
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
            this.m_lb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lb
            // 
            this.m_lb.BackColor = System.Drawing.Color.Transparent;
            this.m_lb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lb.Font = new System.Drawing.Font("苹方-简", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.m_lb.ForeColor = System.Drawing.Color.Black;
            this.m_lb.Location = new System.Drawing.Point(0, 0);
            this.m_lb.Name = "m_lb";
            this.m_lb.Size = new System.Drawing.Size(82, 20);
            this.m_lb.TabIndex = 0;
            this.m_lb.Text = "label1";
            this.m_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_lb);
            this.Name = "ItemLabel";
            this.Size = new System.Drawing.Size(82, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lb;
    }
}
