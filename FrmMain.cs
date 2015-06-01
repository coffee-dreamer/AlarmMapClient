using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Globalization;

using MDS.CLIENT;
using MDS.CLIENT.Domain;
using MDS.Bll;
using AlarmMapClient.Domain;

namespace AlarmMapClient
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmMain : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string docXml;
        private string username;
        private string passwd;
        private string oprname;
        private DOrganization org;
        private string mappic;
        private List<AlarmMsg> MapPicList;
        private List<AlarmMsg> AlarmVideoList;
        public bool isWebLoaded;
        public bool isAlarmExe;
        private bool isClosing;
        private bool isOpenVideoNow;
        private bool isRunningTransTimer;

        private FrmCodePlayer codePlayer;
        private FrmLogin loginFrm;
        public FrmLogin LoginFrm
        {
            get { return loginFrm; }
            set { loginFrm = value; }
        }
        private List<ChannelStatus> channelStatusList;
        private List<string> transportDeviceList;

        private TreeNode treeRootNode;

        private MDS.Domain.AlarmLog curlog;

        private FrmSubMapShow frmSubMapShow;
        private OpaqueCommand cmd;
        private DeviceF df;
        private bool threadState = false;

        private bool isListenCOM;
        private bool isOpenAlarmVideo;
        private int checkAlarmVideoTime;

        public System.Timers.Timer node_timer;
        public bool isStopNodeTimer;
        public bool isInDeviceOpr;
        private bool isAutoLoadNodeStatus;

        bool isLogin;
        private static NetSDK.fTransComCallBack fCallBack;
        TreeNode curCheckNode;
        Device curCheckDevice;

        private TransSDK.pfTransportDataCallBack pfCallBack;

        NetSDK.TransComChannel transComChannel;
        IntPtr ptransComChannel;

        public List<String> onlineDevices;
        private List<String> allDevices;
        private List<SnHwd> loginDevices;
        public bool treeloaded;
        private bool isloadbcf;

        public FrmMain(string xml, string username, string passwd,string oprname)
        {
            InitializeComponent();
            string title = ConfigurationManager.AppSettings["MainTitle"].ToString();
            this.Text = title;


            this.docXml = xml;
            this.username = username;
            this.passwd = passwd;
            this.oprname = oprname;

            this.cmd = new OpaqueCommand();
            this.channelStatusList = new List<ChannelStatus>();
            this.transportDeviceList = new List<string>();
            this.MapPicList = new List<AlarmMsg>();
            this.AlarmVideoList = new List<AlarmMsg>();
            this.onlineDevices = new List<string>();
            this.allDevices = new List<string>();
            this.loginDevices = new List<SnHwd>();

            fCallBack = new NetSDK.fTransComCallBack(this.netSdkTransportDataCallBack);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosing)
            {
                isClosing = true;
                //closeAllTransport();
                foreach (SnHwd sh in this.loginDevices)
                {
                    int lhwd = sh.Lhwd;
                    stopTransportRs232(lhwd);
                }

                Marshal.FreeCoTaskMem(ptransComChannel);
                Application.Exit();
            }
            
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            transComChannel = new NetSDK.TransComChannel();
            transComChannel.TransComType = NetSDK.SERIAL_TYPE.RS232;
            transComChannel.baudrate = 38400;
            transComChannel.databits = 8;
            transComChannel.parity = 0;
            transComChannel.stopbits = 1;

            ptransComChannel = Marshal.AllocCoTaskMem(Marshal.SizeOf(transComChannel));
            Marshal.StructureToPtr(transComChannel, ptransComChannel, false);

            try
            {
                isAutoLoadNodeStatus = true;
                isListenCOM = bool.Parse(ConfigurationManager.AppSettings["ListenCOM"].ToString());
                isOpenAlarmVideo = bool.Parse(ConfigurationManager.AppSettings["OpenAlarmVideo"].ToString());
                isOpenVideoNow = true;
                checkAlarmVideoTime = int.Parse(ConfigurationManager.AppSettings["CheckAlarmVideoTime"].ToString());

                tree_devices.Nodes.Clear();
                treeRootNode = new TreeNode("所有设备");
                treeRootNode.Tag = null;
                //创建线程加载XML树
                cmd.ShowOpaqueLayer(this.tree_devices, 125, true);
                Thread t = new Thread(new ThreadStart(this.LoadXmlThread));
                t.Start();
                
                loadMap();

                BindLogs();
               
                //启动定时器,显示视频弹出窗口
                if (isOpenAlarmVideo) startCheckAlarmVideoTimer();

                //启动定时器,检查透明串口数据
                //if (isAutoLoadNodeStatus) startCheckNodeTimer();

                //绑定透明串口回调(使用转发SDK时接收消息时启用)
                if (isListenCOM) setCallBackOpenTransport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadXmlThread()
        {
            LoadXmlTree(this.treeRootNode,this.docXml);

            UpdateUI uui = new UpdateUI(this.UpdateTreeUI);
            this.BeginInvoke(uui);
        }

        private void LoadChannelMotionTreeThread()
        {
            LoadChannelMotionTree(this.treeRootNode);

            UpdateUI uui = new UpdateUI(this.UpdateChannelTreeUI);
            this.BeginInvoke(uui);
        }
        private void loadMap()
        {
            try
            {
                //System.IO.FileInfo file = new System.IO.FileInfo("index.htm");
                //file.FullName
                // WebBrowser控件显示的网页路径
                this.webBrowser_map.Url = new Uri(ConfigurationManager.AppSettings["WebMapServerURL"].ToString());
                // 将当前类设置为可由脚本访问
                this.webBrowser_map.ObjectForScripting = this;
                this.webBrowser_map.ScriptErrorsSuppressed = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("服务器连接异常！"+ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            // 调用JavaScript的messageBox方法，并传入参数

            object[] objects = new object[1];

            objects[0] = "C#访问JavaScript脚本";

            webBrowser_map.Document.InvokeScript("messageBox", objects);
        }

        // 提供给JavaScript调用的方法
        public void JsMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        private void startCheckNodeTimer()
        {
            isStopNodeTimer = false;

            node_timer = new System.Timers.Timer();
            node_timer.Interval = 1000 * 60;
            node_timer.AutoReset = true;
            node_timer.Enabled = true;
            node_timer.Elapsed += trans_timer_Elapsed;

            log.Info("start checkNodeStateAll timer");
            isRunningTransTimer = false;
            node_timer.Start();
        }
        void trans_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (isRunningTransTimer) return;

            isInDeviceOpr = false;
            isRunningTransTimer = true;
            checkNodeStateAll();
            isRunningTransTimer = false;
        }
        private void checkNodeStateAll(){
            try
            {
                log.Info("checkNodeStateAll nodes count=" + this.tree_devices.Nodes.Count);
                foreach (TreeNode node in this.tree_devices.Nodes)
                {
                    foreach (TreeNode cnode in node.Nodes)
                    {
                        if (isStopNodeTimer) break;

                        checkNodeState(cnode);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }
        private void sendTransportData(DDevice dev)
        {
            int ret = 0;
            try
            {
                int nBufLen = 6;
                byte[] data = new byte[] { 0xFF, 0x55, 0x01, 0x00, 0x01 };

                IntPtr hglobal = System.Runtime.InteropServices.Marshal.AllocHGlobal(6);
                System.Runtime.InteropServices.Marshal.Copy(data, 0, hglobal, data.Length);

                ret = TransSDK.P_Client_WriteTransportData(dev.Id, 0, hglobal, nBufLen);
                log.Error("write transportdata dev=" + dev.Id+",ret="+ret);

                System.Runtime.InteropServices.Marshal.FreeHGlobal(hglobal);

                data = null;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }
        private void checkNodeState(TreeNode node)
        {
            if (isStopNodeTimer) return;
            log.Info("check transport node=" + node.Text);

            if(node.Tag is DDepartment){
                DDepartment dd = (DDepartment)node.Tag;

                //批量获取设备状态
                string deviceStr = "";
                List<Device> ds = null;
                foreach (DDevice dev in dd.Devices)
                {
                    deviceStr += "'" + dev.Id + "',";
                }
                if (deviceStr.Length > 0) deviceStr = deviceStr.Substring(0, deviceStr.Length - 1);
                if (deviceStr.Length > 0) ds = MDSUtils.GetDevices(deviceStr);
                foreach (TreeNode cnode in node.Nodes)
                {
                    if (cnode.Tag is DDepartment) checkNodeState(cnode);

                    DDevice dev = (DDevice)cnode.Tag;
                    Device d = null;
                    if (ds != null && ds.Count > 0)
                    {
                        foreach (Device v in ds)
                        {
                            if (dev.Id.Equals(v.DeviceID))
                            {
                                d = v;
                                break;
                            }
                        }
                    }

                    cnode.ImageIndex = 6;
                    if (d != null)
                    {
                        if (d.Status == 1)//online
                            cnode.ImageIndex = 4;
                        else
                            cnode.ImageIndex = 5;
                    }
                }
            } 
            else if (node.Tag is DDevice)
            {
                DDevice dev = (DDevice)node.Tag;
                log.Info("check transport device=" + dev.Id);
                Device d = MDSUtils.GetDeviceInfo(dev.Id);
                curCheckNode = node;
                curCheckDevice = d;
                node.ImageIndex = 6;

                if (d != null)//&& d.Longitude != 0 && d.Latitude != 0
                {
                    //node.ImageIndex = 1;
                    if (d.Status == 1)//online
                        node.ImageIndex = 4;
                    else
                        node.ImageIndex = 5;
                }
                //else
                //{
                //    node.ImageIndex = 2;
                //    if (d != null && d.Status == 1)
                //        node.SelectedImageIndex = 4;
                //    else if (d != null && d.Status == 2)
                //        node.SelectedImageIndex = 5;
                //}

                //获取布防信息
                //sendTransportData((DDevice)node.Tag);

                //改用NETSDK获取透明串口数据
                //登录
                /*
                isInDeviceOpr = true;
                df = DeviceF.Instance;
                NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
                isLogin = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), (ushort)d.Port, d.UserName, d.PassWord, ref lpDeviceInfo);
                if (isLogin)//在线
                {
                    searchFqStatus(d.DeviceID);
                    //等待5s
                    System.Threading.Thread.Sleep(1000*5);

                    df.LogOut();
                    stopTransportRs232();
                }
                 * */
                isInDeviceOpr = false;
            }
            else if (node.Tag is DChannel){
            }
        }
        private bool searchFqStatus(int lhwd,string deviceId)
        {
            log.Error("查询单片机状态：" + deviceId);
            //if (!openTransportRs232_NETSDK(lhwd,deviceId)) return false;

            int nBufLen = 5;

            byte[] data = new byte[] { 0xFF, 0x55, 0x01, 0x00, 0x01 };

            bool ret = NetDevice.SendTransportData(lhwd,data, nBufLen);

            data = null;

            return ret;
        }
        unsafe private bool openTransportRs232_NETSDK(int lhwd,string szDeviceID)
        {
            //fCallBack = new NetSDK.fTransComCallBack(this.netSdkTransportDataCallBack);

            NetSDK.TransComChannel transComChannel = new NetSDK.TransComChannel();
            transComChannel.TransComType = NetSDK.SERIAL_TYPE.RS232;
            transComChannel.baudrate = 38400;
            transComChannel.databits = 8;
            transComChannel.parity = 0;
            transComChannel.stopbits = 1;

            IntPtr ptransComChannel = Marshal.AllocCoTaskMem(Marshal.SizeOf(transComChannel));
            Marshal.StructureToPtr(transComChannel, ptransComChannel, false);
            bool ret = false;
            int tryNum = 0;
            uint udata = (uint)searchDevInd(szDeviceID);
            //-11405
            while (!ret && tryNum < 5)
            {
                log.Error("H264_DVR_OpenTransComChannel openning...");
                
                ret = NetSDK.H264_DVR_OpenTransComChannel(lhwd, ptransComChannel, fCallBack, udata);//fCallBack
                if (!ret)
                {
                    log.Error("H264_DVR_OpenTransComChannel " + szDeviceID + " error=" + NetSDK.H264_DVR_GetLastError());
                    tryNum++;
                    System.Threading.Thread.Sleep(1000*3);
                }
                else
                    log.Error("H264_DVR_OpenTransComChannel ok");
            }

            Marshal.FreeCoTaskMem(ptransComChannel);

            return ret;
        }
        private bool stopTransportRs232(int lhwd)
        {
            return NetSDK.H264_DVR_CloseTransComChannel(lhwd, NetSDK.SERIAL_TYPE.RS232);
        }
        private int searchDevInd(string id)
        {
            int ind = -1;
            foreach (string devid in allDevices)
            {
                ind++;
                if (devid.Equals(id))
                {
                    break;
                }
            }
            return ind;
        }
        private string searchDevStr(UInt32 id)
        {
            string dev = "";
            int ind = -1;
            foreach (string devid in allDevices)
            {
                ind++;
                if (ind==id)
                {
                    dev = devid;
                    break;
                }
            }
            return dev;
        }
        
        private void netSdkTransportDataCallBack(Int32 lLoginID, int lTransComType, IntPtr pBuffer, UInt32 dwBufSize, UInt32 dwUser)
        {
            log.Error("netSdkTransportDataCallBack len="+dwBufSize);
            byte[] data = new byte[dwBufSize + 1];
            try
            {
                Marshal.Copy(pBuffer, data, 0, (int)dwBufSize);
                //for (int i = 0; i < dwBufSize; i++)
                //{
                //    log.Error("data[" + i + "]=" + data[i]);
                //}
                //解析数据
                int command = data[2];
                int datalen = data[3];

                string devStr = searchDevStr(dwUser);
                log.Error("NETSDK透明串口消息 command=" + command + ",dev=" + devStr);

                int wireChannelId;
                int wirelessChannelId;

                switch (command)
                {
                    case 0X82://0X02 设置布防 撤防 回复
                    case 0X81://0X01 读取系统状态 回复
                        {

                            int sysStatus = data[3 + 1];
                            if (sysStatus == 0X01)//布防
                            {
                                //setNodeIcon(this.curCheckNode, curCheckDevice.DeviceID, 41);
                                //curCheckNode.ImageIndex = 9;
                                setNodeIcon(this.treeRootNode, devStr, 41);
                                //log.Error(curCheckDevice.DeviceID + "-" + curCheckNode.Text + ":布防");
                            }
                            else
                            {
                                setNodeIcon(this.treeRootNode, devStr, 42);
                                //curCheckNode.ImageIndex = 8;
                                //log.Error(curCheckDevice.DeviceID + "-" + curCheckNode.Text + ":撤防");
                            }
                            break;
                        }
                    case 0X83://0X03 设置报警声开启 关闭 回复
                        {
                            break;
                        }
                    case 0X84://0X04 设置有线防区有效电平高 低 回复
                        {
                            break;
                        }
                    case 0X85://0X05 设置4路有线报警无效 有效 回复
                        {
                            break;
                        }
                    case 0X86://0X06 设置16路无线防区报警无效 有效 回复 //0X07 继电器控制
                        {
                            break;
                        }
                    case 0X40://遥控手动报警
                        {
                            break;
                        }
                    case 0X41://遥控布防
                        {
                            //全部布防65
                            setNodeIcon(this.treeRootNode, devStr, 41);

                            SectorLog sectorLog = new SectorLog();
                            string strHostName = Dns.GetHostName();  //得到本机的主机名
                            IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                            string strAddr = ipEntry.AddressList[0].ToString(); //假设本地主机为单网卡
                            sectorLog.ip = strAddr;
                            sectorLog.oprTime = DateTime.Now;
                            sectorLog.port = 0;
                            sectorLog.sn = devStr;
                            sectorLog.usr = "";
                            sectorLog.remark = "遥控布防";
                            MDSUtils.SaveSectorLog(sectorLog);
                            saveBcfLocalLog(sectorLog, devStr);
                            break;
                        }
                    case 0X42://遥控撤防
                        {
                            //全部撤防66
                            setNodeIcon(this.treeRootNode, devStr, 42);

                            SectorLog sectorLog = new SectorLog();
                            string strHostName = Dns.GetHostName();  //得到本机的主机名
                            IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                            string strAddr = ipEntry.AddressList[0].ToString(); //假设本地主机为单网卡
                            sectorLog.ip = strAddr;
                            sectorLog.oprTime = DateTime.Now;
                            sectorLog.port = 0;
                            sectorLog.sn = devStr;
                            sectorLog.usr = "";
                            sectorLog.remark = "遥控撤防";
                            MDSUtils.SaveSectorLog(sectorLog);
                            saveBcfLocalLog(sectorLog,devStr);
                            break;
                        }
                    case 0X43://有线线防区报警
                        {
                            //wireChannelId = data[3 + 1];
                            wireChannelId = data[3 + 1] - 1;
                            saveMsgLocalDb(devStr, wireChannelId, "有线防区");
                            break;
                        }
                    case 0X44://无线防区报警
                        {
                            wirelessChannelId = data[3 + 1];
                            saveMsgLocalDb(devStr, wirelessChannelId, "无线防区");//绑定到4路通道上
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }

            data = null;

            log.Error("netSdkTransportDataCallBack over");
        }
        
        private void loadDepartTree(TreeNode pnode, DDepartment dd,string name)
        {
            foreach (DDepartment depart in dd.Departs)
            {
                if ((depart.Departs != null && depart.Departs.Count > 0) ||
                    depart.Devices != null && depart.Devices.Count > 0)
                {
                    TreeNode node = new TreeNode(depart.Name);
                    node.Tag = depart;
                    pnode.Nodes.Add(node);

                    loadDepartTree(node, depart, name);
                }
            }

            foreach (DDevice dev in dd.Devices)
            {
                if (name != null && dev.Title.IndexOf(name) ==-1) continue;

                TreeNode node = new TreeNode(dev.Title);
                node.Tag = dev;
                node.SelectedImageIndex = 5;
                if (this.onlineDevices.Contains(dev.Id))
                    node.ImageIndex = 4;
                else
                    node.ImageIndex = 5;
                pnode.Nodes.Add(node);
                //添加到设备列表中
                if (!allDevices.Contains(dev.Id))
                    allDevices.Add(dev.Id);

                //登录设备取串口数据

                foreach (DChannel ch in dev.Channels)
                {
                    TreeNode cnode = new TreeNode(ch.Title);
                    cnode.Tag = ch;
                    cnode.ImageIndex = 3;
                    node.Nodes.Add(cnode);
                }
            }
        }

        public void FreshOnlineDevStatus(List<string> onlineDevices,bool refresh)
        {
            isloadbcf = true;
            if (!refresh)
                this.onlineDevices = onlineDevices;
            else
            {
                //打开透明串口
                //暂时不走转发，转发接收不到串口数据
                if (this.isListenCOM)
                {
                    //Thread t2 = new Thread(new ThreadStart(this.openAllTransport));
                    //t2.Start();
                }
            }

            foreach (TreeNode node in this.tree_devices.Nodes)
            {
                foreach (TreeNode cnode in node.Nodes)
                {
                    //log.Error("FreshNode-" + cnode.Text);
                    FreshNode(cnode, refresh);
                }
            }
            isloadbcf = false;
        }
        private void FreshNode(TreeNode node, bool refresh)
        {
            //log.Error(node.Tag+"-"+node.Text);
           
            if (node.Tag is DDepartment)
            {
                foreach (TreeNode cnode in node.Nodes)
                {
                    //log.Error( "cnode-"+cnode.Tag+"-" + cnode.Text);
                    if (cnode.Tag is DDepartment) 
                        FreshNode(cnode,refresh);
                    else if (cnode.Tag is DDevice)
                    {
                        DDevice dev = (DDevice)cnode.Tag;
                        //log.Error("cnode-deviceid-" + dev.Id);
                        if (this.onlineDevices.Contains(dev.Id))
                        {
                           cnode.ImageIndex = 4;
                           //登录设备取串口数据
                           if (refresh)
                           {
                               //log.Error("cnode-refresh-" + cnode.Text);
                               //可能超时,此时会停止在这里
                               Device dev2 = MDSUtils.GetDeviceInfo(dev.Id);
                               SearchTransStatus(dev2);
                           }
                        }
                    }

                }
            }
            else if (node.Tag is DDevice)
            {
                DDevice dev = (DDevice)node.Tag;
                if (this.onlineDevices.Contains(dev.Id))
                {
                    node.ImageIndex = 4;
                    //登录设备取串口数据
                    if (refresh)
                    {
                        Device dev2 = MDSUtils.GetDeviceInfo(dev.Id);
                        SearchTransStatus(dev2);
                    }
                }
            }
            else 
            {
            }
        }
        private SnHwd SearchSnHwd(string sn)
        {
            SnHwd ret = null;
            foreach(SnHwd sh in this.loginDevices){
                if (sh.Sn.Equals(sn))
                {
                    ret = sh;
                    break;
                }
            }
            return ret;
        }
        public void SearchTransStatus(Device dev2)
        {
            SearchTransStatus(dev2,false);
        }
        public void SearchTransStatus(Device dev2, bool relogin)
        {
            log.Error("SearchTransStatus" + dev2.DeviceName);
            //查询设备串口数据
            SnHwd sh = SearchSnHwd(dev2.SerialNum);
            string szDeviceID = dev2.DeviceID; 
            if (sh == null)
            {
                //没有打开过透明串口
                log.Error("透明串口打开:"+szDeviceID);
                int ret = openTransportRs232(szDeviceID);
                if (ret >= 0)
                {
                    sh = new SnHwd(dev2.SerialNum, 1);
                    loginDevices.Add(sh);
                }
            }

            if (sh != null)
            {
                //发送单片机指令查询状态
                int ret = searchDevStatus(szDeviceID);
                if (ret >= 0)
                {
                    log.Error("透明串口发送状态查询数据成功:" + szDeviceID);
                }
            }
        }
        public void SearchTransStatus_NETSDK(Device dev2,bool relogin)
        {
            log.Error("SearchTransStatus"+dev2.DeviceName);
            //登录设备
            NetDevice nd = new NetDevice();
            
            int lhwd = 0;
            int tryNum=0;
            SnHwd sh = SearchSnHwd(dev2.SerialNum);
            if (sh != null)
            {
                lhwd = sh.Lhwd;
                log.Error("取到历史登录信息:" + dev2.SerialNum +"-->"+ lhwd);
                if (relogin) lhwd = 0;//重置
            }
            while(lhwd<=0&&tryNum<5){
                NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
                lhwd = nd.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), 9500, dev2.SerialNum, "", ref lpDeviceInfo);
                if (lhwd <= 0)
                {
                    tryNum++;
                    System.Threading.Thread.Sleep(1000 * 3);
                }
                else
                {
                    if (sh != null) loginDevices.Remove(sh);
                    SnHwd snHwd = new SnHwd(dev2.SerialNum,lhwd);
                    loginDevices.Add(snHwd);

                    openTransportRs232_NETSDK(lhwd, dev2.DeviceID);
                }
            }
            if (lhwd>0) 
            {
                //先停用原来的转发的透传
                //int ret = TransSDK.P_Client_CloseTransport(dev2.DeviceID, 0);
                //if (ret < 0)
                //{
                //    log.Error("TransSDK.P_Client_CloseTransport error="+ret);
                //}

                //暂时不取单片机状态
                
                if (searchFqStatus(lhwd, dev2.DeviceID))
                {
                    log.Error("串口消息写成功:" + dev2.DeviceName);
                    System.Threading.Thread.Sleep(1000);
                }
                else
                    log.Error("串口消息写异常" + dev2.DeviceName);
                 
                //stopTransportRs232(lhwd);

                //重新开启转发的透传
                //if(ret==0) openTransportRs232(dev2.DeviceID);
                //log.Error("SearchTransStatus openTransportRs232 over:" + dev2.DeviceName);

                //退出,刷新时会退不出来?
                //nd.LogOut(lhwd);
                //log.Error("SearchTransStatus logout:" + dev2.DeviceName);
            }
        }

        private void ToolStripMenuItem_Play_Click(object sender, EventArgs e)
        {
            TreeNode node = tree_devices.SelectedNode;
            if (node.Tag is DChannel)
            {
                DChannel ch = (DChannel)node.Tag;
                DDevice dev = (DDevice)node.Parent.Tag;

                FrmPlayer player = new FrmPlayer(this,dev.Id, ch.Id,ch.Num-1,this.oprname);
                player.StartPosition = FormStartPosition.CenterParent;
                player.ShowDialog();
            }
        }
        /// <summary>
        /// 显示一级地图标识点位置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_MapShow_Click(object sender, EventArgs e)
        {
            TreeNode node = tree_devices.SelectedNode;
            if (node.Tag == null)
            {
                //显示所有布防点ShowPointNoOpen
                webBrowser_map.Document.InvokeScript("ClearMap", null);
                TreeNodeCollection nodes = tree_devices.Nodes;
                TreeShowPoint(nodes);
            }
            else if (node.Tag is DDevice)
            {
                NodeShowPoint(node, "ShowPoint");
            }
        }
        private void TreeShowPoint(TreeNodeCollection nodes)
        {
                if (nodes != null&&nodes.Count>0)
                {
                    foreach (TreeNode curNode in nodes)
                    {
                        if (curNode.Tag is DDevice)
                            NodeShowPoint(curNode, "ShowPointNoOpen");
                        else
                        {
                            TreeShowPoint(curNode.Nodes);
                        }
                    }
                }
        }
        private void NodeShowPoint(TreeNode node,String fun)
        {
            DDevice dev = (DDevice)node.Tag;
            string deviceId = dev.Id;
            //判断是域名或IP
            IPAddress ip;
            if (!IPAddress.TryParse(dev.Ip, out ip))
            {
                IPAddress[] ips = Dns.GetHostAddresses(dev.Ip);
                ip = ips[0];
            }
            //根据id获取相关信息
            Device c = MDSUtils.GetDeviceInfo(deviceId);
            object[] param = new object[8];
            param[0] = deviceId;
            param[1] = dev.Title;
            param[2] = c.Latitude;
            param[3] = c.Longitude;
            param[4] = ip.ToString();
            param[5] = c.Address;
            param[6] = c.Tel;
            param[7] = c.TrueName;

            webBrowser_map.Document.InvokeScript(fun, param);
        }
        /// <summary>
        /// 设置地图一级地图位置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_MapDWOne_Click(object sender, EventArgs e)
        {
            TreeNode node = tree_devices.SelectedNode;
            if (node.Tag is DDevice)
            {
                DDevice dev = (DDevice)node.Tag;
                string deviceId = dev.Id;
                //判断是域名或IP
                IPAddress ip;
                if (!IPAddress.TryParse(dev.Ip, out ip))
                {
                    try
                    {
                        IPAddress[] ips = Dns.GetHostAddresses(dev.Ip);
                        ip = ips[0];
                    }
                    catch (Exception ee)
                    {
                        
                    }
                }
                //根据id获取相关信息
                Device c = MDSUtils.GetDeviceInfo(deviceId);
                object[] param = new object[6];
                param[0] = deviceId;
                param[1] = ip!=null?ip.ToString():"";
                param[2] = dev.Title;
                param[3] = c.Address;
                param[4] = c.Tel;
                param[5] = c.TrueName;


                //先根据ip大概定位后,选择坐标并回传JsSelectPoint
                webBrowser_map.Document.InvokeScript("SelectPoint", param);
            }
        }
        public void JsSelectPoint(string deviceId, float latitude, float longitude)
        {
            //this.txt_gps.Text = "经度：" + latitude + ",纬度：" + longitude;
            //选择当前设备坐标,保存入库
            bool ret = MDSUtils.UpdateMapOneLL(deviceId, latitude, longitude);
            if (ret)
            {
                MessageBox.Show("设备(" + deviceId + ")坐标设置成功！", "设备设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //更新节点状态
                FreshDeviceNode(this.tree_devices.Nodes,deviceId);
            }
            else
                MessageBox.Show("设备(" + deviceId + ")坐标设置失败！", "设备设置", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void FreshDeviceNode(TreeNodeCollection nodes,string deviceId)
        {
            if (nodes != null && nodes.Count > 0)
            {
                foreach (TreeNode curNode in nodes)
                {
                    if (curNode.Tag is DDevice){
                        if (((DDevice)curNode.Tag).Id.Equals(deviceId))
                        {
                            curNode.ImageIndex = 4;
                            curNode.SelectedImageIndex = 4;
                            break;
                        }
                    }
                    else
                    {
                        FreshDeviceNode(curNode.Nodes,deviceId);
                    }
                }
            }
        }

        public void FreshDeviceStatus(string deviceId,bool isonline){
            FreshDeviceStatus(this.tree_devices.Nodes,deviceId,isonline);
        }
        private void FreshDeviceStatus(TreeNodeCollection nodes,string deviceId,bool isonline)
        {
            foreach (TreeNode curNode in nodes)
            {
                if (curNode.Tag is DDevice)
                {
                    if (((DDevice)curNode.Tag).Id.Equals(deviceId))
                    {
                        if (isonline)
                        {
                            //上线
                            curNode.ImageIndex = 4;
                        }
                        else
                        {
                            //下线
                            curNode.ImageIndex = 5;
                        }
                        break;
                    }
                }
                else
                {
                    FreshDeviceStatus(curNode.Nodes, deviceId, isonline);
                }
            }
        }
        public void JsOpenSubMap(string deviceId, string channelId, int nchannelId, string mappic, float latitude, float longitude, string channelName, string address, string tel, string username)
        {
            //打开二级地图
            //if (mappic != null && latitude!= 0 && longitude!=0)
            //{
                //frmSubMapShow = new FrmSubMapShow(this,mappic, latitude, longitude, channelName, address, tel, username, this.MapPicList);
                //frmSubMapShow.ShowDialog();
                ////后续寻找同一个mappic的坐标点
                //this.mappic = mappic;

                FrmPlayer player = new FrmPlayer(this,deviceId, channelId,nchannelId,this.oprname);
                player.log = curlog;
                player.StartPosition = FormStartPosition.CenterParent;
                player.ShowDialog();
            //}
        }
        /// <summary>
        /// 设置通道的二级地图信息
        /// </summary>
        public void JsSetSubMap(string deviceId)
        {
            //根据id获取相关信息
            Device dev = MDSUtils.GetDeviceInfo(deviceId);
            if (dev != null)
            {
                //设置二级地图,如果已上传图片则可二次上传或设置坐标
                bool isUploaded = (dev.MapPic != null);
                string mapUrl = isUploaded?dev.MapPic.Substring(0, dev.MapPic.LastIndexOf("/")):null;
                float latitude = 0;
                float longitude = 0;
                FrmSecMap frmSecMap = new FrmSecMap(deviceId, username, passwd, isUploaded, mapUrl, latitude, longitude, dev.DeviceName, dev.TrueName, dev.Address, dev.Tel);
                frmSecMap.StartPosition = FormStartPosition.CenterParent;
                frmSecMap.ShowDialog();
            }
        }

        /// <summary>
        /// 配置设备用户相关信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_UserInfo_Click(object sender, EventArgs e)
        {
            TreeNode node = tree_devices.SelectedNode;
            if (node.Tag is DDevice)
            {
                DDevice dev = (DDevice)node.Tag;
                string deviceId = dev.Id;
                string deviceName = dev.Title;
                //配置设备的用户信息
                FrmDeviceInfo frmChannelInfo = new FrmDeviceInfo(deviceId, deviceName);
                frmChannelInfo.StartPosition = FormStartPosition.CenterParent;
                frmChannelInfo.ShowDialog();
            }
        }

        public void BindLogs()
        {
            //取DB数据,绑定到日志dt上显示
            List<MDS.Domain.AlarmLog> logs = AlarmLogBll.QueryLogs();
            this.dgv_alarmlog.DataSource = null;
            this.dgv_alarmlog.DataSource = logs;
            //if (logs.Count > 0)
            //{ //有日志
            //    MDS.Domain.AlarmLog al = logs[0];
            //    FrmCodePlayer codePlayer = new FrmCodePlayer(this, al.DeviceId, al.Channel, this.oprname);
            //    codePlayer.ShowDialog();
            //}
        }

        //报警显示
        public void CheckAlarmMsg()
        {
            isAlarmExe = true;

            //检查有没有没有读的告警消息，并显示在日志栏和地图上
            AlarmData shareData = AlarmData.ShareData();
            List<AlarmMsg> msgs = shareData.allMsgs();
            msgs.Sort();
            //取DB数据,绑定到日志dt上显示
            BindLogs();

            //绑定到MAP显示
            webBrowser_map.Document.InvokeScript("ClearMap");
            MapPicList.Clear();
            for (int i = msgs.Count-1; i >=0; i--)
            {
                AlarmMsg msg = msgs[i];
                if (msg.Type == TransSDK.enum_P_Client_AlarmMsg.enum_LOCAL_LOG)
                    continue;

                if (msg.IsDeled)
                {
                    //this.dgv_alarmlog.DataSource = null;
                    //msgs.Remove(msg);
                    //this.dgv_alarmlog.DataSource = msgs;
                }
                else if (!msg.IsAlarmed)
                {
                    object[] param = new object[14];

                    if(i==0) 
                        param[0] = true;
                    else
                        param[0] = false;
                    param[1] = msg.ChannelName;
                    param[2] = msg.UserName;
                    param[3] = msg.Tel;
                    param[4] = msg.Address;
                    param[5] = msg.DeviceId;
                    param[6] = msg.Channel;
                    param[7] = msg.Nchannel;
                    param[8] = msg.Latitude;
                    param[9] = msg.Longitude;
                    param[10] = msg.MapPic;
                    param[11] = msg.Latitude2;
                    param[12] = msg.Longitude2;
                    param[13] = msg.Ip;

                    webBrowser_map.Document.InvokeScript("AlarmMsg", param);

                    if (this.mappic != null && this.mappic.Equals(msg.MapPic))
                        MapPicList.Add(msg);
                    //System.Threading.Thread.Sleep(5000);

                    //需判断当前窗口是否打开中,如果打开则只更新信息
                    if (!msg.IsViewed)
                    {
                        bool noinlist=false;
                        //if (!this.AlarmVideoList.Contains(msg)) AlarmVideoList.Add(msg);
                        /*
                        foreach (AlarmMsg m in this.AlarmVideoList)
                        {
                            if (msg.DeviceId.Equals(m.DeviceId) && msg.Nchannel == m.Nchannel)
                            {
                                m.AlarmTime = msg.AlarmTime;
                                noinlist = true;
                                break;
                            }
                        }*/
                        //如果有打开窗口,则不再打开
                        if (AlarmVideoList.Count > 0) noinlist = true;

                        if (!noinlist) AlarmVideoList.Add(msg);
                        msg.IsViewed = true;
                    }
                }
                else
                {
                    //超过30分钟移除
                    /*
                    DateTime now = DateTime.Now;
                    TimeSpan nt = now - msg.AlarmTime;
                    if (nt.TotalSeconds > 60 * 30)
                    {
                        this.dgv_alarmlog.DataSource = null;
                        msgs.Remove(msg);
                        this.dgv_alarmlog.DataSource = msgs;
                    }
                    */
                }

                msgs.Remove(msg);
            }
            //显示二级地图的所有告警点
            if (MapPicList.Count > 0 && frmSubMapShow != null && frmSubMapShow.isWebLoaded)
                frmSubMapShow.ShowAlarmAllPoint(MapPicList);          

            isAlarmExe = false;
        }

        private void startCheckAlarmVideoTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = checkAlarmVideoTime;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //foreach (AlarmMsg m in this.AlarmVideoList)
            //{
            //    showAlarmVideo(m);
            //}
            try
            {
                if (this.AlarmVideoList.Count > 0)
                {
                    AlarmMsg m = this.AlarmVideoList.Last();
                    showAlarmVideo(m);
                }
            }
            catch(Exception ex){
                log.Error(ex.ToString());
            }
        }

        private void showAlarmVideo(AlarmMsg msg)
        {
            if (!this.isOpenVideoNow)
            {
                this.AlarmVideoList.Remove(msg);
                return;
            }

            if (codePlayer == null || codePlayer.IsDisposed)
            {
                codePlayer = new FrmCodePlayer(this, msg.DeviceId, msg.Nchannel, this.oprname, msg.AlarmTime, msg.alarmTxt == null ? "联动防区" : msg.alarmTxt);
                codePlayer.StartPosition = FormStartPosition.CenterParent;
                codePlayer.ShowDialog();

                this.AlarmVideoList.Remove(msg);
            }
            else
            {
                if (codePlayer.IsSetting)
                {
                    log.Error("codePlayer.IsSetting sleep");
                    return;
                }
                //更换video参数后播放 || !codePlayer.InPlaying(msg.DeviceId, msg.Nchannel)
                if (codePlayer.NewVideo(msg.DeviceId, msg.Nchannel) )
                {
                    //等待用户关闭

                    //二次show会顿住?
                    //codePlayer.StartPosition = FormStartPosition.CenterParent;
                    //codePlayer.SetPara(msg.DeviceId, msg.Nchannel, msg.AlarmTime);
                }
                else//已在播放
                {
                    codePlayer.SetTitle(msg.AlarmTime);
                }

            }
        }

        private void ToolStripMenuItem_ReloadMap_Click(object sender, EventArgs e)
        {
            this.webBrowser_map.Refresh();
        }

        private void ToolStripMenuItem_ShowHideAlarmData_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2Collapsed = !this.splitContainer2.Panel2Collapsed;
        }

        private void ToolStripMenuItem_ShowHideDeviceTree_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2Collapsed = !this.splitContainer1.Panel2Collapsed;
        }

        private void webBrowser_map_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            isWebLoaded = true;

            object[] objects = new object[2];

            objects[0] = ConfigurationManager.AppSettings["City"].ToString();
            objects[1] = 15;

            webBrowser_map.Document.InvokeScript("toCenter", objects);
        }

        public void closeSubMapFrm()
        {
            this.mappic = null;
            this.MapPicList.Clear();
            this.frmSubMapShow = null;
        }

        private bool checkLog()
        {
            if (this.curlog == null)
            {
                MessageBox.Show("请选择告警行后接警！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        private void ToolStripMenuItem_log_dealalarm_Click(object sender, EventArgs e)
        {
            if (!checkLog()) return;

            //显示坐标及用户信息
            object[] param = new object[14];

            param[0] = true;
            param[1] = curlog.ChannelName;
            param[2] = curlog.UserName;
            param[3] = curlog.Tel;
            param[4] = curlog.Address;
            param[5] = curlog.DeviceId;
            param[6] = curlog.Channel;
            param[7] = curlog.Nchannel;
            param[8] = curlog.Latitude;
            param[9] = curlog.Longitude;
            param[10] = curlog.MapPic;
            param[11] = curlog.Latitude2;
            param[12] = curlog.Longitude2;
            param[13] = null;

            webBrowser_map.Document.InvokeScript("AlarmMsg", param);


            //更新db
            FrmPlayer player = new FrmPlayer(this, curlog.DeviceId, curlog.Channel, curlog.Nchannel, this.oprname);
            player.tabind = 2;
            player.log = curlog;
            player.StartPosition = FormStartPosition.CenterParent;
            player.ShowDialog();
            /**
             curlog.IsAlarmed = 1;
             AlarmLogBll.UpdateLog(curlog);
             */
        }

        private void ToolStripMenuItem_log_viewvideo_Click(object sender, EventArgs e)
        {
            if (!checkLog()) return;
            //打开窗口
            FrmPlayer player = new FrmPlayer(this,curlog.DeviceId, curlog.Channel,curlog.Nchannel, this.oprname);
            player.tabind = 1;
            player.log = curlog;
            player.StartPosition = FormStartPosition.CenterParent;
            player.ShowDialog();
        }

        private void ToolStripMenuItem_log_viewuser_Click(object sender, EventArgs e)
        {
            if (!checkLog()) return;
            //打开窗口
            FrmPlayer player = new FrmPlayer(this, curlog.DeviceId, curlog.Channel, curlog.Nchannel, this.oprname);
            player.tabind = 3;
            player.log = curlog;
            player.StartPosition = FormStartPosition.CenterParent;
            player.ShowDialog();
        }

        private void ToolStripMenuItem_log_sms_Click(object sender, EventArgs e)
        {
            if (!checkLog()) return;
            //发送SMS

        }

        private void ToolStripMenuItem_log_search_Click(object sender, EventArgs e)
        {
            if (!checkLog()) return;
            //打开窗口
            FrmLogQuery logquery = new FrmLogQuery();
            logquery.ShowDialog();
        }

        private void tree_devices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //右键选择后，设置右键菜单的状态
            TreeNode node = tree_devices.SelectedNode;
            if (node.Tag == null)
            {
                foreach (ToolStripItem item in this.contextMenuStrip_tree.Items)
                {
                    if (item.Name.Equals("ToolStripMenuItem_MapShow"))
                        item.Enabled = true;
                    else
                        item.Enabled = false;
                }
            }
            else if (node.Tag is DChannel)
            {
                foreach (ToolStripItem item in this.contextMenuStrip_tree.Items)
                {
                    if (item.Name.Equals("ToolStripMenuItem_Play")){
                        item.Enabled = true;
                    }else
                        item.Enabled = false;
                }
            }
            else if(node.Tag is  DDevice){
                foreach (ToolStripItem item in this.contextMenuStrip_tree.Items)
                {
                    if (item.Name.Equals("ToolStripMenuItem_MapDW") || 
                        item.Name.Equals("ToolStripMenuItem_UserInfo") || 
                        item.Name.Equals("ToolStripMenuItem_MapShow") || 
                        item.Name.Equals("toolStripMenuItem_bf_one") || 
                        item.Name.Equals("ToolStripMenuItem_bf_two") || 
                        item.Name.Equals("ToolStripMenuItem_cf") ||
                        item.Name.Equals("exePlan_ToolStripMenuItem"))
                        item.Enabled = true;
                    else
                        item.Enabled = false;
                }
            }
            else if (node.Tag is DDepartment)
            {
                foreach (ToolStripItem item in this.contextMenuStrip_tree.Items)
                {
                    if (item.Name.Equals("ToolStripMenuItem_MapDW") ||
                        item.Name.Equals("ToolStripMenuItem_UserInfo") ||
                        item.Name.Equals("ToolStripMenuItem_MapShow") ||
                        item.Name.Equals("toolStripMenuItem_bf_one") ||
                        item.Name.Equals("ToolStripMenuItem_bf_two") ||
                        item.Name.Equals("ToolStripMenuItem_cf"))
                        item.Enabled = true;
                    else
                        item.Enabled = false;
                }
            }
        }

        private void dgv_alarmlog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<MDS.Domain.AlarmLog> logs = (List<MDS.Domain.AlarmLog>)this.dgv_alarmlog.DataSource;
            if (e.RowIndex < 0 || e.RowIndex >= logs.Count) return;
            this.curlog = logs[e.RowIndex];

            foreach (ToolStripItem item in this.contextMenuStrip_log.Items)
            {
                if (item.Name.Equals("ToolStripMenuItem_log_dealalarm"))
                {
                    if (curlog.IsAlarmed == 1)
                    {
                        item.Enabled = false;
                    }
                    else
                        item.Enabled = true;
                }
                else
                    item.Enabled = true;
            }

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //search
            string name = this.txt_name.Text;
            if (name.Equals("") || name.Equals(" ")) name = null;
            tree_devices.Nodes.Clear();

            TreeNode rootnode = new TreeNode("所有设备");
            rootnode.Tag = org;
            tree_devices.Nodes.Add(rootnode);

            foreach (DDepartment dd in org.Departs)
            {
                loadDepartTree(rootnode, dd,name);
            }
            rootnode.Expand();
        }

        /// <summary>
        /// 一级布防:设备的所有视频通道移动侦测启用,所有报警通道启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_bf_one_Click(object sender, EventArgs e)
        {
            SectorLog sectorLog = new SectorLog();

            TreeNode node = tree_devices.SelectedNode;
            if (node != null && (node.Tag is DDevice))
            {
                DDevice dev = (DDevice)node.Tag;
                Device dev2 = MDSUtils.GetDeviceInfo(dev.Id);

                //登录设备
                DeviceF df = DeviceF.Instance;
                NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
                //bool ret = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), (ushort)dev.Port, dev.User, dev.Password, ref lpDeviceInfo);
                bool ret = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), 9500, dev2.SerialNum, "", ref lpDeviceInfo);

                int bSuccess;
                int errNum = 0;
                if (ret)
                {
                    sectorLog.ip = dev.Ip;
                    sectorLog.usr = dev.User;
                    sectorLog.port = dev.Port;
                    sectorLog.sn = lpDeviceInfo.sSerialNumber;
                    //布防移动侦测
                    foreach (TreeNode chNode in node.Nodes)
                    {
                        DChannel ch = (DChannel)chNode.Tag;
                        //先检测是否为启用状态
                        bSuccess = df.GetMotionCfg((int)ch.Num - 1);
                        if (bSuccess > 0)
                        {
                            if (df.motionCfg.bEnable <= 0)
                            {
                                bSuccess = df.SetMotionCfg((int)ch.Num - 1, 1, 1);
                                if (bSuccess <= 0)
                                {
                                    errNum++;
                                    log.Error("通道移动侦测启用异常：code=" + bSuccess);
                                }
                            }
                        }
                    }

                    //布防报警通道
                    for (int i = 0; i < lpDeviceInfo.byAlarmInPortNum; i++)
                    {
                        bSuccess = df.GetAlarmInputCfg(i);
                        if (bSuccess > 0)
                        {
                            if (df.alarmInputCfg.bEnable <= 0)
                            {
                                bSuccess = df.SetAlarmInputCfg(i, 1, 1);
                                if (bSuccess <= 0)
                                {
                                    errNum++;
                                    log.Error("报警通道启用异常：code=" + bSuccess);
                                }
                            }
                        }
                    }

                    //单片机报警启用
                    int nBufLen = 6;
                    byte[] data = new byte[] { 0xFF, 0x55, 0x02, 0x01, 0x01, 0x04 };
                    if (df.SendTransportData(data, nBufLen))
                    {
                        //切换icon
                        node.ImageIndex = 9;
                    }

                    df.LogOut();
                    if (errNum > 0)
                    {
                        MessageBox.Show("设备的所有视频通道移动侦测启用,所有报警通道启用,部分异常!", "一级布防", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //sector log
                        sectorLog.remark = "一级布防:设备的所有视频通道移动侦测启用,所有报警通道启用,部分异常!";
                    }
                    else
                    {
                        MessageBox.Show("设备的所有视频通道移动侦测启用,所有报警通道启用,报警器启用", "一级布防", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //sector log
                        sectorLog.remark = "一级布防:设备的所有视频通道移动侦测启用,所有报警通道启用,报警器启用";
                    }
                }
                else
                {
                    MessageBox.Show("设备登录异常", "一级布防", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //sector log
                    sectorLog.remark = "一级布防:设备登录异常";
                }

                MDSUtils.SaveSectorLog(sectorLog);

               saveBcfLocalLog(sectorLog, dev2.DeviceID);
            }
        }
        /// <summary>
        /// 二级布防:设备的所有视频通道移动侦测不启用,所有报警通道启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_bf_two_Click(object sender, EventArgs e)
        {
            SectorLog sectorLog = new SectorLog();

            TreeNode node = tree_devices.SelectedNode;
            if (node != null && (node.Tag is DDevice))
            {
                DDevice dev = (DDevice)node.Tag;
                Device dev2 = MDSUtils.GetDeviceInfo(dev.Id);

                //登录设备
                DeviceF df = DeviceF.Instance;
                NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
                //bool ret = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), (ushort)dev.Port, dev.User, dev.Password, ref lpDeviceInfo);
                bool ret = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), 9500, dev2.SerialNum, "", ref lpDeviceInfo);

                int bSuccess;
                int errNum = 0;
                if (ret)
                {
                    sectorLog.ip = dev.Ip;
                    sectorLog.usr = dev.User;
                    sectorLog.port = dev.Port;
                    sectorLog.sn = lpDeviceInfo.sSerialNumber;
                    //布防移动侦测
                    foreach (TreeNode chNode in node.Nodes)
                    {
                        DChannel ch = (DChannel)chNode.Tag;

                        //先检测是否为不启用状态
                        bSuccess = df.GetMotionCfg((int)ch.Num - 1);
                        if (bSuccess > 0)
                        {
                            if (df.motionCfg.bEnable > 0)
                            {
                                bSuccess = df.SetMotionCfg((int)ch.Num - 1, 0, 1);
                                if (bSuccess <= 0)
                                {
                                    errNum++;
                                    log.Error("通道移动侦测不启用异常：code=" + bSuccess);
                                }
                            }
                        }
                    }

                    //布防报警通道
                    for (int i = 0; i < lpDeviceInfo.byAlarmInPortNum; i++)
                    {
                        bSuccess = df.GetAlarmInputCfg(i);
                        if (bSuccess > 0)
                        {
                            if (df.alarmInputCfg.bEnable <= 0)
                            {
                                bSuccess = df.SetAlarmInputCfg(i, 1, 1);
                                if (bSuccess <= 0)
                                {
                                    errNum++;
                                    log.Error("报警通道启用异常：code=" + bSuccess);
                                }
                            }
                        }
                    }

                    //单片机报警启用
                    int nBufLen = 6;
                    byte[] data = new byte[] { 0xFF, 0x55, 0x02, 0x01, 0x01, 0x04 };
                    if (df.SendTransportData(data, nBufLen))
                    {
                        //切换icon
                        node.ImageIndex = 9;
                    }

                    df.LogOut();
                    if (errNum > 0)
                    {
                        MessageBox.Show("设备的所有视频通道移动侦测不启用,所有报警通道启用,部分异常!", "二级布防", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //sector log
                        sectorLog.remark = "二级布防:设备的所有视频通道移动侦测不启用,所有报警通道启用,部分异常!";
                    }
                    else
                    {
                        MessageBox.Show("设备的所有视频通道移动侦测不启用,所有报警通道启用,报警器启用", "二级布防", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //sector log
                        sectorLog.remark = "二级布防:设备的所有视频通道移动侦测不启用,所有报警通道启用,报警器启用";
                    }
                }
                else
                {
                    MessageBox.Show("设备登录异常", "二级布防", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //sector log
                    sectorLog.remark = "二级布防:设备登录异常";
                }

                MDSUtils.SaveSectorLog(sectorLog);
                saveBcfLocalLog(sectorLog, dev2.DeviceID);
            }
        }
        /// <summary>
        ///  撤防:所有视频通道的移动侦测，报警输入通道关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_cf_Click(object sender, EventArgs e)
        {
            SectorLog sectorLog = new SectorLog();

            TreeNode node = tree_devices.SelectedNode;
            if (node != null && (node.Tag is DDevice))
            {
                DDevice dev = (DDevice)node.Tag;
                Device  dev2 = MDSUtils.GetDeviceInfo(dev.Id);

                //登录设备
                DeviceF df = DeviceF.Instance;
                NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
                //bool ret = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), (ushort)dev.Port, dev.User, dev.Password, ref lpDeviceInfo);
                bool ret = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), 9500, dev2.SerialNum, "", ref lpDeviceInfo);

                int bSuccess;
                int errNum=0;
                if (ret)
                {
                    sectorLog.ip = dev.Ip;
                    sectorLog.usr = dev.User;
                    sectorLog.port = dev.Port;
                    sectorLog.sn = lpDeviceInfo.sSerialNumber;
                    //布防移动侦测
                    foreach (TreeNode chNode in node.Nodes)
                    {
                        DChannel ch = (DChannel)chNode.Tag;
                        //先检测是否为不启用状态
                        bSuccess = df.GetMotionCfg((int)ch.Num - 1);
                        if (bSuccess > 0)
                        {
                            if (df.motionCfg.bEnable > 0)
                            {
                                bSuccess = df.SetMotionCfg((int)ch.Num - 1, 0, 1);
                                if (bSuccess <= 0)
                                {
                                    errNum++;
                                    log.Error("通道移动侦测撤防异常：code=" + bSuccess);
                                }
                            }
                        }
                        else
                        {
                            errNum++;
                            break;
                        }
                    }

                    //布防报警通道,暂时不关闭
                    /*
                    for (int i = 0; i < lpDeviceInfo.byAlarmInPortNum; i++)
                    {
                        bSuccess = df.GetAlarmInputCfg(i);
                        if (bSuccess > 0)
                        {
                            if (df.alarmInputCfg.bEnable > 0)
                            {
                                bSuccess = df.SetAlarmInputCfg(i, 0, 1);
                                if (bSuccess <= 0)
                                {
                                    errNum++;
                                    log.Error("报警通道撤防异常：code=" + bSuccess);
                                }
                            }
                        }
                        else
                        {
                            errNum++;
                            break;
                        }
                    }
                    */
                    //单片机报警停用
                    int nBufLen = 6;
                    byte[] data = new byte[] { 0xFF, 0x55, 0x02, 0x01, 0x00, 0x03 };
                    if (df.SendTransportData(data, nBufLen))
                    {
                        //切换icon
                        node.ImageIndex = 8;
                    }

                    df.LogOut();

                    if (errNum > 0)
                    {
                        MessageBox.Show("所有视频通道的移动侦测,报警输入通道关闭,部分异常!", "撤防", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //sector log
                        sectorLog.remark = "撤防:所有视频通道的移动侦测,报警输入通道关闭,部分异常!";
                    }
                    else
                    {
                        MessageBox.Show("所有视频通道的移动侦测,报警输入通道关闭,报警器撤防", "撤防", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //sector log
                        sectorLog.remark = "撤防:所有视频通道的移动侦测,报警输入通道关闭,报警器撤防";
                    }
                }
                else
                {
                    MessageBox.Show("设备登录异常", "撤防", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //sector log
                    sectorLog.remark = "撤防:设备登录异常";
                }

                MDSUtils.SaveSectorLog(sectorLog);
                saveBcfLocalLog(sectorLog, dev2.DeviceID);
            }
        }

        private void ToolStripMenuItem_RefreshDev_Click(object sender, EventArgs e)
        {
            int dlen = TransSDK.P_Client_GetDevicesLen();
            if (dlen > 0)
            {
                byte[] pBuffer = new byte[dlen + 1];
                int rlen = TransSDK.P_Client_GetDevices(pBuffer, dlen + 1);
                string xml = Encoding.Default.GetString(pBuffer);

                tree_devices.Nodes.Clear();
                this.treeRootNode = new TreeNode("所有设备");
                treeRootNode.Tag = null;
                LoadXmlTree(treeRootNode, xml);
                tree_devices.Nodes.Add(treeRootNode);
            }
        }

        private delegate void UpdateUI();
        private void UpdateTreeUI()
        {
            tree_devices.Nodes.Add(treeRootNode);
            cmd.HideOpaqueLayer();

            //通道移动侦测状态查询
            //Thread t = new Thread(new ThreadStart(this.LoadChannelMotionTreeThread));
            //t.Start();
        }

        private void UpdateChannelTreeUI()
        {
            UpdateChannelTreeNodeUI(this.treeRootNode);
        }
        private void UpdateChannelTreeNodeUI(TreeNode rootnode)
        {
            foreach (TreeNode node in rootnode.Nodes)
            {
                if (node.Tag is DDevice)
                {
                    foreach (TreeNode cnode in node.Nodes)
                    {
                        DChannel ch = (DChannel)cnode.Tag;

                        ChannelStatus cs;
                        List<ChannelStatus> q = channelStatusList.Where(c => c.ChannelId.Equals(ch.Id)).ToList();
                        if (q != null && q.Count >= 1)
                        {
                            cs = q[0];

                            if (cs.Status > 0)
                            {
                                if (cs.Enable)
                                {
                                    cnode.ImageIndex = 9;//黄

                                }
                                else
                                {
                                    cnode.ImageIndex = 8;//黑
                                }
                            }
                            else
                            {
                                cnode.ImageIndex = 7;//红
                            }
                        }
                    }
                }
                else
                {
                    UpdateChannelTreeNodeUI(node);
                }
            }
        }
        //需改造,将状态信息保存到list中后更新ui时使用
        private void LoadChannelMotionTree(TreeNode rootnode)
        {
            try
            {
                string confip = ConfigurationManager.AppSettings["VideoServerIP"].ToString();
                DeviceF df = DeviceF.Instance;

                foreach (TreeNode node in rootnode.Nodes)
                {
                    if (node.Tag is DDevice)
                    {
                        DDevice dev = (DDevice)node.Tag;
                        if (dev == null || !(node.ImageIndex==4||node.SelectedImageIndex==4))//offline
                            continue;

                        Device dev2 = MDSUtils.GetDeviceInfo(dev.Id);

                        NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
                        //bool ret = df.Login(confip, (ushort)dev.Port, dev.User == null ? "admin" : dev.User, dev.Password == null ? "" : dev.Password, ref lpDeviceInfo);
                        bool ret = df.Login(ConfigurationManager.AppSettings["VideoServerIP"].ToString(), 9500, dev2.SerialNum, "", ref lpDeviceInfo);
                        if (!ret) continue;

                        foreach (TreeNode cnode in node.Nodes)
                        {
                            DChannel ch = (DChannel)cnode.Tag;
                            int bsuccess = df.GetMotionCfg(ch.Num - 1);
                            
                            ChannelStatus cs = new ChannelStatus();
                            cs.ChannelId = ch.Id;
                            cs.Status = bsuccess;
                            cs.Enable = df.motionCfg.bEnable>0?true:false;

                            channelStatusList.Add(cs); 

                        }
                        df.LogOut();
                    }
                    else
                    {
                        LoadChannelMotionTree(node);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                //MessageBox.Show(e.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadXmlTree(TreeNode rootnode,string xml)
        {
            DateTime time_s, time_e;
            time_s = DateTime.Now;
            try
            {
                GroupXmlParser parser = new GroupXmlParser(xml);
                org = parser.Parse();
                rootnode.Tag = org;

                dgv_alarmlog.AutoGenerateColumns = false;

                foreach (DDepartment dd in org.Departs)
                {
                    loadDepartTree(rootnode, dd, null);
                }
                rootnode.Expand();
                treeloaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally{
                time_e = DateTime.Now;
                TimeSpan t = time_e - time_s;
                log.Error("用时："+t.TotalSeconds);
            }
        }

        private void adduser_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUpdateUser upuser = new FrmUpdateUser();
            upuser.tabind = 1;
            upuser.StartPosition = FormStartPosition.CenterParent;
            upuser.ShowDialog();
        }

        private void changepw_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUpdateUser upuser = new FrmUpdateUser();
            upuser.tabind = 2;
            upuser.StartPosition = FormStartPosition.CenterParent;
            upuser.ShowDialog();
        }

        private void deluser_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUpdateUser upuser = new FrmUpdateUser();
            upuser.tabind = 3;
            upuser.StartPosition = FormStartPosition.CenterParent;
            upuser.ShowDialog();
        }

        //打开所有设备透明串口
        private void openAllTransport()
        {
            cmd.ShowOpaqueLayer(this.tree_devices, 125, true);


            //绑定透明串口回调
            if (isListenCOM) setCallBackOpenTransport();

            openTreeNodeTransport(this.treeRootNode);
            //string szDeviceID = "3301061000130";
            //openTransportRs232(szDeviceID);

            cmd.HideOpaqueLayer();
        }
        private void closeAllTransport()
        {
            foreach (string szDeviceID in transportDeviceList)
            {
                log.Error(szDeviceID+" close");
                TransSDK.P_Client_CloseTransport(szDeviceID, 0);
            }
            transportDeviceList.Clear();
        }
        public void closeTransport(string szDeviceID)
        {
            TransSDK.P_Client_CloseTransport(szDeviceID, 0);
        }
        private void openTreeNodeTransport(TreeNode rootnode)
        {
            foreach (TreeNode node in rootnode.Nodes)
            {
                if (node.Tag is DDevice)
                {
                    DDevice dev = (DDevice)node.Tag;
                    if (dev == null || (node.ImageIndex == 5 ))//offline
                        continue;

                    string szDeviceID = dev.Id;
                    //log.Error("透明串口打开:"+szDeviceID);
                    openTransportRs232(szDeviceID);
                }
                else if(node.Tag is DChannel){
                }
                else
                {
                    openTreeNodeTransport(node);
                }
            }
        }
        private int searchDevStatus(string szDeviceID)
        {
            int nBufLen = 6;

            byte[] data = new byte[] { 0xFF, 0x55, 0x01, 0x00, 0x01 };

            GCHandle hObject = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr pObject = hObject.AddrOfPinnedObject();

            int ret= TransSDK.P_Client_WriteTransportData(szDeviceID, 0, pObject, nBufLen);


            //if (hObject.IsAllocated)

            hObject.Free();
            data = null;

            return ret;
        }
        public int openTransportRs232(string szDeviceID)
        {
           int ret=-1;
           try
           {
               int tryNum = 0;
               while (ret < 0 && tryNum<5)
               {
                   ret = TransSDK.P_Client_OpenTransport(szDeviceID, 0, 38400, 8, 1, 0);
                   if (ret < 0)
                   {
                       tryNum++;
                       System.Threading.Thread.Sleep(1000*3);
                   }
               }
               if (ret < 0)
               {
                   log.Error("P_Client_OpenTransport szDeviceID=" + szDeviceID + ",error=" + ret);
                   //设置icon为没用侦听状态,同时可加入右键菜单开启侦听

               }
               else
               {
                   log.Error("P_Client_OpenTransport szDeviceID=" + szDeviceID + " sucess");
                   //转发SDK暂时写动作不成功
                   //ret = searchDevStatus(szDeviceID);
                   //log.Error("P_Client_WriteTransportData szDeviceID=" + szDeviceID + ",error=" + ret);
                   if (!transportDeviceList.Contains(szDeviceID))
                       transportDeviceList.Add(szDeviceID);
               }
           }
            catch(Exception e){
                log.Error(e.ToString());
            }
           return ret;
        }
        private int setCallBackOpenTransport(){
            pfCallBack = new TransSDK.pfTransportDataCallBack(this.transportDataCallBack);
            GC.KeepAlive(pfCallBack);
            return TransSDK.P_Client_SetTransPortCallBack(pfCallBack,0);
        }
        /// <summary>
        /// 透明串口回调
        /// </summary>
        /// <param name="szDeviceID"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="dwUser"></param>
        private void transportDataCallBack(string szDeviceID, IntPtr pBuffer, Int32 dwBufSize, Int32 dwUser){
            //return;
            //判断是否为告警消息
            byte[] data = new byte[dwBufSize + 1];
            try
            {
                
                Marshal.Copy(pBuffer, data, 0, (int)dwBufSize);
                //for (int i = 0; i < dwBufSize; i++)
                //{
                //    log.Error("data[" + i + "]=" + data[i]);
                //}
                //解析数据
                int command = data[2];
                int datalen = data[3];

                int wireChannelId;
                int wirelessChannelId;
                log.Error("透明串口消息 szDeviceID=" + szDeviceID + " command=" + command);
                switch (command)
                {
                    case 0X41://遥控布防
                        {
                            //全部布防
                            setNodeIcon(this.treeRootNode, szDeviceID, 41);

                            SectorLog sectorLog = new SectorLog();
                            string strHostName = Dns.GetHostName();  //得到本机的主机名
                            IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                            string strAddr = ipEntry.AddressList[0].ToString(); //假设本地主机为单网卡
                            sectorLog.ip = strAddr;
                            sectorLog.oprTime = DateTime.Now;
                            sectorLog.port = 0;
                            sectorLog.sn = szDeviceID;
                            sectorLog.usr = "";
                            sectorLog.remark = "遥控布防";
                            MDSUtils.SaveSectorLog(sectorLog);
                            saveBcfLocalLog(sectorLog, szDeviceID);
                            break;
                        }
                    case 0X42://遥控撤防
                        {
                            //全部撤防
                            setNodeIcon(this.treeRootNode, szDeviceID, 42);

                            SectorLog sectorLog = new SectorLog();
                            string strHostName = Dns.GetHostName();  //得到本机的主机名
                            IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                            string strAddr = ipEntry.AddressList[0].ToString(); //假设本地主机为单网卡
                            sectorLog.ip = strAddr;
                            sectorLog.oprTime = DateTime.Now;
                            sectorLog.port = 0;
                            sectorLog.sn = szDeviceID;
                            sectorLog.usr = "";
                            sectorLog.remark = "遥控撤防";
                            MDSUtils.SaveSectorLog(sectorLog);
                            saveBcfLocalLog(sectorLog, szDeviceID);
                            break;
                        }
                    case 0X43://有线线防区报警
                        {
                            wireChannelId = data[3 + 1]-1;
                            saveMsgLocalDb(szDeviceID, wireChannelId, "有线防区");
                            break;
                        }
                    case 0X44://无线防区报警
                        {
                            wirelessChannelId = data[3 + 1];
                            saveMsgLocalDb(szDeviceID, wirelessChannelId, "无线防区");//绑定到4路通道上
                            break;
                        }
                    case 0X82:
                    case 0X81://0X01 读取系统状态 回复
                        {
                            int sysStatus = data[3 + 1];//1 布防 0 撤防
                            if(sysStatus==1)
                                setNodeIcon(this.treeRootNode, szDeviceID, 41);
                            else
                                setNodeIcon(this.treeRootNode, szDeviceID, 42);

                            break;
                        }
                    default: break;
                }
            }
            catch(Exception ex){
                log.Error(ex.ToString());
            }

            data = null;
        }
        private void setNodeIcon(TreeNode rootnode,string szDeviceID, int type)
        {
            foreach (TreeNode node in rootnode.Nodes)
            {
                if (node.Tag is DDevice)
                {
                    DDevice dev = (DDevice)node.Tag;
                    if (dev != null && dev.Id.Equals(szDeviceID))
                    {
                        if (type == 41)//布防
                        {
                            node.ImageIndex = 9;
                        }
                        else if (type == 42)
                        {
                            node.ImageIndex = 8;
                        }
                        break;
                    }
                }
                else
                    this.setNodeIcon(node,szDeviceID,type);
            }
        }
        private void saveMsgLocalDb(string szDeviceID, int nchannel, string alarmTxt)
        {
            int channel = nchannel + 1;
            string channelId;
            if (channel < 10) channelId = szDeviceID + "0" + channel;
            else channelId = szDeviceID + channel;

            AlarmData shareData = AlarmData.ShareData();

            AlarmMsg amsg = new AlarmMsg();
            amsg.Type = TransSDK.enum_P_Client_AlarmMsg.enum_P_Client_ALARM_EMAX;
            amsg.DeviceId = szDeviceID;
            amsg.Channel = channelId;
            amsg.Nchannel = nchannel;
            amsg.Status = 1;//1 产生 2 消失
            amsg.Param = 0;// 0
            amsg.IsAlarmed = false;
            amsg.IsDeled = false;
            amsg.AlarmTime = DateTime.Now;
            amsg.alarmTxt = alarmTxt;
            //从HTTP网络获取相关信息合并,增强ams信息量

            Channel c = MDSUtils.GetChannelInfo(channelId);
            Device d = MDSUtils.GetDeviceInfo(szDeviceID);
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
                amsg.ChannelName = amsg.alarmTxt + "|" + dev==null?szDeviceID:dev.DeviceName + "->" + c.ChannelName;
                amsg.MapPic = d.MapPic == null ? "" : d.MapPic;
            }

            shareData.addMsg(amsg);

            //本地入库
            AlarmLogBll.Save(amsg);

            //显示
            this.LoginFrm.UIMsgShow();
        }

        private void tree_devices_Leave(object sender, EventArgs e)
        {
            //失去焦点
            tree_devices.SelectedNode = null;
        }

        private void ToolStripMenuItem_DisableOpenVideo_Click(object sender, EventArgs e)
        {
            //临时禁止弹屏告警
            this.isOpenVideoNow = !this.isOpenVideoNow;

            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            if (this.isOpenVideoNow)
                menu.Text = "禁止弹屏";
            else
                menu.Text = "允许弹屏";
        }

        private void exePlan_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = tree_devices.SelectedNode;

            DDevice dev = (DDevice)node.Tag;
            string deviceId = dev.Id;
            FrmExePlan ep = new FrmExePlan(deviceId, this.oprname, this.docXml);
            ep.ShowDialog();
        }

        private void ToolStripMenuItem_AutoLoadDeviceStatus_Click(object sender, EventArgs e)
        {
            this.isAutoLoadNodeStatus = !this.isAutoLoadNodeStatus;

            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            if (this.isAutoLoadNodeStatus)
            {
                menu.Text = "禁止刷新设备状态";
                this.isStopNodeTimer = false;
                this.node_timer.Start();
            }
            else
            {
                menu.Text = "自动刷新设备状态";
                this.node_timer.Stop();
                this.isStopNodeTimer = true;
                
            }

        }

        private void ToolStripMenuItem_RefreshBCF_Click(object sender, EventArgs e)
        {
            if (isloadbcf) return;

            foreach (TreeNode node in this.tree_devices.Nodes)
            {
                foreach (TreeNode cnode in node.Nodes)
                {
                    log.Error("FreshNode-" + cnode.Text);
                    FreshNode(cnode, true);
                }
            }
        }

        private void saveBcfLocalLog(SectorLog log, string szDeviceID)
        {
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
            amsg.alarmTxt = log.remark;
            amsg.ChannelName = log.remark;
            //获取设备信息
            Device d = MDSUtils.GetDeviceInfo(szDeviceID);
            amsg.UserName = d.TrueName == null ? "" : d.TrueName;
            amsg.Tel = d.Tel == null ? "" : d.Tel;
            amsg.Address = d.Address == null ? "" : d.Address;
            AlarmLogBll.Save(amsg);
        }
    }
}
