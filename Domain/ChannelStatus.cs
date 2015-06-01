using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient.Domain
{
    public class ChannelStatus
    {
        private string channelId;

        public string ChannelId
        {
            get { return channelId; }
            set { channelId = value; }
        }
        private bool enable;

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
