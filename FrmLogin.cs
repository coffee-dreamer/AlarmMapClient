using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Configuration;
using System.IO;
using System.Resources;
using System.Media;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Net;

using MDS.CLIENT;
using MDS.Request;
using MDS.Reponse;
using MDS.CLIENT.Domain;
using MDS.ConstTag;

using MDS.Bll;

namespace AlarmMapClient
{
    public partial class FrmLogin : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private System.Object lockThis = new System.Object();
        private System.Object lockThis2 = new System.Object();

        private bool isInit;
        private bool isDevStatusGetOver=false;
        private int initNum=0;
        private FrmMain frmMain;
        private TransSDK.pAlarmMessageCallBack alarmMessageCallBack;
        private TransSDK.pfmessageCallBack messageCallBack;
        private OpaqueCommand cmd;
        private List<String> onlineDevices;
        private List<String> uplineDevices;
        private List<String> downlineDevices;
        private int isOnlineOrOffline = 0;

        public FrmLogin()
        {
            InitializeComponent();

            isInit = false;
            string title = ConfigurationManager.AppSettings["LoginTitle"].ToString();
            this.Text = title;
            ChangeImage();
            //cmd = new OpaqueCommand();
            onlineDevices = new List<string>();
            uplineDevices = new List<string>();
            downlineDevices = new List<string>();

            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Interval = 1000*5;
            //timer.AutoReset = true;
            //timer.Enabled = true;
            //timer.Elapsed += timer_Elapsed;

            isDevStatusGetOver = false;
            System.Timers.Timer uptimer = new System.Timers.Timer();
            uptimer.Interval = 100;
            uptimer.AutoReset = true;
            uptimer.Enabled = true;
            uptimer.Elapsed += uptimer_Elapsed;
        }
        void uptimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!isDevStatusGetOver) return;

            lock (lockThis2)
            {

                string szDeviceID = null;
                bool isup = false;
                lock (uplineDevices)
                {
                    if (uplineDevices.Count >= 1)
                    {
                        szDeviceID = uplineDevices[0];

                        isup = true;
                    }
                }
                if (szDeviceID == null)
                {
                    lock (downlineDevices)
                    {
                        if (downlineDevices.Count >= 1)
                        {
                            szDeviceID = downlineDevices[0];

                            isup = false;
                        }
                    }
                }

                //通知主界面
                if (frmMain != null && frmMain.isWebLoaded && szDeviceID != null)
                {
                    if (isup)
                    {
                        lock (uplineDevices)
                        {
                            uplineDevices.RemoveAt(0);
                            if(initNum>0) initNum--;
                        }
                        //上线
                        //int ret = -1;
                        //int tryNum = 0;
                        //while (ret < 0&&tryNum<5)
                        //{
                        //    ret = frmMain.openTransportRs232(szDeviceID);
                        //    tryNum++;
                        //    System.Threading.Thread.Sleep(1000*5);
                        //}
                        //更新在线掉线状态
                        frmMain.FreshDeviceStatus(szDeviceID, true);

                        //查询单片机布撤防状态
                        Device dev2 = MDSUtils.GetDeviceInfo(szDeviceID);
                        frmMain.SearchTransStatus(dev2, true);
                    }
                    else
                    {
                        lock (downlineDevices)
                        {
                            downlineDevices.RemoveAt(0);
                        }
                        //下线
                        //frmMain.closeTransport(szDeviceID);
                        frmMain.FreshDeviceStatus(szDeviceID, false);
                    }
                    this.isOnlineOrOffline = 0;
                    if (initNum <= 0)
                    {
                        //获取设备信息
                        Device d = MDSUtils.GetDeviceInfo(szDeviceID);

                        SectorLog sectorLog = new SectorLog();
                        string strHostName = Dns.GetHostName();  //得到本机的主机名
                        IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                        string strAddr = ipEntry.AddressList[0].ToString(); //假设本地主机为单网卡
                        sectorLog.ip = strAddr;
                        sectorLog.oprTime = DateTime.Now;
                        sectorLog.port = 0;
                        sectorLog.sn = szDeviceID;
                        sectorLog.usr = "";
                        this.isOnlineOrOffline = isup ? 1 : 2;
                        sectorLog.remark = (isup ? d.DeviceName + "|" + "设备上线" : d.DeviceName + "|" + "设备下线");
                        MDSUtils.SaveSectorLog(sectorLog);

                        //同时存入本地DB用于显示
                        AlarmMsg amsg = new AlarmMsg();
                        amsg.Type = TransSDK.enum_P_Client_AlarmMsg.enum_LOCAL_LOG;
                        amsg.DeviceId = szDeviceID;
                        amsg.Channel = "无";
                        amsg.Nchannel = 0;
                        amsg.Status = 1;//1 产生 2 消失
                        amsg.Param = 0;// 0
                        amsg.IsAlarmed = false;
                        amsg.IsDeled = false;
                        amsg.AlarmTime = DateTime.Now;
                        amsg.IsViewed = false;
                        amsg.alarmTxt = sectorLog.remark;
                        amsg.ChannelName = sectorLog.remark;
                        
                        amsg.UserName = d.TrueName == null ? "" : d.TrueName;
                        amsg.Tel = d.Tel == null ? "" : d.Tel;
                        amsg.Address = d.Address == null ? "" : d.Address;
                        AlarmLogBll.Save(amsg);

                        //调用报警检查,通道UI线程更新,并由其同时调用JS函数进行地图展示
                        UIMsgShow();
                    }
                }
                
            }
        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (frmMain != null && frmMain.treeloaded && this.onlineDevices.Count > 0 && frmMain.isWebLoaded)
            {
                ((System.Timers.Timer)sender).Stop();

                frmMain.FreshOnlineDevStatus(this.onlineDevices,false);
                frmMain.FreshOnlineDevStatus(this.onlineDevices, true);

                isDevStatusGetOver = true;
            }
        }

        private void ChangeImage() { 
            string path = System.Windows.Forms.Application.StartupPath;
            string imageFileName = ConfigurationManager.AppSettings["ImageFile"].ToString();
            path = path + "\\" + imageFileName;
            DirectoryInfo dir = new DirectoryInfo(path);

            if(!dir.Exists){
                dir.Create();
            }

            string imgtype = ConfigurationManager.AppSettings["ImageType"].ToString(); ;
            string[] ImageType = imgtype.Split('|');

            for(int i = 0;i < ImageType.Length;i++){
                string[] files = Directory.GetFiles(path, ImageType[i]);
                if(files != null && files.Length != 0){
                    this.pictureBox1.Image = Image.FromFile(files[0]);
                    break;
                }
            }
        }
        private bool CheckOpr(string oprname, string oprpwd)
        {
            bool ret = false;
            //从XML文件校验用户
            //XmlDocument xmlDoc=new XmlDocument();
            //xmlDoc.Load("OprData.xml");
            XDocument  doc = XDocument.Load("OprData.xml");
            var query = from t in doc.Descendants("user")
                        select new
                        {
                            username = t.Element("username").Value,
                            password = t.Element("password").Value
                        };
            foreach (var user in query)
            {
                if (oprname.Equals(user.username) && oprpwd.Equals(user.password))
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            //cmd.ShowOpaqueLayer(this, 125, true);
            

            if (this.cb_opruser.Text == null || this.cb_opruser.Text.Equals(""))
            {
                //cmd.HideOpaqueLayer();
                MessageBox.Show("操作员不能为空！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //检查操作员权限,加载XML数据进行比较校验
                if (CheckOpr(this.cb_opruser.Text, this.txt_oprpwd.Text) == false)
                {
                    //cmd.HideOpaqueLayer();
                    MessageBox.Show("操作员信息不正确！", "校验错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                //Int32 dwUser = 0;
                int ret = -1;

                if (isInit == false)
                {
                    messageCallBack = new TransSDK.pfmessageCallBack(pfmessageCallBack);
                    GC.KeepAlive(messageCallBack);
                    ret = TransSDK.P_Client_Init(messageCallBack, this.Handle);
                    if (ret == 0)
                        isInit = true;
                    else
                    {
                        isInit = false;
                        cmd.HideOpaqueLayer();
                        MessageBox.Show("登录失败！");
                        return;
                    }
                }

                alarmMessageCallBack = new TransSDK.pAlarmMessageCallBack(this.AlarmMessageCallBack);
                GC.KeepAlive(alarmMessageCallBack);
                TransSDK.P_Client_SetAlarmCallBack(alarmMessageCallBack, this.Handle);
                //从配置文件取ip:220.250.1.142
                ret = TransSDK.P_Client_LoginServer(this.txt_server.Text, 9000, txt_userid.Text, txt_pwd.Text, 6000);
                if (ret >= 0)
                {
                    //初始化NETSDK
                    DeviceF.Instance.Init(this.Handle);

                    //MessageBox.Show("登录成功！");
                    //保存操作员用户名及密码
                    if (this.cb_userpwd.Checked)
                    {
                        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        config.AppSettings.Settings.Remove("OprUserId");
                        // Add an Application Setting.  
                        config.AppSettings.Settings.Add("OprUserId", this.cb_opruser.Text);
                        // Save the changes in App.config file.  
                        config.Save(ConfigurationSaveMode.Modified);
                        config.AppSettings.Settings.Remove("OprPwd");
                        // Add an Application Setting.  
                        config.AppSettings.Settings.Add("OprPwd", this.txt_oprpwd.Text);
                        // Save the changes in App.config file.  
                        config.Save(ConfigurationSaveMode.Modified);

                        // Force a reload of a changed section.  
                        //ConfigurationManager.RefreshSection("appSettings");
                    }

                    int dlen = TransSDK.P_Client_GetDevicesLen();
                    //System.Threading.Thread.Sleep(1000);
                    //MessageBox.Show("返回设备数据长度：" + dlen);
                    if (dlen > 0)
                    {
                        byte[] pBuffer = new byte[dlen + 1];
                        int rlen = TransSDK.P_Client_GetDevices(pBuffer, dlen + 1);
                        string xml = Encoding.Default.GetString(pBuffer);
                        //MessageBox.Show("实际设备数据长度：" + rlen + "\r\n" + xml);
                        log.Debug(xml);
                        this.Hide();
                        this.Opacity = 0.0f;
                        this.ShowInTaskbar = false;

                        frmMain = new FrmMain(xml, txt_userid.Text, txt_pwd.Text, cb_opruser.Text);
                        frmMain.StartPosition = FormStartPosition.CenterScreen;
                        frmMain.WindowState = FormWindowState.Maximized;
                        frmMain.LoginFrm = this;
                        frmMain.ShowDialog();

                        pBuffer = null;
                        xml = null;
                    }
                    else
                        MessageBox.Show("获取设备列表长度出错！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    switch (ret)
                    {
                        case ErrCode.P_CLIENT_UNKNOW_ERROR:
                            MessageBox.Show("未知错误！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ErrCode.P_CLIENT_LOGIN_TIMEOUT:
                            MessageBox.Show("登陆超时！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ErrCode.P_CLIENT_NO_USER:
                            MessageBox.Show("没有当前用户！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ErrCode.P_CLIENT_PWD_ERROR:
                            MessageBox.Show("密码错误！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ErrCode.P_CLIENT_HAS_LOGIN:
                            MessageBox.Show("用户已被其他地方登陆！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ErrCode.P_CLIENT_CONNECT_ERROR:
                            MessageBox.Show("连接服务器失败！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ErrCode.P_CLIENT_GET_GROUP_TIMEOUT:
                            MessageBox.Show("获取权限组列表超时！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("未知错误:" + ret + "！", "平台登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //cmd.HideOpaqueLayer();
            }
        }

        private void pfmessageCallBack(TransSDK.enum_P_Client_Msg enumType, int nType2, string szDeviceID, int channel, int nParam1, string pParam1, Int32 dwUser)
        {
            //enum_P_Client_Device_Status 设备状态
            //检测设备状态的变化?
            /**
             *  1.设备状态显示 【在线布防 在线撤防 掉线，这个用3.0加最新的透传接口，应该可以解决】
                2.设备操作日志【遥控布、手机、PC 撤防日志 掉线日志 上线日志 】
                3.手机远程布撤防
             * */
            //enum_P_Client_Device_Status 1 在线 0 不在线
            log.Error("pfmessageCallBack:" + szDeviceID + ",client msg type:" + enumType + ",nType2=" + nType2);
            if (enumType == TransSDK.enum_P_Client_Msg.enum_P_Client_Device_Status)
            {
                //保存信息
                if (nType2 == 1)
                {
                    if (!onlineDevices.Contains(szDeviceID))
                        onlineDevices.Add(szDeviceID);

                    if(!uplineDevices.Contains(szDeviceID)){
                        lock (uplineDevices)
                        {
                            uplineDevices.Add(szDeviceID);
                            if (!isDevStatusGetOver) initNum++;
                        }
                    }
                }
                else
                {
                    if (onlineDevices.Contains(szDeviceID))
                        onlineDevices.Remove(szDeviceID);

                    if (!downlineDevices.Contains(szDeviceID))
                    {
                        lock (downlineDevices)
                        {
                            downlineDevices.Add(szDeviceID);
                        }
                    }
                }

               //

            }
            else if (enumType == TransSDK.enum_P_Client_Msg.enum_P_Client_CMS_Server_Status)
            {
                
            }
            else if (enumType == TransSDK.enum_P_Client_Msg.enum_P_Client_Reload_DeviceData)
            {
            }
            else if (enumType == TransSDK.enum_P_Client_Msg.enum_P_Client_Dev_Grant_Result)
            {
                //设备状态获取完成
                isDevStatusGetOver = true;
            }
        }

        private delegate void UpdateMsgUI();

        private void AlarmMessageCallBack(TransSDK.enum_P_Client_AlarmMsg enumType, string szDeviceID, int nchannel, int nStatus, int nParam1, Int32 dwUser)
        {
            try
            {
                log.Error("pAlarmMessageCallBack:" + szDeviceID);
                //MessageBox.Show("pAlarmMessageCallBack:" + szDeviceID);
                int channel = nchannel + 1;
                string channelId;
                if (channel < 10) channelId = szDeviceID + "0" + channel;
                else channelId = szDeviceID + channel;

                //如果是消失告警则从LIST中移除
                AlarmData shareData = AlarmData.ShareData();
                //第一次从DB加载旧的数据

                //if (nStatus == 2)
                //{
                //    foreach (AlarmMsg msg in shareData.allMsgs())
                //    {
                //        if (msg.DeviceId.Equals(szDeviceID) && msg.Channel.Equals(channelId) && msg.Type == enumType)
                //        {
                //            msg.IsDeled = true;
                //            break;
                //        }
                //    }

                //    //更新本地库
                //    AlarmLogBll.UpdateStatus(szDeviceID, channelId, (int)enumType);

                //    UpdateMsgUI umu = new UpdateMsgUI(UpdateMainFrmUI);
                //    this.BeginInvoke(umu);
                //}
                //else
                {
                    bool isold = false;//是否为旧的已经报警过的设备通道
                    //判断原来是否已存在
                    foreach (AlarmMsg msg in shareData.allMsgs())
                    {
                        if (msg.DeviceId.Equals(szDeviceID) && msg.Channel.Equals(channelId) && msg.Type == enumType)
                        {
                            isold = true;
                            msg.IsDeled = false;
                            msg.IsAlarmed = false;
                            msg.AlarmTime = DateTime.Now;
                            msg.IsViewed = false;
                            break;
                        }
                    }

                    AlarmMsg amsg = new AlarmMsg();
                    amsg.Type = enumType;
                    amsg.DeviceId = szDeviceID;
                    amsg.Channel = channelId;
                    amsg.Nchannel = nchannel;
                    amsg.Status = nStatus;//1 产生 2 消失
                    amsg.Param = nParam1;// 0
                    amsg.IsAlarmed = false;
                    amsg.IsDeled = false;
                    amsg.AlarmTime = DateTime.Now;
                    amsg.IsViewed = false;
                    amsg.alarmTxt = "联动防区";
                    bool isYidongZhence = false;
                    switch (enumType)
                    {
                        case TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_Unknown:amsg.alarmTxt = "联动防区(未知)";break;
                        case TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_VIDEO_LOST:amsg.alarmTxt = "联动防区(视频丢失)";break;
                        case TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_EXTERNAL_ALARM:amsg.alarmTxt = "联动防区(外部报警)";break;
                        case TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_MOTION_DETECT:amsg.alarmTxt = "联动防区(移动侦测)"; isYidongZhence = true;break;
                        case TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_VIDEO_SHELTER:amsg.alarmTxt = "联动防区(视频遮挡)";break;
                        case TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_DISK_FULL:amsg.alarmTxt = "联动防区(硬盘满)";break;
                        case TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_DISK_FAULT:amsg.alarmTxt = "联动防区(硬盘故障)";break;
                        case TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_EMAX: amsg.alarmTxt = "联动防区(单片机告警消息)"; break;
                        default: break;
                    }
                    //从HTTP网络获取相关信息合并,增强ams信息量

                    if(!isYidongZhence){
                        Channel c = null;
                        Device d = null;
                        Alert a = null;

                        //如果外部报警取报警通道名称，共它取摄像头名称
                        c = MDSUtils.GetChannelInfo(channelId);
                        d = MDSUtils.GetDeviceInfo(szDeviceID);
                        if (enumType == TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_EXTERNAL_ALARM)
                        {
                            string alertId = "";
                            if (channel < 10) alertId = szDeviceID + "00" + channel;
                            else if (channel < 100 && channel >= 10) alertId = szDeviceID + "0" + channel;
                            else alertId = szDeviceID + channel;

                            a = MDSUtils.GetAlertById(alertId);
                        }
                        if (c != null)
                        {
                            amsg.Latitude = d.Latitude;
                            amsg.Longitude = d.Longitude;
                            amsg.Latitude2 = c.Latitude;
                            amsg.Longitude2 = c.Longitude;
                            //获取设备信息
                            Device dev = MDSUtils.GetDeviceInfo(szDeviceID);

                            amsg.UserName = d.TrueName == null ? "" : d.TrueName;
                            amsg.Tel = d.Tel == null ? "" : d.Tel;
                            amsg.Address = d.Address == null ? "" : d.Address;
                            if (a == null)
                                amsg.ChannelName = amsg.alarmTxt + "|" + dev.DeviceName + "->" + c.ChannelName;
                            else
                                amsg.ChannelName = amsg.alarmTxt + "|" + dev.DeviceName + "->" + a.AlertName;
                            amsg.MapPic = d.MapPic == null ? "" : d.MapPic;

                            dev = null;
                            c = null;
                            d = null;
                        }

                        if (!isold)//新通道报警则播放队列
                        {
                            shareData.addMsg(amsg);
                        }


                        //本地入库
                        AlarmLogBll.Save(amsg);

                        //调用报警检查,通道UI线程更新,并由其同时调用JS函数进行地图展示
                        this.isOnlineOrOffline = 0;
                        UIMsgShow();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                MessageBox.Show("服务异常！" + ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        public void UIMsgShow()
        {
            UpdateMsgUI umu = new UpdateMsgUI(AlarmUpdateMainFrmUI);
            this.BeginInvoke(umu);
        }
        private void UpdateMainFrmUI()
        {
            if (frmMain != null && frmMain.isWebLoaded)
            {
                while (frmMain.isAlarmExe == true)
                    System.Threading.Thread.Sleep(1000);
                frmMain.CheckAlarmMsg();
            }
        }
        private void AlarmUpdateMainFrmUI()
        {
            //播放声音
            //Assembly assembly;
            //assembly = Assembly.GetExecutingAssembly();
            
            SoundPlayer sp;
            if(this.isOnlineOrOffline == 1){
                sp = new SoundPlayer(Properties.Resources.online);
            }
            else if (this.isOnlineOrOffline == 2)
            {
                sp = new SoundPlayer(Properties.Resources.offline);
            }
            else {
                sp = new SoundPlayer(Properties.Resources.ALARM8);
            }
            
            sp.Play();
            lock (lockThis)
            {
                if (frmMain != null && frmMain.isWebLoaded)//&& frmMain.isAlarmExe == false
                    frmMain.CheckAlarmMsg();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isInit) TransSDK.P_Client_ClearUp();

            DeviceF.Instance.Cleanup();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            //加载XML配置操作员信息
            XDocument  doc = XDocument.Load("OprData.xml");
            var query = from t in doc.Descendants("user")
                        select new
                        {
                            username = t.Element("username").Value,
                            password = t.Element("password").Value
                        };
            this.cb_opruser.DataSource = query.ToList();
            int ind=0,i=0;
            foreach (var v in query)
            {
                if(v.username.Equals(ConfigurationManager.AppSettings["OprUserId"].ToString()))
                {
                    ind = i;
                    break;
                }
                i++;
            }

            //取历史保存用户名及密码
            this.txt_server.Text = ConfigurationManager.AppSettings["VideoServerIP"].ToString();
            this.txt_userid.Text = ConfigurationManager.AppSettings["UserId"].ToString();
            this.txt_pwd.Text = ConfigurationManager.AppSettings["Pwd"].ToString();
            this.cb_opruser.SelectedIndex  = ind;
            //this.txt_opruser.Text = ConfigurationManager.AppSettings["OprUserId"].ToString();
            this.txt_oprpwd.Text = ConfigurationManager.AppSettings["OprPwd"].ToString();

            //隐藏平台设置
            this.Height -= this.splitContainer_login.Panel2.Height;
            this.splitContainer_login.Panel2Collapsed = true;

            //删除本地DB历史数据
            //string alarmendTime = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss");
            DateTime alarmstartTime = DateTime.Now.AddDays(-365*10);
            DateTime alarmendTime = DateTime.Now.AddDays(-30);
            AlarmLogBll.DelLogs(null, null, alarmendTime);
        }

        //保存平台设置
        private void btn_server_set_Click(object sender, EventArgs e)
        {
            if (this.txt_userid.Text != null && this.txt_server.Text!=null)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("UserId");
                // Add an Application Setting.  
                config.AppSettings.Settings.Add("UserId", this.txt_userid.Text);
                // Save the changes in App.config file.  
                config.Save(ConfigurationSaveMode.Modified);
                config.AppSettings.Settings.Remove("Pwd");
                // Add an Application Setting.  
                config.AppSettings.Settings.Add("Pwd", this.txt_pwd.Text);
                // Save the changes in App.config file.  
                config.Save(ConfigurationSaveMode.Modified);

                config.AppSettings.Settings.Remove("VideoServerIP");
                config.AppSettings.Settings.Add("VideoServerIP", this.txt_server.Text);
                config.Save(ConfigurationSaveMode.Modified);
                config.AppSettings.Settings.Remove("WebMapServerURL");
                config.AppSettings.Settings.Add("WebMapServerURL", "http://" + this.txt_server.Text + ":8100//Web/index.html");
                config.Save(ConfigurationSaveMode.Modified);
                config.AppSettings.Settings.Remove("WebMapServerURL2");
                config.AppSettings.Settings.Add("WebMapServerURL2", "http://" + this.txt_server.Text + ":8100/Web/subMap.html");
                config.Save(ConfigurationSaveMode.Modified);
                config.AppSettings.Settings.Remove("WebMapFileURL");
                config.AppSettings.Settings.Add("WebMapFileURL", "http://" + this.txt_server.Text + ":8100/Web/ImgData");
                config.Save(ConfigurationSaveMode.Modified);
                config.AppSettings.Settings.Remove("MDSServerURL");
                config.AppSettings.Settings.Add("MDSServerURL", "http://" + this.txt_server.Text + ":8100/MDSService/SI");
                config.Save(ConfigurationSaveMode.Modified);
                config.AppSettings.Settings.Remove("WebServerURL");
                config.AppSettings.Settings.Add("WebServerURL", "http://" + this.txt_server.Text + ":8100");
                config.Save(ConfigurationSaveMode.Modified);
            }
        }

        private void cb_serverset_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_serverset.Checked)
            {
                this.splitContainer_login.Panel2Collapsed = false;
                this.Height = 305;
            }
            else
            {
                this.Height -= this.splitContainer_login.Panel2.Height;
                this.splitContainer_login.Panel2Collapsed = true;
            }
        }

    }
}
