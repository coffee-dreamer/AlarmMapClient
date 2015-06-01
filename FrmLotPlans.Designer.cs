namespace AlarmMapClient
{
    partial class FrmLotPlans
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("所有设备");
            this.panel1 = new System.Windows.Forms.Panel();
            this.select_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.device_treeView = new System.Windows.Forms.TreeView();
            this.plan_treeView = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.select_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 276);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 32);
            this.panel1.TabIndex = 2;
            // 
            // select_button
            // 
            this.select_button.Location = new System.Drawing.Point(170, 6);
            this.select_button.Name = "select_button";
            this.select_button.Size = new System.Drawing.Size(75, 23);
            this.select_button.TabIndex = 0;
            this.select_button.Text = "保存";
            this.select_button.UseVisualStyleBackColor = true;
            this.select_button.Click += new System.EventHandler(this.select_button_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.device_treeView);
            this.panel2.Controls.Add(this.plan_treeView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(457, 276);
            this.panel2.TabIndex = 3;
            // 
            // device_treeView
            // 
            this.device_treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.device_treeView.HideSelection = false;
            this.device_treeView.Location = new System.Drawing.Point(164, 0);
            this.device_treeView.Name = "device_treeView";
            treeNode2.Name = "所有设备";
            treeNode2.Text = "所有设备";
            this.device_treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.device_treeView.Size = new System.Drawing.Size(293, 276);
            this.device_treeView.TabIndex = 3;
            this.device_treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.device_treeView_AfterCheck);
            // 
            // plan_treeView
            // 
            this.plan_treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.plan_treeView.HideSelection = false;
            this.plan_treeView.ItemHeight = 20;
            this.plan_treeView.LineColor = System.Drawing.Color.White;
            this.plan_treeView.Location = new System.Drawing.Point(0, 0);
            this.plan_treeView.Name = "plan_treeView";
            this.plan_treeView.ShowLines = false;
            this.plan_treeView.ShowRootLines = false;
            this.plan_treeView.Size = new System.Drawing.Size(164, 276);
            this.plan_treeView.TabIndex = 2;
            // 
            // FrmLotPlans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 308);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "FrmLotPlans";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量设置";
            this.Load += new System.EventHandler(this.FrmLotPlans_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button select_button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView plan_treeView;
        private System.Windows.Forms.TreeView device_treeView;
    }
}