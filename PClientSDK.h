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


#define P_CLIENT_MAX_ID_LEN				32		//ID�ų���
#define P_CLIENT_NAME_LEN				256

#define P_CLIENT_SUCCESS				0	//�ɹ�
#define P_CLIENT_UNKNOW_ERROR			-1	//δ֪����
#define P_CLIENT_LOGIN_TIMEOUT			-2	//��½��ʱ
#define P_CLIENT_NO_USER				-3	//û�е�ǰ�û�
#define P_CLIENT_PWD_ERROR				-4	//�������
#define P_CLIENT_HAS_LOGIN				-5	//�û��ѱ������ط���½
#define P_CLIENT_CONNECT_ERROR			-6	//���ӷ�����ʧ��
#define P_CLIENT_GET_GROUP_TIMEOUT		-7	//��ȡȨ�����б�ʱ
#define P_CLIENT_GET_ORG_TIMEOUT		-8	//��ȡȨ������Ϣ��ʱ
#define P_CLIENT_PARSE_ERROR			-9	//�ļ��������������ǿͻ��˺ͷ������汾��ƥ��
#define P_CLIENT_NO_GROUP				-10	//��ǰ�û�û���κε�Ȩ��
#define P_CLIENT_INVALID_USERID			-11	//��Ч�û�id�������쳣��Ϣ
#define P_CLIENT_NO_SESSION				-12	//�û����Ӳ����ڣ������쳣��Ϣ
#define P_CLIENT_USER_LOCKED			-13	//�û�������
#define P_CLIENT_SETCONFIG_FAILED		-14	//��������ʧ��

#define P_CLIENT_NO_INIT				-50	//û�г�ʼ��
#define P_CLIENT_NO_LOGIN				-51	//δ��½
#define P_CLIENT_INPUT_PARAM_ERROR		-52	//��Ч�Ĳ���
#define P_CLIENT_NO_DEVICE				-53	//�豸������


#define P_CLIENT_REAL_RTSP_ERROR					-100	//������ý��ʧ��
#define P_CLIENT_REAL_LOGIN_DEVICE_ERROR			-101	//��ý�������豸ʧ��
#define P_CLIENT_REAL_OPENVIDEO_ERROR				-102	//��ý��������Ƶʧ��
#define P_CLIENT_REAL_RTP_LISTEN_ERROR				-103	//pc����rtpʧ��
#define P_CLIENT_REAL_RTP_CONNECT_ERROR				-104	//pc������ý����Ƶ�˿�ʧ��
#define P_CLIENT_REAL_PORT_ERROR					-105	//pc����˿�ʧ�ܣ������ǳ�����Χ
#define P_CLIENT_REAL_MTS_EXCEPTION					-106	//MTS�쳣����
#define P_CLIENT_REAL_TIME_OUT						-107	//�ȴ���ʱ
#define P_CLIENT_REAL_NOT_FOUND						-108	//û�ҵ�ͨ�����������豸û�е�¼�ɹ������������û����豸
#define P_CLIENT_REAL_INVALID_PROTOCOL				-109	//�����Э�����ͣ�Ŀǰֻ��tcp��udp
#define P_CLIENT_REAL_OPENREAL_ERROR				-110	//����Ƶʧ��
#define P_CLIENT_REAL_NOT_FOUND_VIDEO				-111	//û�з��ֵ�ǰ��Ƶ
#define P_CLIENT_REAL_RTP_EXCEPTION					-112	//�����쳣����������ý���Ѿ�ɾ���˵�ǰ��Ƶͨ��
#define P_CLIENT_REAL_NO_RIGHT						-113	//û��Ԥ��Ȩ��
#define P_CLIENT_REAL_DEV_OFFLINE					-114	//�豸������
#define P_CLIENT_REAL_LOGINDEV_FAILED				-115	//�豸��½ʧ�ܣ�


#define P_CLIENT_PLAYBACK_INVLAID_TIME				-200	//��Ч��ʱ��
#define P_CLIENT_PLAYBACK_INVLAID_SOURCE			-201	//��Ч����Դ
#define P_CLIENT_PLAYBACK_INVLAID_TYPE				-202	//��Ч������
#define P_CLIENT_PLAYBACK_INVALID_PARAM				-203	//��Ч����
#define P_CLIENT_PLAYBACK_NO_CAMERA					-204	//����ͷ������
#define P_CLIENT_PLAYBACK_NO_RIGHT					-205	//û��Ȩ��
#define P_CLIENT_PLAYBACK_TIME_OUT					-206	//��ʱ
#define P_CLIENT_PLAYBACK_LOGIN_FAILED				-207	//��½�豸ʧ��
#define P_CLIENT_PLAYBACK_QUERY_FAILED				-208	//��ѯʧ��
#define P_CLIENT_PLAYBACK_NO_VOD					-209	//û�е㲥����
#define P_CLIENT_PLAYBACK_NO_RECORD					-210	//û��¼���ļ�
#define P_CLIENT_PLAYBACK_SS_EXCEPTION				-211	//�طŷ����쳣����
#define P_CLIENT_PLAYBACK_OPENFILE_ERRPR			-212	//���ļ�ʧ��
#define P_CLIENT_PLAYBACK_WRITEFILE_ERRPR			-213	//д�ļ�ʧ��
#define P_CLIENT_PLAYBACK_EXCEPTION					-214	//�ط��쳣����
#define P_CLIENT_PLAYBACK_CONNECT_ERROR				-215	//���ӻطŷ���ʧ��
#define P_CLIENT_PLAYBACK_SERVER_ACK_ERROR			-216	//����Ӧ��ʧ�ܣ������������豸����
#define P_CLIENT_PLAYBACK_CONNECT_RTP_ERROR			-217	//����ý��ͨ��ʧ��
#define P_CLIENT_PLAYBACK_HAS_OPEND					-218	//�Ѿ����ڻط�״̬
#define P_CLIENT_PLAYBACK_NO_DEVICE					-219	//�Ҳ����豸
#define P_CLIENT_PLAYBACK_LIMIT_DEV					-220	//�����豸�ط�����
#define P_CLIENT_PLAYBACK_LIMIT_SYS					-221	//����ϵͳ�ط�����
#define P_CLIENT_PLAYBACK_HAS_OPENED_OTHER			-222	//��ǰͨ���������ͻ���ռ��
#define P_CLIENT_PLAYBACK_FAILED_DEV				-223	//�ط�ʧ�ܣ�SDK����



