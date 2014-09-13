namespace MIS_1
{
    partial class Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manager));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.managerIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.managerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.joinTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.managerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.projectDataSet = new MIS_1.ProjectDataSet();
            this.managerTableAdapter = new MIS_1.ProjectDataSetTableAdapters.ManagerTableAdapter();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnAddMan = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnDeleteMan = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnAlterMan = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(32, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(664, 420);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "管理员列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.managerIdDataGridViewTextBoxColumn,
            this.managerNameDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.joinTimeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.managerBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(658, 400);
            this.dataGridView1.TabIndex = 0;
            // 
            // managerIdDataGridViewTextBoxColumn
            // 
            this.managerIdDataGridViewTextBoxColumn.DataPropertyName = "ManagerId";
            this.managerIdDataGridViewTextBoxColumn.HeaderText = "ManagerId";
            this.managerIdDataGridViewTextBoxColumn.Name = "managerIdDataGridViewTextBoxColumn";
            this.managerIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // managerNameDataGridViewTextBoxColumn
            // 
            this.managerNameDataGridViewTextBoxColumn.DataPropertyName = "ManagerName";
            this.managerNameDataGridViewTextBoxColumn.HeaderText = "ManagerName";
            this.managerNameDataGridViewTextBoxColumn.Name = "managerNameDataGridViewTextBoxColumn";
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
            this.passwordDataGridViewTextBoxColumn.HeaderText = "Password";
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            // 
            // joinTimeDataGridViewTextBoxColumn
            // 
            this.joinTimeDataGridViewTextBoxColumn.DataPropertyName = "JoinTime";
            this.joinTimeDataGridViewTextBoxColumn.HeaderText = "JoinTime";
            this.joinTimeDataGridViewTextBoxColumn.Name = "joinTimeDataGridViewTextBoxColumn";
            // 
            // managerBindingSource
            // 
            this.managerBindingSource.DataMember = "Manager";
            this.managerBindingSource.DataSource = this.projectDataSet;
            // 
            // projectDataSet
            // 
            this.projectDataSet.DataSetName = "ProjectDataSet";
            this.projectDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // managerTableAdapter
            // 
            this.managerTableAdapter.ClearBeforeFill = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnAddMan,
            this.toolStripBtnDeleteMan,
            this.toolStripBtnAlterMan,
            this.toolStripBtnFind,
            this.toolStripTextBoxFind});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(738, 40);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnAddMan
            // 
            this.toolStripBtnAddMan.Image = global::MIS_1.Properties.Resources.add_user_icon;
            this.toolStripBtnAddMan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnAddMan.Name = "toolStripBtnAddMan";
            this.toolStripBtnAddMan.Size = new System.Drawing.Size(36, 37);
            this.toolStripBtnAddMan.Text = "添加";
            this.toolStripBtnAddMan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnAddMan.ToolTipText = "添加管理员";
            this.toolStripBtnAddMan.Click += new System.EventHandler(this.toolStripBtnAddMan_Click);
            // 
            // toolStripBtnDeleteMan
            // 
            this.toolStripBtnDeleteMan.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnDeleteMan.Image")));
            this.toolStripBtnDeleteMan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnDeleteMan.Name = "toolStripBtnDeleteMan";
            this.toolStripBtnDeleteMan.Size = new System.Drawing.Size(36, 37);
            this.toolStripBtnDeleteMan.Text = "删除";
            this.toolStripBtnDeleteMan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnDeleteMan.ToolTipText = "删除管理员";
            this.toolStripBtnDeleteMan.Click += new System.EventHandler(this.toolStripBtnDeleteMan_Click);
            // 
            // toolStripBtnAlterMan
            // 
            this.toolStripBtnAlterMan.Image = global::MIS_1.Properties.Resources.修改;
            this.toolStripBtnAlterMan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnAlterMan.Name = "toolStripBtnAlterMan";
            this.toolStripBtnAlterMan.Size = new System.Drawing.Size(36, 37);
            this.toolStripBtnAlterMan.Text = "修改";
            this.toolStripBtnAlterMan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnAlterMan.ToolTipText = "修改管理员信息";
            this.toolStripBtnAlterMan.Click += new System.EventHandler(this.toolStripBtnAlterMan_Click);
            // 
            // toolStripBtnFind
            // 
            this.toolStripBtnFind.Image = global::MIS_1.Properties.Resources.搜索;
            this.toolStripBtnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnFind.Name = "toolStripBtnFind";
            this.toolStripBtnFind.Size = new System.Drawing.Size(36, 37);
            this.toolStripBtnFind.Text = "查找";
            this.toolStripBtnFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnFind.Click += new System.EventHandler(this.toolStripBtnFind_Click);
            // 
            // toolStripTextBoxFind
            // 
            this.toolStripTextBoxFind.BackColor = System.Drawing.Color.White;
            this.toolStripTextBoxFind.Name = "toolStripTextBoxFind";
            this.toolStripTextBoxFind.Size = new System.Drawing.Size(100, 40);
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(738, 509);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Manager";
            this.Text = "实时视频车辆分类数据管理";
            this.Load += new System.EventHandler(this.Manager_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ProjectDataSet projectDataSet;
        private System.Windows.Forms.BindingSource managerBindingSource;
        private MIS_1.ProjectDataSetTableAdapters.ManagerTableAdapter managerTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn managerIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn managerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn joinTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnAddMan;
        private System.Windows.Forms.ToolStripButton toolStripBtnDeleteMan;
        private System.Windows.Forms.ToolStripButton toolStripBtnAlterMan;
        private System.Windows.Forms.ToolStripButton toolStripBtnFind;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFind;
    }
}