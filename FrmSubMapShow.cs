using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace AlarmMapClient
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmSubMapShow : Form
    {
        private string surl;
        private float latitude;
        private float longitude;
        private string channelName;
        private string address;
        private string tel;
        private string username;
        private FrmMain mainFrm;

        public bool isWebLoaded;

        private List<AlarmMsg> mapPicList;
        //后续加入多点告警功能
        public FrmSubMapShow(FrmMain mainFrm,string url, float latitude, float longitude, string channelName, string address, string tel, string username, List<AlarmMsg> mapPicList)
        {
            InitializeComponent();

            this.surl = url;
            this.latitude = latitude;
            this.longitude = longitude;
            this.channelName = channelName;
            this.address = address;
            this.tel = tel;
            this.username = username;
            this.mapPicList = mapPicList;
            this.mainFrm = mainFrm;
        }

        private void FrmSubMapShow_Load(object sender, EventArgs e)
        {
            // WebBrowser控件显示的网页路径
            this.webBrowserMap.Url = new Uri(ConfigurationManager.AppSettings["WebMapServerURL2"].ToString());
            // 将当前类设置为可由脚本访问
            this.webBrowserMap.ObjectForScripting = this;
            this.webBrowserMap.ScriptErrorsSuppressed = true;
        }
        /// <summary>
        /// 显示同一地图的所有节点
        /// </summary>
        /// <param name="AlarmMsgList"></param>
        public void ShowAlarmAllPoint(List<AlarmMsg> AlarmMsgList)
        {
            webBrowserMap.Document.InvokeScript("ClearMap");

            foreach(AlarmMsg msg in AlarmMsgList){
                bool isopen = false;
                if (this.latitude == msg.Latitude2 && this.longitude == msg.Longitude2)
                    isopen = true;

                object[] param = new object[7];
                param[0] = isopen;
                param[1] = msg.Latitude2;
                param[2] = msg.Longitude2;
                param[3] = msg.ChannelName;
                param[4] = msg.Address;
                param[5] = msg.Tel;
                param[6] = msg.UserName;
                webBrowserMap.Document.InvokeScript("addPoint", param);
            }
        }
        //添加主节点
        private void loadMainNode()
        {
            object[] param = new object[7];
            param[0] = true;
            param[1] = latitude;
            param[2] = longitude;
            param[3] = channelName;
            param[4] = address;
            param[5] = tel;
            param[6] = username;
            webBrowserMap.Document.InvokeScript("addPoint", param);
        }

        private void webBrowserMap_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            isWebLoaded = true;
            loadMapPoint();

            if (mapPicList != null && mapPicList.Count > 0)
                this.ShowAlarmAllPoint(mapPicList);
            else
                loadMainNode();
        }

        private void loadMapPoint()
        {
            object[] param = new object[1];
            param[0] = surl;
            webBrowserMap.Document.InvokeScript("loadMap", param);
        }

        private void FrmSubMapShow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.mainFrm.closeSubMapFrm();
        }
    }
}
