#ifndef P_CLIENT_NET_SDK
#define P_CLIENT_NET_SDK



#ifdef WIN32
#include <Windows.h>

#ifdef PCLIENTSDK_EXPORTS
#define P_CLIENT_API  __declspec(dllexport) 
#else
#define P_CLIENT_API  __declspec(dllimport)
#endif

#define CALLBACK __stdcall
#define CALL_METHOD  __stdcall  //__cdecl

#else	//linux

#define P_CLIENT_API	extern "C"
#define CALL_METHOD
#define CALLBACK

#define NULL	0
#define HWND	void*

#endif

#include <list>


#if !defined(uint64)
#ifdef WIN32
typedef unsigned __int64	uint64;
#else
typedef unsigned long long	uint64;
#endif
#endif


#define P_CLIENT_MAX_ID_LEN				32		//ID号长度
#define P_CLIENT_NAME_LEN				256

#define P_CLIENT_SUCCESS				0	//成功
#define P_CLIENT_UNKNOW_ERROR			-1	//未知错误
#define P_CLIENT_LOGIN_TIMEOUT			-2	//登陆超时
#define P_CLIENT_NO_USER				-3	//没有当前用户
#define P_CLIENT_PWD_ERROR				-4	//密码错误
#define P_CLIENT_HAS_LOGIN				-5	//用户已被其他地方登陆
#define P_CLIENT_CONNECT_ERROR			-6	//连接服务器失败
#define P_CLIENT_GET_GROUP_TIMEOUT		-7	//获取权限组列表超时
#define P_CLIENT_GET_ORG_TIMEOUT		-8	//获取权限组信息超时
#define P_CLIENT_PARSE_ERROR			-9	//文件解析出错，或许是客户端和服务器版本不匹配
#define P_CLIENT_NO_GROUP				-10	//当前用户没有任何的权限
#define P_CLIENT_INVALID_USERID			-11	//无效用户id，属于异常信息
#define P_CLIENT_NO_SESSION				-12	//用户连接不存在，属于异常信息
#define P_CLIENT_USER_LOCKED			-13	//用户被锁定
#define P_CLIENT_SETCONFIG_FAILED		-14	//保存配置失败

#define P_CLIENT_NO_INIT				-50	//没有初始化
#define P_CLIENT_NO_LOGIN				-51	//未登陆
#define P_CLIENT_INPUT_PARAM_ERROR		-52	//无效的参数
#define P_CLIENT_NO_DEVICE				-53	//设备部存在


#define P_CLIENT_REAL_RTSP_ERROR					-100	//连接流媒体失败
#define P_CLIENT_REAL_LOGIN_DEVICE_ERROR			-101	//流媒体连接设备失败
#define P_CLIENT_REAL_OPENVIDEO_ERROR				-102	//流媒体连接视频失败
#define P_CLIENT_REAL_RTP_LISTEN_ERROR				-103	//pc侦听rtp失败
#define P_CLIENT_REAL_RTP_CONNECT_ERROR				-104	//pc连接流媒体视频端口失败
#define P_CLIENT_REAL_PORT_ERROR					-105	//pc解码端口失败，可能是超过范围
#define P_CLIENT_REAL_MTS_EXCEPTION					-106	//MTS异常断线
#define P_CLIENT_REAL_TIME_OUT						-107	//等待超时
#define P_CLIENT_REAL_NOT_FOUND						-108	//没找到通道，或许是设备没有登录成功，或许服务器没添加设备
#define P_CLIENT_REAL_INVALID_PROTOCOL				-109	//错误的协议类型，目前只有tcp和udp
#define P_CLIENT_REAL_OPENREAL_ERROR				-110	//打开视频失败
#define P_CLIENT_REAL_NOT_FOUND_VIDEO				-111	//没有发现当前视频
#define P_CLIENT_REAL_RTP_EXCEPTION					-112	//连接异常，可能是流媒体已经删除了当前视频通道
#define P_CLIENT_REAL_NO_RIGHT						-113	//没有预览权限
#define P_CLIENT_REAL_DEV_OFFLINE					-114	//设备不在线
#define P_CLIENT_REAL_LOGINDEV_FAILED				-115	//设备登陆失败，


