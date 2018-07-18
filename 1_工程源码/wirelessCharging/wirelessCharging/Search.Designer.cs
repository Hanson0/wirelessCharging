namespace wirelessCharging
{
    partial class Search
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.dtPStart = new System.Windows.Forms.DateTimePicker();
            this.dtPEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textSn = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.textBox = new DevExpress.XtraEditors.TextEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::wirelessCharging.Wait), true, true, true);
            this.rabtnCheckByDate = new System.Windows.Forms.RadioButton();
            this.rabtnCheckBySn = new System.Windows.Forms.RadioButton();
            this.rabtnCheckByBox = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.textSn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dtPStart
            // 
            this.dtPStart.Location = new System.Drawing.Point(138, 12);
            this.dtPStart.Name = "dtPStart";
            this.dtPStart.Size = new System.Drawing.Size(156, 26);
            this.dtPStart.TabIndex = 2;
            this.dtPStart.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dtPEnd
            // 
            this.dtPEnd.Location = new System.Drawing.Point(326, 12);
            this.dtPEnd.Name = "dtPEnd";
            this.dtPEnd.Size = new System.Drawing.Size(156, 26);
            this.dtPEnd.TabIndex = 3;
            this.dtPEnd.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSearch.Location = new System.Drawing.Point(486, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(77, 26);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "查询";
            this.btnSearch.ToolTip = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnExport
            // 
            this.btnExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExport.Location = new System.Drawing.Point(569, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(77, 26);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "导出Excel";
            this.btnExport.ToolTip = "导出到Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "~";
            // 
            // textSn
            // 
            this.textSn.Location = new System.Drawing.Point(138, 44);
            this.textSn.Name = "textSn";
            this.textSn.Size = new System.Drawing.Size(344, 24);
            this.textSn.TabIndex = 11;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 104);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(973, 465);
            this.gridControl1.TabIndex = 15;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(138, 74);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(344, 24);
            this.textBox.TabIndex = 16;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // rabtnCheckByDate
            // 
            this.rabtnCheckByDate.AutoSize = true;
            this.rabtnCheckByDate.Location = new System.Drawing.Point(28, 12);
            this.rabtnCheckByDate.Name = "rabtnCheckByDate";
            this.rabtnCheckByDate.Size = new System.Drawing.Size(104, 22);
            this.rabtnCheckByDate.TabIndex = 18;
            this.rabtnCheckByDate.TabStop = true;
            this.rabtnCheckByDate.Text = "按日期查询";
            this.rabtnCheckByDate.UseVisualStyleBackColor = true;
            // 
            // rabtnCheckBySn
            // 
            this.rabtnCheckBySn.AutoSize = true;
            this.rabtnCheckBySn.Location = new System.Drawing.Point(28, 45);
            this.rabtnCheckBySn.Name = "rabtnCheckBySn";
            this.rabtnCheckBySn.Size = new System.Drawing.Size(104, 22);
            this.rabtnCheckBySn.TabIndex = 19;
            this.rabtnCheckBySn.TabStop = true;
            this.rabtnCheckBySn.Text = "按条码查询";
            this.rabtnCheckBySn.UseVisualStyleBackColor = true;
            // 
            // rabtnCheckByBox
            // 
            this.rabtnCheckByBox.AutoSize = true;
            this.rabtnCheckByBox.Location = new System.Drawing.Point(28, 76);
            this.rabtnCheckByBox.Name = "rabtnCheckByBox";
            this.rabtnCheckByBox.Size = new System.Drawing.Size(104, 22);
            this.rabtnCheckByBox.TabIndex = 20;
            this.rabtnCheckByBox.TabStop = true;
            this.rabtnCheckByBox.Text = "按箱号查询";
            this.rabtnCheckByBox.UseVisualStyleBackColor = true;
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 569);
            this.Controls.Add(this.rabtnCheckByBox);
            this.Controls.Add(this.rabtnCheckBySn);
            this.Controls.Add(this.rabtnCheckByDate);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.textSn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtPEnd);
            this.Controls.Add(this.dtPStart);
            this.Controls.Add(this.gridControl1);
            this.Name = "Search";
            this.Text = "Search";
            this.Load += new System.EventHandler(this.Search_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textSn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtPStart;
        private System.Windows.Forms.DateTimePicker dtPEnd;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textSn;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit textBox;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private System.Windows.Forms.RadioButton rabtnCheckByDate;
        private System.Windows.Forms.RadioButton rabtnCheckBySn;
        private System.Windows.Forms.RadioButton rabtnCheckByBox;
    }
}