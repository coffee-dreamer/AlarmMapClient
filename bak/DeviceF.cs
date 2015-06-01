using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;

namespace AlarmMapClient
{
    public class DeviceF
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo;
        public NetSDK.SDK_MOTIONCONFIG motionCfg;
        public NetSDK.SDK_ALARM_INPUTCONFIG alarmInputCfg;
        private Int32 lhwd;
        public bool init;

        private string url, username, password;
        private ushort port;

        private static DeviceF instance;
        private DeviceF() { }
        public static DeviceF Instance 
　　    { 
　　        get 
　　        {
              if (instance == null)
              {
                  instance = new DeviceF();

                  //instance.Init();
              }
　　            return instance; 
　　        } 
　　    }

        private void HConnect(int lLoginID, string pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //MessageBox.Show("网络断开异常,请重连接!");
            int err = 0;
            lhwd = NetSDK.H264_DVR_Login(url, port, username, password, out lpDeviceInfo, out err, NetSDK.SocketStyle.TCPSOCKET);
        }

        public int Init(IntPtr handle)
        {
            int linit = 0;
            try{
                //uint dwUser = 0;

                //NetSDK.fDisConnect fdisconnect = new NetSDK.fDisConnect(HConnect);
                linit = NetSDK.H264_DVR_Init(null, handle);
                if (linit == 1) init = true;
                else init = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return linit;
        }

        public bool Login(string serverUrl, ushort serverPort, string userName, string pwd, out NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo)
        {
            bool ret = false;
            this.url = serverUrl;
            this.port = serverPort;
            this.username = userName;
            this.password = pwd;

            lpDeviceInfo = new NetSDK.LPH264_DVR_DEVICEINFO();
            try
            {
                int err = 0;

                NetSDK.H264_DVR_SetConnectTime(30000, 1);
                lhwd = NetSDK.H264_DVR_Login(serverUrl, serverPort, userName, pwd, out lpDeviceInfo, out err, NetSDK.SocketStyle.TCPSOCKET);
                if (lhwd == 0)
                {
                    ret = false;
                    log.Error("用户名或密码错误,或网络异常!请稍候再试!ERR=" + err + "\r\n" + serverUrl+":"+serverPort+" user="+username+"/pwd="+pwd);
                }
                else
                {
                    ret = true;
                    log.Info("报警输入通道数:" + lpDeviceInfo.byAlarmInPortNum + " 报警输出通道数:" + lpDeviceInfo.byAlarmOutPortNum + "  视频输出通道数:" + lpDeviceInfo.iVideoOutChannel + "  视频输入通道数:" + lpDeviceInfo.byChanNum + "  数字通道数:" + lpDeviceInfo.iDigChannel);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return ret;
        }

        public void LogOut()
        {
            int ret;
            try
            {
                //退出DVR登录
                if (lhwd > 0)
                {
                    ret = NetSDK.H264_DVR_Logout(lhwd);
                    log.Info("SDK LogOut,ret="+ret);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public void Cleanup()
        {
            bool ret;
            try
            {
                ret = NetSDK.H264_DVR_Cleanup();
                log.Info("SDK Cleanup,ret=" + ret);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public int GetMotionCfg(int channelid)
        {
            int nWaitTime = 10000;
            uint dwRetLen = 0;
            int len = Marshal.SizeOf(typeof(NetSDK.SDK_MOTIONCONFIG));
            //移动侦测
            motionCfg = new NetSDK.SDK_MOTIONCONFIG();
            IntPtr lpOutBuffer = Marshal.AllocHGlobal(len);
            //IntPtr lpOutBuffer = Marshal.AllocCoTaskMem(len);
            //Marshal.StructureToPtr(motionCfg, lpOutBuffer, true);

            int bSuccess = NetSDK.H264_DVR_GetDevConfig(lhwd, (uint)NetSDK.SDK_CONFIG_TYPE.E_SDK_CONFIG_MOTION, channelid,
                lpOutBuffer, (uint)len, out dwRetLen, nWaitTime);
            if (bSuccess > 0)
            {
                motionCfg = (NetSDK.SDK_MOTIONCONFIG)Marshal.PtrToStructure(lpOutBuffer, typeof(NetSDK.SDK_MOTIONCONFIG));
            }
            else
            {
                log.Error("err:" + bSuccess);
            }
            Marshal.FreeHGlobal(lpOutBuffer);
            //Marshal.FreeCoTaskMem(lpOutBuffer);
            return bSuccess;
        }

        public int SetMotionCfg(int channelid,byte enable,byte level)
        {
            int nWaitTime = 10000;
            motionCfg.bEnable = enable;
            motionCfg.iLevel = level;
            int len = Marshal.SizeOf(typeof(NetSDK.SDK_MOTIONCONFIG));

            IntPtr lpOutBuffer = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(motionCfg, lpOutBuffer, false);

            int bSuccess = NetSDK.H264_DVR_SetDevConfig(lhwd, (uint)NetSDK.SDK_CONFIG_TYPE.E_SDK_CONFIG_MOTION, channelid,
            lpOutBuffer, (uint)len, nWaitTime);

            Marshal.FreeHGlobal(lpOutBuffer);
            return bSuccess;
        }

        public int GetAlarmInputCfg(int channelid)
        {
            int nWaitTime = 10000;
            uint dwRetLen = 0;
            int len = Marshal.SizeOf(typeof(NetSDK.SDK_ALARM_INPUTCONFIG));
            //报警输入
            alarmInputCfg = new NetSDK.SDK_ALARM_INPUTCONFIG();
            IntPtr lpOutBufferIn = Marshal.AllocHGlobal(len);

            int bSuccess = NetSDK.H264_DVR_GetDevConfig(lhwd, (uint)NetSDK.SDK_CONFIG_TYPE.E_SDK_CONFIG_ALARM_IN, channelid,
            lpOutBufferIn, (uint)len, out dwRetLen, nWaitTime);
            if (bSuccess > 0)
            {
                alarmInputCfg = (NetSDK.SDK_ALARM_INPUTCONFIG)Marshal.PtrToStructure(lpOutBufferIn, typeof(NetSDK.SDK_ALARM_INPUTCONFIG));
            }
            else
            {
                log.Error("err:" + bSuccess);
            }
            Marshal.FreeHGlobal(lpOutBufferIn);
            return bSuccess;
        }

        public int SetAlarmInputCfg(int channelid, byte enable, int sensorType)
        {
            int nWaitTime = 10000;
            alarmInputCfg.bEnable = enable;
            alarmInputCfg.iSensorType = sensorType;
            int len = Marshal.SizeOf(typeof(NetSDK.SDK_ALARM_INPUTCONFIG));

            IntPtr lpOutBuffer = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(alarmInputCfg, lpOutBuffer, false);

            int bSuccess = NetSDK.H264_DVR_SetDevConfig(lhwd, (uint)NetSDK.SDK_CONFIG_TYPE.E_SDK_CONFIG_ALARM_IN, channelid,
            lpOutBuffer, (uint)Marshal.SizeOf(alarmInputCfg), nWaitTime);
            Marshal.FreeHGlobal(lpOutBuffer);
            return bSuccess;
        }
    }
}