#define P_CLIENT_PLAYBACK_INVLAID_TIME				-200	//无效的时间
#define P_CLIENT_PLAYBACK_INVLAID_SOURCE			-201	//无效的来源
#define P_CLIENT_PLAYBACK_INVLAID_TYPE				-202	//无效的类型
#define P_CLIENT_PLAYBACK_INVALID_PARAM				-203	//无效参数
#define P_CLIENT_PLAYBACK_NO_CAMERA					-204	//摄像头不存在
#define P_CLIENT_PLAYBACK_NO_RIGHT					-205	//没有权限
#define P_CLIENT_PLAYBACK_TIME_OUT					-206	//超时
#define P_CLIENT_PLAYBACK_LOGIN_FAILED				-207	//登陆设备失败
#define P_CLIENT_PLAYBACK_QUERY_FAILED				-208	//查询失败
#define P_CLIENT_PLAYBACK_NO_VOD					-209	//没有点播服务
#define P_CLIENT_PLAYBACK_NO_RECORD					-210	//没有录像文件
#define P_CLIENT_PLAYBACK_SS_EXCEPTION				-211	//回放服务异常断线
#define P_CLIENT_PLAYBACK_OPENFILE_ERRPR			-212	//打开文件失败
#define P_CLIENT_PLAYBACK_WRITEFILE_ERRPR			-213	//写文件失败
#define P_CLIENT_PLAYBACK_EXCEPTION					-214	//回放异常断线
#define P_CLIENT_PLAYBACK_CONNECT_ERROR				-215	//连接回放服务失败
#define P_CLIENT_PLAYBACK_SERVER_ACK_ERROR			-216	//服务应答失败，可能是连接设备错误
#define P_CLIENT_PLAYBACK_CONNECT_RTP_ERROR			-217	//连接媒体通道失败
#define P_CLIENT_PLAYBACK_HAS_OPEND					-218	//已经处于回放状态
#define P_CLIENT_PLAYBACK_NO_DEVICE					-219	//找不到设备
#define P_CLIENT_PLAYBACK_LIMIT_DEV					-220	//到达设备回放上限
#define P_CLIENT_PLAYBACK_LIMIT_SYS					-221	//到达系统回放上限
#define P_CLIENT_PLAYBACK_HAS_OPENED_OTHER			-222	//当前通道被其他客户端占用
#define P_CLIENT_PLAYBACK_FAILED_DEV				-223	//回放失败，SDK返回



#define P_CLIENT_TALK_EXISIT						-300	//已经有设备在对讲，请先关闭原先的
#define P_CLIENT_TALK_EXCEPTION						-301	//对讲异常，可能是设备已经被流媒体删除
#define P_CLIENT_TALK_NOT_FOUND						-302	//对讲不存在
#define P_CLIENT_TALK_NOT_DEVICE					-303	//服务器没有添加设备或者设备未登录
#define P_CLIENT_TALK_AUDIO_COLLECT_ERROR			-304	//对讲音频采集失败
#define P_CLIENT_TALK_OPEN_PORT_ERROR				-305	//打开对讲解码端口失败
#define P_CLIENT_TALK_RTSP_ERROR					-306	//连接流媒体失败
#define F_CLIENT_TALK_LOGIN_FAILED					-307	//登陆设备失败，直连时候使用


#define P_CLIENT_PTZ_NOT_SUPPORT					-400	//不支持的命令
#define P_CLIENT_CFG_INVALID_PARAM					-401	//输入配置参数错误
#define P_CLIENT_CFG_NO_DMS							-402	//设备授权失败
#define P_CLIENT_CFG_TIMEOUT						-403	//配置超时
#define P_CLIENT_CFG_NOT_MATCH						-404	//不匹配，一般是缓冲长度不够了
#define P_CLIENT_PTZ_NO_TOUR						-405	//没有巡航点
#define P_CLIENT_PTZ_NO_TOUR_PRESET					-406	//巡航点中没有预置点
#define P_CLIENT_PTZ_IS_RUNNINT						-407	//巡航点正在运行中
#define P_CLIENT_PTZ_YIMEOUT						-408	//云台配置超时
#define P_CLIENT_PTZ_SAVE_FALIED					-409	//云台配置失败
#define P_CLIENT_PTZ_OCCUPT							-410	//云台被其他用户占用


#define P_CLIENT_RECORD_NO_DEVICE					-500	//找不到设备
#define P_CLIENT_RECORD_OFFLINE						-501	//设备不在线
#define P_CLIENT_RECORD_HAS_START					-502	//已经在录像
#define P_CLIENT_RECORD_SYSTEM_LIMIT				-503	//到达系统上限
#define P_CLIENT_RECORD_NO_SS						-504	//没有存储服务
#define P_CLIENT_RECORD_NO_MTS						-505	//流媒体服务异常


enum P_Client_TalkMode
{
	enmu_P_Client_Mode = 0,	//客户端模式 sdk内部采集音频发送到设备端
	enmu_P_Server_Mode		//服务器模式 客户外部采集，通过P_Client_SendTalkDataToDevice发送
};

enum enum_P_Client_Msg
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

enum enum_P_Client_Command_Type
{
	enum_P_Client_Cmd_Capture,					//抓图
	enum_P_Client_Cmd_Emap						//电子地图
};

