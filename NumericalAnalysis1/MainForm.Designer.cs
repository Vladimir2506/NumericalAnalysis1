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
            this.pbDst = new System.Windows.Forms.PictureBox();
            this.pbGuide = new System.Windows.Forms.PictureBox();
            this.btnSrcLeft = new System.Windows.Forms.Button();
            this.btnSrcRight = new System.Windows.Forms.Button();
            this.btnGuideRight = new System.Windows.Forms.Button();
            this.btnGuideLeft = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnSrcMrk = new System.Windows.Forms.Button();
            this.btnGuideMrk = new System.Windows.Forms.Button();
            this.gbIntp = new System.Windows.Forms.GroupBox();
            this.rbNearest = new System.Windows.Forms.RadioButton();
            this.rbBilinear = new System.Windows.Forms.RadioButton();
            this.rbBicubic = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbSrc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).BeginInit();
            this.gbIntp.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbSrc
            // 
            this.pbSrc.Location = new System.Drawing.Point(35, 35);
            this.pbSrc.Name = "pbSrc";
            this.pbSrc.Size = new System.Drawing.Size(300, 300);
            this.pbSrc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSrc.TabIndex = 0;
            this.pbSrc.TabStop = false;
            // 
            // pbDst
            // 
            this.pbDst.Location = new System.Drawing.Point(735, 35);
            this.pbDst.Name = "pbDst";
            this.pbDst.Size = new System.Drawing.Size(300, 300);
            this.pbDst.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDst.TabIndex = 1;
            this.pbDst.TabStop = false;
            // 
            // pbGuide
            // 
            this.pbGuide.Location = new System.Drawing.Point(385, 35);
            this.pbGuide.Name = "pbGuide";
            this.pbGuide.Size = new System.Drawing.Size(300, 300);
            this.pbGuide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGuide.TabIndex = 2;
            this.pbGuide.TabStop = false;
            // 
            // btnSrcLeft
            // 
            this.btnSrcLeft.BackColor = System.Drawing.Color.Transparent;
            this.btnSrcLeft.BackgroundImage = global::NumericalAnalysis1.Properties.Resources.left_circle;
            this.btnSrcLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSrcLeft.Location = new System.Drawing.Point(35, 350);
            this.btnSrcLeft.Name = "btnSrcLeft";
            this.btnSrcLeft.Size = new System.Drawing.Size(45, 45);
            this.btnSrcLeft.TabIndex = 3;
            this.btnSrcLeft.UseVisualStyleBackColor = false;
            this.btnSrcLeft.Click += new System.EventHandler(this.OnbtnSrcLeftClick);
            // 
            // btnSrcRight
            // 
            this.btnSrcRight.BackColor = System.Drawing.Color.Transparent;
            this.btnSrcRight.BackgroundImage = global::NumericalAnalysis1.Properties.Resources.right_circle;
            this.btnSrcRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSrcRight.Location = new System.Drawing.Point(290, 350);
            this.btnSrcRight.Name = "btnSrcRight";
            this.btnSrcRight.Size = new System.Drawing.Size(45, 45);
            this.btnSrcRight.TabIndex = 4;
            this.btnSrcRight.UseVisualStyleBackColor = false;
            this.btnSrcRight.Click += new System.EventHandler(this.OnbtnSrcRightClick);
            // 
            // btnGuideRight
            // 
            this.btnGuideRight.BackColor = System.Drawing.Color.Transparent;
            this.btnGuideRight.BackgroundImage = global::NumericalAnalysis1.Properties.Resources.right_circle;
            this.btnGuideRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGuideRight.Location = new System.Drawing.Point(640, 350);
            this.btnGuideRight.Name = "btnGuideRight";
            this.btnGuideRight.Size = new System.Drawing.Size(45, 45);
            this.btnGuideRight.TabIndex = 6;
            this.btnGuideRight.UseVisualStyleBackColor = false;
            this.btnGuideRight.Click += new System.EventHandler(this.OnbtnGuideRightClick);
            // 
            // btnGuideLeft
            // 
            this.btnGuideLeft.BackColor = System.Drawing.Color.Transparent;
            this.btnGuideLeft.BackgroundImage = global::NumericalAnalysis1.Properties.Resources.left_circle;
            this.btnGuideLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGuideLeft.Location = new System.Drawing.Point(385, 350);
            this.btnGuideLeft.Name = "btnGuideLeft";
            this.btnGuideLeft.Size = new System.Drawing.Size(45, 45);
            this.btnGuideLeft.TabIndex = 5;
            this.btnGuideLeft.UseVisualStyleBackColor = false;
            this.btnGuideLeft.Click += new System.EventHandler(this.OnbtnGuideLeftClick);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(735, 350);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(120, 45);
            this.btnExecute.TabIndex = 7;
            this.btnExecute.Text = "应用变换";
            this.btnExecute.UseVisualStyleBackColor = true;
            // 
            // btnSrcMrk
            // 
            this.btnSrcMrk.Location = new System.Drawing.Point(125, 350);
            this.btnSrcMrk.Name = "btnSrcMrk";
            this.btnSrcMrk.Size = new System.Drawing.Size(120, 45);
            this.btnSrcMrk.TabIndex = 8;
            this.btnSrcMrk.UseVisualStyleBackColor = true;
            this.btnSrcMrk.Click += new System.EventHandler(this.OnbtnSrcMrkClick);
            // 
            // btnGuideMrk
            // 
            this.btnGuideMrk.Location = new System.Drawing.Point(475, 350);
            this.btnGuideMrk.Name = "btnGuideMrk";
            this.btnGuideMrk.Size = new System.Drawing.Size(120, 45);
            this.btnGuideMrk.TabIndex = 9;
            this.btnGuideMrk.UseVisualStyleBackColor = true;
            this.btnGuideMrk.Click += new System.EventHandler(this.OnbtnGuideMrkClick);
            // 
            // gbIntp
            // 
            this.gbIntp.Controls.Add(this.rbBicubic);
            this.gbIntp.Controls.Add(this.rbBilinear);
            this.gbIntp.Controls.Add(this.rbNearest);
            this.gbIntp.Location = new System.Drawing.Point(640, 413);
            this.gbIntp.Name = "gbIntp";
            this.gbIntp.Size = new System.Drawing.Size(395, 65);
            this.gbIntp.TabIndex = 10;
            this.gbIntp.TabStop = false;
            this.gbIntp.Text = "插值方法";
            // 
            // rbNearest
            // 
            this.rbNearest.AutoSize = true;
            this.rbNearest.Location = new System.Drawing.Point(20, 25);
            this.rbNearest.Name = "rbNearest";
            this.rbNearest.Size = new System.Drawing.Size(105, 24);
            this.rbNearest.TabIndex = 0;
            this.rbNearest.TabStop = true;
            this.rbNearest.Text = "最近邻插值";
            this.rbNearest.UseVisualStyleBackColor = true;
            this.rbNearest.CheckedChanged += new System.EventHandler(this.OnrbNearestCheck);
            // 
            // rbBilinear
            // 
            this.rbBilinear.AutoSize = true;
            this.rbBilinear.Location = new System.Drawing.Point(145, 25);
            this.rbBilinear.Name = "rbBilinear";
            this.rbBilinear.Size = new System.Drawing.Size(105, 24);
            this.rbBilinear.TabIndex = 1;
            this.rbBilinear.TabStop = true;
            this.rbBilinear.Text = "双线性插值";
            this.rbBilinear.UseVisualStyleBackColor = true;
            this.rbBilinear.CheckedChanged += new System.EventHandler(this.OnrbBilinearCheck);
            // 
            // rbBicubic
            // 
            this.rbBicubic.AutoSize = true;
            this.rbBicubic.Location = new System.Drawing.Point(270, 25);
            this.rbBicubic.Name = "rbBicubic";
            this.rbBicubic.Size = new System.Drawing.Size(105, 24);
            this.rbBicubic.TabIndex = 2;
            this.rbBicubic.TabStop = true;
            this.rbBicubic.Text = "双三次插值";
            this.rbBicubic.UseVisualStyleBackColor = true;
            this.rbBicubic.CheckedChanged += new System.EventHandler(this.OnBicubicCheck);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 603);
            this.Controls.Add(this.gbIntp);
            this.Controls.Add(this.btnGuideMrk);
            this.Controls.Add(this.btnSrcMrk);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnGuideRight);
            this.Controls.Add(this.btnGuideLeft);
            this.Controls.Add(this.btnSrcRight);
            this.Controls.Add(this.btnSrcLeft);
            this.Controls.Add(this.pbGuide);
            this.Controls.Add(this.pbDst);
            this.Controls.Add(this.pbSrc);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "NumericalAnalysis1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pbSrc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).EndInit();
            this.gbIntp.ResumeLayout(false);
            this.gbIntp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSrc;
        private System.Windows.Forms.PictureBox pbDst;
        private System.Windows.Forms.PictureBox pbGuide;
        private System.Windows.Forms.Button btnSrcLeft;
        private System.Windows.Forms.Button btnSrcRight;
        private System.Windows.Forms.Button btnGuideRight;
        private System.Windows.Forms.Button btnGuideLeft;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnSrcMrk;
        private System.Windows.Forms.Button btnGuideMrk;
        private System.Windows.Forms.GroupBox gbIntp;
        private System.Windows.Forms.RadioButton rbBicubic;
        private System.Windows.Forms.RadioButton rbBilinear;
        private System.Windows.Forms.RadioButton rbNearest;
    }
}

