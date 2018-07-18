namespace MeiFenSys
{
    partial class SetStation
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
            this.textStation = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnSureSet = new DevExpress.XtraEditors.SimpleButton();
            this.labStationErrorTip = new DevExpress.XtraEditors.LabelControl();
            this.labPassTip = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textStation.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textStation
            // 
            this.textStation.Location = new System.Drawing.Point(125, 45);
            this.textStation.Name = "textStation";
            this.textStation.Properties.AutoHeight = false;
            this.textStation.Properties.MaxLength = 7;
            this.textStation.Size = new System.Drawing.Size(154, 33);
            this.textStation.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.LineLocation = DevExpress.XtraEditors.LineLocation.Top;
            this.labelControl3.LineStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.labelControl3.Location = new System.Drawing.Point(47, 46);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 27);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "工位";
            // 
            // btnSureSet
            // 
            this.btnSureSet.Location = new System.Drawing.Point(125, 98);
            this.btnSureSet.Name = "btnSureSet";
            this.btnSureSet.Size = new System.Drawing.Size(154, 33);
            this.btnSureSet.TabIndex = 10;
            this.btnSureSet.Text = "确定";
            this.btnSureSet.Click += new System.EventHandler(this.btnSureSet_Click);
            // 
            // labStationErrorTip
            // 
            this.labStationErrorTip.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labStationErrorTip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labStationErrorTip.LineLocation = DevExpress.XtraEditors.LineLocation.Top;
            this.labStationErrorTip.LineStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.labStationErrorTip.Location = new System.Drawing.Point(288, 53);
            this.labStationErrorTip.Name = "labStationErrorTip";
            this.labStationErrorTip.Size = new System.Drawing.Size(0, 20);
            this.labStationErrorTip.TabIndex = 15;
            // 
            // labPassTip
            // 
            this.labPassTip.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPassTip.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labPassTip.LineLocation = DevExpress.XtraEditors.LineLocation.Top;
            this.labPassTip.LineStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.labPassTip.Location = new System.Drawing.Point(294, 53);
            this.labPassTip.Name = "labPassTip";
            this.labPassTip.Size = new System.Drawing.Size(0, 20);
            this.labPassTip.TabIndex = 16;
            // 
            // SetStation
            // 
            this.AcceptButton = this.btnSureSet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 169);
            this.Controls.Add(this.labPassTip);
            this.Controls.Add(this.labStationErrorTip);
            this.Controls.Add(this.btnSureSet);
            this.Controls.Add(this.textStation);
            this.Controls.Add(this.labelControl3);
            this.Name = "SetStation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetStation";
            ((System.ComponentModel.ISupportInitialize)(this.textStation.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textStation;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnSureSet;
        private DevExpress.XtraEditors.LabelControl labStationErrorTip;
        private DevExpress.XtraEditors.LabelControl labPassTip;
    }
}