#define P_CLIENT_TALK_EXISIT						-300	//�Ѿ����豸�ڶԽ������ȹر�ԭ�ȵ�
#define P_CLIENT_TALK_EXCEPTION						-301	//�Խ��쳣���������豸�Ѿ�����ý��ɾ��
#define P_CLIENT_TALK_NOT_FOUND						-302	//�Խ�������
#define P_CLIENT_TALK_NOT_DEVICE					-303	//������û������豸�����豸δ��¼
#define P_CLIENT_TALK_AUDIO_COLLECT_ERROR			-304	//�Խ���Ƶ�ɼ�ʧ��
#define P_CLIENT_TALK_OPEN_PORT_ERROR				-305	//�򿪶Խ�����˿�ʧ��
#define P_CLIENT_TALK_RTSP_ERROR					-306	//������ý��ʧ��
#define F_CLIENT_TALK_LOGIN_FAILED					-307	//��½�豸ʧ�ܣ�ֱ��ʱ��ʹ��


#define P_CLIENT_PTZ_NOT_SUPPORT					-400	//��֧�ֵ�����
#define P_CLIENT_CFG_INVALID_PARAM					-401	//�������ò�������
#define P_CLIENT_CFG_NO_DMS							-402	//�豸��Ȩʧ��
#define P_CLIENT_CFG_TIMEOUT						-403	//���ó�ʱ
#define P_CLIENT_CFG_NOT_MATCH						-404	//��ƥ�䣬һ���ǻ��峤�Ȳ�����
#define P_CLIENT_PTZ_NO_TOUR						-405	//û��Ѳ����
#define P_CLIENT_PTZ_NO_TOUR_PRESET					-406	//Ѳ������û��Ԥ�õ�
#define P_CLIENT_PTZ_IS_RUNNINT						-407	//Ѳ��������������
#define P_CLIENT_PTZ_YIMEOUT						-408	//��̨���ó�ʱ
#define P_CLIENT_PTZ_SAVE_FALIED					-409	//��̨����ʧ��
#define P_CLIENT_PTZ_OCCUPT							-410	//��̨�������û�ռ��


#define P_CLIENT_RECORD_NO_DEVICE					-500	//�Ҳ����豸
#define P_CLIENT_RECORD_OFFLINE						-501	//�豸������
#define P_CLIENT_RECORD_HAS_START					-502	//�Ѿ���¼��
#define P_CLIENT_RECORD_SYSTEM_LIMIT				-503	//����ϵͳ����
#define P_CLIENT_RECORD_NO_SS						-504	//û�д洢����
#define P_CLIENT_RECORD_NO_MTS						-505	//��ý������쳣


enum P_Client_TalkMode
{
	enmu_P_Client_Mode = 0,	//�ͻ���ģʽ sdk�ڲ��ɼ���Ƶ���͵��豸��
	enmu_P_Server_Mode		//������ģʽ �ͻ��ⲿ�ɼ���ͨ��P_Client_SendTalkDataToDevice����
};

enum enum_P_Client_Msg
{
	enum_P_Client_CMS_Server_Status = 0,		//���ط���״̬���ڶ��ߺ������ɹ�ʱ����ʾ
	enum_P_Client_Dev_Grant_Result,				//�豸��Ȩ���,����ֻ��Ϊ��д��־
	enum_P_Client_Device_Status,				//�豸״̬
	enum_P_Client_Video_Exception,				//��Ƶ�쳣��nType2��enum_P_Video_Exception
	enum_P_Client_ReGetInfo_Failed,				//�������Ϣ���ĺ����»�ȡ����ʧ�ܣ���Ҫ��������
	enum_P_Client_User_Error,					//�û�����������ɾ�������˳�����
	enum_P_Client_Reload_DeviceData,			//�����»�ȡ�豸��Ϣ�������Ѿ�������������
	enum_P_Client_Reload_MapData,				//���»�ȡ��ͼ��Ϣ
	enum_P_Client_VideoRequest,					//Ԥ������ nType2Ϊ�ɹ���ʧ��ԭ��
	enum_P_Client_PlaybackRequest,				//�ط���������
	enum_P_Client_PlaybackClose,
	enum_P_Client_TalkRequest,					//�Խ�����
	enum_P_Client_PTZControl,					//��̨���� nType2�� P_CLIENT_PTZCommand

	enum_P_Client_Command						//����ص���������ץͼ��ɣ�Ҫ��򿪣�
};

enum enum_P_Client_Command_Type
{
	enum_P_Client_Cmd_Capture,					//ץͼ
	enum_P_Client_Cmd_Emap						//���ӵ�ͼ
};

