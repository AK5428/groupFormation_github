namespace testForPython
{
    partial class ClassDimension
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
            this.m_pbDim = new System.Windows.Forms.PictureBox();
            this.m_lbName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbDim)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pbDim
            // 
            this.m_pbDim.BackColor = System.Drawing.Color.White;
            this.m_pbDim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pbDim.Image = global::testForPython.Properties.Resources.classDimension10;
            this.m_pbDim.Location = new System.Drawing.Point(0, 0);
            this.m_pbDim.Name = "m_pbDim";
            this.m_pbDim.Size = new System.Drawing.Size(60, 30);
            this.m_pbDim.TabIndex = 0;
            this.m_pbDim.TabStop = false;
            // 
            // m_lbName
            // 
            this.m_lbName.AutoSize = true;
            this.m_lbName.BackColor = System.Drawing.Color.White;
            this.m_lbName.Font = new System.Drawing.Font("苹方-简", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.m_lbName.ForeColor = System.Drawing.Color.Black;
            this.m_lbName.Location = new System.Drawing.Point(5, 5);
            this.m_lbName.Name = "m_lbName";
            this.m_lbName.Size = new System.Drawing.Size(45, 20);
            this.m_lbName.TabIndex = 1;
            this.m_lbName.Text = "label1";
            // 
            // ClassDimension
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_lbName);
            this.Controls.Add(this.m_pbDim);
            this.Name = "ClassDimension";
            this.Size = new System.Drawing.Size(60, 30);
            ((System.ComponentModel.ISupportInitialize)(this.m_pbDim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_pbDim;
        private System.Windows.Forms.Label m_lbName;
    }
}
