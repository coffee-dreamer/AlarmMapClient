using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AlarmMapClient
{
    public class NetSDK
    {
        const int NET_MAX_CHANNUM = 32;			//最大通道个数
        const int NET_N_WEEKS = 7;			//星期数
        const int NET_N_TSECT = 6;			//时间段数
        const int CHANNELNAME_MAX_LEN = 64;  //通道名称最大长度
        const int NET_MD_REGION_ROW = 32;			//动态检测区域行数
        //配置类型
        public enum SDK_CONFIG_TYPE
        {
            E_SDK_CONFIG_NOTHING = 0,		//
            E_SDK_CONFIG_USER,			//用户信息，包含了权限列表，用户列表和组列表
            E_SDK_CONFIG_ADD_USER,		//增加用户
            E_SDK_CONFIG_MODIFY_USER,		//修改用户
            E_SDK_CONFIG_DELETE_USER,
            E_SDK_CONFIG_ADD_GROUP,		//增加组
            E_SDK_CONFIG_MODIFY_GROUP,	//修改组
            E_SDK_COFIG_DELETE_GROUP,
            E_SDK_CONFIG_MODIFY_PSW,		//修改密码


            E_SDK_CONFIG_ABILITY_SYSFUNC = 9,//支持的网络功能
            E_SDK_CONFIG_ABILTY_ENCODE,	//首先获得编码能力
            E_SDK_CONFIG_ABILITY_PTZPRO,	//云台协议
            E_SDK_COMFIG_ABILITY_COMMPRO,	//串口协议

            E_SDK_CONFIG_ABILITY_MOTION_FUNC,	//动态检测块
            E_SDK_CONFIG_ABILITY_BLIND_FUNC,	//视频遮挡块
            E_SDK_CONFIG_ABILITY_DDNS_SERVER,	//DDNS服务支持类型
            E_SDK_CONFIG_ABILITY_TALK,		//对讲编码类型



            E_SDK_CONFIG_SYSINFO = 17,		//系统信息
            E_SDK_CONFIG_SYSNORMAL,	//普通配置
            E_SDK_CONFIG_SYSENCODE,	//编码配置
            E_SDK_CONFIG_SYSNET,		//网络设置
            E_SDK_CONFIG_PTZ,			//云台页面
            E_SDK_CONFIG_COMM,		//串口页面
            E_SDK_CONFIG_RECORD,		//录像设置界面
            E_SDK_CONFIG_MOTION,		//动态检测页面
            E_SDK_CONFIG_SHELTER,		//视频遮挡
            E_SDK_CONFIG_VIDEO_LOSS,  //视频丢失,
            E_SDK_CONFIG_ALARM_IN,	//报警输入
            E_SDK_CONFIG_ALARM_OUT,	//报警输出
            E_SDK_CONFIG_DISK_MANAGER,//硬盘管理界面
            E_SDK_CONFIG_OUT_MODE,	//输出模式界面
            E_SDK_CONFIG_CHANNEL_NAME,//通道名称
            E_SDK_CONFIG_AUTO,		//自动维护界面配置
            E_SDK_CONFIG_DEFAULT,     //恢复默认界面配置
            E_SDK_CONFIG_DISK_INFO,	//硬盘信息
            E_SDK_CONFIG_LOG_INFO,	//查询日志
            E_SDK_CONFIG_NET_IPFILTER,
            E_SDK_CONFIG_NET_DHCP,
            E_SDK_CONFIG_NET_DDNS,
            E_SDK_CONFIG_NET_EMAIL,
            E_SDK_CONFIG_NET_MULTICAST,
            E_SDK_CONFIG_NET_NTP,
            E_SDK_CONFIG_NET_PPPOE,
            E_SDK_CONFIG_NET_DNS,
            E_SDK_CONFIG_NET_FTPSERVER,

            E_SDK_CONFIG_SYS_TIME,	//系统时间	
            E_SDK_CONFIG_CLEAR_LOG,	//清除日志
            E_SDK_REBOOT_DEV,		//重启启动设备
            E_SDK_CONFIG_ABILITY_LANG,	//支持语言
            E_SDK_CONFIG_VIDEO_FORMAT,
            E_SDK_CONFIG_COMBINEENCODE,	//组合编码
            E_SDK_CONFIG_EXPORT,	//配置导出
            E_SDK_CONFIG_IMPORT,	//配置导入
            E_SDK_LOG_EXPORT,		//日志导出
            E_SDK_CONFIG_COMBINEENCODEMODE, //组合编码模式
            E_SDK_WORK_STATE,	//运行状态
            E_SDK_ABILITY_LANGLIST, //实际支持的语言集
            E_SDK_CONFIG_NET_ARSP,
            E_SDK_CONFIG_SNAP_STORAGE,//抓图开关
            E_SDK_CONFIG_NET_3G, //3G拨号
            E_SDK_CONFIG_NET_MOBILE, //手机监控
            E_SDK_CONFIG_UPGRADEINFO, //获取升级信息
            E_SDK_CONFIG_NET_DECODER,
            E_SDK_ABILITY_VSTD, //实际支持的视频制式
            E_SDK_CONFIG_ABILITY_VSTD,	//支持视频制式
            E_SDK_CONFIG_NET_UPNP, //UPUN设置
            E_SDK_CONFIG_NET_WIFI,
            E_SDK_CONFIG_NET_WIFI_AP_LIST,
            E_SDK_CONFIG_SYSENCODE_SIMPLIIFY, //简化的编码配置
            E_SDK_CONFIG_ALARM_CENTER,  //告警中心
            E_SDK_CONFIG_NET_ALARM,
            E_SDK_CONFIG_NET_MEGA,     //互信互通
            E_SDK_CONFIG_NET_XINGWANG, //星望
            E_SDK_CONFIG_NET_SHISOU,   //视搜
            E_SDK_CONFIG_NET_VVEYE,    //VVEYE
            E_SDK_VIDEO_PREVIEW,
            E_SDK_CONFIG_NET_DECODER_V2,
            E_SDK_CONFIG_NET_DECODER_V3,//数字通道
            E_SDK_CONFIG_ABILITY_SERIALNO,	// 序列号
            E_SDK_CONFIG_NET_RTSP,    //RTSP
            E_SDK_GUISET,              //GUISET
            E_SDK_CATCHPIC,               //抓图
            E_SDK_VIDEOCOLOR,             //视频颜色设置
            E_SDK_CONFIG_COMM485,
            E_SDK_COMFIG_ABILITY_COMMPRO485, //串口485
            E_SDK_CONFIG_SYS_TIME_NORTC,	//系统时间noRtc
            E_SDK_CONFIG_REMOTECHANNEL,   //远程通道
            E_SDK_CONFIG_OPENTRANSCOMCHANNEL, //打开透明串口
            E_SDK_CONFIG_CLOSETRANSCOMCHANNEL,  //关闭透明串口
            E_SDK_CONFIG_SERIALWIRTE,  //写入透明串口信息
            E_SDK_CONFIG_SERIALREAD,   //读取透明串口信息
            E_SDK_CONFIG_CHANNELTILE_DOT	//点阵信息
        };

        //云台操作类型
        public enum PTZ_ControlType
        {
	        TILT_UP = 0,			//云台左
	        TILT_DOWN,				//右
	        PAN_LEFT,				//上
	        PAN_RIGHT,				//下
	        PAN_LEFTTOP,			//左上
	        PAN_LEFTDOWN,			//左下
	        PAN_RIGTHTOP,			//右上
	        PAN_RIGTHDOWN,			//右下
	        ZOOM_OUT,				//变倍小
	        ZOOM_IN,				//变倍大
	        FOCUS_FAR,				//焦点后调
	        FOCUS_NEAR,				//焦点前调
	        IRIS_OPEN,				//光圈扩大
	        IRIS_CLOSE,				//光圈缩小

	        EXTPTZ_OPERATION_ALARM,			///< 报警功能 
	        EXTPTZ_LAMP_ON,					///< 灯光开
	        EXTPTZ_LAMP_OFF,				//灯光关
	        EXTPTZ_POINT_SET_CONTROL,			//设置预置点 
	        EXTPTZ_POINT_DEL_CONTROL,			//清除预置点 
	        EXTPTZ_POINT_MOVE_CONTROL,			//转预置点
	        EXTPTZ_STARTPANCRUISE,			//开始水平旋转			
	        EXTPTZ_STOPPANCRUISE,			//停止水平旋转	
	        EXTPTZ_SETLEFTBORDER,			//设置左边界		
	        EXTPTZ_SETRIGHTBORDER,			//设置右边界	
	        EXTPTZ_STARTLINESCAN,			//自动扫描开始 
	        EXTPTZ_CLOSELINESCAN,			//自动扫描开停止 
	        EXTPTZ_ADDTOLOOP,				//加入预置点到巡航	p1巡航线路	p2预置点值	
	        EXTPTZ_DELFROMLOOP,				//删除巡航中预置点	p1巡航线路	p2预置点值	
	        EXTPTZ_POINT_LOOP_CONTROL,			//开始巡航
	        EXTPTZ_POINT_STOP_LOOP_CONTROL,	//停止巡航
	        EXTPTZ_CLOSELOOP,				//清除巡航	p1巡航线路		
	        EXTPTZ_FASTGOTO,				//快速定位	
	        EXTPTZ_AUXIOPEN,				//辅助开关，关闭在子命令中
	        EXTPTZ_OPERATION_MENU,				//球机菜单操作，其中包括开，关，确定等等
	        EXTPTZ_REVERSECOMM,				//镜头翻转
	        EXTPTZ_OPERATION_RESET,			///< 云台复位

	        EXTPTZ_TOTAL,
        };

        //*** */向设备注册
        public enum SocketStyle
        {
            TCPSOCKET = 0,
            UDPSOCKET,
            SOCKETNR
        };

        //报警相关结构体
        //  云台联动结构
        public struct SDK_PtzLinkConfig
        {
            public int iType;		// 联动的类型 
            public int iValue;		// 联动的类型对应的值 
        };
        public struct SDK_EventHandler
        {
            public uint dwRecord;				// 录象掩码 
            public int iRecordLatch;			// 录像延时：10～300 sec  	
            public uint dwTour;					// 轮巡掩码 	
            public uint dwSnapShot;				// 抓图掩码 
            public uint dwAlarmOut;				// 报警输出通道掩码 
            public uint dwMatrix;				// 矩阵掩码 
            public int iEventLatch;			// 联动开始延时时间，s为单位 
            public int iAOLatch;				// 报警输出延时：10～300 sec 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MAX_CHANNUM)]
            public SDK_PtzLinkConfig[] PtzLink;		// 云台联动项 
            public SDK_CONFIG_WORKSHEET schedule;		// 录像时间段

            public byte bRecordEn;				// 录像使能 
            public byte bTourEn;				// 轮巡使能 
            public byte bSnapEn;				// 抓图使能 	
            public byte bAlarmOutEn;			// 报警使能 
            public byte bPtzEn;

            // 云台联动使能 
            public byte bTip;					// 屏幕提示使能 	
            public byte bMail;					// 发送邮件 	
            public byte bMessage;				// 发送消息到报警中心 	
            public byte bBeep;					// 蜂鸣 	
            public byte bVoice;					// 语音提示 		
            public byte bFTP;					// 启动FTP传输 
            public byte bMatrixEn;				// 矩阵使能 
            public byte bLog;					// 日志使能
            public byte bMessagetoNet;			// 消息上传给网络使能 

            public byte bShowInfo;              // 是否在GUI上和编码里显示报警信息
            public uint dwShowInfoMask;         // 要联动显示报警信息的通道掩码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = CHANNELNAME_MAX_LEN)]
            public string pAlarmInfo;//要显示的报警信息

            public byte bShortMsg;              //发送短信
            public byte bMultimediaMsg;         //发送彩信
        };
        ///< 报警输入配置
        public struct SDK_ALARM_INPUTCONFIG
        {
            public byte bEnable;		///< 报警输入开关
            public int iSensorType;	///< 传感器类型常开 or 常闭
            public SDK_EventHandler hEvent;	///< 报警联动
        };
        ///< 遮挡检测配置
        public struct SDK_BLINDDETECTCONFIG
        {
            public bool bEnable;		///< 遮挡检测开启
            public int iLevel;			///< 灵敏度：1～6
            public SDK_EventHandler hEvent;	///< 遮挡检测联动
        };
        ///< 所有通道的报警输入配置
        public struct SDK_ALARM_INPUTCONFIG_ALL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MAX_CHANNUM)]
            public SDK_ALARM_INPUTCONFIG[] vAlarmConfigAll;
        };

        /// 全通道遮挡检测配置
        public struct SDK_BLINDDETECTCONFIG_ALL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MAX_CHANNUM)]
            public SDK_BLINDDETECTCONFIG[] vBlindDetectAll;
        };
        ///< 动态检测设置
        ///[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct SDK_MOTIONCONFIG
        {
            //注意C++ bool类型用byte代替
            public byte bEnable;							// 动态检测开启
            public int iLevel;								// 灵敏度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MD_REGION_ROW)]
            public uint[] mRegion;			// 区域，每一行使用一个二进制串	
            public SDK_EventHandler hEvent;					// 动态检测联动
        };
        ///< 本地报警输出配置
        public struct SDK_AlarmOutConfig
        {
            public int nAlarmOutType;		///< 报警输出类型: 配置,手动,关闭
            public int nAlarmOutStatus;    ///< 报警状态: 0:打开 1;闭合
        };
        ///< 网路报警
        public struct SDK_NETALARMCONFIG
        {
            public bool bEnable;			///< 使能
            public SDK_EventHandler hEvent;	///< 处理参数
        };
        /// 全通道动态检测配置
        public struct SDK_MOTIONCONFIG_ALL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MAX_CHANNUM)]
            public SDK_MOTIONCONFIG[] vMotionDetectAll;
        };
        ///< 视频丢失
        public struct SDK_VIDEOLOSSCONFIG
        {
            bool bEnable;			///< 使能
            public SDK_EventHandler hEvent;	///< 处理参数
        };
        /// 所有通道的视频丢失结构
        public struct SDK_VIDEOLOSSCONFIG_ALL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MAX_CHANNUM)]
            public SDK_VIDEOLOSSCONFIG[] vGenericEventConfig;
        };
        ///< 所有通道的报警输出配置
        public struct SDK_AlarmOutConfigAll
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MAX_CHANNUM)]
            public SDK_AlarmOutConfig[] vAlarmOutConfigAll;
        };
        /// 所有通道的网路报警结构
        public struct SDK_NETALARMCONFIG_ALL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MAX_CHANNUM)]
            public SDK_NETALARMCONFIG[] vNetAlarmConfig;
        };

        public struct ALARMCONFIGALL
        {
            public SDK_MOTIONCONFIG_ALL MotionCfgAll;
            public SDK_BLINDDETECTCONFIG_ALL DectectCfgAll;
            public SDK_VIDEOLOSSCONFIG_ALL VideoLossCfgAll;
            public SDK_ALARM_INPUTCONFIG_ALL AlarmInCfgAll;
            public SDK_AlarmOutConfigAll AlarmOutCfgAll;
            public SDK_NETALARMCONFIG_ALL AlarmRmCfgAll;
        };

        // 串口协议
        public struct SDK_COMMFUNC
        {
            //每个协议最多由64个字符组成
            int nProNum;// 协议个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100 * 32)]
            char[] vCommProtocol;
        };

        public struct SDK_CONFIG_WORKSHEET
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7 * 6)]
            public SDK_TIMESECTION[] tsSchedule;	/*!< 时间段 */
        };

        ///< 录像设置
        public struct SDK_RECORDCONFIG
        {
            int iPreRecord;			///< 预录时间，为零时表示关闭	
            bool bRedundancy;		///< 冗余开关
            bool bSnapShot;			///< 快照开关	
            int iPacketLength;		///< 录像打包长度（分钟）[1, 255]
            int iRecordMode;		///< 录像模式，0 关闭，1 禁止 2 配置
            SDK_CONFIG_WORKSHEET wcWorkSheet;			///< 录像时间段	
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7 * 6)]
            uint[] typeMask;		///< 录像类型掩码
        };

        //录像设置结构体
        public struct SDK_RECORDCONFIG_ALL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32)]
            SDK_RECORDCONFIG[] vRecordConfigAll;
        };

        public struct SDK_SYSTEM_TIME
        {
            public Int32 year;///< 年。   
            public Int32 month;///< 月，January = 1, February = 2, and so on.   
            public Int32 day;///< 日。   
            public Int32 wday;///< 星期，Sunday = 0, Monday = 1, and so on   
            public Int32 hour;///< 时。   
            public Int32 minute;///< 分。   
            public Int32 second;///< 秒。   
            public Int32 isdst;///< 夏令时标识。   
        };

        //录像设置相关结构体
        public struct SDK_TIMESECTION
        {
            //!使能
            public int enable;
            //!开始时间:小时
            public int startHour;
            //!开始时间:分钟
            public int startMinute;
            //!开始时间:秒钟
            public int startSecond;
            //!结束时间:小时
            public int endHour;
            //!结束时间:分钟
            public int endMinute;
            //!结束时间:秒钟
            public int endSecond;
        };

        public enum SERIAL_TYPE
        {
            RS232 = 0,
            RS485 = 1,
        };

        public struct TransComChannel
        {
            public SERIAL_TYPE TransComType;//SERIAL_TYPE
            public UInt32 baudrate;
            public UInt32 databits;
            public UInt32 stopbits;
            public UInt32 parity;
        };

        public struct LPH264_DVR_DEVICEINFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sSoftWareVersion;	///< 软件版本信息
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sHardWareVersion;	///< 硬件版本信息
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sEncryptVersion;	///< 加密版本信息
            public SDK_SYSTEM_TIME tmBuildTime;///< 软件创建时间
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sSerialNumber;			///< 设备序列号

            public int byChanNum;				///< 视频输入通道数
            public int iVideoOutChannel;		///< 视频输出通道数
            public int byAlarmInPortNum;		///< 报警输入通道数
            public int byAlarmOutPortNum;		///< 报警输出通道数
            public int iTalkInChannel;			///< 对讲输入通道数
            public int iTalkOutChannel;		///< 对讲输出通道数
            public int iExtraChannel;			///< 扩展通道数	
            public int iAudioInChannel;		///< 音频输入通道数
            public int iCombineSwitch;			///< 组合编码通道分割模式是否支持切换
            public int iDigChannel;		///<数字通道数
            public UInt32 uiDeviceRunTime;
        }
        //需特别注意int/Int16/Int32等与dll对应关系,不然会溢出
        public struct LPH264_DVR_CLIENTINFO
        {
            public int nChannel;	//通道号
            public int nStream;	//0表示主码流，为1表示子码流
            public int nMode;		//0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
            public int nComType;	//只对组合编码通道有效, 组合编码通道的拼图模式
            public IntPtr hWnd;
        };

        public struct PACKET_INFO_EX
        {
            public Int32 nPacketType;				// 包类型,见MEDIA_PACK_TYPE
            public string pPacketBuffer;				// 缓存区地址
            public UInt32 dwPacketSize;				// 包的大小

            // 绝对时标
            public Int32 nYear;						// 时标:年		
            public Int32 nMonth;						// 时标:月
            public Int32 nDay;						// 时标:日
            public Int32 nHour;						// 时标:时
            public Int32 nMinute;					// 时标:分
            public Int32 nSecond;					// 时标:秒
            public UInt32 dwTimeStamp;					// 相对时标低位，单位为毫秒
            public UInt32 dwTimeStampHigh;        //相对时标高位，单位为毫秒
            public UInt32 dwFrameNum;             //帧序号
            public UInt32 dwFrameRate;            //帧率
            public UInt16 uWidth;              //图像宽度
            public UInt16 uHeight;             //图像高度
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public UInt32[] Reserved;            //保留
        } ;

        //[StructLayout(LayoutKind.Explicit,Size = 4)]
        public struct CONFIG_IPAddress
        {	//IP addr
            //[FieldOffset(0)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Byte[] c;
            //[FieldOffset(0)]
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            //public UInt16[] s;
            //[FieldOffset(0)]
            //public UInt32 l;
        };

        // 告警中心消息内容
        public struct SDK_NetAlarmCenterMsg
        {
            public CONFIG_IPAddress HostIP;		///< 设备IP
            public Int32 nChannel;                   ///< 通道
            public Int32 nType;                      ///< 类型 见AlarmCenterMsgType
            public Int32 nStatus;                    ///< 状态 见AlarmCenterStatus
            public SDK_SYSTEM_TIME Time;           ///< 发生时间
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sEvent;                  ///< 事件
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sSerialID;               ///< 设备序列号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDescrip;                ///< 描述
        };


        public enum SDK_LoalPlayAction
        {
            SDK_Local_PLAY_PAUSE,		    /*<! 暂停播放*/
	        SDK_Local_PLAY_CONTINUE,		/*<! 继续正常播放*/
	        SDK_Local_PLAY_FAST,	        /*<! 加速播放*/
	        SDK_Local_PLAY_SLOW,	        /*<! 减速播放*/	

        }
        //录像文件信息 查询条件结构体
        public struct H264_DVR_FINDINFO
        { 
            public Int32 nChannelN0;			//通道号
	        public Int32 nFileType;			//文件类型, 见SDK_File_Type
	        public H264_DVR_TIME startTime;	//开始时间
	        public H264_DVR_TIME endTime;	//结束时间
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	        public string szCard;		//卡号
	        public IntPtr hWnd;
        }

        public enum SDK_File_Type 
        {
            SDK_RECORD_ALL = 0,
            SDK_RECORD_ALARM = 1, //外部报警录像
            SDK_RECORD_DETECT,	  //视频侦测录像
            SDK_RECORD_REGULAR,	  //普通录像
            SDK_RECORD_MANUAL,	  //手动录像
            SDK_PIC_ALL = 10,
            SDK_PIC_ALARM,		  //外部报警录像
            SDK_PIC_DETECT,		  //视频侦测录像
            SDK_PIC_REGULAR,      //普通录像
            SDK_PIC_MANUAL,       //手动录像
            SDK_TYPE_NUM

        }
        public struct H264_DVR_TIME
        {
            public Int32 dwYear;		//年
            public Int32 dwMonth;	    //月
            public Int32 dwDay;		    //日
            public Int32 dwHour;		//时
            public Int32 dwMinute;	    //分
            public Int32 dwSecond;	    //秒
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct H264_DVR_FILE_DATA 
        { 
            public Int32 ch;						//通道号
	        public Int32 size;					//文件大小
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 108)]
	        public string sFileName;		///< 文件名
	        public SDK_SYSTEM_TIME stBeginTime;	///< 文件开始时间
            public SDK_SYSTEM_TIME stEndTime;		///< 文件结束时间
	        public IntPtr hWnd;
        }

        public struct SDK_SearchByTimeInfo
        {
            Int32 iChannel;			    ///< 录像通道号
	        ///< 录像记录用720个字节的5760位来表示一天中的1440分钟
	        ///< 0000:无录像 0001:F_COMMON 0002:F_ALERT 0003:F_DYNAMIC 0004:F_CARD 0005:F_HAND
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 720)] 
	        public string cRecordBitMap;
        }
        public struct SDK_SearchByTimeResult
        { 
            Int32 nInfoNum;										 ///< 通道的录像记录信息个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_MAX_CHANNUM)]
	        public SDK_SearchByTimeInfo[] ByTimeInfo;    ///< 通道的录像记录信息
        }
        //按时间段查询
        public struct SDK_SearchByTime
        {
            public Int32 nHighChannel;			///< 33~64录像通道号掩码
            public Int32 nLowChannel;			///< 1~32录像通道号掩码
            public Int32 nFileType;              ///< 文件类型, 见SDK_File_Type
            public SDK_SYSTEM_TIME stBeginTime;	    ///< 查询开始时间
            public SDK_SYSTEM_TIME stEndTime;		///< 查询结束时间
            public Int32 iSync;               ///< 是否需要同步
        }
        //回放动作
        public enum SEDK_PlayBackAction
        {
            SDK_PLAY_BACK_PAUSE,		/*<! 暂停回放 */
            SDK_PLAY_BACK_CONTINUE,		/*<! 继续回放 */
            SDK_PLAY_BACK_SEEK,			/*<! 回放定位，时间s为单位 */
            SDK_PLAY_BACK_FAST,	        /*<! 加速回放 */
            SDK_PLAY_BACK_SLOW,	        /*<! 减速回放 */
            SDK_PLAY_BACK_SEEK_PERCENT, /*<! 回放定位百分比 */
        }
        // 抓图
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_CatchPic(Int32 lLoginID, int nChannel, string sFileName);

        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_CatchPicInBuffer(Int32 lLoginID, int nChannel, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, int nBufLen, ref UInt32 pPicLen);

        //SDK初始化
        public delegate void fDisConnect(int lLoginID, string pchDVRIP, int nDVRPort, uint dwUser);
        [DllImport("NetSdk.dll", CharSet = CharSet.Auto)]
        public static extern Int16 H264_DVR_Init(fDisConnect cbDisConnect, IntPtr dwUser);

        ////SDK退出清理
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_Cleanup();

        // 设置连接设备超时时间和尝试次数
        //nWaitTime:单位ms不设置时默认5000ms,
        //nTryTimes:次数,不设置时默认3次
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_SetConnectTime(Int32 nWaitTime, Int32 nTryTimes);

        //向设备注册
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_Login(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword,
                                       ref LPH264_DVR_DEVICEINFO lpDeviceInfo, ref int error);

        //向设备注册扩展接口
        //增加登陆类型 0==web 1 ==升级工具 2 == 搜索工具
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_LoginEx(string sDVRIP, UInt16 wDVRPort, string sUserName, string sPassword,
                                       ref LPH264_DVR_DEVICEINFO lpDeviceInfo, int nType, ref int error);

        //向设备注销
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_Logout(Int32 lLoginID);

        //打开实时预览
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_RealPlay(Int32 lLoginID, ref LPH264_DVR_CLIENTINFO lpClientInfo);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StopRealPlay(Int32 lRealHandle);
        [DllImport("NetSdk.dll")]
        public static extern long H264_DVR_PauseRealPlay(Int32 lRealHandle, bool bPause);

        //设置回调函数，用户自己处理客户端收到的数据
        //注意 unsigned char * 转为 IntPtr
        public delegate void fRealDataCallBack(Int32 lRealHandle, Int32 dwDataType, IntPtr pBuffer, uint lbufsize, Int32 dwUser);
        public delegate void fRealDataCallBack_V2(Int32 lRealHandle, PACKET_INFO_EX pFrame, UInt32 dwUser);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_SetRealDataCallBack(Int32 lRealHandle, fRealDataCallBack cbRealData, Int32 dwUser);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_SetRealDataCallBack_V2(Int32 lRealHandle, fRealDataCallBack_V2 cbRealData, Int32 dwUser);

        //清除回调函数,该函数需要在H264_DVR_StopRealPlay前调用
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_DelRealDataCallBack(Int32 lRealHandle, fRealDataCallBack cbRealData, Int32 dwUser);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_DelRealDataCallBack_V2(Int32 lRealHandle, fRealDataCallBack_V2 cbRealData, Int32 dwUser);

        //获取错误信息
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_GetLastError();


        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_InputData(Int32 nPort, IntPtr pBuf, uint nSize);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_InputVideoData(Int32 nPort, IntPtr pBuf, uint nSize);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_OpenStream(Int32 nPort, [MarshalAs(UnmanagedType.LPArray)] byte[] pFileHeadBuf, uint nSize, uint nBufPoolSize);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_SetStreamOpenMode(Int32 nPort, uint nMode);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_Play(Int32 nPort, IntPtr hWnd);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_Stop(Int32 nPort);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_CloseStream(Int32 nPort);
        [DllImport("H264Play.dll")]
        public static extern uint H264_PLAY_GetLastError(Int32 nPort);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_CatchPic(Int32 nPort, string sFileName);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_InitDDrawDevice(IntPtr hWnd);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_InitDDrawDevice();
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_ReleaseDDrawDevice();

        public delegate void DisplayCallBack(int nPort, IntPtr pBuf, int nSize, int nWidth, int nHeight, int nStamp, int nType, int nUser);
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_SetDisplayCallBack(int nPort, DisplayCallBack pProc, int nUser);

        //设置参数,nChannelNO:配置通道号，-1表示所有通道
        //dwCommand 配置类型 SDK_CONFIG_TYPE: E_SDK_COMFIG_ABILITY_COMMPRO,		//串口协议        SDK_COMMFUNC
        //lpInBuffer 存放输入参数的缓冲区, 根据不同的类型, 输入不同的配置结构
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_SetDevConfig(Int32 lLoginID, UInt32 dwCommand, int nChannelNO, IntPtr lpInBuffer, UInt32 dwInBufferSize, int waittime = 1000);
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_GetDevConfig(Int32 lLoginID, UInt32 dwCommand, int nChannelNO, IntPtr lpOutBuffer, UInt32 dwOutBufferSize, ref uint lpBytesReturned, int waittime = 1000);
        //透明232,485
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_SerialWrite(Int32 lLoginID, SERIAL_TYPE nType, IntPtr pBuffer, int nBufLen);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_SerialRead(Int32 lLoginID, SERIAL_TYPE nType, IntPtr pBuffer, int nBufLen, IntPtr pReadLen);

        // 透明串口回调函数原形
        public delegate void fTransComCallBack(Int32 lLoginID, int lTransComType, IntPtr pBuffer, UInt32 dwBufSize, UInt32 dwUser);
        // 创建透明串口通道
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_OpenTransComChannel(Int32 lLoginID, IntPtr TransInfo, fTransComCallBack cbTransCom, UInt32 lUser);
        //关闭透明串口通道
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_CloseTransComChannel(Int32 lLoginID, SERIAL_TYPE nType);
        //消息
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StartAlarmCenterListen(int nPort, fMessCallBack cbAlarmCenter, UInt32 dwDataUser);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StopAlarmCenterListen();
        //消息（报警）回调原形
        public delegate bool fMessCallBack(Int32 lLoginID, IntPtr pBuf,
                                       UInt32 dwBufLen, Int32 dwUser);
                                      
        //云台控制，默认是开启云台，速度4，用户也可以自行设置速度
        //PTZCommand 对应为PTZ_ControlType枚举类型
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_PTZControl(Int32 lLoginID, int nChannelNo, Int32 lPTZCommand, bool bStop = false, Int32 lSpeed = 4);
				
        //扩展云台控制，包括预置点设置，巡航路线，快速定位等等
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_PTZControlEx(Int32 lLoginID, int nChannelNo, Int32 lPTZCommand,
                                            Int32 lParam1, Int32 lParam2, Int32 lParam3, bool bStop = false);
				                                       
        //打开通道音频 //预览，回放，本地播放 一个函数
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_OpenSound(long lHandle);
        //关闭通道音频 //预览，回放，本地播放 一个函数
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_CloseSound(long lHandle);                                       
        //打开对讲
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_StartLocalVoiceCom(long lLoginID);                                       
        //语音对讲，负责数据传发接口
        //public static extern Int32 H264_DVR_StartVoiceCom_MR(long lLoginID, pfAudioDataCallBack pVcb, long dwDataUser);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_VoiceComSendData(long lVoiceHandle, IntPtr pSendBuf, long lBufSize);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StopVoiceCom(long lVoiceHandle);

        //设置对讲音频编码方式，用户可以不设置，默认为G711A编码
        //public static extern bool H264_DVR_SetTalkMode(long lLoginID, SDK_AudioInFormatConfig* pTalkMode);                                                                 
        // lRecordType录像模式, 见SDK_RecordModeTypes
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StartDVRRecord(long lLoginID, int nChannelNo ,long lRecordType);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StopDVRRecord(long lLoginID, int nChannelNo); 
				
        //开始本地录像
        [DllImport("NetSdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool H264_DVR_StartLocalRecord(Int32 lRealHandle,string szSaveFileName,long type=0);
        //关闭本地录像
        [DllImport("NetSdk.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool H264_DVR_StopLocalRecord(Int32 lRealHandle);
        //播放本地视频文件	         
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_StartLocalPlay(string pFileName, IntPtr hWnd,fPlayDrawCallBack drawCallBack,Int32 user = 0);
        public delegate void fPlayDrawCallBack(Int32 lPlayHand, IntPtr hDc, Int32 nUser);

        //停止PC端播放
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StopLocalPlay(Int32 lPlayHandle);
        //本地文件播放结束回调
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_SetFileEndCallBack(Int32 lPlayHandle, fLocalPlayFileCallBack callBack,Int32 user);

        public delegate void fLocalPlayFileCallBack(Int32 lPlayHand,Int32 nUser);
        //设置信息帧回调
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_SetInfoFrameCallBack(Int32 lPlayHand, InfoFramCallBack callback, Int32 user);

        public delegate void InfoFramCallBack(Int32 lPlayHand, Int32 nType, String pBuf, Int32 nSize, Int32 nUser);
        //pc端文件播放控制
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_LocalPlayCtrl(Int32 lPlayHand, SDK_LoalPlayAction action, Int32 lCtrlValue);
        //查询录像文件
        [DllImport("NetSdk.dll",EntryPoint="H264_DVR_FindFile")]
        public static extern Int32 H264_DVR_FindFile(Int32 lLoginID, ref H264_DVR_FINDINFO lpFindInfo,IntPtr lpFileData, Int32 lMaxCount, ref int findcount, Int32 waittime = 5000);
        //设置回放或本地播放的播放进度
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_SetPlayPos(Int32 lPlayHandle, Single fRelativPos);
        //获取回放或本地播放的播放进度
        [DllImport("NetSdk.dll")]
        public static extern Single H264_DVR_GetPlayPos(Int32 lPlayHandle);
        //按时间查询文件
        [DllImport("NetSdk.dll", EntryPoint = "H264_DVR_FindFileByTime")]
        public static extern Int32 H264_DVR_FindFileByTime(Int32 lLoginID, ref SDK_SearchByTime lpFindInfo, IntPtr lpFileData, Int32 waittime = 10000);
        public delegate void fDownLoadPosCallBack(Int32 lPlayHandle, Int32 lTotalSize, Int32 lDownLoadSize, Int32 dwUser);
        //按时间进行录像回放
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_PlayBackByTimeEx(Int32 lLoginID,IntPtr lpFindInfo, fRealDataCallBack fDownLoadDataCallBack, Int32 dwDataUser, fDownLoadPosCallBack cbDownLoadPos, Int32 dwPosUser);
        //网络回放暂停与恢复以及进度控制
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_PlayBackControl(Int32 lPlayHandle, SEDK_PlayBackAction lControlCode, Int32 lCtrlValue);
        //停止回放
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StopPlayBack(Int32 lPlayHandle);
        //网络回放,按文件名回放
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_PlayBackByName_V2(Int32 lLoginID,IntPtr sPlayBackFile, fDownLoadPosCallBack cbDownLoadPos, fRealDataCallBack_V2 fDownLoadDataCallBack, Int32 dwDataUser);
        //录像下载,用户可以不使用回调，自己通过H264_DVR_GetDownloadPos获取进度
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_GetFileByName(Int32 lLoginID,IntPtr sPlayBackFile,string sSavedFileName, 
											fDownLoadPosCallBack cbDownLoadPos, Int32 dwDataUser);
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_GetFileByTime(Int32 lLoginID, ref H264_DVR_FINDINFO lpFindInfo, string sSavedFileDIR, bool bMerge,fDownLoadPosCallBack cbDownLoadPos, Int32 dwDataUser);
        [DllImport("NetSdk.dll")]
        public static extern bool H264_DVR_StopGetFile(Int32 lFileHandle);
        [DllImport("NetSdk.dll")]
        public static extern Int32 H264_DVR_GetDownloadPos(Int32 lFileHandle);
    }
}
