using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AlarmMapClient
{
    public class TransSDK
    {
        public static int P_CLIENT_MAX_ID_LEN = 32;		//ID号长度

        //云台命令
        public enum P_CLIENT_PTZCommand                 //各个按键编号
        {
            P_CLIENT_PTZ_UP,                            //上
            P_CLIENT_PTZ_DOWN,							//下
            P_CLIENT_PTZ_LEFT,                          //左
            P_CLIENT_PTZ_RIGHT,                         //右
            P_CLIENT_PTZ_LEFTUP,                        //左上
            P_CLIENT_PTZ_LEFTDOWN,                      //左下
            P_CLIENT_PTZ_RIGHTUP,						//右上
            P_CLIENT_PTZ_RIGHTDOWN,                     //右下

            P_CLIENT_PTZ_ADD_ZOOM,						//变倍+        //Camera operation 的枚举顺序勿改，关系其他代码
            P_CLIENT_PTZ_ADD_FOCUS,						//变焦+
            P_CLIENT_PTZ_ADD_APERTURE,                  //光圈+
            P_CLIENT_PTZ_REDUCE_ZOOM,                   //变倍-
            P_CLIENT_PTZ_REDUCE_FOCUS,                  //变焦-
            P_CLIENT_PTZ_REDUCE_APERTURE,               //光圈-

            P_CLIENT_PTZ_GO_PRESET,                     //定位到
            P_CLIENT_PTZ_PRESET_ADD,					//预置点设置 增加
            P_CLIENT_PTZ_PRESET_DEL,					//预置点设置 删除

            P_CLIENT_PTZ_TOUR_START,					//启用
            P_CLIENT_PTZ_TOUR_STOP,						//设置

            P_CLIENT_PTZ_TOUR_ADD,						//巡航设置 增加
            P_CLIENT_PTZ_TOUR_DEL,						//巡航设置 删除
            P_CLIENT_PTZ_TOUR_CLEAR,					//巡航设置 清除

            // 新加的 songe
            P_CLIENT_PTZ_PANCRUISE						//线扫
        };

        public struct P_Client_PrePoint
        {
	        string	code;				//预置点编号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
	        string	revered;			//revered
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	        string	name;			//名字
        };
        public struct P_Client_PrePointTotal
        {
	        int nNum;							//预置点个数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
	        P_Client_PrePoint[] point;		//最大256个预置点
        };

        public enum enum_P_Client_Msg
        {
            enum_P_Client_CMS_Server_Status = 0,		//主控服务状态，在断线和重练成功时候提示
            enum_P_Client_Dev_Grant_Result,				//设备授权结果,可能只是为了写日志
            enum_P_Client_Device_Status,				//设备状态
            enum_P_Client_Video_Exception,				//视频异常，nType2见enum_P_Video_Exception
            enum_P_Client_ReGetInfo_Failed,				//服务端信息更改后，重新获取数据失败，需要重启程序
            enum_P_Client_User_Error,					//用户被锁定或者删除，请退出程序
            enum_P_Client_Reload_DeviceData,			//请重新获取设备信息，数据已经被服务器更改
            enum_P_Client_Reload_MapData,				//重新获取地图信息
            enum_P_Client_VideoRequest,					//预览请求， nType2为成功或失败原因
            enum_P_Client_PlaybackRequest,				//回放下载请求
            enum_P_Client_PlaybackClose,
            enum_P_Client_TalkRequest,					//对讲请求
            enum_P_Client_PTZControl,					//云台控制 nType2见 P_CLIENT_PTZCommand

            enum_P_Client_Command						//命令回调，可以如抓图完成（要求打开）
        };
        public enum enum_P_Client_AlarmMsg
        {
            enum_P_Client_ALARM_Unknown = 0,		//未知
            enum_P_Client_ALARM_VIDEO_LOST,			//视频丢失
            enum_P_Client_ALARM_EXTERNAL_ALARM,		//外部报警
            enum_P_Client_ALARM_MOTION_DETECT,		//移动侦测
            enum_P_Client_ALARM_VIDEO_SHELTER,		//视频遮挡
            enum_P_Client_ALARM_DISK_FULL,			//硬盘满
            enum_P_Client_ALARM_DISK_FAULT,			//硬盘故障
            enum_P_Client_ALARM_EMAX,          //单片机告警消息
            enum_LOCAL_LOG //上线及布防日志
        };
        // 实时监视数据回调函数原形
        public delegate void pfRealDataCallBack(Int32 lRealHandle, Int32 dwDataType, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, Int32 dwBufSize, Int32 dwUser);
        //预览条件结构体
        public struct P_Client_RealInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szChannelID;	//通道ID
            public int nStream;							//码流类型 1 主码流 2 辅码流
            public IntPtr hWnd;								//窗口句柄(为NULL时候内部不解码，回调原始数据给外部)
            public pfRealDataCallBack realDataFunc;						//视频数据回调
            public Int32 dwUser;								//用户参数
        };
        /*
        描述： 消息回调
        参数： 
        nType		异常类型，参见enum_P_Client_Msg
        nType2		子类型，状态
        szDeviceID	设备ID
        channel		通道号
        pParam1		保留，可代表设备ID
        lUser		用户参数
        返回值：
        */
        public delegate void pfmessageCallBack(enum_P_Client_Msg enumType, int nType2, string szDeviceID, int channel, int nParam1, string pParam1, Int32 dwUser);

        public delegate void pAlarmMessageCallBack(enum_P_Client_AlarmMsg enumType, string szDeviceID, int channel, int nStatus, int nParam1, Int32 dwUser);

        //透明通道数据回调
        public delegate void pfTransportDataCallBack(string szDeviceID, IntPtr pBuffer, Int32 dwBufSize, Int32 dwUser);

        //初始化
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_Init(pfmessageCallBack callback, IntPtr dwUser);

        //注销
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_ClearUp();


        //设置报警回调
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_SetAlarmCallBack(pAlarmMessageCallBack callback, IntPtr dwUser);

        /*
        描述：登陆服务
        参数：
        ip  :	服务器地址
        port	服务器端口
        user	登陆用户
        password登陆密码
        waittime超时时间

        返回值：>=0 成功,否则失败
        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_LoginServer(string ip, int port, string user, string password, int waittime = 5000);

        /*
        描述：获取设备信息长度
        返回：需要开辟的内存长度
        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_GetDevicesLen();

        /*
        描述：获取设备信息
        参数: 
        pBuf	内存地址
        nLen	内存长度
        返回：	真正的数据长度
        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_GetDevices([MarshalAs(UnmanagedType.LPArray)]byte[] pBuf, int nLen);


        /*
        描述： 预览视频
        参数： pRealInfo  预览条件
        返回值：>0 成功， 《=0 失败
        （这里的成功只能说明命令发送成功，具体是否成功由回调决定）
        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_ConnectRealPlay(ref P_Client_RealInfo pRealInfo);

        /*
        描述: 停止预览
        参数: realHandle 预览句柄  P_Client_ConnectRealPlay返回值
        返回: >=0 成功
        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_StopRealPlay(int realHandle);

        /*
        描述: 停止预览
        参数: realHandle 预览句柄  P_Client_ConnectRealPlay返回值
        返回: >=0 成功
        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_StopRealPlay(IntPtr hWnd, bool bWait = false);
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_StopRealPlayByID(int realHandle, bool bWait = false);

        /*
        描述：设置网络传输模式
        参数：0 udp 1 tcp

        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_SetMediaProtocol(int nMode);

        /*
        描述: 云台控制
        参数 
        szCameraId	通道ID
        返回值
        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_PTZControl(string szCameraId, int nCmd, int nParam1, int nParam2, int nParam3, bool bStop);

        //获取摄像头预置点信息，只有在打开视频后才可以获取P_Client_PrePointTotal*
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_GetPrePonitData(string cameraID, IntPtr preInfo);

        //获取巡航点信息
        //如果data为空，则返回需要的长度,目前固定7*1027
        //如果不为空，则返回实际长度，data中赋值
        //返回 小于等于0 是出错或者没有记录
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_GetPTZTourData(string cameraID, string data);

        /*
        开启透明通道
        参数说明：
        szDeviceID		设备ID
        nType		串口类型 0: 232  1=485
        nBaudrate	波特率  9600
        nDatabits	数据位  8
        nStopbits	停止位  1
        nParity		检验位  2
        */
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_OpenTransport(string szDeviceID, int nType, int nBaudrate, int nDatabits, int nStopbits, int nParity );

        //关闭透明通道
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_CloseTransport(string szDeviceID, int nType);

        //设备透明通道数据回调
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_SetTransPortCallBack(pfTransportDataCallBack callback, Int32 dwUser);

        //写入透明通道数据
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_WriteTransportData(string szDeviceID, int nType, IntPtr pbuf, int nLen);


        //开始中心录像
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_StartCenterRecord(string szCamereID);
        [DllImport("ClientSDK.dll")]
        public static extern int P_Client_StopCenterRecord(string szCamereID);
    }
}
