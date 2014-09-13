namespace MIS_1
{
    partial class VehicleQueryOptionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerBeginTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxVehicle = new System.Windows.Forms.CheckBox();
            this.checkBoxRegion = new System.Windows.Forms.CheckBox();
            this.checkBoxRoad = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxVehicleType = new System.Windows.Forms.ComboBox();
            this.vehicleTypeBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet14 = new MIS_1.ProjectDataSet14();
            this.comboBoxRoad = new System.Windows.Forms.ComboBox();
            this.comboBoxRegion = new System.Windows.Forms.ComboBox();
            this.regionBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet13 = new MIS_1.ProjectDataSet13();
            this.vehicleTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet6 = new MIS_1.ProjectDataSet6();
            this.regionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet5 = new MIS_1.ProjectDataSet5();
            this.regionTableAdapter = new MIS_1.ProjectDataSet5TableAdapters.RegionTableAdapter();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.vehicleTypeTableAdapter = new MIS_1.ProjectDataSet6TableAdapters.VehicleTypeTableAdapter();
            this.regionTableAdapter1 = new MIS_1.ProjectDataSet13TableAdapters.RegionTableAdapter();
            this.vehicleTypeTableAdapter1 = new MIS_1.ProjectDataSet14TableAdapters.VehicleTypeTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet5)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间";
            // 
            // dateTimePickerBeginTime
            // 
            this.dateTimePickerBeginTime.CustomFormat = "yyyy-MM-dd   HH:mm:ss";
            this.dateTimePickerBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerBeginTime.Location = new System.Drawing.Point(112, 31);
            this.dateTimePickerBeginTime.Name = "dateTimePickerBeginTime";
            this.dateTimePickerBeginTime.Size = new System.Drawing.Size(217, 21);
            this.dateTimePickerBeginTime.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "结束时间";
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "yyyy-MM-dd   HH:mm:ss";
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(112, 87);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(217, 21);
            this.dateTimePickerEndTime.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxVehicle);
            this.groupBox1.Controls.Add(this.checkBoxRegion);
            this.groupBox1.Controls.Add(this.checkBoxRoad);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxVehicleType);
            this.groupBox1.Controls.Add(this.comboBoxRoad);
            this.groupBox1.Controls.Add(this.comboBoxRegion);
            this.groupBox1.Controls.Add(this.dateTimePickerEndTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePickerBeginTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 326);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置条件";
            // 
            // checkBoxVehicle
            // 
            this.checkBoxVehicle.AutoSize = true;
            this.checkBoxVehicle.Checked = true;
            this.checkBoxVehicle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVehicle.Location = new System.Drawing.Point(353, 279);
            this.checkBoxVehicle.Name = "checkBoxVehicle";
            this.checkBoxVehicle.Size = new System.Drawing.Size(48, 16);
            this.checkBoxVehicle.TabIndex = 4;
            this.checkBoxVehicle.Text = "所有";
            this.checkBoxVehicle.UseVisualStyleBackColor = true;
            this.checkBoxVehicle.CheckedChanged += new System.EventHandler(this.checkBoxVehicle_CheckedChanged);
            // 
            // checkBoxRegion
            // 
            this.checkBoxRegion.AutoSize = true;
            this.checkBoxRegion.Checked = true;
            this.checkBoxRegion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRegion.Location = new System.Drawing.Point(353, 163);
            this.checkBoxRegion.Name = "checkBoxRegion";
            this.checkBoxRegion.Size = new System.Drawing.Size(48, 16);
            this.checkBoxRegion.TabIndex = 4;
            this.checkBoxRegion.Text = "所有";
            this.checkBoxRegion.UseVisualStyleBackColor = true;
            this.checkBoxRegion.CheckedChanged += new System.EventHandler(this.checkBoxRegion_CheckedChanged);
            // 
            // checkBoxRoad
            // 
            this.checkBoxRoad.AutoSize = true;
            this.checkBoxRoad.Checked = true;
            this.checkBoxRoad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRoad.Location = new System.Drawing.Point(353, 224);
            this.checkBoxRoad.Name = "checkBoxRoad";
            this.checkBoxRoad.Size = new System.Drawing.Size(48, 16);
            this.checkBoxRoad.TabIndex = 4;
            this.checkBoxRoad.Text = "所有";
            this.checkBoxRoad.UseVisualStyleBackColor = true;
            this.checkBoxRoad.CheckedChanged += new System.EventHandler(this.checkBoxRoad_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "车辆类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "路口";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "区/县";
            // 
            // comboBoxVehicleType
            // 
            this.comboBoxVehicleType.DataSource = this.vehicleTypeBindingSource1;
            this.comboBoxVehicleType.DisplayMember = "Name";
            this.comboBoxVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVehicleType.Enabled = false;
            this.comboBoxVehicleType.FormattingEnabled = true;
            this.comboBoxVehicleType.Location = new System.Drawing.Point(113, 275);
            this.comboBoxVehicleType.Name = "comboBoxVehicleType";
            this.comboBoxVehicleType.Size = new System.Drawing.Size(216, 20);
            this.comboBoxVehicleType.TabIndex = 2;
            // 
            // vehicleTypeBindingSource1
            // 
            this.vehicleTypeBindingSource1.DataMember = "VehicleType";
            this.vehicleTypeBindingSource1.DataSource = this.projectDataSet14;
            // 
            // projectDataSet14
            // 
            this.projectDataSet14.DataSetName = "ProjectDataSet14";
            this.projectDataSet14.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboBoxRoad
            // 
            this.comboBoxRoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRoad.Enabled = false;
            this.comboBoxRoad.FormattingEnabled = true;
            this.comboBoxRoad.Location = new System.Drawing.Point(113, 223);
            this.comboBoxRoad.Name = "comboBoxRoad";
            this.comboBoxRoad.Size = new System.Drawing.Size(216, 20);
            this.comboBoxRoad.TabIndex = 2;
            // 
            // comboBoxRegion
            // 
            this.comboBoxRegion.DataSource = this.regionBindingSource1;
            this.comboBoxRegion.DisplayMember = "RegionName";
            this.comboBoxRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegion.Enabled = false;
            this.comboBoxRegion.FormattingEnabled = true;
            this.comboBoxRegion.Location = new System.Drawing.Point(112, 159);
            this.comboBoxRegion.Name = "comboBoxRegion";
            this.comboBoxRegion.Size = new System.Drawing.Size(216, 20);
            this.comboBoxRegion.TabIndex = 2;
            this.comboBoxRegion.SelectionChangeCommitted += new System.EventHandler(this.comboBoxRegion_SelectionChangeCommitted);
            // 
            // regionBindingSource1
            // 
            this.regionBindingSource1.DataMember = "Region";
            this.regionBindingSource1.DataSource = this.projectDataSet13;
            // 
            // projectDataSet13
            // 
            this.projectDataSet13.DataSetName = "ProjectDataSet13";
            this.projectDataSet13.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vehicleTypeBindingSource
            // 
            this.vehicleTypeBindingSource.DataMember = "VehicleType";
            this.vehicleTypeBindingSource.DataSource = this.projectDataSet6;
            // 
            // projectDataSet6
            // 
            this.projectDataSet6.DataSetName = "ProjectDataSet6";
            this.projectDataSet6.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // regionBindingSource
            // 
            this.regionBindingSource.DataMember = "Region";
            this.regionBindingSource.DataSource = this.projectDataSet5;
            // 
            // projectDataSet5
            // 
            this.projectDataSet5.DataSetName = "ProjectDataSet5";
            this.projectDataSet5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // regionTableAdapter
            // 
            this.regionTableAdapter.ClearBeforeFill = true;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnQuery.Location = new System.Drawing.Point(159, 406);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(98, 30);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "确定";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCancel.Location = new System.Drawing.Point(321, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // vehicleTypeTableAdapter
            // 
            this.vehicleTypeTableAdapter.ClearBeforeFill = true;
            // 
            // regionTableAdapter1
            // 
            this.regionTableAdapter1.ClearBeforeFill = true;
            // 
            // vehicleTypeTableAdapter1
            // 
            this.vehicleTypeTableAdapter1.ClearBeforeFill = true;
            // 
            // VehicleQueryOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 496);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "VehicleQueryOptionForm";
            this.Text = "实时视频车辆分类数据管理";
            this.Load += new System.EventHandler(this.VehicleQueryOptionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerBeginTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxRoad;
        private ProjectDataSet5 projectDataSet5;
        private System.Windows.Forms.BindingSource regionBindingSource;
        private MIS_1.ProjectDataSet5TableAdapters.RegionTableAdapter regionTableAdapter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxVehicleType;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnCancel;
        private ProjectDataSet6 projectDataSet6;
        private System.Windows.Forms.BindingSource vehicleTypeBindingSource;
        private MIS_1.ProjectDataSet6TableAdapters.VehicleTypeTableAdapter vehicleTypeTableAdapter;
        private System.Windows.Forms.CheckBox checkBoxVehicle;
        private System.Windows.Forms.CheckBox checkBoxRegion;
        private System.Windows.Forms.CheckBox checkBoxRoad;
        private ProjectDataSet13 projectDataSet13;
        private System.Windows.Forms.BindingSource regionBindingSource1;
        private MIS_1.ProjectDataSet13TableAdapters.RegionTableAdapter regionTableAdapter1;
        private ProjectDataSet14 projectDataSet14;
        private System.Windows.Forms.BindingSource vehicleTypeBindingSource1;
        private MIS_1.ProjectDataSet14TableAdapters.VehicleTypeTableAdapter vehicleTypeTableAdapter1;
        private System.Windows.Forms.ComboBox comboBoxRegion;
    }
}