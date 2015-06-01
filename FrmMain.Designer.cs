namespace AlarmMapClient
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("设备列表");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设备管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_RefreshDev = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ShowHideDeviceTree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_AutoLoadDeviceStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adduser_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changepw_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deluser_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ReloadMap = new System.Windows.Forms.ToolStripMenuItem();
            this.报警管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ShowHideAlarmData = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_DisableOpenVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_tree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Play = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_MapShow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_MapDW = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_UserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_bf_one = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_bf_two = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_cf = new System.Windows.Forms.ToolStripMenuItem();
            this.exePlan_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.webBrowser_map = new System.Windows.Forms.WebBrowser();
            this.dgv_alarmlog = new System.Windows.Forms.DataGridView();
            this.DeviceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChannelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsDel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip_log = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_log_dealalarm = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_log_viewvideo = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_log_viewuser = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_log_sms = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_log_search = new System.Windows.Forms.ToolStripMenuItem();
            this.tree_devices = new System.Windows.Forms.TreeView();
            this.imageList_tree = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_RefreshBCF = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip_tree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_alarmlog)).BeginInit();
            this.contextMenuStrip_log.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设备管理ToolStripMenuItem,
            this.用户管理ToolStripMenuItem,
            this.地图管理ToolStripMenuItem,
            this.报警管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设备管理ToolStripMenuItem
            // 
            this.设备管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_RefreshDev,
            this.ToolStripMenuItem_ShowHideDeviceTree,
            this.toolStripMenuItem4,
            this.ToolStripMenuItem_AutoLoadDeviceStatus,
            this.toolStripMenuItem5,
            this.ToolStripMenuItem_RefreshBCF});
            this.设备管理ToolStripMenuItem.Name = "设备管理ToolStripMenuItem";
            this.设备管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.设备管理ToolStripMenuItem.Text = "设备管理";
            // 
            // ToolStripMenuItem_RefreshDev
            // 
            this.ToolStripMenuItem_RefreshDev.Image = global::AlarmMapClient.Properties.Resources.video;
            this.ToolStripMenuItem_RefreshDev.Name = "ToolStripMenuItem_RefreshDev";
            this.ToolStripMenuItem_RefreshDev.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItem_RefreshDev.Text = "刷新在线设备";
            this.ToolStripMenuItem_RefreshDev.Click += new System.EventHandler(this.ToolStripMenuItem_RefreshDev_Click);
            // 
            // ToolStripMenuItem_ShowHideDeviceTree
            // 
            this.ToolStripMenuItem_ShowHideDeviceTree.Name = "ToolStripMenuItem_ShowHideDeviceTree";
            this.ToolStripMenuItem_ShowHideDeviceTree.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItem_ShowHideDeviceTree.Text = "隐藏/显示设备树";
            this.ToolStripMenuItem_ShowHideDeviceTree.Click += new System.EventHandler(this.ToolStripMenuItem_ShowHideDeviceTree_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(181, 6);
            // 
            // ToolStripMenuItem_AutoLoadDeviceStatus
            // 
            this.ToolStripMenuItem_AutoLoadDeviceStatus.Name = "ToolStripMenuItem_AutoLoadDeviceStatus";
            this.ToolStripMenuItem_AutoLoadDeviceStatus.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItem_AutoLoadDeviceStatus.Text = "自动刷新设备状态";
            this.ToolStripMenuItem_AutoLoadDeviceStatus.Click += new System.EventHandler(this.ToolStripMenuItem_AutoLoadDeviceStatus_Click);
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adduser_ToolStripMenuItem,
            this.changepw_ToolStripMenuItem,
            this.deluser_ToolStripMenuItem});
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            // 
            // adduser_ToolStripMenuItem
            // 
            this.adduser_ToolStripMenuItem.Name = "adduser_ToolStripMenuItem";
            this.adduser_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.adduser_ToolStripMenuItem.Text = "新增用户";
            this.adduser_ToolStripMenuItem.Click += new System.EventHandler(this.adduser_ToolStripMenuItem_Click);
            // 
            // changepw_ToolStripMenuItem
            // 
            this.changepw_ToolStripMenuItem.Name = "changepw_ToolStripMenuItem";
            this.changepw_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.changepw_ToolStripMenuItem.Text = "修改密码";
            this.changepw_ToolStripMenuItem.Click += new System.EventHandler(this.changepw_ToolStripMenuItem_Click);
            // 
            // deluser_ToolStripMenuItem
            // 
            this.deluser_ToolStripMenuItem.Name = "deluser_ToolStripMenuItem";
            this.deluser_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deluser_ToolStripMenuItem.Text = "删除用户";
            this.deluser_ToolStripMenuItem.Click += new System.EventHandler(this.deluser_ToolStripMenuItem_Click);
            // 
            // 地图管理ToolStripMenuItem
            // 
            this.地图管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ReloadMap});
            this.地图管理ToolStripMenuItem.Name = "地图管理ToolStripMenuItem";
            this.地图管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.地图管理ToolStripMenuItem.Text = "地图管理";
            // 
            // ToolStripMenuItem_ReloadMap
            // 
            this.ToolStripMenuItem_ReloadMap.Name = "ToolStripMenuItem_ReloadMap";
            this.ToolStripMenuItem_ReloadMap.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItem_ReloadMap.Text = "重加载地图";
            this.ToolStripMenuItem_ReloadMap.Click += new System.EventHandler(this.ToolStripMenuItem_ReloadMap_Click);
            // 
            // 报警管理ToolStripMenuItem
            // 
            this.报警管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ShowHideAlarmData,
            this.ToolStripMenuItem_DisableOpenVideo});
            this.报警管理ToolStripMenuItem.Name = "报警管理ToolStripMenuItem";
            this.报警管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.报警管理ToolStripMenuItem.Text = "报警管理";
            // 
            // ToolStripMenuItem_ShowHideAlarmData
            // 
            this.ToolStripMenuItem_ShowHideAlarmData.Name = "ToolStripMenuItem_ShowHideAlarmData";
            this.ToolStripMenuItem_ShowHideAlarmData.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_ShowHideAlarmData.Text = "隐藏/显示报警详细";
            this.ToolStripMenuItem_ShowHideAlarmData.Click += new System.EventHandler(this.ToolStripMenuItem_ShowHideAlarmData_Click);
            // 
            // ToolStripMenuItem_DisableOpenVideo
            // 
            this.ToolStripMenuItem_DisableOpenVideo.Name = "ToolStripMenuItem_DisableOpenVideo";
            this.ToolStripMenuItem_DisableOpenVideo.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_DisableOpenVideo.Text = "禁止弹屏";
            this.ToolStripMenuItem_DisableOpenVideo.Click += new System.EventHandler(this.ToolStripMenuItem_DisableOpenVideo_Click);
            // 
            // contextMenuStrip_tree
            // 
            this.contextMenuStrip_tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Play,
            this.toolStripMenuItem1,
            this.ToolStripMenuItem_MapShow,
            this.ToolStripMenuItem_MapDW,
            this.toolStripMenuItem2,
            this.ToolStripMenuItem_UserInfo,
            this.toolStripMenuItem3,
            this.toolStripMenuItem_bf_one,
            this.ToolStripMenuItem_bf_two,
            this.ToolStripMenuItem_cf,
            this.exePlan_ToolStripMenuItem});
            this.contextMenuStrip_tree.Name = "contextMenuStrip_tree";
            this.contextMenuStrip_tree.Size = new System.Drawing.Size(125, 198);
            // 
            // ToolStripMenuItem_Play
            // 
            this.ToolStripMenuItem_Play.Name = "ToolStripMenuItem_Play";
            this.ToolStripMenuItem_Play.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_Play.Text = "播放";
            this.ToolStripMenuItem_Play.Click += new System.EventHandler(this.ToolStripMenuItem_Play_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 6);
            // 
            // ToolStripMenuItem_MapShow
            // 
            this.ToolStripMenuItem_MapShow.Name = "ToolStripMenuItem_MapShow";
            this.ToolStripMenuItem_MapShow.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_MapShow.Text = "地图显示";
            this.ToolStripMenuItem_MapShow.Click += new System.EventHandler(this.ToolStripMenuItem_MapShow_Click);
            // 
            // ToolStripMenuItem_MapDW
            // 
            this.ToolStripMenuItem_MapDW.Name = "ToolStripMenuItem_MapDW";
            this.ToolStripMenuItem_MapDW.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_MapDW.Text = "重新定位";
            this.ToolStripMenuItem_MapDW.Click += new System.EventHandler(this.ToolStripMenuItem_MapDWOne_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(121, 6);
            // 
            // ToolStripMenuItem_UserInfo
            // 
            this.ToolStripMenuItem_UserInfo.Name = "ToolStripMenuItem_UserInfo";
            this.ToolStripMenuItem_UserInfo.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_UserInfo.Text = "信息配置";
            this.ToolStripMenuItem_UserInfo.Click += new System.EventHandler(this.ToolStripMenuItem_UserInfo_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(121, 6);
            // 
            // toolStripMenuItem_bf_one
            // 
            this.toolStripMenuItem_bf_one.Name = "toolStripMenuItem_bf_one";
            this.toolStripMenuItem_bf_one.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_bf_one.Text = "一级布防";
            this.toolStripMenuItem_bf_one.Click += new System.EventHandler(this.toolStripMenuItem_bf_one_Click);
            // 
            // ToolStripMenuItem_bf_two
            // 
            this.ToolStripMenuItem_bf_two.Name = "ToolStripMenuItem_bf_two";
            this.ToolStripMenuItem_bf_two.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_bf_two.Text = "二级布防";
            this.ToolStripMenuItem_bf_two.Click += new System.EventHandler(this.ToolStripMenuItem_bf_two_Click);
            // 
            // ToolStripMenuItem_cf
            // 
            this.ToolStripMenuItem_cf.Name = "ToolStripMenuItem_cf";
            this.ToolStripMenuItem_cf.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_cf.Text = "撤防";
            this.ToolStripMenuItem_cf.Click += new System.EventHandler(this.ToolStripMenuItem_cf_Click);
            // 
            // exePlan_ToolStripMenuItem
            // 
            this.exePlan_ToolStripMenuItem.Name = "exePlan_ToolStripMenuItem";
            this.exePlan_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.exePlan_ToolStripMenuItem.Text = "执行计划";
            this.exePlan_ToolStripMenuItem.Click += new System.EventHandler(this.exePlan_ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tree_devices);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 705);
            this.splitContainer1.SplitterDistance = 990;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.webBrowser_map);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgv_alarmlog);
            this.splitContainer2.Size = new System.Drawing.Size(990, 705);
            this.splitContainer2.SplitterDistance = 537;
            this.splitContainer2.TabIndex = 0;
            // 
            // webBrowser_map
            // 
            this.webBrowser_map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser_map.Location = new System.Drawing.Point(0, 0);
            this.webBrowser_map.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_map.Name = "webBrowser_map";
            this.webBrowser_map.ScriptErrorsSuppressed = true;
            this.webBrowser_map.Size = new System.Drawing.Size(990, 537);
            this.webBrowser_map.TabIndex = 0;
            this.webBrowser_map.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_map_DocumentCompleted);
            // 
            // dgv_alarmlog
            // 
            this.dgv_alarmlog.AllowUserToAddRows = false;
            this.dgv_alarmlog.AllowUserToDeleteRows = false;
            this.dgv_alarmlog.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_alarmlog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_alarmlog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeviceId,
            this.ChannelName,
            this.AlarmTime,
            this.Tel,
            this.Address,
            this.IsDel});
            this.dgv_alarmlog.ContextMenuStrip = this.contextMenuStrip_log;
            this.dgv_alarmlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_alarmlog.Location = new System.Drawing.Point(0, 0);
            this.dgv_alarmlog.Name = "dgv_alarmlog";
            this.dgv_alarmlog.ReadOnly = true;
            this.dgv_alarmlog.RowTemplate.Height = 23;
            this.dgv_alarmlog.Size = new System.Drawing.Size(990, 164);
            this.dgv_alarmlog.TabIndex = 0;
            this.dgv_alarmlog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_alarmlog_CellClick);
            // 
            // DeviceId
            // 
            this.DeviceId.DataPropertyName = "DeviceId";
            this.DeviceId.HeaderText = "设备编号";
            this.DeviceId.Name = "DeviceId";
            this.DeviceId.ReadOnly = true;
            // 
            // ChannelName
            // 
            this.ChannelName.DataPropertyName = "ChannelName";
            this.ChannelName.HeaderText = "通道";
            this.ChannelName.Name = "ChannelName";
            this.ChannelName.ReadOnly = true;
            this.ChannelName.Width = 250;
            // 
            // AlarmTime
            // 
            this.AlarmTime.DataPropertyName = "AlarmTime";
            this.AlarmTime.HeaderText = "报警时间";
            this.AlarmTime.Name = "AlarmTime";
            this.AlarmTime.ReadOnly = true;
            this.AlarmTime.Width = 120;
            // 
            // Tel
            // 
            this.Tel.DataPropertyName = "Tel";
            this.Tel.HeaderText = "电话";
            this.Tel.Name = "Tel";
            this.Tel.ReadOnly = true;
            this.Tel.Width = 120;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "地址";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 200;
            // 
            // IsDel
            // 
            this.IsDel.DataPropertyName = "IsDel";
            this.IsDel.FalseValue = "0";
            this.IsDel.HeaderText = "消失";
            this.IsDel.Name = "IsDel";
            this.IsDel.ReadOnly = true;
            this.IsDel.TrueValue = "1";
            this.IsDel.Visible = false;
            this.IsDel.Width = 60;
            // 
            // contextMenuStrip_log
            // 
            this.contextMenuStrip_log.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_log_dealalarm,
            this.ToolStripMenuItem_log_viewvideo,
            this.ToolStripMenuItem_log_viewuser,
            this.ToolStripMenuItem_log_sms,
            this.ToolStripMenuItem_log_search});
            this.contextMenuStrip_log.Name = "contextMenuStrip_log";
            this.contextMenuStrip_log.Size = new System.Drawing.Size(149, 114);
            // 
            // ToolStripMenuItem_log_dealalarm
            // 
            this.ToolStripMenuItem_log_dealalarm.Enabled = false;
            this.ToolStripMenuItem_log_dealalarm.Name = "ToolStripMenuItem_log_dealalarm";
            this.ToolStripMenuItem_log_dealalarm.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_log_dealalarm.Text = "处理警情";
            this.ToolStripMenuItem_log_dealalarm.Click += new System.EventHandler(this.ToolStripMenuItem_log_dealalarm_Click);
            // 
            // ToolStripMenuItem_log_viewvideo
            // 
            this.ToolStripMenuItem_log_viewvideo.Enabled = false;
            this.ToolStripMenuItem_log_viewvideo.Name = "ToolStripMenuItem_log_viewvideo";
            this.ToolStripMenuItem_log_viewvideo.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_log_viewvideo.Text = "查看关联视频";
            this.ToolStripMenuItem_log_viewvideo.Click += new System.EventHandler(this.ToolStripMenuItem_log_viewvideo_Click);
            // 
            // ToolStripMenuItem_log_viewuser
            // 
            this.ToolStripMenuItem_log_viewuser.Enabled = false;
            this.ToolStripMenuItem_log_viewuser.Name = "ToolStripMenuItem_log_viewuser";
            this.ToolStripMenuItem_log_viewuser.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_log_viewuser.Text = "查看用户信息";
            this.ToolStripMenuItem_log_viewuser.Click += new System.EventHandler(this.ToolStripMenuItem_log_viewuser_Click);
            // 
            // ToolStripMenuItem_log_sms
            // 
            this.ToolStripMenuItem_log_sms.Enabled = false;
            this.ToolStripMenuItem_log_sms.Name = "ToolStripMenuItem_log_sms";
            this.ToolStripMenuItem_log_sms.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_log_sms.Text = "短信通知用户";
            this.ToolStripMenuItem_log_sms.Click += new System.EventHandler(this.ToolStripMenuItem_log_sms_Click);
            // 
            // ToolStripMenuItem_log_search
            // 
            this.ToolStripMenuItem_log_search.Enabled = false;
            this.ToolStripMenuItem_log_search.Name = "ToolStripMenuItem_log_search";
            this.ToolStripMenuItem_log_search.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_log_search.Text = "日志综合查询";
            this.ToolStripMenuItem_log_search.Click += new System.EventHandler(this.ToolStripMenuItem_log_search_Click);
            // 
            // tree_devices
            // 
            this.tree_devices.ContextMenuStrip = this.contextMenuStrip_tree;
            this.tree_devices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree_devices.ImageIndex = 0;
            this.tree_devices.ImageList = this.imageList_tree;
            this.tree_devices.Location = new System.Drawing.Point(0, 0);
            this.tree_devices.Name = "tree_devices";
            treeNode1.Name = "设备列表";
            treeNode1.Text = "设备列表";
            this.tree_devices.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tree_devices.SelectedImageIndex = 0;
            this.tree_devices.Size = new System.Drawing.Size(270, 639);
            this.tree_devices.TabIndex = 2;
            this.tree_devices.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_devices_AfterSelect);
            this.tree_devices.Leave += new System.EventHandler(this.tree_devices_Leave);
            // 
            // imageList_tree
            // 
            this.imageList_tree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_tree.ImageStream")));
            this.imageList_tree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_tree.Images.SetKeyName(0, "db_24.png");
            this.imageList_tree.Images.SetKeyName(1, "ok_24.png");
            this.imageList_tree.Images.SetKeyName(2, "no_24.png");
            this.imageList_tree.Images.SetKeyName(3, "camera_24.png");
            this.imageList_tree.Images.SetKeyName(4, "lamp_on_24.png");
            this.imageList_tree.Images.SetKeyName(5, "lamp_off_24.png");
            this.imageList_tree.Images.SetKeyName(6, "d_24.png");
            this.imageList_tree.Images.SetKeyName(7, "channel_cf.png");
            this.imageList_tree.Images.SetKeyName(8, "channel_off.png");
            this.imageList_tree.Images.SetKeyName(9, "channel_on.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_search);
            this.groupBox1.Controls.Add(this.txt_name);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 639);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 66);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索";
            // 
            // btn_search
            // 
            this.btn_search.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_search.Location = new System.Drawing.Point(3, 38);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(264, 23);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "搜       索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_name
            // 
            this.txt_name.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_name.Location = new System.Drawing.Point(3, 17);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(264, 21);
            this.txt_name.TabIndex = 0;
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(181, 6);
            // 
            // ToolStripMenuItem_RefreshBCF
            // 
            this.ToolStripMenuItem_RefreshBCF.Name = "ToolStripMenuItem_RefreshBCF";
            this.ToolStripMenuItem_RefreshBCF.Size = new System.Drawing.Size(184, 22);
            this.ToolStripMenuItem_RefreshBCF.Text = "刷新设备布撤防状态";
            this.ToolStripMenuItem_RefreshBCF.Click += new System.EventHandler(this.ToolStripMenuItem_RefreshBCF_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报警中心";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip_tree.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_alarmlog)).EndInit();
            this.contextMenuStrip_log.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设备管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报警管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RefreshDev;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_tree;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Play;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_MapShow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_MapDW;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.WebBrowser webBrowser_map;
        private System.Windows.Forms.DataGridView dgv_alarmlog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_UserInfo;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ReloadMap;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShowHideAlarmData;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShowHideDeviceTree;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_log;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_log_dealalarm;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_log_viewvideo;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_log_viewuser;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_log_sms;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_log_search;
        private System.Windows.Forms.ImageList imageList_tree;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_bf_one;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_bf_two;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_cf;
        private System.Windows.Forms.TreeView tree_devices;
        private System.Windows.Forms.ToolStripMenuItem adduser_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changepw_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deluser_ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChannelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_DisableOpenVideo;
        private System.Windows.Forms.ToolStripMenuItem exePlan_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AutoLoadDeviceStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RefreshBCF;
    }
}