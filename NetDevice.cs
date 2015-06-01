using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AlarmMapClient
{
    public class NetDevice
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo;
        public NetSDK.SDK_MOTIONCONFIG motionCfg;
        public NetSDK.SDK_ALARM_INPUTCONFIG alarmInputCfg;
        public bool init;

        private string url, username, password;
        private ushort port;

        private void HConnect(int lLoginID, string pchDVRIP, int nDVRPort, uint dwUser)
        {
            //MessageBox.Show("网络断开异常,请重连接!");
            //int err = 0;
            //NetSDK.H264_DVR_Login(url, port, username, password, ref lpDeviceInfo, ref err);
        }

        public Int16 Init(IntPtr dwUser)
        {
            Int16 linit = 0;
            try{
                //uint dwUser = 0;

                //NetSDK.fDisConnect fdisconnect = new NetSDK.fDisConnect(HConnect);
                linit = NetSDK.H264_DVR_Init(null, dwUser);
                if (linit == 1) init = true;
                else init = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return linit;
        }

        public int Login(string serverUrl, ushort serverPort, string userName, string pwd, ref NetSDK.LPH264_DVR_DEVICEINFO lpDeviceInfo)
        {
            int lhwd=-1;
            this.url = serverUrl;
            this.port = serverPort;
            this.username = userName;
            this.password = pwd;
            try
            {
                int err = 0;

                NetSDK.H264_DVR_SetConnectTime(3000, 1);
                lhwd = NetSDK.H264_DVR_Login(serverUrl, serverPort, userName, pwd, ref lpDeviceInfo, ref err);
                if (lhwd == 0)
                {
                    log.Error("用户名或密码错误,或网络异常!请稍候再试!ERR=" + err + "\r\n" + serverUrl+":"+serverPort+" user="+username+"/pwd="+pwd);
                }
                else
                {
                    log.Info("报警输入通道数:" + lpDeviceInfo.byAlarmInPortNum + " 报警输出通道数:" + lpDeviceInfo.byAlarmOutPortNum + "  视频输出通道数:" + lpDeviceInfo.iVideoOutChannel + "  视频输入通道数:" + lpDeviceInfo.byChanNum + "  数字通道数:" + lpDeviceInfo.iDigChannel);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return lhwd;
        }

        public void LogOut(int lhwd)
        {
            int ret;
            try
            {
                //退出DVR登录
                if (lhwd > 0)
                {
                    ret = NetSDK.H264_DVR_Logout(lhwd);
                    log.Error("SDK LogOut,ret="+ret);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        public void Cleanup()
        {
            int ret;
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

        public Int32 Play(int lhwd,int channel, IntPtr handle, int stream)
        {
            NetSDK.LPH264_DVR_CLIENTINFO playstru = new NetSDK.LPH264_DVR_CLIENTINFO();

            playstru.nChannel = channel;
            playstru.nStream = stream;
            playstru.nMode = 0;
            playstru.hWnd = handle;
            Int32 m_iPlayhandle = NetSDK.H264_DVR_RealPlay(lhwd, ref playstru);

            return m_iPlayhandle;
        }

        public bool Stop(Int32 m_iPlayhandle)
        {
            return NetSDK.H264_DVR_StopRealPlay(m_iPlayhandle);
        }

        public int GetMotionCfg(int lhwd,int channelid)
        {
            int nWaitTime = 10000;
            uint dwRetLen = 0;
            //移动侦测
            motionCfg = new NetSDK.SDK_MOTIONCONFIG();
            //IntPtr lpOutBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NetSDK.SDK_MOTIONCONFIG)));
            IntPtr lpOutBuffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(NetSDK.SDK_MOTIONCONFIG)));
            //Marshal.StructureToPtr(motionCfg, lpOutBuffer, true);

            int bSuccess = NetSDK.H264_DVR_GetDevConfig(lhwd, (uint)NetSDK.SDK_CONFIG_TYPE.E_SDK_CONFIG_MOTION, channelid,
                lpOutBuffer, (uint)Marshal.SizeOf(typeof(NetSDK.SDK_MOTIONCONFIG)), ref dwRetLen, nWaitTime);
            if (bSuccess > 0)
            {
                motionCfg = (NetSDK.SDK_MOTIONCONFIG)Marshal.PtrToStructure(lpOutBuffer, typeof(NetSDK.SDK_MOTIONCONFIG));
            }
            //Marshal.FreeHGlobal(lpOutBuffer);
            Marshal.FreeCoTaskMem(lpOutBuffer);
            return bSuccess;
        }

        public int SetMotionCfg(int lhwd, int channelid, byte enable, byte level)
        {
            int nWaitTime = 10000;
            motionCfg.bEnable = enable;
            motionCfg.iLevel = level;

            IntPtr lpOutBuffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(motionCfg));
            Marshal.StructureToPtr(motionCfg, lpOutBuffer, false);

            int bSuccess = NetSDK.H264_DVR_SetDevConfig(lhwd, (uint)NetSDK.SDK_CONFIG_TYPE.E_SDK_CONFIG_MOTION, channelid,
            lpOutBuffer, (uint)Marshal.SizeOf(motionCfg), nWaitTime);
            return bSuccess;
        }

        public int GetAlarmInputCfg(int lhwd, int channelid)
        {
            int nWaitTime = 10000;
            uint dwRetLen = 0;
            int len = Marshal.SizeOf(typeof(NetSDK.SDK_ALARM_INPUTCONFIG));
            //报警输入
            alarmInputCfg = new NetSDK.SDK_ALARM_INPUTCONFIG();
            IntPtr lpOutBufferIn = Marshal.AllocCoTaskMem(len);

            int bSuccess = NetSDK.H264_DVR_GetDevConfig(lhwd, (uint)NetSDK.SDK_CONFIG_TYPE.E_SDK_CONFIG_ALARM_IN, channelid,
            lpOutBufferIn, (uint)len, ref dwRetLen, nWaitTime);
            if (bSuccess > 0)
            {
                alarmInputCfg = (NetSDK.SDK_ALARM_INPUTCONFIG)Marshal.PtrToStructure(lpOutBufferIn, typeof(NetSDK.SDK_ALARM_INPUTCONFIG));
            }
            Marshal.FreeCoTaskMem(lpOutBufferIn);
            return bSuccess;
        }

        public int SetAlarmInputCfg(int lhwd, int channelid, byte enable, int sensorType)
        {
            int nWaitTime = 10000;
            alarmInputCfg.bEnable = enable;
            alarmInputCfg.iSensorType = sensorType;

            IntPtr lpOutBuffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(alarmInputCfg));
            Marshal.StructureToPtr(alarmInputCfg, lpOutBuffer, false);

            int bSuccess = NetSDK.H264_DVR_SetDevConfig(lhwd, (uint)NetSDK.SDK_CONFIG_TYPE.E_SDK_CONFIG_ALARM_IN, channelid,
            lpOutBuffer, (uint)Marshal.SizeOf(alarmInputCfg), nWaitTime);
            return bSuccess;
        }

        public static bool SendTransportData(int lhwd, byte[] data, int nBufLen)
        {
            IntPtr pBuffer;

            GCHandle hObject = GCHandle.Alloc(data, GCHandleType.Pinned);
            pBuffer = hObject.AddrOfPinnedObject();

            bool ret = NetSDK.H264_DVR_SerialWrite(lhwd, NetSDK.SERIAL_TYPE.RS232, pBuffer, nBufLen);

            //if (hObject.IsAllocated)
                hObject.Free();

            return ret;
        }
    }
}