enum enum_P_Client_AlarmMsg
{
	enum_P_Client_ALARM_Unknown = 0,		//δ֪
	enum_P_Client_ALARM_VIDEO_LOST,			//��Ƶ��ʧ
	enum_P_Client_ALARM_EXTERNAL_ALARM,		//�ⲿ����
	enum_P_Client_ALARM_MOTION_DETECT,		//�ƶ����
	enum_P_Client_ALARM_VIDEO_SHELTER,		//��Ƶ�ڵ�
	enum_P_Client_ALARM_DISK_FULL,			//Ӳ����
	enum_P_Client_ALARM_DISK_FAULT,			//Ӳ�̹���
};

//�豸��Ȩʧ��
enum enum_P_Client_Grant_Reason
{
	enum_P_Client_Connect_Dms_Failed = 1,
	enum_P_Client_No_Dms_To_Use,						//û��DMS����
	enum_P_Client_Dev_Grant_Changed,					//�豸��Ȩ���ĵ���������
	enum_P_Client_Add_Dvr_Failed,						//���DVRʧ��
	enum_P_Client_Grant_Failed,							//��Ȩ��DMSʧ��
	enum_P_Client_No_Device,
	enum_P_Client_Grant_Session_Exist,					//��Ȩ�Ự�Ѵ���
};

//��Ƶ�쳣ԭ��
enum enum_P_Video_Exception
{
	enum_P_Client_Video_MTS_Exception = 0,			//��ý���쳣
	enum_P_Client_Video_Right_Canceled,				//Ȩ�ޱ�ȡ��
	enum_P_Client_Video_Dev_Removed,				//�豸���Ƴ�
	enum_P_Client_Video_Session_Removed,			//����ý��ر�
	enum_P_Client_Video_Vistor_Disc,				//ֱ���豸����
};

//��̨����
enum P_CLIENT_PTZCommand                 //�����������
{
	P_CLIENT_PTZ_UP,                            //��
	P_CLIENT_PTZ_DOWN,							//��
	P_CLIENT_PTZ_LEFT,                          //��
	P_CLIENT_PTZ_RIGHT,                         //��
	P_CLIENT_PTZ_LEFTUP,                        //����
	P_CLIENT_PTZ_LEFTDOWN,                      //����
	P_CLIENT_PTZ_RIGHTUP,						//����
	P_CLIENT_PTZ_RIGHTDOWN,                     //����

	P_CLIENT_PTZ_ADD_ZOOM,						//�䱶+        //Camera operation ��ö��˳����ģ���ϵ��������
	P_CLIENT_PTZ_ADD_FOCUS,						//�佹+
	P_CLIENT_PTZ_ADD_APERTURE,                  //��Ȧ+
	P_CLIENT_PTZ_REDUCE_ZOOM,                   //�䱶-
	P_CLIENT_PTZ_REDUCE_FOCUS,                  //�佹-
	P_CLIENT_PTZ_REDUCE_APERTURE,               //��Ȧ-
 
 	P_CLIENT_PTZ_GO_PRESET,                     //��λ��
 	P_CLIENT_PTZ_PRESET_ADD,					//Ԥ�õ����� ����
 	P_CLIENT_PTZ_PRESET_DEL,					//Ԥ�õ����� ɾ��
 
 	P_CLIENT_PTZ_TOUR_START,					//����
 	P_CLIENT_PTZ_TOUR_STOP,						//����
 
 	P_CLIENT_PTZ_TOUR_ADD,						//Ѳ������ ����
 	P_CLIENT_PTZ_TOUR_DEL,						//Ѳ������ ɾ��
 	P_CLIENT_PTZ_TOUR_CLEAR,					//Ѳ������ ���

	// �¼ӵ� songe
	P_CLIENT_PTZ_PANCRUISE						//��ɨ
};

//¼����Դ
enum enum_P_Record_Source
{
	enum_P_Client_Source_Platform = 0,			//¼����Դƽ̨
	enum_P_Client_Source_Device,				//¼����Դ�豸
};

//¼������
enum enum_P_Record_Type
{
	enum_P_Client_Record_All = 0,		//¼����Դƽ̨
	enum_P_Client_Record_Alarm,			//�ⲿ����¼��
	enum_P_Client_Record_Motion,		//��̬���¼��

	enum_P_Client_Record_REGULAR,		//��ͨ¼��
	enum_P_Client_Record_MANUAL,		//�ֶ�¼
	enum_P_Client_PIC_ALL,				//����ͼƬ
	enum_P_Client_PIC_ALARM,			//�ⲿ����ͼƬ
	enum_P_Client_PIC_DETECT,			//��Ƶ���ͼƬ
	enum_P_Client_PIC_REGULAR,			//��ͨͼƬ
	enum_P_Client_PIC_MANUAL,			//�ֶ�ͼƬ
	enum_p_Client_Track = 10,			//�켣�طţ����ڳ���
};
//¼���������
enum enum_P_Playback_Control
{
	enum_P_Client_Playback_Normal = 0,			//�����ٶȲ���
	enum_P_Client_Playback_Fast,				//���
	enum_P_Client_Playback_Slow,				//����
	enum_P_Client_Playback_Pause,				//��ͣ
	enum_P_Client_Playback_Resume,				//��ͣ�ָ�
	enum_P_Client_Playback_PrevFrame,			//ǰһ֡
	enum_P_Client_Playback_NextFrame			//��һ֡
};

