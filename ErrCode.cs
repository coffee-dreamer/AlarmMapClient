using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient
{
    public static class ErrCode
    {
        public const int P_CLIENT_UNKNOW_ERROR = -1;//未知错误
        public const int P_CLIENT_LOGIN_TIMEOUT = -2;//登陆超时
        public const int P_CLIENT_NO_USER = -3;//没有当前用户
        public const int P_CLIENT_PWD_ERROR = -4;//密码错误
        public const int P_CLIENT_HAS_LOGIN = -5;//用户已被其他地方登陆
        public const int P_CLIENT_CONNECT_ERROR = -6;//连接服务器失败
        public const int P_CLIENT_GET_GROUP_TIMEOUT = -7;//获取权限组列表超时
        public const int P_CLIENT_GET_ORG_TIMEOUT = -8;//获取权限组信息超时
        public const int P_CLIENT_NO_GROUP = -10;	//当前用户没有任何的权限
        public const int P_CLIENT_INVALID_USERID = -11;	//无效用户id，属于异常信息
        public const int P_CLIENT_NO_SESSION = -12;	//用户连接不存在，属于异常信息
        public const int P_CLIENT_USER_LOCKED = -13;	//用户被锁定
        public const int P_CLIENT_SETCONFIG_FAILED = -14;	//保存配置失败

        public const int P_CLIENT_NO_INIT = -50;	//没有初始化
        public const int P_CLIENT_NO_LOGIN = -51;	//未登陆
        public const int P_CLIENT_INPUT_PARAM_ERROR = -52;	//无效的参数
        public const int P_CLIENT_NO_DEVICE = -53;	//设备部存在
    }
}
