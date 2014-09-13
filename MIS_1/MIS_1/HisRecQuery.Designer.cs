namespace MIS_1
{
    partial class HisRecQuery
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxVehicle = new System.Windows.Forms.CheckBox();
            this.checkBoxRegion = new System.Windows.Forms.CheckBox();
            this.checkBoxRoad = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxVehicleType = new System.Windows.Forms.ComboBox();
            this.hisRecordInfoBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet18BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet18 = new MIS_1.ProjectDataSet18();
            this.comboBoxRoad = new System.Windows.Forms.ComboBox();
            this.hisRecordInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxRegion = new System.Windows.Forms.ComboBox();
            this.hisRecordInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerBeginTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.vehicleTypeBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet14 = new MIS_1.ProjectDataSet14();
            this.regionBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet13 = new MIS_1.ProjectDataSet13();
            this.projectDataSet6 = new MIS_1.ProjectDataSet6();
            this.vehicleTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.regionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet5 = new MIS_1.ProjectDataSet5();
            this.regionTableAdapter1 = new MIS_1.ProjectDataSet13TableAdapters.RegionTableAdapter();
            this.vehicleTypeTableAdapter = new MIS_1.ProjectDataSet6TableAdapters.VehicleTypeTableAdapter();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.regionTableAdapter = new MIS_1.ProjectDataSet5TableAdapters.RegionTableAdapter();
            this.vehicleTypeTableAdapter1 = new MIS_1.ProjectDataSet14TableAdapters.VehicleTypeTableAdapter();
            this.hisRecordInfoTableAdapter = new MIS_1.ProjectDataSet18TableAdapters.HisRecordInfoTableAdapter();
            this.projectDataSet19 = new MIS_1.ProjectDataSet19();
            this.regionBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.regionTableAdapter2 = new MIS_1.ProjectDataSet19TableAdapters.RegionTableAdapter();
            this.projectDataSet20 = new MIS_1.ProjectDataSet20();
            this.vehicleTypeBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.vehicleTypeTableAdapter2 = new MIS_1.ProjectDataSet20TableAdapters.VehicleTypeTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hisRecordInfoBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet18BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hisRecordInfoBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hisRecordInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource2)).BeginInit();
            this.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(7, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 326);
            this.groupBox1.TabIndex = 5;
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
            this.comboBoxVehicleType.DataSource = this.vehicleTypeBindingSource2;
            this.comboBoxVehicleType.DisplayMember = "Name";
            this.comboBoxVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVehicleType.Enabled = false;
            this.comboBoxVehicleType.FormattingEnabled = true;
            this.comboBoxVehicleType.Location = new System.Drawing.Point(113, 275);
            this.comboBoxVehicleType.Name = "comboBoxVehicleType";
            this.comboBoxVehicleType.Size = new System.Drawing.Size(216, 20);
            this.comboBoxVehicleType.TabIndex = 2;
            // 
            // hisRecordInfoBindingSource2
            // 
            this.hisRecordInfoBindingSource2.DataMember = "HisRecordInfo";
            this.hisRecordInfoBindingSource2.DataSource = this.projectDataSet18BindingSource;
            // 
            // projectDataSet18BindingSource
            // 
            this.projectDataSet18BindingSource.DataSource = this.projectDataSet18;
            this.projectDataSet18BindingSource.Position = 0;
            // 
            // projectDataSet18
            // 
            this.projectDataSet18.DataSetName = "ProjectDataSet18";
            this.projectDataSet18.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // hisRecordInfoBindingSource1
            // 
            this.hisRecordInfoBindingSource1.DataMember = "HisRecordInfo";
            this.hisRecordInfoBindingSource1.DataSource = this.projectDataSet18BindingSource;
            // 
            // comboBoxRegion
            // 
            this.comboBoxRegion.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.regionBindingSource1, "RegionName", true));
            this.comboBoxRegion.DataSource = this.regionBindingSource2;
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
            // hisRecordInfoBindingSource
            // 
            this.hisRecordInfoBindingSource.DataMember = "HisRecordInfo";
            this.hisRecordInfoBindingSource.DataSource = this.projectDataSet18BindingSource;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "结束时间";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间";
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
            // projectDataSet6
            // 
            this.projectDataSet6.DataSetName = "ProjectDataSet6";
            this.projectDataSet6.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vehicleTypeBindingSource
            // 
            this.vehicleTypeBindingSource.DataMember = "VehicleType";
            this.vehicleTypeBindingSource.DataSource = this.projectDataSet6;
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
            // regionTableAdapter1
            // 
            this.regionTableAdapter1.ClearBeforeFill = true;
            // 
            // vehicleTypeTableAdapter
            // 
            this.vehicleTypeTableAdapter.ClearBeforeFill = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCancel.Location = new System.Drawing.Point(312, 410);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnQuery.Location = new System.Drawing.Point(150, 410);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(98, 30);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "确定";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // regionTableAdapter
            // 
            this.regionTableAdapter.ClearBeforeFill = true;
            // 
            // vehicleTypeTableAdapter1
            // 
            this.vehicleTypeTableAdapter1.ClearBeforeFill = true;
            // 
            // hisRecordInfoTableAdapter
            // 
            this.hisRecordInfoTableAdapter.ClearBeforeFill = true;
            // 
            // projectDataSet19
            // 
            this.projectDataSet19.DataSetName = "ProjectDataSet19";
            this.projectDataSet19.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // regionBindingSource2
            // 
            this.regionBindingSource2.DataMember = "Region";
            this.regionBindingSource2.DataSource = this.projectDataSet19;
            // 
            // regionTableAdapter2
            // 
            this.regionTableAdapter2.ClearBeforeFill = true;
            // 
            // projectDataSet20
            // 
            this.projectDataSet20.DataSetName = "ProjectDataSet20";
            this.projectDataSet20.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vehicleTypeBindingSource2
            // 
            this.vehicleTypeBindingSource2.DataMember = "VehicleType";
            this.vehicleTypeBindingSource2.DataSource = this.projectDataSet20;
            // 
            // vehicleTypeTableAdapter2
            // 
            this.vehicleTypeTableAdapter2.ClearBeforeFill = true;
            // 
            // HisRecQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 488);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnQuery);
            this.Name = "HisRecQuery";
            this.Text = "HisRecQuery";
            this.Load += new System.EventHandler(this.HisRecQuery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hisRecordInfoBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet18BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hisRecordInfoBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hisRecordInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleTypeBindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxVehicle;
        private System.Windows.Forms.CheckBox checkBoxRegion;
        private System.Windows.Forms.CheckBox checkBoxRoad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxVehicleType;
        private System.Windows.Forms.BindingSource vehicleTypeBindingSource1;
        private ProjectDataSet14 projectDataSet14;
        private System.Windows.Forms.ComboBox comboBoxRoad;
        private System.Windows.Forms.ComboBox comboBoxRegion;
        private System.Windows.Forms.BindingSource regionBindingSource1;
        private ProjectDataSet13 projectDataSet13;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerBeginTime;
        private System.Windows.Forms.Label label1;
        private ProjectDataSet6 projectDataSet6;
        private System.Windows.Forms.BindingSource vehicleTypeBindingSource;
        private System.Windows.Forms.BindingSource regionBindingSource;
        private ProjectDataSet5 projectDataSet5;
        private MIS_1.ProjectDataSet13TableAdapters.RegionTableAdapter regionTableAdapter1;
        private MIS_1.ProjectDataSet6TableAdapters.VehicleTypeTableAdapter vehicleTypeTableAdapter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnQuery;
        private MIS_1.ProjectDataSet5TableAdapters.RegionTableAdapter regionTableAdapter;
        private MIS_1.ProjectDataSet14TableAdapters.VehicleTypeTableAdapter vehicleTypeTableAdapter1;
        private System.Windows.Forms.BindingSource projectDataSet18BindingSource;
        private ProjectDataSet18 projectDataSet18;
        private System.Windows.Forms.BindingSource hisRecordInfoBindingSource;
        private MIS_1.ProjectDataSet18TableAdapters.HisRecordInfoTableAdapter hisRecordInfoTableAdapter;
        private System.Windows.Forms.BindingSource hisRecordInfoBindingSource2;
        private System.Windows.Forms.BindingSource hisRecordInfoBindingSource1;
        private ProjectDataSet19 projectDataSet19;
        private System.Windows.Forms.BindingSource regionBindingSource2;
        private MIS_1.ProjectDataSet19TableAdapters.RegionTableAdapter regionTableAdapter2;
        private ProjectDataSet20 projectDataSet20;
        private System.Windows.Forms.BindingSource vehicleTypeBindingSource2;
        private MIS_1.ProjectDataSet20TableAdapters.VehicleTypeTableAdapter vehicleTypeTableAdapter2;
    }
}