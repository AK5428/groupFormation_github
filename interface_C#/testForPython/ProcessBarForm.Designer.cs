namespace testForPython
{
    partial class ProcessBarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_lbpresent = new System.Windows.Forms.Label();
            this.m_pbConform = new System.Windows.Forms.PictureBox();
            this.m_pbProcessBar = new System.Windows.Forms.PictureBox();
            this.m_pbBack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbConform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbProcessBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbBack)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lbpresent
            // 
            this.m_lbpresent.AutoSize = true;
            this.m_lbpresent.BackColor = System.Drawing.Color.White;
            this.m_lbpresent.Font = new System.Drawing.Font("苹方-简", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.m_lbpresent.Location = new System.Drawing.Point(486, 168);
            this.m_lbpresent.Name = "m_lbpresent";
            this.m_lbpresent.Size = new System.Drawing.Size(63, 28);
            this.m_lbpresent.TabIndex = 4;
            this.m_lbpresent.Text = "100%";
            // 
            // m_pbConform
            // 
            this.m_pbConform.BackColor = System.Drawing.Color.White;
            this.m_pbConform.Image = global::testForPython.Properties.Resources.下一步按钮;
            this.m_pbConform.Location = new System.Drawing.Point(240, 214);
            this.m_pbConform.Name = "m_pbConform";
            this.m_pbConform.Size = new System.Drawing.Size(90, 54);
            this.m_pbConform.TabIndex = 5;
            this.m_pbConform.TabStop = false;
            this.m_pbConform.Click += new System.EventHandler(this.m_pbConform_Click);
            // 
            // m_pbProcessBar
            // 
            this.m_pbProcessBar.BackColor = System.Drawing.Color.White;
            this.m_pbProcessBar.Image = global::testForPython.Properties.Resources.进度条;
            this.m_pbProcessBar.Location = new System.Drawing.Point(50, 177);
            this.m_pbProcessBar.Name = "m_pbProcessBar";
            this.m_pbProcessBar.Size = new System.Drawing.Size(433, 10);
            this.m_pbProcessBar.TabIndex = 3;
            this.m_pbProcessBar.TabStop = false;
            // 
            // m_pbBack
            // 
            this.m_pbBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(224)))), ((int)(((byte)(231)))));
            this.m_pbBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pbBack.Image = global::testForPython.Properties.Resources.Frame_81;
            this.m_pbBack.Location = new System.Drawing.Point(0, 0);
            this.m_pbBack.Name = "m_pbBack";
            this.m_pbBack.Size = new System.Drawing.Size(579, 365);
            this.m_pbBack.TabIndex = 1;
            this.m_pbBack.TabStop = false;
            this.m_pbBack.Resize += new System.EventHandler(this.m_pbBack_Resize);
            // 
            // ProcessBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 365);
            this.Controls.Add(this.m_pbConform);
            this.Controls.Add(this.m_lbpresent);
            this.Controls.Add(this.m_pbProcessBar);
            this.Controls.Add(this.m_pbBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProcessBarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProcessBarForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_pbConform)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbProcessBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_pbBack;
        private System.Windows.Forms.Label m_lbpresent;
        private System.Windows.Forms.PictureBox m_pbProcessBar;
        private System.Windows.Forms.PictureBox m_pbConform;
    }
}