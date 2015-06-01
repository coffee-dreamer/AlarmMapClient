using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient
{
    public class AlarmMsg : IComparable
    {
        public string alarmTxt { get; set; }

        private bool isViewed;

        public bool IsViewed
        {
            get { return isViewed; }
            set { isViewed = value; }
        }

        private float latitude;

        public float Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private float longitude;

        public float Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        private float latitude2;

        public float Latitude2
        {
            get { return latitude2; }
            set { latitude2 = value; }
        }
        private float longitude2;

        public float Longitude2
        {
            get { return longitude2; }
            set { longitude2 = value; }
        }

        private TransSDK.enum_P_Client_AlarmMsg type;

        public TransSDK.enum_P_Client_AlarmMsg Type
        {
            get { return type; }
            set { type = value; }
        }
        private string deviceId;

        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }
        private string channel;

        public string Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        private int nchannel;

        public int Nchannel
        {
            get { return nchannel; }
            set { nchannel = value; }
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private int param;

        public int Param
        {
            get { return param; }
            set { param = value; }
        }

        private bool isAlarmed;

        public bool IsAlarmed
        {
            get { return isAlarmed; }
            set { isAlarmed = value; }
        }

        private bool isDeled;

        public bool IsDeled
        {
            get { return isDeled; }
            set { isDeled = value; }
        }

        private DateTime alarmTime;

        public DateTime AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }

        private string channelName;

        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string tel;

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string mapPic;

        public string MapPic
        {
            get { return mapPic; }
            set { mapPic = value; }
        }

        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public int CompareTo(object obj)
        {
            int result;
             try
             {
                 AlarmMsg msg = obj as AlarmMsg;
                 if (this.isDeled && !msg.isDeled)
                 {
                     result = 0;
                 }
                 else
                 {
                     if (this.alarmTime > msg.alarmTime)
                     {
                         result = 1;
                     }
                     else
                         result = 0;
                 }
                 return result;
             }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
