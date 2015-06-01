using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;
using System.Configuration;

using MDS.CLIENT;
using MDS.CLIENT.Domain;
using MDS.ConstTag;
using MDS.Bll;

namespace AlarmMapClient
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmCodePlayer : Form
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FrmMain frm;
        private int channelId;
        private string deviceId;
        private string oprName;
        private int nStream;

        private DeviceF df;
        private NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo;
        private bool isLogin;
        private bool isClosing;
        private bool isSetting;

        public bool IsSetting
        {
            get { return isSetting; }
            set { isSetting = value; }
        }
        private Device dev;

        private int handle;

        public FrmCodePlayer()
        {
            InitializeComponent();
        }

        public FrmCodePlayer(FrmMain frm, string deviceId, int channelId, string oprName, DateTime alarmTime,string alarmTxt)
        {
            InitializeComponent();
            this.frm = frm;
            this.channelId = channelId;
            this.deviceId = deviceId;
            this.nStream = 2;

            this.Text = alarmTxt+"|设备【" + this.deviceId + "|" + (channelId+1) + "】," + alarmTime.ToLongTimeString().ToString();

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = true;
        }
        public bool InPlaying(string deviceId, int channelId)
        {
            if (this.deviceId.Equals(deviceId) && this.channelId == channelId && isLogin)
                return true;
            else
                return false;
        }
        public bool NewVideo(string deviceId, int channelId)
        {
            if (this.deviceId.Equals(deviceId) && this.channelId == channelId)
                return false;
            else
                return true;
        }
        public void SetPara(string deviceId, int channelId,DateTime alarmTime){
            //if (this.deviceId.Equals(deviceId) && this.channelId == channelId&&isLogin)
            //    return;
            //停止旧视频
            isSetting = true;
            if (isLogin)
            {
                stopVideo();
                df.LogOut();

                isLogin = false;
            }

            this.channelId = channelId;
            this.deviceId = deviceId;
            this.Text = "设备【" + this.deviceId + "|" + (channelId + 1) + "】" + alarmTime.ToLongTimeString().ToString();
            this.Show();
            this.Activate();

            //loginDev();
            //playVideo();

            isSetting = false;
            logger.Error("SetPara 显示界面 isLogin=" + isLogin);
        }
        public void SetTitle(DateTime alarmTime)
        {
            isSetting = true;
            this.Text = "设备【" + this.deviceId + "】" + alarmTime.ToLongTimeString().ToString();
            isSetting = false;
            logger.Error("SetTitle 设置标题");
            //this.Refresh();
        }
        private void FrmCodePlayer_Load(object sender, EventArgs e)
        {

        }

        private void FrmCodePlayer_Shown(object sender, EventArgs e)
        {
            logger.Error("FrmCodePlayer_Shown 显示界面");
            if (!isLogin) loginDev();

            playVideo();
        }

        private bool loginDev()
        {
            //加载摄像头
            this.dev = MDSUtils.GetDeviceInfo(this.deviceId);
            if (this.dev == null) return false;
            
            //登录设备
            df = DeviceF.Instance;
            lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
            //isLogin = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), (ushort)this.dev.Port, this.dev.UserName, this.dev.PassWord, ref lpDeviceInfo);
            isLogin = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), 9500, this.dev.SerialNum, "", ref lpDeviceInfo);

            return isLogin;
        }

        [HandleProcessCorruptedStateExceptions]
        private void playVideo()
        {
            if (!isLogin)
            {
                //MessageBox.Show("设备登录异常！", "播放异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                logger.Error("设置没有登录,隐藏窗口");
                //this.Hide();
                this.Close();
                return;
            }

            try
            {
                handle = df.Play(this.channelId, panel1.Handle, this.nStream);

                if (handle <= 0)
                {
                    //MessageBox.Show("无法播放该通道视频：" + handle, "播放异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Error("无法播放该通道视频：" + handle);
                    df.LogOut();
                    isLogin = false;
                    //this.Hide();
                    this.Close();
                }
                
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(e.ToString());
                df.LogOut();
                isLogin = false;
                //this.Hide();
                this.Close();
            }
        }

        private void stopVideo()
        {
            try
            {
                if (isLogin && handle>0) df.Stop(handle); 
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(e.ToString());
            }
        }

        //尝试读取或写入受保护的内存。这通常指示其他内存已损坏
        [HandleProcessCorruptedStateExceptions]
        private void FrmCodePlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosing)
            {
                e.Cancel = true;
                return;
            }
            try
            {
                isClosing = true;
                stopVideo();
                df.LogOut();
                isLogin = false;
                logger.Error("登录退出.");

                //e.Cancel = true;
                //this.Hide();

                this.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

    }
}