enum enum_P_Client_AlarmMsg
{
	enum_P_Client_ALARM_Unknown = 0,		//未知
	enum_P_Client_ALARM_VIDEO_LOST,			//视频丢失
	enum_P_Client_ALARM_EXTERNAL_ALARM,		//外部报警
	enum_P_Client_ALARM_MOTION_DETECT,		//移动侦测
	enum_P_Client_ALARM_VIDEO_SHELTER,		//视频遮挡
	enum_P_Client_ALARM_DISK_FULL,			//硬盘满
	enum_P_Client_ALARM_DISK_FAULT,			//硬盘故障
};

//设备授权失败
enum enum_P_Client_Grant_Reason
{
	enum_P_Client_Connect_Dms_Failed = 1,
	enum_P_Client_No_Dms_To_Use,						//没有DMS可用
	enum_P_Client_Dev_Grant_Changed,					//设备授权更改到其他服务
	enum_P_Client_Add_Dvr_Failed,						//添加DVR失败
	enum_P_Client_Grant_Failed,							//授权到DMS失败
	enum_P_Client_No_Device,
	enum_P_Client_Grant_Session_Exist,					//授权会话已存在
};

//视频异常原因
enum enum_P_Video_Exception
{
	enum_P_Client_Video_MTS_Exception = 0,			//流媒体异常
	enum_P_Client_Video_Right_Canceled,				//权限被取消
	enum_P_Client_Video_Dev_Removed,				//设备被移除
	enum_P_Client_Video_Session_Removed,			//被流媒体关闭
	enum_P_Client_Video_Vistor_Disc,				//直连设备断线
};

//云台命令
enum P_CLIENT_PTZCommand                 //各个按键编号
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

//录像来源
enum enum_P_Record_Source
{
	enum_P_Client_Source_Platform = 0,			//录像来源平台
	enum_P_Client_Source_Device,				//录像来源设备
};

//录像类型
enum enum_P_Record_Type
{
	enum_P_Client_Record_All = 0,		//录像来源平台
	enum_P_Client_Record_Alarm,			//外部报警录像
	enum_P_Client_Record_Motion,		//动态监测录像

	enum_P_Client_Record_REGULAR,		//普通录像
	enum_P_Client_Record_MANUAL,		//手动录
	enum_P_Client_PIC_ALL,				//所有图片
	enum_P_Client_PIC_ALARM,			//外部报警图片
	enum_P_Client_PIC_DETECT,			//视频侦测图片
	enum_P_Client_PIC_REGULAR,			//普通图片
	enum_P_Client_PIC_MANUAL,			//手动图片
	enum_p_Client_Track = 10,			//轨迹回放，对于车载
};
//录像控制类型
enum enum_P_Playback_Control
{
	enum_P_Client_Playback_Normal = 0,			//正常速度播放
	enum_P_Client_Playback_Fast,				//快进
	enum_P_Client_Playback_Slow,				//慢放
	enum_P_Client_Playback_Pause,				//暂停
	enum_P_Client_Playback_Resume,				//暂停恢复
	enum_P_Client_Playback_PrevFrame,			//前一帧
	enum_P_Client_Playback_NextFrame			//下一帧
};

//设备远程配置类型
enum enum_P_Device_Config_Type
{
	enum_P_Device_Encode = 0,			//视频质量（码流、分辨率、帧数、主副通道等）P_Client_CONFIG_ENCODE_SIMPLIIFY
	enum_P_Device_PTZ,					//云台配置
	enum_P_Device_CHANNEL,				//通道名
	enum_P_Device_RECORD,				//录像计划
	enum_P_Device_ARSP,					//主动注册服务器功能
	enum_P_Device_MOTION,				//移动侦测配置
	enum_P_Device_LOSS,					//移动丢失配置
	enum_P_Device_SHELL,				//移动遮挡配置
	enum_P_Device_ALARMIN,				//报警输入配置
	enum_P_Device_ALARMOUT,				//报警输出控制 SDK_AlarmOutConfigAll
	enum_P_Device_GPSInfor=11,			//gps信息	   P_Client_CarGPSInfor
	enum_P_Device_NetDigitChannel,		//解码器，数字通道配置 P_Client_NetDecorderConfigAll

	enum_P_Device_ScreemDiv	,			//设备监视器分屏数 P_Client_DeviceScreenDiv
	enum_P_Device_ConfigNum
};

//访问模式
enum enum_P_Visit_Type
{
	enum_P_platform = 0,	//通过平台中转
	enum_P_DevVisit,		//直连设备
};

enum enum_P_DevScreenDiv
{
	enum_P_SPLIT1=1,    // 单画面
	enum_P_SPLIT4,      // 四画面
	enum_P_SPLIT8,      // 八画面
	enum_P_SPLIT9,      // 九画面
	enum_P_SPLIT16,     // 16画面
	enum_P_SPLIT25,		// 25画面	
	enum_P_SPLIT36     // 36画面
};

