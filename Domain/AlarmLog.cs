using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Domain
{
    [Serializable]
    public class AlarmLog
    {
        public virtual int Id { get; set; }

        public virtual int AlarmType { get; set; }

        public virtual string DeviceId { get; set; }

        public virtual string Channel { get; set; }

        public virtual int Nchannel { get; set; }

        public virtual int Status { get; set; }

        public virtual int Param { get; set; }

        public virtual int IsDel { get; set; }

        public virtual string DelTime { get; set; }

        public virtual int IsAlarmed { get; set; }

        public virtual string AlarmDealTime { get; set; }

        public virtual string AlarmTime { get; set; }

        public virtual float Latitude { get; set; }

        public virtual float Longitude { get; set; }

        public virtual float Latitude2 { get; set; }

        public virtual float Longitude2 { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Tel { get; set; }

        public virtual string Address { get; set; }

        public virtual string ChannelName { get; set; }

        public virtual string MapPic { get; set; }

        public virtual string AlarmDealName { get; set; }

        public virtual string ZbLog { get; set; }
    }
}