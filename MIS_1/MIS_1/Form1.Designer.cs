namespace MIS_1
{
    partial class MainFram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFram));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginIn = new System.Windows.Forms.ToolStripMenuItem();
            this.注销ToolStripLogOff = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginOff = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.车辆管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripManager = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripBaseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripHisRecQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.车辆管理窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnStatistic = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.添加车型ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.添加道口ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.添加区县ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.添加市ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBtnManager = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnHisRecord = new System.Windows.Forms.ToolStripButton();
            this.添加车型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加市ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加区县ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加道口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectDataSet16 = new MIS_1.ProjectDataSet16();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.操作ToolStripMenuItem,
            this.窗口ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(913, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginIn,
            this.注销ToolStripLogOff,
            this.LoginOff});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // LoginIn
            // 
            this.LoginIn.Name = "LoginIn";
            this.LoginIn.Size = new System.Drawing.Size(98, 22);
            this.LoginIn.Text = "登录";
            this.LoginIn.Click += new System.EventHandler(this.LoginIn_Click);
            // 
            // 注销ToolStripLogOff
            // 
            this.注销ToolStripLogOff.Name = "注销ToolStripLogOff";
            this.注销ToolStripLogOff.Size = new System.Drawing.Size(98, 22);
            this.注销ToolStripLogOff.Text = "注销";
            this.注销ToolStripLogOff.Click += new System.EventHandler(this.注销ToolStripLogOff_Click);
            // 
            // LoginOff
            // 
            this.LoginOff.Name = "LoginOff";
            this.LoginOff.Size = new System.Drawing.Size(98, 22);
            this.LoginOff.Text = "退出";
            this.LoginOff.Click += new System.EventHandler(this.LoginOff_Click);
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.车辆管理ToolStripMenuItem,
            this.toolStripManager,
            this.ToolStripBaseInfo,
            this.ToolStripHisRecQuery});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 车辆管理ToolStripMenuItem
            // 
            this.车辆管理ToolStripMenuItem.Name = "车辆管理ToolStripMenuItem";
            this.车辆管理ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.车辆管理ToolStripMenuItem.Text = "车辆管理";
            this.车辆管理ToolStripMenuItem.Click += new System.EventHandler(this.车辆管理ToolStripMenuItem_Click);
            // 
            // toolStripManager
            // 
            this.toolStripManager.Name = "toolStripManager";
            this.toolStripManager.Size = new System.Drawing.Size(146, 22);
            this.toolStripManager.Text = "管理员";
            this.toolStripManager.Click += new System.EventHandler(this.toolStripManager_Click);
            // 
            // ToolStripBaseInfo
            // 
            this.ToolStripBaseInfo.Name = "ToolStripBaseInfo";
            this.ToolStripBaseInfo.Size = new System.Drawing.Size(146, 22);
            this.ToolStripBaseInfo.Text = "基本信息管理";
            this.ToolStripBaseInfo.Click += new System.EventHandler(this.ToolStripBaseInfo_Click);
            // 
            // ToolStripHisRecQuery
            // 
            this.ToolStripHisRecQuery.Name = "ToolStripHisRecQuery";
            this.ToolStripHisRecQuery.Size = new System.Drawing.Size(146, 22);
            this.ToolStripHisRecQuery.Text = "历史记录查询";
            this.ToolStripHisRecQuery.Click += new System.EventHandler(this.ToolStripHisRecQuery_Click);
            // 
            // 窗口ToolStripMenuItem
            // 
            this.窗口ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.车辆管理窗口ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.窗口ToolStripMenuItem.Name = "窗口ToolStripMenuItem";
            this.窗口ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.窗口ToolStripMenuItem.Text = "窗口";
            // 
            // 车辆管理窗口ToolStripMenuItem
            // 
            this.车辆管理窗口ToolStripMenuItem.Name = "车辆管理窗口ToolStripMenuItem";
            this.车辆管理窗口ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.车辆管理窗口ToolStripMenuItem.Text = "打开主窗口";
            this.车辆管理窗口ToolStripMenuItem.Click += new System.EventHandler(this.车辆管理窗口ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.删除ToolStripMenuItem.Text = "关闭主窗口";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem,
            this.版本更新ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 版本更新ToolStripMenuItem
            // 
            this.版本更新ToolStripMenuItem.Name = "版本更新ToolStripMenuItem";
            this.版本更新ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.版本更新ToolStripMenuItem.Text = "版本更新";
            this.版本更新ToolStripMenuItem.Click += new System.EventHandler(this.版本更新ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnFirst,
            this.toolStripBtnLast,
            this.toolStripSeparator1,
            this.toolStripButtonReLoad,
            this.toolStripBtnDelete,
            this.toolStripBtnStatistic,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1,
            this.toolStripBtnManager,
            this.toolStripBtnHisRecord});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(913, 38);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnFirst
            // 
            this.toolStripBtnFirst.BackColor = System.Drawing.Color.Transparent;
            this.toolStripBtnFirst.Image = global::MIS_1.Properties.Resources.首记录1;
            this.toolStripBtnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnFirst.Name = "toolStripBtnFirst";
            this.toolStripBtnFirst.Size = new System.Drawing.Size(47, 35);
            this.toolStripBtnFirst.Text = "首记录";
            this.toolStripBtnFirst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnFirst.ToolTipText = "首记录";
            this.toolStripBtnFirst.Click += new System.EventHandler(this.toolStripBtnFirst_Click);
            // 
            // toolStripBtnLast
            // 
            this.toolStripBtnLast.Image = global::MIS_1.Properties.Resources.尾记录1;
            this.toolStripBtnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnLast.Name = "toolStripBtnLast";
            this.toolStripBtnLast.Size = new System.Drawing.Size(47, 35);
            this.toolStripBtnLast.Text = "尾记录";
            this.toolStripBtnLast.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnLast.ToolTipText = "尾记录";
            this.toolStripBtnLast.Click += new System.EventHandler(this.toolStripBtnLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripButtonReLoad
            // 
            this.toolStripButtonReLoad.Image = global::MIS_1.Properties.Resources.reload;
            this.toolStripButtonReLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReLoad.Name = "toolStripButtonReLoad";
            this.toolStripButtonReLoad.Size = new System.Drawing.Size(35, 35);
            this.toolStripButtonReLoad.Text = "刷新";
            this.toolStripButtonReLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonReLoad.Click += new System.EventHandler(this.toolStripButtonReLoad_Click);
            // 
            // toolStripBtnDelete
            // 
            this.toolStripBtnDelete.Image = global::MIS_1.Properties.Resources.删除1;
            this.toolStripBtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnDelete.Name = "toolStripBtnDelete";
            this.toolStripBtnDelete.Size = new System.Drawing.Size(35, 35);
            this.toolStripBtnDelete.Text = "删除";
            this.toolStripBtnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnDelete.ToolTipText = "删除";
            this.toolStripBtnDelete.Click += new System.EventHandler(this.toolStripBtnDelete_Click);
            // 
            // toolStripBtnStatistic
            // 
            this.toolStripBtnStatistic.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnStatistic.Image")));
            this.toolStripBtnStatistic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnStatistic.Name = "toolStripBtnStatistic";
            this.toolStripBtnStatistic.Size = new System.Drawing.Size(35, 35);
            this.toolStripBtnStatistic.Text = "统计";
            this.toolStripBtnStatistic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnStatistic.ToolTipText = "数据统计图";
            this.toolStripBtnStatistic.Click += new System.EventHandler(this.toolStripBtnStatistic_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加车型ToolStripMenuItem1,
            this.添加道口ToolStripMenuItem1,
            this.添加区县ToolStripMenuItem1,
            this.添加市ToolStripMenuItem1});
            this.toolStripDropDownButton1.Image = global::MIS_1.Properties.Resources.icon_administration;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(44, 35);
            this.toolStripDropDownButton1.Text = "管理";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripDropDownButton1.ToolTipText = "添加到数据库";
            // 
            // 添加车型ToolStripMenuItem1
            // 
            this.添加车型ToolStripMenuItem1.Name = "添加车型ToolStripMenuItem1";
            this.添加车型ToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.添加车型ToolStripMenuItem1.Text = "车型管理";
            this.添加车型ToolStripMenuItem1.Click += new System.EventHandler(this.添加车型ToolStripMenuItem1_Click);
            // 
            // 添加道口ToolStripMenuItem1
            // 
            this.添加道口ToolStripMenuItem1.Name = "添加道口ToolStripMenuItem1";
            this.添加道口ToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.添加道口ToolStripMenuItem1.Text = "道口管理";
            this.添加道口ToolStripMenuItem1.Click += new System.EventHandler(this.添加道口ToolStripMenuItem1_Click);
            // 
            // 添加区县ToolStripMenuItem1
            // 
            this.添加区县ToolStripMenuItem1.Name = "添加区县ToolStripMenuItem1";
            this.添加区县ToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.添加区县ToolStripMenuItem1.Text = "区/县管理";
            this.添加区县ToolStripMenuItem1.Click += new System.EventHandler(this.添加区县ToolStripMenuItem1_Click);
            // 
            // 添加市ToolStripMenuItem1
            // 
            this.添加市ToolStripMenuItem1.Name = "添加市ToolStripMenuItem1";
            this.添加市ToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.添加市ToolStripMenuItem1.Text = "市管理";
            this.添加市ToolStripMenuItem1.Click += new System.EventHandler(this.添加市ToolStripMenuItem1_Click);
            // 
            // toolStripBtnManager
            // 
            this.toolStripBtnManager.Image = global::MIS_1.Properties.Resources.workgroup_manager_icon;
            this.toolStripBtnManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnManager.Name = "toolStripBtnManager";
            this.toolStripBtnManager.Size = new System.Drawing.Size(47, 35);
            this.toolStripBtnManager.Text = "管理员";
            this.toolStripBtnManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnManager.ToolTipText = "管理普通管理员信息";
            this.toolStripBtnManager.Click += new System.EventHandler(this.toolStripBtnManager_Click);
            // 
            // toolStripBtnHisRecord
            // 
            this.toolStripBtnHisRecord.Image = global::MIS_1.Properties.Resources.history;
            this.toolStripBtnHisRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnHisRecord.Name = "toolStripBtnHisRecord";
            this.toolStripBtnHisRecord.Size = new System.Drawing.Size(59, 35);
            this.toolStripBtnHisRecord.Text = "历史记录";
            this.toolStripBtnHisRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripBtnHisRecord.Click += new System.EventHandler(this.toolStripBtnHisRecord_Click);
            // 
            // 添加车型ToolStripMenuItem
            // 
            this.添加车型ToolStripMenuItem.Name = "添加车型ToolStripMenuItem";
            this.添加车型ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加车型ToolStripMenuItem.Text = "添加车型";
            // 
            // 添加市ToolStripMenuItem
            // 
            this.添加市ToolStripMenuItem.Name = "添加市ToolStripMenuItem";
            this.添加市ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加市ToolStripMenuItem.Text = "添加市";
            // 
            // 添加区县ToolStripMenuItem
            // 
            this.添加区县ToolStripMenuItem.Name = "添加区县ToolStripMenuItem";
            this.添加区县ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加区县ToolStripMenuItem.Text = "添加区/县";
            // 
            // 添加道口ToolStripMenuItem
            // 
            this.添加道口ToolStripMenuItem.Name = "添加道口ToolStripMenuItem";
            this.添加道口ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加道口ToolStripMenuItem.Text = "添加道口";
            // 
            // projectDataSet16
            // 
            this.projectDataSet16.DataSetName = "ProjectDataSet16";
            this.projectDataSet16.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = this.projectDataSet16;
            this.bindingSource1.Position = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 526);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(913, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // MainFram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 548);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFram";
            this.Text = "实时视频车辆分类数据管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFram_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFram_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSet16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoginIn;
        private System.Windows.Forms.ToolStripMenuItem LoginOff;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripManager;
        private System.Windows.Forms.ToolStripMenuItem 车辆管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注销ToolStripLogOff;
        private System.Windows.Forms.ToolStripButton toolStripBtnFirst;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripBtnDelete;
        private System.Windows.Forms.ToolStripButton toolStripBtnStatistic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 添加车型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加市ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加区县ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加道口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加车型ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 添加市ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 添加区县ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 添加道口ToolStripMenuItem1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private ProjectDataSet16 projectDataSet16;
        private System.Windows.Forms.ToolStripMenuItem ToolStripBaseInfo;
        private System.Windows.Forms.ToolStripMenuItem 窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 车辆管理窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripBtnManager;
        private System.Windows.Forms.ToolStripButton toolStripButtonReLoad;
        private System.Windows.Forms.ToolStripButton toolStripBtnHisRecord;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本更新ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripHisRecQuery;
    }
}

