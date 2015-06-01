using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Configuration;

using MDS.CLIENT;
using MDS.CLIENT.Domain;
using MDS.ConstTag;

namespace AlarmMapClient
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmSecMap : Form
    {
        private string mapurl;

        public string Mapurl
        {
            get { return mapurl; }
            set { mapurl = value; }
        }
        private bool isUploaded;

        public bool IsUploaded
        {
            get { return isUploaded; }
            set { isUploaded = value; }
        }

        private float latitude;

        public float Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private float longitude;

        public float Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        private string deviceId;
        private string channelId;
        private string channelName;
        private string username;
        private string passwd;

        private string title;
        private string truename;
        private string addr;
        private string tel;


        public FrmSecMap(string deviceId, string username, string passwd, bool isUploaded, string mapurl, float latitude, float longitude, string title, string truename, string addr, string tel)
        {
            InitializeComponent();

            this.deviceId = deviceId;
            this.username = username;
            this.passwd = passwd;
            this.isUploaded = isUploaded;
            this.mapurl = mapurl;
            this.latitude = latitude;
            this.longitude = longitude;

            this.title = title;
            this.truename = truename;
            this.addr = addr;
            this.tel = tel;

            this.Text = "【"+title+"】二级地图设置";

            // WebBrowser控件显示的网页路径
            this.webBrowser_Map.Url = new Uri(ConfigurationManager.AppSettings["WebMapServerURL2"].ToString());
            // 将当前类设置为可由脚本访问
            this.webBrowser_Map.ObjectForScripting = this;
            this.webBrowser_Map.ScriptErrorsSuppressed = true;
        }

        private void btn_selfile_Click(object sender, EventArgs e)
        {
            FileDialog fdialog = new OpenFileDialog();
            fdialog.Filter = "jpg files(*.jpg)|*.jpg";

            if (fdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txt_file.Text = fdialog.FileName;
                this.btn_upload.Enabled = true;
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            //upload
            //progressBar.Visible = true;
            lblState.Text = "";
            string serverUrl = ConfigurationManager.AppSettings["WebServerURL"].ToString() + "/Web/Upload.aspx?username=" + this.username + "&pwd=" + this.passwd;
            int ret = FileUtils.UploadRequest(serverUrl, this.txt_file.Text, "map.jpg", lblState);
            if (ret == 1)
            {
                MessageBox.Show("上传成功！", "地图", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //重新加载图片列表
                this.FrmSecMap_Load(this, null);
            }
            else
                MessageBox.Show("上传失败，请稍候再试！", "地图", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FrmSecMap_Load(object sender, EventArgs e)
        {
            this.btn_upload.Enabled = false;
            this.btn_del_mappic.Visible = false;
            lblState.Text = "";

            //加载设备的通道信息
            List<Channel> channels = MDSUtils.GetChannelInfoByDevice(this.deviceId);
            foreach (Channel c in channels)
            {
                TreeNode node = new TreeNode(c.ChannelName);
                node.Tag = c;
                node.ImageIndex = 0;

                treeView_device.Nodes.Add(node);
            }

            //获取用户所有二级地图列表,如果有设置二级地图则默认显示已选择的二级地图
            List<MapPic> pics = MDSUtils.GetMapPicList(this.username);

            //加载已有图片
            this.splitContainer2.Panel2.AutoScroll = true;
            if (pics != null && pics.Count > 0)
            {
                this.splitContainer2.Panel2.Controls.Clear();

                WebClient wc = new WebClient();
                foreach (MapPic mpic in pics)
                {

                    string url = ConfigurationManager.AppSettings["WebMapFileURL"].ToString() + mpic.PicPath + "/" + mpic.FileName;//全路径
                    Image image = Image.FromStream(wc.OpenRead(url));

                    PictureBox pic = new PictureBox();
                    pic.Dock = DockStyle.Top;
                    pic.Width = this.splitContainer2.Panel2.Width;
                    pic.Height = this.splitContainer2.Panel2.Width;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Image = image;
                    pic.Margin = new Padding(1, 1, 1, 1);
                    pic.Cursor = Cursors.Hand;
                    pic.DoubleClick += pic_DoubleClick;
                    pic.Tag = url;

                    ToolTip p = new ToolTip();
                    p.ShowAlways = true;
                    p.ToolTipTitle = "地图图片:" + url;
                    p.SetToolTip(pic, "双击切换此地图");

                    this.splitContainer2.Panel2.Controls.Add(pic);

                }
                this.splitContainer1.Panel2Collapsed = false;
            }
            else
                this.splitContainer1.Panel2Collapsed = true;

        }
        /// <summary>
        /// 切换二级地图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pic_DoubleClick(object sender, EventArgs e)
        {
            webBrowser_Map.Document.InvokeScript("ClearMap", null);

            PictureBox pic = (PictureBox)sender;
            //调用js切换二级地图
            this.Mapurl = ((string)pic.Tag).Substring(0, ((string)pic.Tag).LastIndexOf("/"));
            reloadMap(this.Mapurl);

            //更新设备地图?
            if (MessageBox.Show("更新设备地图?", "地图", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
                MDSUtils.UpdateMapPic(this.deviceId, (string)pic.Tag);
        }
        /// <summary>
        /// 打开或关闭图片列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selimages_Click(object sender, EventArgs e)
        {
            //选择已有图片
            this.splitContainer1.Panel2Collapsed = !this.splitContainer1.Panel2Collapsed;
        }
        /// <summary>
        /// 设置二级地图坐标以及二级地图地址
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public void JsSelectPoint(string channelId,string pic, float latitude, float longitude)
        {
            //this.txt_gps.Text = "经度：" + latitude + ",纬度：" + longitude;
            //选择当前通道坐标,保存入库
            bool ret = MDSUtils.UpdateMapTwoLL(channelId, latitude, longitude);
            if (ret)
            {
                MessageBox.Show("二级地图及通道(" + channelId + ")坐标设置成功！", "通道设置", MessageBoxButtons.OK, MessageBoxIcon.Information);

                treeView_device.Nodes.Clear();
                List<Channel> channels = MDSUtils.GetChannelInfoByDevice(this.deviceId);
                foreach (Channel c in channels)
                {
                    TreeNode node = new TreeNode(c.ChannelName);
                    node.Tag = c;
                    node.ImageIndex = 0;

                    treeView_device.Nodes.Add(node);
                }
                reloadMap(this.Mapurl);
            }
            else
                MessageBox.Show("二级地图及通道(" + channelId + ")坐标设置失败！", "通道设置", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 删除当前二级地图图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_del_mappic_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser_Map_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //调用js显示MAP
            if (isUploaded && this.Mapurl != null)
            {
                reloadMap(this.Mapurl);
            }
            else
            {
                //提示调用上传面板上传jpg图片
                MessageBox.Show("请选择二级地图图片并上传或从右侧已有图片中选择地图图片！", "二级地图", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void reloadMap(string url)
        {
            object[] param0 = new object[1];
            param0[0] = url;
            webBrowser_Map.Document.InvokeScript("loadMap", param0);
            //加载所有通道点,调用js显示
            foreach (TreeNode node in treeView_device.Nodes)
            {
                Channel c = (Channel)node.Tag;

                if (c.Latitude != 0 && c.Longitude != 0)
                {
                    object[] param = new object[7];
                    param[0] = false;
                    param[1] = c.Latitude;
                    param[2] = c.Longitude;
                    param[3] = this.title+"->"+c.ChannelName;
                    param[4] = this.addr;
                    param[5] = this.tel;
                    param[6] = this.truename;

                    webBrowser_Map.Document.InvokeScript("addPoint", param);
                }
            }
        }

        private void treeView_device_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeView_device.SelectedNode;
            if (node != null)
            {
                Channel c = (Channel)node.Tag;
                this.channelId = c.ChannelID;
                this.channelName = c.ChannelName;

                this.latitude = c.Latitude;
                this.longitude = c.Longitude;
            }
        }

        private void ToolStripMenuItem_dw_Click(object sender, EventArgs e)
        {
            object[] param = new object[2];
            param[0] = this.channelId;
            param[1] = this.channelName;

            webBrowser_Map.Document.InvokeScript("SelectPoint", param);
        }

        private void btn_reload_map_Click(object sender, EventArgs e)
        {
            this.webBrowser_Map.Refresh();
            this.Close();
        }
    }
}