//�豸Զ����������
enum enum_P_Device_Config_Type
{
	enum_P_Device_Encode = 0,			//��Ƶ�������������ֱ��ʡ�֡��������ͨ���ȣ�P_Client_CONFIG_ENCODE_SIMPLIIFY
	enum_P_Device_PTZ,					//��̨����
	enum_P_Device_CHANNEL,				//ͨ����
	enum_P_Device_RECORD,				//¼��ƻ�
	enum_P_Device_ARSP,					//����ע�����������
	enum_P_Device_MOTION,				//�ƶ��������
	enum_P_Device_LOSS,					//�ƶ���ʧ����
	enum_P_Device_SHELL,				//�ƶ��ڵ�����
	enum_P_Device_ALARMIN,				//������������
	enum_P_Device_ALARMOUT,				//����������� SDK_AlarmOutConfigAll
	enum_P_Device_GPSInfor=11,			//gps��Ϣ	   P_Client_CarGPSInfor
	enum_P_Device_NetDigitChannel,		//������������ͨ������ P_Client_NetDecorderConfigAll

	enum_P_Device_ScreemDiv	,			//�豸������������ P_Client_DeviceScreenDiv
	enum_P_Device_ConfigNum
};

//����ģʽ
enum enum_P_Visit_Type
{
	enum_P_platform = 0,	//ͨ��ƽ̨��ת
	enum_P_DevVisit,		//ֱ���豸
};

enum enum_P_DevScreenDiv
{
	enum_P_SPLIT1=1,    // ������
	enum_P_SPLIT4,      // �Ļ���
	enum_P_SPLIT8,      // �˻���
	enum_P_SPLIT9,      // �Ż���
	enum_P_SPLIT16,     // 16����
	enum_P_SPLIT25,		// 25����	
	enum_P_SPLIT36     // 36����
};

// ʵʱ�������ݻص�����ԭ��
typedef void (CALLBACK *pfRealDataCallBack)(long lRealHandle, long dwDataType, char *pBuffer, long dwBufSize, long dwUser);
// �طţ����ؽ����ص���Ϣ
typedef void (CALLBACK *pPlayEndCallBack)(long lPlaybackHandle, int nParam, long dwUser);

//Ԥ�������ṹ��
struct P_Client_RealInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//ͨ��ID
	int				nStream;							//�������� 1 ������ 2 ������
	HWND			hWnd;								//���ھ��(ΪNULLʱ���ڲ������룬�ص�ԭʼ���ݸ��ⲿ)
	pfRealDataCallBack realDataFunc;						//��Ƶ���ݻص�
	long			dwUser;								//�û�����
};

struct P_Client_TIME
{
	DWORD dwYear;		//��
	DWORD dwMonth;		//��
	DWORD dwDay;		//��
	DWORD dwHour;		//ʱ
	DWORD dwMinute;		//��
	DWORD dwSecond;		//��
};

// ¼���ѯ����
struct P_Client_QueryRecInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//ͨ��ID
	int				nSource;							//��Դ	enum_P_Record_Source
	int				nRecordType;						//����	enum_P_Record_Type
	char			szCardInfo[P_CLIENT_MAX_ID_LEN];	//���ţ�����Ϊ����¼��ʱ��Ч��
	P_Client_TIME	sStartTime;							//��ʼʱ��
	P_Client_TIME	sEndTime;							//����ʱ��
	int				nDevVisit;							//�Ƿ�ֱ��
};

//¼���¼��Ϣ
struct P_Client_RecordInfo 
{
	char			source;							//��Դ����	
	char			recordType;						//¼������
	char			reserved[2];					//�ֽڶ������
	P_Client_TIME	startTime;						//��ʼʱ��
	P_Client_TIME	endTime;						//����ʱ��
	char			name[P_CLIENT_NAME_LEN];		//¼�����֣���ͬ���Ҷ��ļ��ı�ʶ��ͬ��
	int				length;							//�ļ�����

	//����������¼������Ҫ����Ϣ
	int				planId;							//¼��ƻ�ID
	int				ssId;							//�洢����ID
	char			diskId[P_CLIENT_MAX_ID_LEN];	//����ID
	int				fileHandle;						//�ļ����

	char			devId[P_CLIENT_MAX_ID_LEN];		//֧�ֱ���¼��طţ���Ϊ����Դ��¼���豸�Ƿֿ���
	int				channelNo;
};

//�ļ��ط������ṹ��
struct P_Client_PlaybackInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//ͨ��ID
	HWND			hWnd;								//���ھ��(ΪNULLʱ���ڲ������룬�ص�ԭʼ���ݸ��ⲿ)
	int				nDevVisit;							//�Ƿ�ֱ��
	pfRealDataCallBack playbackDataFunc;				//��Ƶ���ݻص�
	long			dwUser;								//�û�����
	int				nPlayType;							//0Ĭ�ϣ�1 �켣�ط�
	std::list<P_Client_RecordInfo*> recInfo;
};
//�ļ����������ṹ��
struct P_Client_DownloadByFileInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//ͨ��ID
	int				nDevVisit;							//�Ƿ�ֱ��
	pPlayEndCallBack posCallback;
	long			lPosUser;
};

//ʱ��ط������ṹ��
struct P_Client_PlayByTimeInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//ͨ��ID
	HWND			hWnd;								//���ھ��(ΪNULLʱ���ڲ������룬�ص�ԭʼ���ݸ��ⲿ)
	int				nSource;							//��Դ��ƽ̨�����豸	enum_P_Record_Source
	int				nDevVisit;							//�Ƿ�ֱ��
	P_Client_TIME	sStartTime;							//��ʼʱ��
	P_Client_TIME	sEndTime;							//����ʱ��
	int				nType;								//¼������	enum_P_Record_Type
	pfRealDataCallBack playbackDataFunc;				//��Ƶ���ݻص�
	long			dwUser;								//�û�����
};