// 实时监视数据回调函数原形
typedef void (CALLBACK *pfRealDataCallBack)(long lRealHandle, long dwDataType, char *pBuffer, long dwBufSize, long dwUser);
// 回放，下载结束回调信息
typedef void (CALLBACK *pPlayEndCallBack)(long lPlaybackHandle, int nParam, long dwUser);

//预览条件结构体
struct P_Client_RealInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//通道ID
	int				nStream;							//码流类型 1 主码流 2 辅码流
	HWND			hWnd;								//窗口句柄(为NULL时候内部不解码，回调原始数据给外部)
	pfRealDataCallBack realDataFunc;						//视频数据回调
	long			dwUser;								//用户参数
};

struct P_Client_TIME
{
	DWORD dwYear;		//年
	DWORD dwMonth;		//月
	DWORD dwDay;		//日
	DWORD dwHour;		//时
	DWORD dwMinute;		//分
	DWORD dwSecond;		//秒
};

// 录像查询条件
struct P_Client_QueryRecInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//通道ID
	int				nSource;							//来源	enum_P_Record_Source
	int				nRecordType;						//类型	enum_P_Record_Type
	char			szCardInfo[P_CLIENT_MAX_ID_LEN];	//卡号（类型为卡号录像时有效）
	P_Client_TIME	sStartTime;							//起始时间
	P_Client_TIME	sEndTime;							//结束时间
	int				nDevVisit;							//是否直连
};

//录像记录信息
struct P_Client_RecordInfo 
{
	char			source;							//来源类型	
	char			recordType;						//录像类型
	char			reserved[2];					//字节对齐填充
	P_Client_TIME	startTime;						//起始时间
	P_Client_TIME	endTime;						//结束时间
	char			name[P_CLIENT_NAME_LEN];		//录像名字（不同厂家对文件的标识不同）
	int				length;							//文件长度

	//下面是中心录像所需要的信息
	int				planId;							//录像计划ID
	int				ssId;							//存储服务ID
	char			diskId[P_CLIENT_MAX_ID_LEN];	//磁盘ID
	int				fileHandle;						//文件句柄

	char			devId[P_CLIENT_MAX_ID_LEN];		//支持报警录像回放，因为报警源与录像设备是分开的
	int				channelNo;
};

//文件回放条件结构体
struct P_Client_PlaybackInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//通道ID
	HWND			hWnd;								//窗口句柄(为NULL时候内部不解码，回调原始数据给外部)
	int				nDevVisit;							//是否直连
	pfRealDataCallBack playbackDataFunc;				//视频数据回调
	long			dwUser;								//用户参数
	int				nPlayType;							//0默认，1 轨迹回放
	std::list<P_Client_RecordInfo*> recInfo;
};
//文件下载条件结构体
struct P_Client_DownloadByFileInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//通道ID
	int				nDevVisit;							//是否直连
	pPlayEndCallBack posCallback;
	long			lPosUser;
};

//时间回放条件结构体
struct P_Client_PlayByTimeInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//通道ID
	HWND			hWnd;								//窗口句柄(为NULL时候内部不解码，回调原始数据给外部)
	int				nSource;							//来源，平台还是设备	enum_P_Record_Source
	int				nDevVisit;							//是否直连
	P_Client_TIME	sStartTime;							//开始时间
	P_Client_TIME	sEndTime;							//结束时间
	int				nType;								//录像类型	enum_P_Record_Type
	pfRealDataCallBack playbackDataFunc;				//视频数据回调
	long			dwUser;								//用户参数
};

//时间下载条件结构体
struct P_Client_DownLoadByTimeInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//通道ID
	int				nSource;							//来源，平台还是设备	enum_P_Record_Source
	int				nDevVisit;							//是否直连
	P_Client_TIME	sStartTime;							//开始时间
	P_Client_TIME	sEndTime;							//结束时间
	int				nType;								//录像类型	enum_P_Record_Type
	pPlayEndCallBack PosCallback;
	long			lPosUser;
};



struct P_Client_PrePoint
{
	unsigned char	code;				//预置点编号
	unsigned char	revered[3];			//revered
	char			name[32];			//名字
};
//文件下载条件结构体
struct P_Client_PrePointTotal
{
	int nNum;							//预置点个数
	P_Client_PrePoint point[256];		//最大256个预置点
};

//gps信息结构体
struct P_Client_GPS_node
{
	double  latitude; //经度
	double  longitude; //纬度
};

struct P_Client_CarGPSInfor
{
	P_Client_TIME	startTime;//查询的开始时间
	P_Client_TIME	endTime;//查询的结束时间
	int				numbers;//获取信息条数
	P_Client_GPS_node *nodeBuffer;//设置为NULL时候，numbers返回对应的数据条数
};

#define P_Client_NAME_PASSWORD_LEN	64
#define P_Client_MAX_DECORDR_CH		32
#define P_Client_MAX_CHANNUM 32

