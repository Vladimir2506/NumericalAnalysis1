namespace NumericalAnalysis1
{
    partial class MainForm
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
                DisposeMats();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbSrc = new System.Windows.Forms.PictureBox();
            this.pbGuide = new System.Windows.Forms.PictureBox();
            this.pbDst = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSrc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDst)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSrc
            // 
            this.pbSrc.Location = new System.Drawing.Point(33, 34);
            this.pbSrc.Name = "pbSrc";
            this.pbSrc.Size = new System.Drawing.Size(284, 222);
            this.pbSrc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbSrc.TabIndex = 0;
            this.pbSrc.TabStop = false;
            // 
            // pbGuide
            // 
            this.pbGuide.Location = new System.Drawing.Point(301, 262);
            this.pbGuide.Name = "pbGuide";
            this.pbGuide.Size = new System.Drawing.Size(284, 222);
            this.pbGuide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbGuide.TabIndex = 1;
            this.pbGuide.TabStop = false;
            // 
            // pbDst
            // 
            this.pbDst.Location = new System.Drawing.Point(557, 34);
            this.pbDst.Name = "pbDst";
            this.pbDst.Size = new System.Drawing.Size(284, 222);
            this.pbDst.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbDst.TabIndex = 2;
            this.pbDst.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.pbDst);
            this.Controls.Add(this.pbGuide);
            this.Controls.Add(this.pbSrc);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "NumericalAnalysis1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pbSrc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSrc;
        private System.Windows.Forms.PictureBox pbGuide;
        private System.Windows.Forms.PictureBox pbDst;
    }
}