//ʱ�����������ṹ��
struct P_Client_DownLoadByTimeInfo
{
	char			szChannelID[P_CLIENT_MAX_ID_LEN];	//ͨ��ID
	int				nSource;							//��Դ��ƽ̨�����豸	enum_P_Record_Source
	int				nDevVisit;							//�Ƿ�ֱ��
	P_Client_TIME	sStartTime;							//��ʼʱ��
	P_Client_TIME	sEndTime;							//����ʱ��
	int				nType;								//¼������	enum_P_Record_Type
	pPlayEndCallBack PosCallback;
	long			lPosUser;
};



struct P_Client_PrePoint
{
	unsigned char	code;				//Ԥ�õ���
	unsigned char	revered[3];			//revered
	char			name[32];			//����
};
//�ļ����������ṹ��
struct P_Client_PrePointTotal
{
	int nNum;							//Ԥ�õ����
	P_Client_PrePoint point[256];		//���256��Ԥ�õ�
};

//gps��Ϣ�ṹ��
struct P_Client_GPS_node
{
	double  latitude; //����
	double  longitude; //γ��
};

struct P_Client_CarGPSInfor
{
	P_Client_TIME	startTime;//��ѯ�Ŀ�ʼʱ��
	P_Client_TIME	endTime;//��ѯ�Ľ���ʱ��
	int				numbers;//��ȡ��Ϣ����
	P_Client_GPS_node *nodeBuffer;//����ΪNULLʱ��numbers���ض�Ӧ����������
};

#define P_Client_NAME_PASSWORD_LEN	64
#define P_Client_MAX_DECORDR_CH		32
#define P_Client_MAX_CHANNUM 32

///< ��������ַ����
struct P_Client_NetDecorderConfig
{
	bool Enable;						///< �Ƿ���
	char UserName[P_Client_NAME_PASSWORD_LEN];	///< DDNS��������, Ŀǰ��: JUFENG
	char PassWord[P_Client_NAME_PASSWORD_LEN];	///< ������
	char Address[P_Client_NAME_PASSWORD_LEN];
	int Protocol;
	int Port;							///< ���������Ӷ˿�
	int Channel;						///< ����������ͨ����
	int Interval;                       ///< ��Ѳ�ļ��ʱ��(s),0:��ʾ����
	char ConfName[P_Client_NAME_PASSWORD_LEN];	///<��������
	int DevType;						///<�豸����
	int StreamType;						///<���ӵ���������CaptureChannelTypes
};

/*��������������*/
enum P_Client_DecorderConnType
{
	P_Client_CONN_SINGLE = 0, 	/*������*/
	P_Client_CONN_MULTI = 1,		/*��������Ѳ*/
	P_Client_CONN_TYPE_NR,
};

/*����ͨ��������*/
struct P_Client_NetDigitChnConfig
{
	bool Enable;		/*����ͨ���Ƿ���*/		
	int ConnType;		/*�������ͣ�ȡDecoderConnectType��ֵ*/
	int TourIntv;		/*������ʱ��Ѳ���*/
	unsigned int SingleConnId;	/*������ʱ����������ID, ��1��ʼ��0��ʾ��Ч*/
	bool EnCheckTime;	/*������ʱ*/
	P_Client_NetDecorderConfig NetDecorderConf[32]; /*�����豸ͨ�����ñ�*/
	int nNetDeorde; // �ж��ٸ�
};

/*��������ͨ��������*/
struct P_Client_NetDecorderConfigAll
{
	P_Client_NetDigitChnConfig DigitChnConf[P_Client_MAX_DECORDR_CH];
};


struct P_Client_VIDEO_FORMAT
{
	int		iCompression;			//  ѹ��ģʽ 	
	int		iResolution;			//  �ֱ��� ����ö��SDK_CAPTURE_SIZE_t
	int		iBitRateControl;		//  �������� ����ö��SDK_capture_brc_t
	int		iQuality;				//  �����Ļ��� ����1-6		
	int		nFPS;					//  ֡��ֵ��NTSC/PAL������,������ʾ����һ֡		
	int		nBitRate;				//  0-4096k,���б���Ҫ�ɿͻ��˱��棬�豸ֻ����ʵ�ʵ�����ֵ�������±ꡣ
	int		iGOP;					//  ��������I֮֡��ļ��ʱ�䣬2-12 
};

struct P_Client_AUDIO_FORMAT
{
	int		nBitRate;				//  ����kbps	
	int		nFrequency;				//  ����Ƶ��	
	int		nMaxVolume;				//  ���������ֵ
};

// �򻯰汾��������
// ý���ʽ
struct P_Client_MEDIA_FORMAT
{
	P_Client_VIDEO_FORMAT vfFormat;			//  ��Ƶ��ʽ���� 			
	P_Client_AUDIO_FORMAT afFormat;			//  ��Ƶ��ʽ���� 
	bool	bVideoEnable;				//  ������Ƶ���� 
	bool	bAudioEnable;				//  ������Ƶ���� 	
} ;


/// ��������
struct P_Client_CONFIG_ENCODE_SIMPLIIFY
{
	P_Client_MEDIA_FORMAT dstMainFmt;		//  ��������ʽ 	
	P_Client_MEDIA_FORMAT dstExtraFmt;	//  ��������ʽ 
};