///< 解码器地址设置
struct P_Client_NetDecorderConfig
{
	bool Enable;						///< 是否开启
	char UserName[P_Client_NAME_PASSWORD_LEN];	///< DDNS类型名称, 目前有: JUFENG
	char PassWord[P_Client_NAME_PASSWORD_LEN];	///< 主机名
	char Address[P_Client_NAME_PASSWORD_LEN];
	int Protocol;
	int Port;							///< 解码器连接端口
	int Channel;						///< 解码器连接通道号
	int Interval;                       ///< 轮巡的间隔时间(s),0:表示永久
	char ConfName[P_Client_NAME_PASSWORD_LEN];	///<配置名称
	int DevType;						///<设备类型
	int StreamType;						///<连接的码流类型CaptureChannelTypes
};

/*解码器连接类型*/
enum P_Client_DecorderConnType
{
	P_Client_CONN_SINGLE = 0, 	/*单连接*/
	P_Client_CONN_MULTI = 1,		/*多连接轮巡*/
	P_Client_CONN_TYPE_NR,
};

/*数字通道的配置*/
struct P_Client_NetDigitChnConfig
{
	bool Enable;		/*数字通道是否开启*/		
	int ConnType;		/*连接类型，取DecoderConnectType的值*/
	int TourIntv;		/*多连接时轮巡间隔*/
	unsigned int SingleConnId;	/*单连接时的连接配置ID, 从1开始，0表示无效*/
	bool EnCheckTime;	/*开启对时*/
	P_Client_NetDecorderConfig NetDecorderConf[32]; /*网络设备通道配置表*/
	int nNetDeorde; // 有多少个
};

/*所有数字通道的配置*/
struct P_Client_NetDecorderConfigAll
{
	P_Client_NetDigitChnConfig DigitChnConf[P_Client_MAX_DECORDR_CH];
};


struct P_Client_VIDEO_FORMAT
{
	int		iCompression;			//  压缩模式 	
	int		iResolution;			//  分辨率 参照枚举SDK_CAPTURE_SIZE_t
	int		iBitRateControl;		//  码流控制 参照枚举SDK_capture_brc_t
	int		iQuality;				//  码流的画质 档次1-6		
	int		nFPS;					//  帧率值，NTSC/PAL不区分,负数表示多秒一帧		
	int		nBitRate;				//  0-4096k,该列表主要由客户端保存，设备只接收实际的码流值而不是下标。
	int		iGOP;					//  描述两个I帧之间的间隔时间，2-12 
};

struct P_Client_AUDIO_FORMAT
{
	int		nBitRate;				//  码流kbps	
	int		nFrequency;				//  采样频率	
	int		nMaxVolume;				//  最大音量阈值
};

// 简化版本编码配置
// 媒体格式
struct P_Client_MEDIA_FORMAT
{
	P_Client_VIDEO_FORMAT vfFormat;			//  视频格式定义 			
	P_Client_AUDIO_FORMAT afFormat;			//  音频格式定义 
	bool	bVideoEnable;				//  开启视频编码 
	bool	bAudioEnable;				//  开启音频编码 	
} ;


/// 编码设置
struct P_Client_CONFIG_ENCODE_SIMPLIIFY
{
	P_Client_MEDIA_FORMAT dstMainFmt;		//  主码流格式 	
	P_Client_MEDIA_FORMAT dstExtraFmt;	//  辅码流格式 
};

/// 全通道编码配置
struct P_Client_EncodeConfigAll_SIMPLIIFY
{
	P_Client_CONFIG_ENCODE_SIMPLIIFY vEncodeConfigAll[P_Client_MAX_CHANNUM];
};


struct P_Client_DeviceScreenDiv
{
	int iValue;
	int iState; // 0
};

/*
描述： 消息回调
参数： 
nType		异常类型，参见enum_P_Client_Msg
nType2		子类型，状态
szDeviceID	设备ID
channel		通道号
nParam1		保留  nType为预览，回放，对讲等请求时候，这里存在的就是请求的ID
pParam1		保留，可代表设备ID
lUser		用户参数
返回值：
*/
typedef void (CALLBACK *pfmessageCallBack)(enum_P_Client_Msg enumType, int nType2, char *szDeviceID, int channel, int nParam1, char* pParam1, long dwUser);

typedef void (CALLBACK *pAlarmMessageCallBack)(enum_P_Client_AlarmMsg enumType, char *szDeviceID, int channel, int nStatus, int nParam1, long dwUser);

//语音对讲数据回调
typedef void (CALLBACK *pfTalkDataCallBack)(char *pBuffer, long dwBufSize, long dwUser);

//透明通道数据回调
typedef void (CALLBACK *pfTransportDataCallBack)(char* szDeviceID, char *pBuffer, long dwBufSize, long dwUser);
//设备车载信息数据回调
typedef void (CALLBACK *pfUploadDataCallBack)(char* szDeviceID, int nType, char *pBuffer, long dwBufSize, long dwUser);


