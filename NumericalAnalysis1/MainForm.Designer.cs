﻿namespace NumericalAnalysis1
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
                imgSrc.Dispose();
                imgGuide.Dispose();
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
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnSrcMrk = new System.Windows.Forms.Button();
            this.btnGuideMrk = new System.Windows.Forms.Button();
            this.gbIntp = new System.Windows.Forms.GroupBox();
            this.rbBicubic = new System.Windows.Forms.RadioButton();
            this.rbBilinear = new System.Windows.Forms.RadioButton();
            this.rbNearest = new System.Windows.Forms.RadioButton();
            this.gbTsfm = new System.Windows.Forms.GroupBox();
            this.rbTps = new System.Windows.Forms.RadioButton();
            this.rbBsp = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoadImgSrc = new System.Windows.Forms.Button();
            this.btnLoadMrkSrc = new System.Windows.Forms.Button();
            this.btnLoadImgGuide = new System.Windows.Forms.Button();
            this.btnLoadMrkGuide = new System.Windows.Forms.Button();
            this.lblSrc = new System.Windows.Forms.Label();
            this.lblGuide = new System.Windows.Forms.Label();
            this.lblDst = new System.Windows.Forms.Label();
            this.btnGuideRight = new System.Windows.Forms.Button();
            this.btnGuideLeft = new System.Windows.Forms.Button();
            this.btnSrcRight = new System.Windows.Forms.Button();
            this.btnSrcLeft = new System.Windows.Forms.Button();
            this.pbGuide = new System.Windows.Forms.PictureBox();
            this.pbDst = new System.Windows.Forms.PictureBox();
            this.pbSrc = new System.Windows.Forms.PictureBox();
            this.btnSrcDetect = new System.Windows.Forms.Button();
            this.btnGuideDetect = new System.Windows.Forms.Button();
            this.btnSaveSrcMrk = new System.Windows.Forms.Button();
            this.btnSaveMrkGuide = new System.Windows.Forms.Button();
            this.gbIntp.SuspendLayout();
            this.gbTsfm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSrc)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.White;
            this.btnExecute.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnExecute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnExecute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExecute.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnExecute.Location = new System.Drawing.Point(735, 400);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(120, 45);
            this.btnExecute.TabIndex = 7;
            this.btnExecute.Text = "应用变换";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.OnbtnExecuteClick);
            // 
            // btnSrcMrk
            // 
            this.btnSrcMrk.BackColor = System.Drawing.Color.White;
            this.btnSrcMrk.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnSrcMrk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSrcMrk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnSrcMrk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrcMrk.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSrcMrk.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSrcMrk.Location = new System.Drawing.Point(125, 400);
            this.btnSrcMrk.Name = "btnSrcMrk";
            this.btnSrcMrk.Size = new System.Drawing.Size(120, 45);
            this.btnSrcMrk.TabIndex = 8;
            this.btnSrcMrk.UseVisualStyleBackColor = false;
            this.btnSrcMrk.Click += new System.EventHandler(this.OnbtnSrcMrkClick);
            // 
            // btnGuideMrk
            // 
            this.btnGuideMrk.BackColor = System.Drawing.Color.White;
            this.btnGuideMrk.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnGuideMrk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGuideMrk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnGuideMrk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuideMrk.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGuideMrk.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnGuideMrk.Location = new System.Drawing.Point(475, 400);
            this.btnGuideMrk.Name = "btnGuideMrk";
            this.btnGuideMrk.Size = new System.Drawing.Size(120, 45);
            this.btnGuideMrk.TabIndex = 9;
            this.btnGuideMrk.UseVisualStyleBackColor = false;
            this.btnGuideMrk.Click += new System.EventHandler(this.OnbtnGuideMrkClick);
            // 
            // gbIntp
            // 
            this.gbIntp.BackColor = System.Drawing.Color.Transparent;
            this.gbIntp.Controls.Add(this.rbBicubic);
            this.gbIntp.Controls.Add(this.rbBilinear);
            this.gbIntp.Controls.Add(this.rbNearest);
            this.gbIntp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbIntp.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbIntp.ForeColor = System.Drawing.Color.RoyalBlue;
            this.gbIntp.Location = new System.Drawing.Point(735, 525);
            this.gbIntp.Margin = new System.Windows.Forms.Padding(0);
            this.gbIntp.Name = "gbIntp";
            this.gbIntp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gbIntp.Size = new System.Drawing.Size(300, 65);
            this.gbIntp.TabIndex = 10;
            this.gbIntp.TabStop = false;
            this.gbIntp.Text = "插值方法";
            // 
            // rbBicubic
            // 
            this.rbBicubic.AutoSize = true;
            this.rbBicubic.Location = new System.Drawing.Point(204, 25);
            this.rbBicubic.Name = "rbBicubic";
            this.rbBicubic.Size = new System.Drawing.Size(90, 29);
            this.rbBicubic.TabIndex = 2;
            this.rbBicubic.TabStop = true;
            this.rbBicubic.Text = "双三次";
            this.rbBicubic.UseVisualStyleBackColor = true;
            this.rbBicubic.CheckedChanged += new System.EventHandler(this.OnBicubicCheck);
            // 
            // rbBilinear
            // 
            this.rbBilinear.AutoSize = true;
            this.rbBilinear.Location = new System.Drawing.Point(108, 25);
            this.rbBilinear.Name = "rbBilinear";
            this.rbBilinear.Size = new System.Drawing.Size(90, 29);
            this.rbBilinear.TabIndex = 1;
            this.rbBilinear.TabStop = true;
            this.rbBilinear.Text = "双线性";
            this.rbBilinear.UseVisualStyleBackColor = true;
            this.rbBilinear.CheckedChanged += new System.EventHandler(this.OnrbBilinearCheck);
            // 
            // rbNearest
            // 
            this.rbNearest.AutoSize = true;
            this.rbNearest.Location = new System.Drawing.Point(20, 25);
            this.rbNearest.Name = "rbNearest";
            this.rbNearest.Size = new System.Drawing.Size(90, 29);
            this.rbNearest.TabIndex = 0;
            this.rbNearest.TabStop = true;
            this.rbNearest.Text = "最近邻";
            this.rbNearest.UseVisualStyleBackColor = true;
            this.rbNearest.CheckedChanged += new System.EventHandler(this.OnrbNearestCheck);
            // 
            // gbTsfm
            // 
            this.gbTsfm.BackColor = System.Drawing.Color.Transparent;
            this.gbTsfm.Controls.Add(this.rbTps);
            this.gbTsfm.Controls.Add(this.rbBsp);
            this.gbTsfm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbTsfm.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbTsfm.ForeColor = System.Drawing.Color.RoyalBlue;
            this.gbTsfm.Location = new System.Drawing.Point(735, 450);
            this.gbTsfm.Margin = new System.Windows.Forms.Padding(0);
            this.gbTsfm.Name = "gbTsfm";
            this.gbTsfm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gbTsfm.Size = new System.Drawing.Size(300, 65);
            this.gbTsfm.TabIndex = 11;
            this.gbTsfm.TabStop = false;
            this.gbTsfm.Text = "变换方法";
            // 
            // rbTps
            // 
            this.rbTps.AutoSize = true;
            this.rbTps.Location = new System.Drawing.Point(175, 25);
            this.rbTps.Name = "rbTps";
            this.rbTps.Size = new System.Drawing.Size(103, 29);
            this.rbTps.TabIndex = 1;
            this.rbTps.TabStop = true;
            this.rbTps.Text = "TPS变换";
            this.rbTps.UseVisualStyleBackColor = true;
            this.rbTps.CheckedChanged += new System.EventHandler(this.OnrbTpsCheck);
            // 
            // rbBsp
            // 
            this.rbBsp.AutoSize = true;
            this.rbBsp.Location = new System.Drawing.Point(20, 25);
            this.rbBsp.Name = "rbBsp";
            this.rbBsp.Size = new System.Drawing.Size(120, 29);
            this.rbBsp.TabIndex = 0;
            this.rbBsp.TabStop = true;
            this.rbBsp.Text = "B样条变换";
            this.rbBsp.UseVisualStyleBackColor = true;
            this.rbBsp.CheckedChanged += new System.EventHandler(this.OnrbBspCheck);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSave.Location = new System.Drawing.Point(915, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保存图片";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.OnbtnSaveClick);
            // 
            // btnLoadImgSrc
            // 
            this.btnLoadImgSrc.BackColor = System.Drawing.Color.White;
            this.btnLoadImgSrc.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnLoadImgSrc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLoadImgSrc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnLoadImgSrc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadImgSrc.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadImgSrc.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnLoadImgSrc.Location = new System.Drawing.Point(35, 465);
            this.btnLoadImgSrc.Name = "btnLoadImgSrc";
            this.btnLoadImgSrc.Size = new System.Drawing.Size(120, 45);
            this.btnLoadImgSrc.TabIndex = 13;
            this.btnLoadImgSrc.Text = "加载图片";
            this.btnLoadImgSrc.UseVisualStyleBackColor = false;
            this.btnLoadImgSrc.Click += new System.EventHandler(this.OmbtnLoadImgSrcClick);
            // 
            // btnLoadMrkSrc
            // 
            this.btnLoadMrkSrc.BackColor = System.Drawing.Color.White;
            this.btnLoadMrkSrc.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnLoadMrkSrc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLoadMrkSrc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnLoadMrkSrc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadMrkSrc.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadMrkSrc.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnLoadMrkSrc.Location = new System.Drawing.Point(215, 465);
            this.btnLoadMrkSrc.Name = "btnLoadMrkSrc";
            this.btnLoadMrkSrc.Size = new System.Drawing.Size(120, 45);
            this.btnLoadMrkSrc.TabIndex = 14;
            this.btnLoadMrkSrc.Text = "加载关键点";
            this.btnLoadMrkSrc.UseVisualStyleBackColor = false;
            this.btnLoadMrkSrc.Click += new System.EventHandler(this.OnbtnLoadMrkSrcClick);
            // 
            // btnLoadImgGuide
            // 
            this.btnLoadImgGuide.BackColor = System.Drawing.Color.White;
            this.btnLoadImgGuide.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnLoadImgGuide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLoadImgGuide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnLoadImgGuide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadImgGuide.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadImgGuide.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnLoadImgGuide.Location = new System.Drawing.Point(385, 465);
            this.btnLoadImgGuide.Name = "btnLoadImgGuide";
            this.btnLoadImgGuide.Size = new System.Drawing.Size(120, 45);
            this.btnLoadImgGuide.TabIndex = 15;
            this.btnLoadImgGuide.Text = "加载图片";
            this.btnLoadImgGuide.UseVisualStyleBackColor = false;
            this.btnLoadImgGuide.Click += new System.EventHandler(this.OmbtnLoadImgGuideClick);
            // 
            // btnLoadMrkGuide
            // 
            this.btnLoadMrkGuide.BackColor = System.Drawing.Color.White;
            this.btnLoadMrkGuide.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnLoadMrkGuide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLoadMrkGuide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnLoadMrkGuide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadMrkGuide.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadMrkGuide.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnLoadMrkGuide.Location = new System.Drawing.Point(565, 465);
            this.btnLoadMrkGuide.Name = "btnLoadMrkGuide";
            this.btnLoadMrkGuide.Size = new System.Drawing.Size(120, 45);
            this.btnLoadMrkGuide.TabIndex = 16;
            this.btnLoadMrkGuide.Text = "加载关键点";
            this.btnLoadMrkGuide.UseVisualStyleBackColor = false;
            this.btnLoadMrkGuide.Click += new System.EventHandler(this.OnbtnLoadMrkGuideClick);
            // 
            // lblSrc
            // 
            this.lblSrc.AutoSize = true;
            this.lblSrc.BackColor = System.Drawing.Color.Transparent;
            this.lblSrc.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSrc.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblSrc.Location = new System.Drawing.Point(150, 365);
            this.lblSrc.Name = "lblSrc";
            this.lblSrc.Size = new System.Drawing.Size(69, 25);
            this.lblSrc.TabIndex = 17;
            this.lblSrc.Text = "源图片";
            // 
            // lblGuide
            // 
            this.lblGuide.AutoSize = true;
            this.lblGuide.BackColor = System.Drawing.Color.Transparent;
            this.lblGuide.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGuide.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblGuide.Location = new System.Drawing.Point(500, 365);
            this.lblGuide.Name = "lblGuide";
            this.lblGuide.Size = new System.Drawing.Size(88, 25);
            this.lblGuide.TabIndex = 18;
            this.lblGuide.Text = "导向图片";
            // 
            // lblDst
            // 
            this.lblDst.AutoSize = true;
            this.lblDst.BackColor = System.Drawing.Color.Transparent;
            this.lblDst.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDst.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblDst.Location = new System.Drawing.Point(850, 365);
            this.lblDst.Name = "lblDst";
            this.lblDst.Size = new System.Drawing.Size(88, 25);
            this.lblDst.TabIndex = 19;
            this.lblDst.Text = "目标图片";
            // 
            // btnGuideRight
            // 
            this.btnGuideRight.BackColor = System.Drawing.Color.White;
            this.btnGuideRight.BackgroundImage = global::NumericalAnalysis1.Properties.Resources.right_circle;
            this.btnGuideRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGuideRight.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnGuideRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGuideRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnGuideRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuideRight.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGuideRight.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnGuideRight.Location = new System.Drawing.Point(640, 400);
            this.btnGuideRight.Name = "btnGuideRight";
            this.btnGuideRight.Size = new System.Drawing.Size(45, 45);
            this.btnGuideRight.TabIndex = 6;
            this.btnGuideRight.UseVisualStyleBackColor = false;
            this.btnGuideRight.Click += new System.EventHandler(this.OnbtnGuideRightClick);
            // 
            // btnGuideLeft
            // 
            this.btnGuideLeft.BackColor = System.Drawing.Color.White;
            this.btnGuideLeft.BackgroundImage = global::NumericalAnalysis1.Properties.Resources.left_circle;
            this.btnGuideLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGuideLeft.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnGuideLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGuideLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnGuideLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuideLeft.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGuideLeft.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnGuideLeft.Location = new System.Drawing.Point(385, 400);
            this.btnGuideLeft.Name = "btnGuideLeft";
            this.btnGuideLeft.Size = new System.Drawing.Size(45, 45);
            this.btnGuideLeft.TabIndex = 5;
            this.btnGuideLeft.UseVisualStyleBackColor = false;
            this.btnGuideLeft.Click += new System.EventHandler(this.OnbtnGuideLeftClick);
            // 
            // btnSrcRight
            // 
            this.btnSrcRight.BackColor = System.Drawing.Color.White;
            this.btnSrcRight.BackgroundImage = global::NumericalAnalysis1.Properties.Resources.right_circle;
            this.btnSrcRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSrcRight.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnSrcRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSrcRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnSrcRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrcRight.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSrcRight.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSrcRight.Location = new System.Drawing.Point(290, 400);
            this.btnSrcRight.Name = "btnSrcRight";
            this.btnSrcRight.Size = new System.Drawing.Size(45, 45);
            this.btnSrcRight.TabIndex = 4;
            this.btnSrcRight.UseVisualStyleBackColor = false;
            this.btnSrcRight.Click += new System.EventHandler(this.OnbtnSrcRightClick);
            // 
            // btnSrcLeft
            // 
            this.btnSrcLeft.BackColor = System.Drawing.Color.White;
            this.btnSrcLeft.BackgroundImage = global::NumericalAnalysis1.Properties.Resources.left_circle;
            this.btnSrcLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSrcLeft.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnSrcLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSrcLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnSrcLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrcLeft.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSrcLeft.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSrcLeft.Location = new System.Drawing.Point(35, 400);
            this.btnSrcLeft.Name = "btnSrcLeft";
            this.btnSrcLeft.Size = new System.Drawing.Size(45, 45);
            this.btnSrcLeft.TabIndex = 3;
            this.btnSrcLeft.UseVisualStyleBackColor = false;
            this.btnSrcLeft.Click += new System.EventHandler(this.OnbtnSrcLeftClick);
            // 
            // pbGuide
            // 
            this.pbGuide.BackColor = System.Drawing.Color.Transparent;
            this.pbGuide.Location = new System.Drawing.Point(385, 35);
            this.pbGuide.Name = "pbGuide";
            this.pbGuide.Size = new System.Drawing.Size(300, 300);
            this.pbGuide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGuide.TabIndex = 2;
            this.pbGuide.TabStop = false;
            // 
            // pbDst
            // 
            this.pbDst.BackColor = System.Drawing.Color.Transparent;
            this.pbDst.Location = new System.Drawing.Point(735, 35);
            this.pbDst.Name = "pbDst";
            this.pbDst.Size = new System.Drawing.Size(300, 300);
            this.pbDst.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDst.TabIndex = 1;
            this.pbDst.TabStop = false;
            // 
            // pbSrc
            // 
            this.pbSrc.BackColor = System.Drawing.Color.Transparent;
            this.pbSrc.Location = new System.Drawing.Point(35, 35);
            this.pbSrc.Name = "pbSrc";
            this.pbSrc.Size = new System.Drawing.Size(300, 300);
            this.pbSrc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSrc.TabIndex = 0;
            this.pbSrc.TabStop = false;
            // 
            // btnSrcDetect
            // 
            this.btnSrcDetect.BackColor = System.Drawing.Color.White;
            this.btnSrcDetect.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnSrcDetect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSrcDetect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnSrcDetect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSrcDetect.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSrcDetect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSrcDetect.Location = new System.Drawing.Point(35, 534);
            this.btnSrcDetect.Name = "btnSrcDetect";
            this.btnSrcDetect.Size = new System.Drawing.Size(120, 45);
            this.btnSrcDetect.TabIndex = 21;
            this.btnSrcDetect.Text = "检测关键点";
            this.btnSrcDetect.UseVisualStyleBackColor = false;
            this.btnSrcDetect.Click += new System.EventHandler(this.OnbtnSrcDetectClick);
            // 
            // btnGuideDetect
            // 
            this.btnGuideDetect.BackColor = System.Drawing.Color.White;
            this.btnGuideDetect.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnGuideDetect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGuideDetect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnGuideDetect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuideDetect.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGuideDetect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnGuideDetect.Location = new System.Drawing.Point(385, 534);
            this.btnGuideDetect.Name = "btnGuideDetect";
            this.btnGuideDetect.Size = new System.Drawing.Size(120, 45);
            this.btnGuideDetect.TabIndex = 22;
            this.btnGuideDetect.Text = "检测关键点";
            this.btnGuideDetect.UseVisualStyleBackColor = false;
            this.btnGuideDetect.Click += new System.EventHandler(this.OnbtnGuideDetectClick);
            // 
            // btnSaveSrcMrk
            // 
            this.btnSaveSrcMrk.BackColor = System.Drawing.Color.White;
            this.btnSaveSrcMrk.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnSaveSrcMrk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSaveSrcMrk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnSaveSrcMrk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSrcMrk.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveSrcMrk.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSaveSrcMrk.Location = new System.Drawing.Point(215, 534);
            this.btnSaveSrcMrk.Name = "btnSaveSrcMrk";
            this.btnSaveSrcMrk.Size = new System.Drawing.Size(120, 45);
            this.btnSaveSrcMrk.TabIndex = 23;
            this.btnSaveSrcMrk.Text = "保存关键点";
            this.btnSaveSrcMrk.UseVisualStyleBackColor = false;
            this.btnSaveSrcMrk.Click += new System.EventHandler(this.OnbtnSaveSrcMrkClick);
            // 
            // btnSaveMrkGuide
            // 
            this.btnSaveMrkGuide.BackColor = System.Drawing.Color.White;
            this.btnSaveMrkGuide.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnSaveMrkGuide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSaveMrkGuide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnSaveMrkGuide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveMrkGuide.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveMrkGuide.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSaveMrkGuide.Location = new System.Drawing.Point(565, 534);
            this.btnSaveMrkGuide.Name = "btnSaveMrkGuide";
            this.btnSaveMrkGuide.Size = new System.Drawing.Size(120, 45);
            this.btnSaveMrkGuide.TabIndex = 24;
            this.btnSaveMrkGuide.Text = "保存关键点";
            this.btnSaveMrkGuide.UseVisualStyleBackColor = false;
            this.btnSaveMrkGuide.Click += new System.EventHandler(this.OnbtnSaveMrkGuideClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1072, 603);
            this.Controls.Add(this.btnSaveMrkGuide);
            this.Controls.Add(this.btnSaveSrcMrk);
            this.Controls.Add(this.btnGuideDetect);
            this.Controls.Add(this.btnSrcDetect);
            this.Controls.Add(this.lblDst);
            this.Controls.Add(this.lblGuide);
            this.Controls.Add(this.lblSrc);
            this.Controls.Add(this.btnLoadMrkGuide);
            this.Controls.Add(this.btnLoadImgGuide);
            this.Controls.Add(this.btnLoadMrkSrc);
            this.Controls.Add(this.btnLoadImgSrc);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbTsfm);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "NumericalAnalysis1";
            this.Load += new System.EventHandler(this.OnLoad);
            this.gbIntp.ResumeLayout(false);
            this.gbIntp.PerformLayout();
            this.gbTsfm.ResumeLayout(false);
            this.gbTsfm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSrc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox gbTsfm;
        private System.Windows.Forms.RadioButton rbTps;
        private System.Windows.Forms.RadioButton rbBsp;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoadImgSrc;
        private System.Windows.Forms.Button btnLoadMrkSrc;
        private System.Windows.Forms.Button btnLoadImgGuide;
        private System.Windows.Forms.Button btnLoadMrkGuide;
        private System.Windows.Forms.Label lblSrc;
        private System.Windows.Forms.Label lblGuide;
        private System.Windows.Forms.Label lblDst;
        private System.Windows.Forms.Button btnSrcDetect;
        private System.Windows.Forms.Button btnGuideDetect;
        private System.Windows.Forms.Button btnSaveSrcMrk;
        private System.Windows.Forms.Button btnSaveMrkGuide;
    }
}