/// ȫͨ����������
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
������ ��Ϣ�ص�
������ 
nType		�쳣���ͣ��μ�enum_P_Client_Msg
nType2		�����ͣ�״̬
szDeviceID	�豸ID
channel		ͨ����
nParam1		����  nTypeΪԤ�����طţ��Խ�������ʱ��������ڵľ��������ID
pParam1		�������ɴ����豸ID
lUser		�û�����
����ֵ��
*/
typedef void (CALLBACK *pfmessageCallBack)(enum_P_Client_Msg enumType, int nType2, char *szDeviceID, int channel, int nParam1, char* pParam1, long dwUser);

typedef void (CALLBACK *pAlarmMessageCallBack)(enum_P_Client_AlarmMsg enumType, char *szDeviceID, int channel, int nStatus, int nParam1, long dwUser);

//�����Խ����ݻص�
typedef void (CALLBACK *pfTalkDataCallBack)(char *pBuffer, long dwBufSize, long dwUser);

//͸��ͨ�����ݻص�
typedef void (CALLBACK *pfTransportDataCallBack)(char* szDeviceID, char *pBuffer, long dwBufSize, long dwUser);
//�豸������Ϣ���ݻص�
typedef void (CALLBACK *pfUploadDataCallBack)(char* szDeviceID, int nType, char *pBuffer, long dwBufSize, long dwUser);


//��ȡ�汾��
P_CLIENT_API char* CALL_METHOD P_Client_Version();


//��ʼ��
P_CLIENT_API int CALL_METHOD P_Client_Init(pfmessageCallBack callback, long dwUser);

//ע��
P_CLIENT_API int CALL_METHOD P_Client_ClearUp();


//���ñ����ص�
P_CLIENT_API int CALL_METHOD P_Client_SetAlarmCallBack(pAlarmMessageCallBack callback, long dwUser);

/*
��������½����
������
ip  :	��������ַ
port	�������˿�
user	��½�û�
password��½����
waittime��ʱʱ��

����ֵ��>=0 �ɹ�,����ʧ��
*/
P_CLIENT_API int CALL_METHOD P_Client_LoginServer(char *ip, int port, char* user, char* password, int waittime = 5000);


/*
���������÷���ģʽ
������enum_P_Visit_Type

*/
P_CLIENT_API int CALL_METHOD P_Client_SetVisitType(int nType);

/*
��������ȡ�豸��Ϣ����
���أ���Ҫ���ٵ��ڴ泤��
*/
P_CLIENT_API int CALL_METHOD P_Client_GetDevicesLen();

/*
��������ȡ�豸��Ϣ
����: 
pBuf	�ڴ��ַ
nLen	�ڴ泤��
���أ�	���������ݳ���
*/
P_CLIENT_API int CALL_METHOD P_Client_GetDevices(char *pBuf, int nLen);


/*
��������ȡ�豸״̬
����: 
deviceId	�豸id
���أ�	�豸״̬
*/
P_CLIENT_API int CALL_METHOD P_Client_GetDeviceStatus(char *deviceId);


/*
��������ȡ������������Ϣ
����: 
type	����  1=��ѯ��Ϣ  2 ���ӵ�ͼ��Ϣ 3 ����ȫ������ 4 =�������������� 5=������������ 6 ��������ǽԤ��
pBuf	�ڴ��ַ
nLen	�ڴ泤��
���أ�	>0 ���������ĳ��ȣ�=0������û���ݣ�< 0�������

˵����  ��һ������ⲿ��֪��ʵ�ʳ��ȣ����԰�pbuf����null��nLenΪ0�������ڲ����ḳֵ������ֱ�ӷ����������ȣ�
�ڶ��ε���ʱ�����ⲿ����õ��ڴ�ͳ��ȣ��ڲ��ٸ�ֵ

*/
P_CLIENT_API int CALL_METHOD P_Client_GetDataInfo(int type, char *pBuf, int nLen);

/*
����������������������Ϣ
����: 
type	����  1=��ѯ��Ϣ  2 ���ӵ�ͼ��Ϣ 3 ����ȫ������ 4 =�������������� 5=������������ 6 ��������ǽԤ��
pBuf	�ڴ��ַ
nLen	�ڴ泤��
���أ�	>=0 ����ɹ�
*/
P_CLIENT_API int CALL_METHOD P_Client_SaveDataInfo(int type, char *pBuf, int nLen);

/*
���������µ��ӵ�ͼ��Ϣ
	  ���ڵ��ӵ�ͼ�ɷ�������ã���Ҫ��ʵ��ȡ��������
���أ�>=0 ����ɹ�
	  �����Ƿ��ȡ�ɹ����ص���enum_P_Client_Command->enum_P_Client_Cmd_Emap->nParam1
*/
P_CLIENT_API int CALL_METHOD P_Client_UpdateEmapInfo();


/*
������ Ԥ����Ƶ
������ pRealInfo  Ԥ������
����ֵ��>0 �ɹ��� ��=0 ʧ��
������ĳɹ�ֻ��˵������ͳɹ��������Ƿ�ɹ��ɻص�������
*/
P_CLIENT_API int CALL_METHOD P_Client_ConnectRealPlay(P_Client_RealInfo *pRealInfo);

/*
����: ֹͣԤ��
����: realHandle Ԥ�����  P_Client_ConnectRealPlay����ֵ
����: >=0 �ɹ�
*/
P_CLIENT_API int CALL_METHOD P_Client_StopRealPlay(HWND  hWnd,bool bWait=false);
P_CLIENT_API int CALL_METHOD P_Client_StopRealPlayByID(int realHandle,bool bWait=false);