//获取版本号
P_CLIENT_API char* CALL_METHOD P_Client_Version();


//初始化
P_CLIENT_API int CALL_METHOD P_Client_Init(pfmessageCallBack callback, long dwUser);

//注销
P_CLIENT_API int CALL_METHOD P_Client_ClearUp();


//设置报警回调
P_CLIENT_API int CALL_METHOD P_Client_SetAlarmCallBack(pAlarmMessageCallBack callback, long dwUser);

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
P_CLIENT_API int CALL_METHOD P_Client_LoginServer(char *ip, int port, char* user, char* password, int waittime = 5000);


/*
描述：设置访问模式
参数：enum_P_Visit_Type

*/
P_CLIENT_API int CALL_METHOD P_Client_SetVisitType(int nType);

/*
描述：获取设备信息长度
返回：需要开辟的内存长度
*/
P_CLIENT_API int CALL_METHOD P_Client_GetDevicesLen();

/*
描述：获取设备信息
参数: 
pBuf	内存地址
nLen	内存长度
返回：	真正的数据长度
*/
P_CLIENT_API int CALL_METHOD P_Client_GetDevices(char *pBuf, int nLen);


/*
描述：获取设备状态
参数: 
deviceId	设备id
返回：	设备状态
*/
P_CLIENT_API int CALL_METHOD P_Client_GetDeviceStatus(char *deviceId);


/*
描述：获取其他的数据信息
参数: 
type	类型  1=轮询信息  2 电子地图信息 3 报警全局配置 4 =报警布撤防配置 5=报警联动配置 6 解码器上墙预案
pBuf	内存地址
nLen	内存长度
返回：	>0 代表真正的长度，=0代表本身没数据，< 0代表错误

说明：  第一次如果外部不知道实际长度，可以把pbuf传入null，nLen为0，这样内部不会赋值，而是直接返回真正长度，
第二次调用时候传入外部分配好的内存和长度，内部再赋值

*/
P_CLIENT_API int CALL_METHOD P_Client_GetDataInfo(int type, char *pBuf, int nLen);

/*
描述：设置其他的数据信息
参数: 
type	类型  1=轮询信息  2 电子地图信息 3 报警全局配置 4 =报警布撤防配置 5=报警联动配置 6 解码器上墙预案
pBuf	内存地址
nLen	内存长度
返回：	>=0 代表成功
*/
P_CLIENT_API int CALL_METHOD P_Client_SaveDataInfo(int type, char *pBuf, int nLen);

/*
描述：更新电子地图信息
	  由于电子地图由服务端配置，需要事实获取才有意义
返回：>=0 代表成功
	  具体是否获取成功看回调：enum_P_Client_Command->enum_P_Client_Cmd_Emap->nParam1
*/
P_CLIENT_API int CALL_METHOD P_Client_UpdateEmapInfo();


/*
描述： 预览视频
参数： pRealInfo  预览条件
返回值：>0 成功， 《=0 失败
（这里的成功只能说明命令发送成功，具体是否成功由回调决定）
*/
P_CLIENT_API int CALL_METHOD P_Client_ConnectRealPlay(P_Client_RealInfo *pRealInfo);

/*
描述: 停止预览
参数: realHandle 预览句柄  P_Client_ConnectRealPlay返回值
返回: >=0 成功
*/
P_CLIENT_API int CALL_METHOD P_Client_StopRealPlay(HWND  hWnd,bool bWait=false);
P_CLIENT_API int CALL_METHOD P_Client_StopRealPlayByID(int realHandle,bool bWait=false);

/*
描述: 云台控制
参数 
szCameraId	通道ID
返回值
*/
P_CLIENT_API int CALL_METHOD P_Client_PTZControl(char *szCameraId, int nCmd, int nParam1, int nParam2, int nParam3, bool bStop);


/*
描述：开始语音对讲
参数：szDevIp  设备地址
szDeviceID   设备ID
dataCallback 数据回调   客户端模式不用填写
nUser		 用户参数   客户端模式不用填写
返回值： 》0 成功
*/
P_CLIENT_API int CALL_METHOD P_Client_StartTalk(char *szDeviceID, pfTalkDataCallBack dataCallback, int nUser);

P_CLIENT_API int CALL_METHOD P_Client_StopTalk(int handle);


/*
描述：查询录像信息
参数：
返回：录像文件句柄
*/
P_CLIENT_API int CALL_METHOD P_Client_QueryRecord(P_Client_QueryRecInfo* pInfo);

/*
描述：查询一条录像信息
参数：
返回：1 成功，0 失败
*/
P_CLIENT_API int CALL_METHOD P_Client_GetNextRecord(int nQueryHandle, P_Client_RecordInfo* pRecInfo);

