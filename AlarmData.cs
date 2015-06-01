using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient
{
    public class AlarmData
    {
        private static AlarmData alarmData;
        private List<AlarmMsg> alarmMsgs;
        private System.Object lockThis = new System.Object();

        private AlarmData()
        {
            alarmMsgs = new List<AlarmMsg>();
        }

        public static AlarmData ShareData()
        {
            if (alarmData == null)
                alarmData = new AlarmData();

            return alarmData;
        }

        public void addMsg(AlarmMsg msg)
        {
            lock (lockThis)
            {
                //alarmMsgs.Add(msg);
                alarmMsgs.Insert(0, msg);
            }
        }

        public void removeMsg(AlarmMsg msg)
        {
            lock (lockThis)
            {
                alarmMsgs.Remove(msg);
            }
        }

        public List<AlarmMsg> allMsgs()
        {
            return alarmMsgs;
        }
    }
}