/*
����: ��̨����
���� 
szCameraId	ͨ��ID
����ֵ
*/
P_CLIENT_API int CALL_METHOD P_Client_PTZControl(char *szCameraId, int nCmd, int nParam1, int nParam2, int nParam3, bool bStop);


/*
��������ʼ�����Խ�
������szDevIp  �豸��ַ
szDeviceID   �豸ID
dataCallback ���ݻص�   �ͻ���ģʽ������д
nUser		 �û�����   �ͻ���ģʽ������д
����ֵ�� ��0 �ɹ�
*/
P_CLIENT_API int CALL_METHOD P_Client_StartTalk(char *szDeviceID, pfTalkDataCallBack dataCallback, int nUser);

P_CLIENT_API int CALL_METHOD P_Client_StopTalk(int handle);


/*
��������ѯ¼����Ϣ
������
���أ�¼���ļ����
*/
P_CLIENT_API int CALL_METHOD P_Client_QueryRecord(P_Client_QueryRecInfo* pInfo);

/*
��������ѯһ��¼����Ϣ
������
���أ�1 �ɹ���0 ʧ��
*/
P_CLIENT_API int CALL_METHOD P_Client_GetNextRecord(int nQueryHandle, P_Client_RecordInfo* pRecInfo);

/*
����������¼���ѯ
������
nQueryHandle P_Client_QueryRecord�ķ���ֵ
���أ�
*/
P_CLIENT_API int CALL_METHOD P_Client_CloseQueryRecord(int nQueryHandle);


/*
������¼��ط�
������
szCameraID  ����ͷID
pRecInfo	¼����Ϣ
����:>0�ɹ�������ʧ��
*/
P_CLIENT_API int CALL_METHOD P_Client_PlaybackByFile(P_Client_PlaybackInfo* pPlayInfo, pPlayEndCallBack endCallback, long lPosUser);
P_CLIENT_API int CALL_METHOD P_Client_DownLoadByFile(P_Client_DownloadByFileInfo* pPlayInfo, char *szFileName, P_Client_RecordInfo* pRecInfo);


/*
��������ȡ��ǰ���ڲ��ŵ�ʱ��,�����ȡ����ֻ���ڲ�������Ч, ���ݻ�ȡ��ʱ�䣬�ⲿ�Լ��������
������nHandle P_Client_PlaybackByFile����ֵ
pTime ��ȡ����ʱ��
����ֵ >=0�ɹ�;
*/
P_CLIENT_API int CALL_METHOD P_Client_GetPlayCurTime(HWND hWnd,   P_Client_TIME *pTime);
P_CLIENT_API int CALL_METHOD P_Client_GetPlayCurTimeByID(int nHandle, P_Client_TIME *pTime);

/*
������ֹͣ�ļ��ط�
������nHandle  P_Client_PlaybackByFile�ķ���ֵ
*/
P_CLIENT_API int CALL_METHOD P_Client_StopPlaybackByFile(HWND hWnd);
P_CLIENT_API int CALL_METHOD P_Client_StopPlaybackByFileByID(int nHandle);
P_CLIENT_API int CALL_METHOD P_Client_StopDownloadByFile(int nHandle);

/*
��������ʱ��ط�
������
���أ���0 �ɹ�
*/
P_CLIENT_API int CALL_METHOD P_Client_PlaybackByTime(P_Client_PlayByTimeInfo* pPlayInfo, pPlayEndCallBack endCallback, long lUser);
P_CLIENT_API int CALL_METHOD P_Client_DownLoadByTime(P_Client_DownLoadByTimeInfo* pDoanloadInfo, char* szFileName);

/*
������ֹͣʱ��ط�
������nHandle  P_Client_PlaybackByTime�ķ���ֵ
*/
P_CLIENT_API int CALL_METHOD P_Client_StopPlaybackByTime(HWND hWnd);
P_CLIENT_API int CALL_METHOD P_Client_StopPlaybackByTimeByID(int nHandle);
P_CLIENT_API int CALL_METHOD P_Client_StopDownloadByTime(int nHandle);

/*
���������ûط�ʱ��ƫ��
������nHandle  P_Client_PlaybackByTime�ķ���ֵ
	 nTime   ʱ��ƫ�ƣ���Ϊ��λ
����ֵ����=0�ɹ�������ʧ��
*/
P_CLIENT_API int CALL_METHOD P_Client_SetPlayTimePos(HWND hWnd, int nTime);
P_CLIENT_API int CALL_METHOD P_Client_SetPlayTimePosByID(int nHandle, int nTime);

/*
������¼�����
������nHandle P_Client_PlaybackByTime����P_Client_PlaybackByFile����ֵ
	  control ���Ʋ���
����ֵ:
*/
P_CLIENT_API int CALL_METHOD P_Client_PlaybackControl(HWND hWnd, enum_P_Playback_Control control );
P_CLIENT_API int CALL_METHOD P_Client_PlaybackControlByID(int nHandle, enum_P_Playback_Control control );