/*
描述：结束录像查询
参数：
nQueryHandle P_Client_QueryRecord的返回值
返回：
*/
P_CLIENT_API int CALL_METHOD P_Client_CloseQueryRecord(int nQueryHandle);


/*
描述：录像回放
参数：
szCameraID  摄像头ID
pRecInfo	录像信息
返回:>0成功，否则失败
*/
P_CLIENT_API int CALL_METHOD P_Client_PlaybackByFile(P_Client_PlaybackInfo* pPlayInfo, pPlayEndCallBack endCallback, long lPosUser);
P_CLIENT_API int CALL_METHOD P_Client_DownLoadByFile(P_Client_DownloadByFileInfo* pPlayInfo, char *szFileName, P_Client_RecordInfo* pRecInfo);


/*
描述：获取当前正在播放的时间,这个获取进度只在内部解码有效, 根据获取的时间，外部自己计算进度
参数：nHandle P_Client_PlaybackByFile返回值
pTime 获取到的时间
返回值 >=0成功;
*/
P_CLIENT_API int CALL_METHOD P_Client_GetPlayCurTime(HWND hWnd,   P_Client_TIME *pTime);
P_CLIENT_API int CALL_METHOD P_Client_GetPlayCurTimeByID(int nHandle, P_Client_TIME *pTime);

/*
描述：停止文件回放
参数：nHandle  P_Client_PlaybackByFile的返回值
*/
P_CLIENT_API int CALL_METHOD P_Client_StopPlaybackByFile(HWND hWnd);
P_CLIENT_API int CALL_METHOD P_Client_StopPlaybackByFileByID(int nHandle);
P_CLIENT_API int CALL_METHOD P_Client_StopDownloadByFile(int nHandle);

/*
描述：按时间回放
参数：
返回：》0 成功
*/
P_CLIENT_API int CALL_METHOD P_Client_PlaybackByTime(P_Client_PlayByTimeInfo* pPlayInfo, pPlayEndCallBack endCallback, long lUser);
P_CLIENT_API int CALL_METHOD P_Client_DownLoadByTime(P_Client_DownLoadByTimeInfo* pDoanloadInfo, char* szFileName);

/*
描述：停止时间回放
参数：nHandle  P_Client_PlaybackByTime的返回值
*/
P_CLIENT_API int CALL_METHOD P_Client_StopPlaybackByTime(HWND hWnd);
P_CLIENT_API int CALL_METHOD P_Client_StopPlaybackByTimeByID(int nHandle);
P_CLIENT_API int CALL_METHOD P_Client_StopDownloadByTime(int nHandle);

/*
描述：设置回放时间偏移
参数：nHandle  P_Client_PlaybackByTime的返回值
	 nTime   时间偏移，秒为单位
返回值：》=0成功，否则失败
*/
P_CLIENT_API int CALL_METHOD P_Client_SetPlayTimePos(HWND hWnd, int nTime);
P_CLIENT_API int CALL_METHOD P_Client_SetPlayTimePosByID(int nHandle, int nTime);

/*
描述：录像控制
参数：nHandle P_Client_PlaybackByTime或者P_Client_PlaybackByFile返回值
	  control 控制策略
返回值:
*/
P_CLIENT_API int CALL_METHOD P_Client_PlaybackControl(HWND hWnd, enum_P_Playback_Control control );
P_CLIENT_API int CALL_METHOD P_Client_PlaybackControlByID(int nHandle, enum_P_Playback_Control control );

/*
描述：抓图
参数：nHandle  P_Client_PlaybackByTime或者P_Client_PlaybackByFile或者P_Client_ConnectRealPlay返回值
      pFile	   文件，包含全路径和名称
*/
P_CLIENT_API int CALL_METHOD P_Client_Capture(HWND hWnd, char* pFile);

P_CLIENT_API int CALL_METHOD P_Client_OpenSound(HWND hWnd);
P_CLIENT_API int CALL_METHOD P_Client_CloseSound(HWND hWnd);

P_CLIENT_API int CALL_METHOD P_Client_StartLocalRecord(HWND hWnd, char* pFile);
P_CLIENT_API int CALL_METHOD P_Client_StopLocalRecord(HWND hWnd);


P_CLIENT_API int CALL_METHOD P_Client_SetVolume(HWND hWnd, int nVolume);
P_CLIENT_API int CALL_METHOD P_Client_SetVolume(int nHandle, int nVolume);


P_CLIENT_API int CALL_METHOD P_Client_GetVolume(HWND hWnd, int* nVolume);
P_CLIENT_API int CALL_METHOD P_Client_GetVolume(int nHandle, int* nVolume);


//设置亮度，色度
P_CLIENT_API int CALL_METHOD P_Client_AdjustColor(HWND hWnd, int nBrightness, int nContrast, int nSaturation, int nHue);
P_CLIENT_API int CALL_METHOD P_Client_GetColor(HWND hWnd, int *nBrightness, int *nContrast, int *nSaturation, int *nHue);

