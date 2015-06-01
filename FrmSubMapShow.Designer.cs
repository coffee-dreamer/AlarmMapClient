namespace AlarmMapClient
{
    partial class FrmSubMapShow
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
            this.webBrowserMap = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserMap
            // 
            this.webBrowserMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserMap.Location = new System.Drawing.Point(0, 0);
            this.webBrowserMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserMap.Name = "webBrowserMap";
            this.webBrowserMap.Size = new System.Drawing.Size(711, 472);
            this.webBrowserMap.TabIndex = 0;
            this.webBrowserMap.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserMap_DocumentCompleted);
            // 
            // FrmSubMapShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 472);
            this.Controls.Add(this.webBrowserMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSubMapShow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "二级地图中心";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSubMapShow_FormClosed);
            this.Load += new System.EventHandler(this.FrmSubMapShow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserMap;
    }
}