/*
������ץͼ
������nHandle  P_Client_PlaybackByTime����P_Client_PlaybackByFile����P_Client_ConnectRealPlay����ֵ
      pFile	   �ļ�������ȫ·��������
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


//�������ȣ�ɫ��
P_CLIENT_API int CALL_METHOD P_Client_AdjustColor(HWND hWnd, int nBrightness, int nContrast, int nSaturation, int nHue);
P_CLIENT_API int CALL_METHOD P_Client_GetColor(HWND hWnd, int *nBrightness, int *nContrast, int *nSaturation, int *nHue);

//�ֲ��Ŵ�
P_CLIENT_API int CALL_METHOD P_Client_SelectedRangeVideo(HWND hWnd, int nLeft, int nTop, int nRight, int nBottom, int nWidth, int nHeight, int nEnable);


/// �ⲿ��ʹ�õ�OSD��ض���
typedef enum 
{
	P_CLIENT_OSD_TXT_FONT_ARIAL = 1,
	P_CLIENT_OSD_TXT_FONT_SERIF,
	P_CLIENT_OSD_TXT_FONT_SANS,

	P_CLIENT_OSD_TXT_FONT_SIMSUN = 101,  // �������壬����
	P_CLIENT_OSD_TXT_FONT_SIMHEI
} P_CLIENT_OSD_TXT_FONT;

// OSD������Ϣ��λ�ú������С��CIF���ã��洰�����Ŷ����š���TODO��D1����
typedef struct
{
	int pos_x; //ֻ�����λ�ã��Դ��ڵİٷֱ�����ʾ
	int pos_y;
	COLORREF color;
	char text[256];
	P_CLIENT_OSD_TXT_FONT font_type; // OSD_TXT_FONT_XXX
	int font_size; //�Դ��ڵİٷֱ�����ʾ
	int isBold; //�Ƿ����
	int isTransparent;//�Ƿ�͸����ɫ
	COLORREF bkColor;
} P_CLIENT_OSD_INFO_TXT;

//OSD����
P_CLIENT_API int CALL_METHOD P_Clinet_SetOSDEnalble(HWND hWnd, bool bEnable);
P_CLIENT_API int CALL_METHOD P_Client_SetOSDText(HWND hWnd, const P_CLIENT_OSD_INFO_TXT* osd_txt);

//���������ȼ�
/*
fluency   0 ��ʵʱ�ģ�����������ʾ
		  1 ʵʱ��,Ҳ��Ĭ�ϵ�
		  2 �����ȼ�1
		  3 �����ȼ�2
		  4 �����ȼ�3
		  5 �����ȼ�4
		  6 �����ȼ�5		û����һ�������������ʱ����1�룬Ҳ���������ʱ5��
*/
P_CLIENT_API int CALL_METHOD P_Client_SetFluency(HWND hWnd, int fluency);

//����Զ������
/*
nType �豸�������ͣ���enum_P_Device_Config_Type
*/
P_CLIENT_API int CALL_METHOD P_Client_SetDeviceConfig(int nType, char* szDeviceID, int nChannelNo, char *pConfigBuf, int nLen);
P_CLIENT_API int CALL_METHOD P_Client_GetDeviceConfig(int nType, char* szDeviceID, int nChannelNo, char *pConfigBuf, int nLen, int &nRetLen);


/*
����͸��ͨ��
����˵����
szDeviceID		�豸ID
nType		�������� 0: 232  1=485
nBaudrate	������  9600
nDatabits	����λ  8
nStopbits	ֹͣλ  1
nParity		����λ  2
*/
P_CLIENT_API int CALL_METHOD P_Client_OpenTransport(char* szDeviceID, int nType, int nBaudrate, int nDatabits, int nStopbits, int nParity );

//�ر�͸��ͨ��
P_CLIENT_API int CALL_METHOD P_Client_CloseTransport(char* szDeviceID, int nType);

//�豸͸��ͨ�����ݻص�
P_CLIENT_API int CALL_METHOD P_Client_SetTransPortCallBack(pfTransportDataCallBack callback, long dwUser);

//д��͸��ͨ������
P_CLIENT_API int CALL_METHOD P_Client_WriteTransportData(char* szDeviceID, int nType, char *pbuf, int nLen);


//��ʼ����¼��
P_CLIENT_API int CALL_METHOD P_Client_StartCenterRecord(char* szCamereID);
P_CLIENT_API int CALL_METHOD P_Client_StopCenterRecord(char* szCamereID);


//�豸������Ϣ���ݻص�
P_CLIENT_API int CALL_METHOD P_Client_SetUploadDataCallBack(pfUploadDataCallBack callback, long dwUser);
/*
//�����豸�Ƿ�Ҫ���������ϱ�����Ϣ�����糵��GPS��Ϣ
szDeviceID �豸ID
nType	1 ��Ҫ���գ�0 ������
*/
P_CLIENT_API int CALL_METHOD P_Client_SetDeviceUploadData(char* szDeviceID, int nType);


/*
��ȡ��ǰ��Ƶ��GPS��Ϣ
*/
P_CLIENT_API int CALL_METHOD P_Client_GetDeviceUploadData(HWND hWnd, char* buf, int len);


//��ȡ����ͷԤ�õ���Ϣ��ֻ���ڴ���Ƶ��ſ��Ի�ȡ
P_CLIENT_API int CALL_METHOD P_Client_GetPrePonitData(char* cameraID, P_Client_PrePointTotal *preInfo);

//��ȡѲ������Ϣ
//���dataΪ�գ��򷵻���Ҫ�ĳ���,Ŀǰ�̶�7*1027
//�����Ϊ�գ��򷵻�ʵ�ʳ��ȣ�data�и�ֵ
//���� С�ڵ���0 �ǳ������û�м�¼
P_CLIENT_API int CALL_METHOD P_Client_GetPTZTourData(char* cameraID, char *data);


P_CLIENT_API int CALL_METHOD P_Client_ModifyPassword(const char* szOldPsw, const char* szNewPsw);
#endif