//局部放大
P_CLIENT_API int CALL_METHOD P_Client_SelectedRangeVideo(HWND hWnd, int nLeft, int nTop, int nRight, int nBottom, int nWidth, int nHeight, int nEnable);


/// 外部所使用的OSD相关定义
typedef enum 
{
	P_CLIENT_OSD_TXT_FONT_ARIAL = 1,
	P_CLIENT_OSD_TXT_FONT_SERIF,
	P_CLIENT_OSD_TXT_FONT_SANS,

	P_CLIENT_OSD_TXT_FONT_SIMSUN = 101,  // 中文字体，宋体
	P_CLIENT_OSD_TXT_FONT_SIMHEI
} P_CLIENT_OSD_TXT_FONT;

// OSD文字信息，位置和字体大小按CIF设置，随窗口缩放而缩放。（TODO：D1？）
typedef struct
{
	int pos_x; //只是相对位置，以窗口的百分比来表示
	int pos_y;
	COLORREF color;
	char text[256];
	P_CLIENT_OSD_TXT_FONT font_type; // OSD_TXT_FONT_XXX
	int font_size; //以窗口的百分比来表示
	int isBold; //是否粗体
	int isTransparent;//是否透明底色
	COLORREF bkColor;
} P_CLIENT_OSD_INFO_TXT;

//OSD叠加
P_CLIENT_API int CALL_METHOD P_Clinet_SetOSDEnalble(HWND hWnd, bool bEnable);
P_CLIENT_API int CALL_METHOD P_Client_SetOSDText(HWND hWnd, const P_CLIENT_OSD_INFO_TXT* osd_txt);

//设置流畅等级
/*
fluency   0 最实时的，基本用来演示
		  1 实时性,也是默认的
		  2 流畅等级1
		  3 流畅等级2
		  4 流畅等级3
		  5 流畅等级4
		  6 流畅等级5		没增加一级流畅，最大延时增加1秒，也就是最大延时5秒
*/
P_CLIENT_API int CALL_METHOD P_Client_SetFluency(HWND hWnd, int fluency);

//设置远程配置
/*
nType 设备配置类型，见enum_P_Device_Config_Type
*/
P_CLIENT_API int CALL_METHOD P_Client_SetDeviceConfig(int nType, char* szDeviceID, int nChannelNo, char *pConfigBuf, int nLen);
P_CLIENT_API int CALL_METHOD P_Client_GetDeviceConfig(int nType, char* szDeviceID, int nChannelNo, char *pConfigBuf, int nLen, int &nRetLen);


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
P_CLIENT_API int CALL_METHOD P_Client_OpenTransport(char* szDeviceID, int nType, int nBaudrate, int nDatabits, int nStopbits, int nParity );

//关闭透明通道
P_CLIENT_API int CALL_METHOD P_Client_CloseTransport(char* szDeviceID, int nType);

//设备透明通道数据回调
P_CLIENT_API int CALL_METHOD P_Client_SetTransPortCallBack(pfTransportDataCallBack callback, long dwUser);

//写入透明通道数据
P_CLIENT_API int CALL_METHOD P_Client_WriteTransportData(char* szDeviceID, int nType, char *pbuf, int nLen);


//开始中心录像
P_CLIENT_API int CALL_METHOD P_Client_StartCenterRecord(char* szCamereID);
P_CLIENT_API int CALL_METHOD P_Client_StopCenterRecord(char* szCamereID);


//设备车载信息数据回调
P_CLIENT_API int CALL_METHOD P_Client_SetUploadDataCallBack(pfUploadDataCallBack callback, long dwUser);
/*
//设置设备是否要接收主动上报的信息，例如车载GPS信息
szDeviceID 设备ID
nType	1 需要接收，0 不接收
*/
P_CLIENT_API int CALL_METHOD P_Client_SetDeviceUploadData(char* szDeviceID, int nType);


/*
获取当前视频的GPS信息
*/
P_CLIENT_API int CALL_METHOD P_Client_GetDeviceUploadData(HWND hWnd, char* buf, int len);


//获取摄像头预置点信息，只有在打开视频后才可以获取
P_CLIENT_API int CALL_METHOD P_Client_GetPrePonitData(char* cameraID, P_Client_PrePointTotal *preInfo);

//获取巡航点信息
//如果data为空，则返回需要的长度,目前固定7*1027
//如果不为空，则返回实际长度，data中赋值
//返回 小于等于0 是出错或者没有记录
P_CLIENT_API int CALL_METHOD P_Client_GetPTZTourData(char* cameraID, char *data);


P_CLIENT_API int CALL_METHOD P_Client_ModifyPassword(const char* szOldPsw, const char* szNewPsw);
#endif
