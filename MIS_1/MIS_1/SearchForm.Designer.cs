namespace MIS_1
{
    partial class VehicleSearchForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VehicleSearchForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textVehicleSearch = new System.Windows.Forms.TextBox();
            this.linkLabelVehicle = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVehicleSearch = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expr1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recordTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicleInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet12 = new MIS_1.ProjectDataSet12();
            this.vehicleInfoTableAdapter = new MIS_1.ProjectDataSet12TableAdapters.VehicleInfoTableAdapter();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet12)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(0, 513);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // textVehicleSearch
            // 
            this.textVehicleSearch.Location = new System.Drawing.Point(99, 45);
            this.textVehicleSearch.Name = "textVehicleSearch";
            this.textVehicleSearch.Size = new System.Drawing.Size(264, 21);
            this.textVehicleSearch.TabIndex = 1;
            // 
            // linkLabelVehicle
            // 
            this.linkLabelVehicle.AutoSize = true;
            this.linkLabelVehicle.Location = new System.Drawing.Point(498, 52);
            this.linkLabelVehicle.Name = "linkLabelVehicle";
            this.linkLabelVehicle.Size = new System.Drawing.Size(77, 12);
            this.linkLabelVehicle.TabIndex = 4;
            this.linkLabelVehicle.TabStop = true;
            this.linkLabelVehicle.Text = "设置搜索条件";
            this.linkLabelVehicle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelVehicle_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabelVehicle);
            this.panel1.Controls.Add(this.textVehicleSearch);
            this.panel1.Controls.Add(this.btnVehicleSearch);
            this.panel1.Location = new System.Drawing.Point(267, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 108);
            this.panel1.TabIndex = 6;
            // 
            // btnVehicleSearch
            // 
            this.btnVehicleSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVehicleSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnVehicleSearch.Image")));
            this.btnVehicleSearch.Location = new System.Drawing.Point(394, 45);
            this.btnVehicleSearch.Name = "btnVehicleSearch";
            this.btnVehicleSearch.Size = new System.Drawing.Size(85, 26);
            this.btnVehicleSearch.TabIndex = 2;
            this.btnVehicleSearch.Text = "搜索";
            this.btnVehicleSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVehicleSearch.UseVisualStyleBackColor = true;
            this.btnVehicleSearch.Click += new System.EventHandler(this.btnVehicleSearch_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.HotTracking = true;
            this.treeView1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.treeView1.Location = new System.Drawing.Point(3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(222, 461);
            this.treeView1.TabIndex = 9;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(267, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 340);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "车辆信息";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.expr1DataGridViewTextBoxColumn,
            this.regionNameDataGridViewTextBoxColumn,
            this.recordTimeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.vehicleInfoBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(569, 320);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "编号";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "类型";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // expr1DataGridViewTextBoxColumn
            // 
            this.expr1DataGridViewTextBoxColumn.DataPropertyName = "Expr1";
            this.expr1DataGridViewTextBoxColumn.HeaderText = "路口";
            this.expr1DataGridViewTextBoxColumn.Name = "expr1DataGridViewTextBoxColumn";
            // 
            // regionNameDataGridViewTextBoxColumn
            // 
            this.regionNameDataGridViewTextBoxColumn.DataPropertyName = "RegionName";
            this.regionNameDataGridViewTextBoxColumn.HeaderText = "所属区县";
            this.regionNameDataGridViewTextBoxColumn.Name = "regionNameDataGridViewTextBoxColumn";
            // 
            // recordTimeDataGridViewTextBoxColumn
            // 
            this.recordTimeDataGridViewTextBoxColumn.DataPropertyName = "RecordTime";
            this.recordTimeDataGridViewTextBoxColumn.HeaderText = "时间";
            this.recordTimeDataGridViewTextBoxColumn.Name = "recordTimeDataGridViewTextBoxColumn";
            // 
            // vehicleInfoBindingSource
            // 
            this.vehicleInfoBindingSource.DataMember = "VehicleInfo";
            this.vehicleInfoBindingSource.DataSource = this.projectDataSet12;
            // 
            // projectDataSet12
            // 
            this.projectDataSet12.DataSetName = "ProjectDataSet12";
            this.projectDataSet12.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vehicleInfoTableAdapter
            // 
            this.vehicleInfoTableAdapter.ClearBeforeFill = true;
            // 
            // VehicleSearchForm
            // 
            this.AcceptButton = this.btnVehicleSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 529);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VehicleSearchForm";
            this.Text = "车辆管理";
            this.Load += new System.EventHandler(this.VehicleSearchForm_Load);
            this.Shown += new System.EventHandler(this.VehicleSearchForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet12)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVehicleSearch;
        private System.Windows.Forms.TextBox textVehicleSearch;
        private System.Windows.Forms.LinkLabel linkLabelVehicle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ProjectDataSet12 projectDataSet12;
        private System.Windows.Forms.BindingSource vehicleInfoBindingSource;
        private MIS_1.ProjectDataSet12TableAdapters.VehicleInfoTableAdapter vehicleInfoTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expr1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn recordTimeDataGridViewTextBoxColumn;
    }
}