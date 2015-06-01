namespace AlarmMapClient
{
    partial class FrmSecMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSecMap));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_reload_map = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.btn_del_mappic = new System.Windows.Forms.Button();
            this.btn_selimages = new System.Windows.Forms.Button();
            this.btn_selfile = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.txt_file = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webBrowser_Map = new System.Windows.Forms.WebBrowser();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView_device = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_tree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_dw = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList_tree = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuStrip_tree.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_reload_map);
            this.panel1.Controls.Add(this.lblState);
            this.panel1.Controls.Add(this.btn_del_mappic);
            this.panel1.Controls.Add(this.btn_selimages);
            this.panel1.Controls.Add(this.btn_selfile);
            this.panel1.Controls.Add(this.btn_upload);
            this.panel1.Controls.Add(this.txt_file);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 41);
            this.panel1.TabIndex = 0;
            // 
            // btn_reload_map
            // 
            this.btn_reload_map.Location = new System.Drawing.Point(807, 9);
            this.btn_reload_map.Name = "btn_reload_map";
            this.btn_reload_map.Size = new System.Drawing.Size(75, 23);
            this.btn_reload_map.TabIndex = 7;
            this.btn_reload_map.Text = "清理缓存";
            this.btn_reload_map.UseVisualStyleBackColor = true;
            this.btn_reload_map.Click += new System.EventHandler(this.btn_reload_map_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(476, 14);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(53, 12);
            this.lblState.TabIndex = 6;
            this.lblState.Text = "上传进度";
            // 
            // btn_del_mappic
            // 
            this.btn_del_mappic.Location = new System.Drawing.Point(607, 9);
            this.btn_del_mappic.Name = "btn_del_mappic";
            this.btn_del_mappic.Size = new System.Drawing.Size(94, 23);
            this.btn_del_mappic.TabIndex = 5;
            this.btn_del_mappic.Text = "删除当前图片";
            this.btn_del_mappic.UseVisualStyleBackColor = true;
            this.btn_del_mappic.Click += new System.EventHandler(this.btn_del_mappic_Click);
            // 
            // btn_selimages
            // 
            this.btn_selimages.Location = new System.Drawing.Point(707, 9);
            this.btn_selimages.Name = "btn_selimages";
            this.btn_selimages.Size = new System.Drawing.Size(94, 23);
            this.btn_selimages.TabIndex = 3;
            this.btn_selimages.Text = "选择已有图片";
            this.btn_selimages.UseVisualStyleBackColor = true;
            this.btn_selimages.Click += new System.EventHandler(this.btn_selimages_Click);
            // 
            // btn_selfile
            // 
            this.btn_selfile.Location = new System.Drawing.Point(326, 9);
            this.btn_selfile.Name = "btn_selfile";
            this.btn_selfile.Size = new System.Drawing.Size(81, 23);
            this.btn_selfile.TabIndex = 2;
            this.btn_selfile.Text = "选择文件";
            this.btn_selfile.UseVisualStyleBackColor = true;
            this.btn_selfile.Click += new System.EventHandler(this.btn_selfile_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(413, 9);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(57, 23);
            this.btn_upload.TabIndex = 1;
            this.btn_upload.Text = "上传";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // txt_file
            // 
            this.txt_file.Location = new System.Drawing.Point(13, 11);
            this.txt_file.Name = "txt_file";
            this.txt_file.Size = new System.Drawing.Size(307, 21);
            this.txt_file.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webBrowser_Map);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 689);
            this.splitContainer1.SplitterDistance = 754;
            this.splitContainer1.TabIndex = 1;
            // 
            // webBrowser_Map
            // 
            this.webBrowser_Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser_Map.Location = new System.Drawing.Point(0, 0);
            this.webBrowser_Map.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_Map.Name = "webBrowser_Map";
            this.webBrowser_Map.Size = new System.Drawing.Size(754, 689);
            this.webBrowser_Map.TabIndex = 2;
            this.webBrowser_Map.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_Map_DocumentCompleted);
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
            this.splitContainer2.Panel1.Controls.Add(this.treeView_device);
            this.splitContainer2.Size = new System.Drawing.Size(250, 689);
            this.splitContainer2.SplitterDistance = 229;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView_device
            // 
            this.treeView_device.ContextMenuStrip = this.contextMenuStrip_tree;
            this.treeView_device.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_device.ImageIndex = 0;
            this.treeView_device.ImageList = this.imageList_tree;
            this.treeView_device.Location = new System.Drawing.Point(0, 0);
            this.treeView_device.Name = "treeView_device";
            this.treeView_device.SelectedImageIndex = 0;
            this.treeView_device.Size = new System.Drawing.Size(250, 229);
            this.treeView_device.TabIndex = 0;
            this.treeView_device.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_device_AfterSelect);
            // 
            // contextMenuStrip_tree
            // 
            this.contextMenuStrip_tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_dw});
            this.contextMenuStrip_tree.Name = "contextMenuStrip_tree";
            this.contextMenuStrip_tree.Size = new System.Drawing.Size(125, 26);
            // 
            // ToolStripMenuItem_dw
            // 
            this.ToolStripMenuItem_dw.Name = "ToolStripMenuItem_dw";
            this.ToolStripMenuItem_dw.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_dw.Text = "重新定位";
            this.ToolStripMenuItem_dw.Click += new System.EventHandler(this.ToolStripMenuItem_dw_Click);
            // 
            // imageList_tree
            // 
            this.imageList_tree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_tree.ImageStream")));
            this.imageList_tree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_tree.Images.SetKeyName(0, "camera_24.png");
            // 
            // FrmSecMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSecMap";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "二级地图设置";
            this.Load += new System.EventHandler(this.FrmSecMap_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuStrip_tree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_selfile;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.TextBox txt_file;
        private System.Windows.Forms.Button btn_selimages;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowser_Map;
        private System.Windows.Forms.Button btn_del_mappic;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView_device;
        private System.Windows.Forms.ImageList imageList_tree;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_tree;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_dw;
        private System.Windows.Forms.Button btn_reload_map;
    }
}