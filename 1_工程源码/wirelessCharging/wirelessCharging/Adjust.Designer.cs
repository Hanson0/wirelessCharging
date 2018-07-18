namespace wirelessCharging
{
    partial class Adjust
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
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel5 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel6 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel7 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel8 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnStartAdjust = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.EditValue = 300;
            this.trackBarControl1.Location = new System.Drawing.Point(317, 170);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            trackBarLabel5.Label = "50";
            trackBarLabel5.Value = 50;
            trackBarLabel6.Label = "350";
            trackBarLabel6.Value = 350;
            trackBarLabel7.Label = "650";
            trackBarLabel7.Value = 650;
            trackBarLabel8.Label = "950";
            trackBarLabel8.Value = 950;
            this.trackBarControl1.Properties.Labels.AddRange(new DevExpress.XtraEditors.Repository.TrackBarLabel[] {
            trackBarLabel5,
            trackBarLabel6,
            trackBarLabel7,
            trackBarLabel8});
            this.trackBarControl1.Properties.Maximum = 1000;
            this.trackBarControl1.Properties.Minimum = 50;
            this.trackBarControl1.Size = new System.Drawing.Size(559, 56);
            this.trackBarControl1.TabIndex = 1;
            this.trackBarControl1.Value = 300;
            this.trackBarControl1.EditValueChanged += new System.EventHandler(this.trackBarControl1_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(891, 184);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(0, 18);
            this.labelControl1.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(184, 170);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(127, 36);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "      校准电流\r\n（50mA-1000mA）";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // btnStartAdjust
            // 
            this.btnStartAdjust.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartAdjust.Appearance.Options.UseFont = true;
            this.btnStartAdjust.Location = new System.Drawing.Point(757, 302);
            this.btnStartAdjust.Name = "btnStartAdjust";
            this.btnStartAdjust.Size = new System.Drawing.Size(119, 50);
            this.btnStartAdjust.TabIndex = 4;
            this.btnStartAdjust.Text = "开始校准";
            this.btnStartAdjust.Click += new System.EventHandler(this.btnStartAdjust_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelControl3.Location = new System.Drawing.Point(12, 31);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(1063, 81);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "校准方法：\r\n1、对比CC模式的负载仪上显示的电流值，滑动滑条到对应刻度值位置，开始校准，校准成功后，最后重新上电测试模块;\r\n2、调节负载仪上多组电流值，重复1" +
    ".（如:300mA,400mA,200mA）";
            // 
            // Adjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 592);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnStartAdjust);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.trackBarControl1);
            this.Name = "Adjust";
            this.Text = "Adjust";
            this.Load += new System.EventHandler(this.Adjust_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnStartAdjust;
        private DevExpress.XtraEditors.LabelControl labelControl3;

    }
}