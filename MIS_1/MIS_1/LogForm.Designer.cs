namespace MIS_1
{
    partial class LogForm
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
            this.comboBoxLogName = new System.Windows.Forms.ComboBox();
            this.managerBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet8 = new MIS_1.ProjectDataSet8();
            this.managerBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLogPassword = new System.Windows.Forms.TextBox();
            this.projectDataSet4 = new MIS_1.ProjectDataSet4();
            this.managerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.managerTableAdapter = new MIS_1.ProjectDataSet4TableAdapters.ManagerTableAdapter();
            this.btnLogLog = new System.Windows.Forms.Button();
            this.btnLogCancel = new System.Windows.Forms.Button();
            this.managerTableAdapter1 = new MIS_1.ProjectDataSet7TableAdapters.ManagerTableAdapter();
            this.managerTableAdapter2 = new MIS_1.ProjectDataSet8TableAdapters.ManagerTableAdapter();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.managerBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managerBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managerBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxLogName
            // 
            this.comboBoxLogName.DataSource = this.managerBindingSource2;
            this.comboBoxLogName.DisplayMember = "ManagerName";
            this.comboBoxLogName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogName.FormattingEnabled = true;
            this.comboBoxLogName.Location = new System.Drawing.Point(295, 46);
            this.comboBoxLogName.Name = "comboBoxLogName";
            this.comboBoxLogName.Size = new System.Drawing.Size(191, 22);
            this.comboBoxLogName.TabIndex = 0;
            // 
            // managerBindingSource2
            // 
            this.managerBindingSource2.DataMember = "Manager";
            this.managerBindingSource2.DataSource = this.projectDataSet8;
            // 
            // projectDataSet8
            // 
            this.projectDataSet8.DataSetName = "ProjectDataSet8";
            this.projectDataSet8.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // managerBindingSource1
            // 
            this.managerBindingSource1.DataMember = "Manager";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("YouYuan", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(237, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "姓名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("YouYuan", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(237, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // textBoxLogPassword
            // 
            this.textBoxLogPassword.Location = new System.Drawing.Point(295, 132);
            this.textBoxLogPassword.Name = "textBoxLogPassword";
            this.textBoxLogPassword.Size = new System.Drawing.Size(191, 23);
            this.textBoxLogPassword.TabIndex = 1;
            this.textBoxLogPassword.UseSystemPasswordChar = true;
            // 
            // projectDataSet4
            // 
            this.projectDataSet4.DataSetName = "ProjectDataSet4";
            this.projectDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // managerBindingSource
            // 
            this.managerBindingSource.DataMember = "Manager";
            this.managerBindingSource.DataSource = this.projectDataSet4;
            // 
            // managerTableAdapter
            // 
            this.managerTableAdapter.ClearBeforeFill = true;
            // 
            // btnLogLog
            // 
            this.btnLogLog.AutoEllipsis = true;
            this.btnLogLog.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLogLog.Location = new System.Drawing.Point(260, 236);
            this.btnLogLog.Name = "btnLogLog";
            this.btnLogLog.Size = new System.Drawing.Size(98, 26);
            this.btnLogLog.TabIndex = 2;
            this.btnLogLog.Text = "登录";
            this.btnLogLog.UseVisualStyleBackColor = false;
            this.btnLogLog.Click += new System.EventHandler(this.btnLogLog_Click);
            // 
            // btnLogCancel
            // 
            this.btnLogCancel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLogCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLogCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLogCancel.Location = new System.Drawing.Point(379, 236);
            this.btnLogCancel.Name = "btnLogCancel";
            this.btnLogCancel.Size = new System.Drawing.Size(98, 26);
            this.btnLogCancel.TabIndex = 3;
            this.btnLogCancel.Text = "取消";
            this.btnLogCancel.UseVisualStyleBackColor = false;
            this.btnLogCancel.Click += new System.EventHandler(this.btnLogCancel_Click);
            // 
            // managerTableAdapter1
            // 
            this.managerTableAdapter1.ClearBeforeFill = true;
            // 
            // managerTableAdapter2
            // 
            this.managerTableAdapter2.ClearBeforeFill = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("SimSun", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(70, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(310, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "实时视频车辆分类数据管理";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxLogName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnLogCancel);
            this.groupBox1.Controls.Add(this.btnLogLog);
            this.groupBox1.Controls.Add(this.textBoxLogPassword);
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(74, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 293);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录系统";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MIS_1.Properties.Resources.user_ico_login;
            this.pictureBox1.Location = new System.Drawing.Point(17, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(214, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // LogForm
            // 
            this.AcceptButton = this.btnLogLog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::MIS_1.Properties.Resources.Logsong;
            this.CancelButton = this.btnLogCancel;
            this.ClientSize = new System.Drawing.Size(771, 520);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogForm";
            this.Text = "实时视频车辆分类数据管理";
            this.Load += new System.EventHandler(this.LogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.managerBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managerBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managerBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxLogName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLogPassword;
        private ProjectDataSet4 projectDataSet4;
        private System.Windows.Forms.BindingSource managerBindingSource;
        private MIS_1.ProjectDataSet4TableAdapters.ManagerTableAdapter managerTableAdapter;
        private System.Windows.Forms.Button btnLogLog;
        private System.Windows.Forms.Button btnLogCancel;
        private System.Windows.Forms.BindingSource managerBindingSource1;
        private MIS_1.ProjectDataSet7TableAdapters.ManagerTableAdapter managerTableAdapter1;
        private ProjectDataSet8 projectDataSet8;
        private System.Windows.Forms.BindingSource managerBindingSource2;
        private MIS_1.ProjectDataSet8TableAdapters.ManagerTableAdapter managerTableAdapter2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}