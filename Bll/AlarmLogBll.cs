using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MDS.Dal;
using MDS.Domain;

using AlarmMapClient;
using System.Globalization;

namespace MDS.Bll
{
    public class AlarmLogBll
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<AlarmLog> QueryLogs()
        {
            List<AlarmLog> logs = null;
            AlarmLogDal dal = new AlarmLogDal();
            try
            {
                var query = dal.GetAlarmLogs();
                var list = query.ToList();
                DateTime lastday = DateTime.Now.AddDays(-1);
                logs = list.Where(c => c.IsAlarmed == 0 && Convert.ToDateTime(c.AlarmTime) > lastday).OrderByDescending(c => c.AlarmTime).ToList();

                list = null;
                query = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                dal.Close();
            }
            return logs;
        }

        public static void DelLogs(string deviceId, string channelName, DateTime alarmendTime)
        {
            /*
            List<AlarmLog> logs = QueryLogsByDeviceIdAndChannelName(deviceId,channelName,alarmstartTime,alarmendTime);
            if (logs != null && logs.Count > 0)
            {
                logs.RemoveAll(c => { return true; });
            }
             * */

            AlarmLogDal dal = new AlarmLogDal();
            string sql = " from AlarmLog c where 1=1";
            try
            {
                if (deviceId != null && !deviceId.Equals(""))
                    sql += " and c.DeviceId='" + deviceId + "' ";
                if (channelName != null && !channelName.Equals(""))
                    sql += " and c.ChannelName='" + channelName + "' ";
                if (alarmendTime != null)
                    sql += (" and c.AlarmTime<='" + alarmendTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                log.Error(sql + "\r\n");
                dal.Del(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                log.Error(sql + "\r\n" + e.ToString());
            }
            finally
            {
                dal.Close();
                sql = null;
            }

        }

        public static void DelLogs(string deviceId, string channelName, DateTime alarmstartTime, DateTime alarmendTime)
        {
            /*
            List<AlarmLog> logs = QueryLogsByDeviceIdAndChannelName(deviceId,channelName,alarmstartTime,alarmendTime);
            if (logs != null && logs.Count > 0)
            {
                logs.RemoveAll(c => { return true; });
            }
             * */
            
            AlarmLogDal dal = new AlarmLogDal();
            string sql = " from AlarmLog c where 1=1";
            try
            {
                if (deviceId != null&&!deviceId.Equals(""))
                    sql += " and c.DeviceId='" + deviceId + "' ";
                if (channelName != null && !channelName.Equals(""))
                    sql += " and c.ChannelName='" + channelName + "' ";
                if (alarmstartTime != null)
                    sql += (" and c.AlarmTime>='" + alarmstartTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                if (alarmendTime != null)
                    sql += (" and c.AlarmTime<='" + alarmendTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
                log.Error(sql + "\r\n");
                dal.Del(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                log.Error(sql+"\r\n"+e.ToString());
            }
            finally
            {
                dal.Close();
                sql = null;
            }
             
        }

        public static List<AlarmLog> QueryLogsByDeviceIdAndChannelName(string deviceId, string channelName, DateTime alarmstartTime, DateTime alarmendTime)
        {
            List<AlarmLog> logs = null;
            AlarmLogDal dal = new AlarmLogDal();
            try
            {
                var query = dal.GetAlarmLogs();
                var list = query.ToList();
                DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy-MM-dd HH:mm";
                if (string.IsNullOrWhiteSpace(deviceId) && string.IsNullOrWhiteSpace(channelName))
                {
                    logs = list.Where(c => Convert.ToDateTime(c.AlarmTime, dtFormat).CompareTo(alarmstartTime) >= 0 && Convert.ToDateTime(c.AlarmTime, dtFormat).CompareTo(alarmendTime) < 0).OrderByDescending(c => c.DeviceId).ToList();
                }
                else if (string.IsNullOrWhiteSpace(channelName) && !string.IsNullOrWhiteSpace(deviceId))
                {
                    logs = list.Where(c => c.DeviceId.Contains(deviceId) && Convert.ToDateTime(c.AlarmTime, dtFormat).CompareTo(alarmstartTime) >= 0 && Convert.ToDateTime(c.AlarmTime, dtFormat).CompareTo(alarmendTime) < 0).OrderByDescending(c => c.DeviceId).ToList();
                }
                else if (string.IsNullOrWhiteSpace(deviceId) && !string.IsNullOrWhiteSpace(channelName))
                {
                    logs = list.Where(c => c.ChannelName.Contains(channelName) && Convert.ToDateTime(c.AlarmTime, dtFormat).CompareTo(alarmstartTime) >= 0 && Convert.ToDateTime(c.AlarmTime, dtFormat).CompareTo(alarmendTime) < 0).OrderByDescending(c => c.DeviceId).ToList();
                }
                else
                {
                    logs = list.Where(c => c.DeviceId.Contains(deviceId) && c.ChannelName.Contains(channelName) && Convert.ToDateTime(c.AlarmTime, dtFormat).CompareTo(alarmstartTime) >= 0 && Convert.ToDateTime(c.AlarmTime, dtFormat).CompareTo(alarmendTime) < 0).OrderByDescending(c => c.DeviceId).ToList();
                }

                list = null;
                query = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                dal.Close();
            }
            return logs;
        }

        public static List<AlarmLog> QueryLogsByDeviceId(string deviceId)
        {
            List<AlarmLog> logs = null;
            AlarmLogDal dal = new AlarmLogDal();
            try
            {
                var query = dal.GetAlarmLogs();
                var list = query.ToList();
                logs = list.Where(c => c.DeviceId == deviceId).OrderByDescending(c => c.DeviceId).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                dal.Close();
            }
            return logs;
        }

        public static bool UpdateAllLog(string oprName)
        {
            AlarmLogDal dal = new AlarmLogDal();
            List<AlarmLog> logs = dal.GetAlarmLogs().ToList();
            try
            {
                dal.BeginTranscation();
                foreach (AlarmLog log in logs)
                {
                    if (log.IsAlarmed == 1) continue;

                    log.IsAlarmed = 1;
                    log.AlarmDealTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    log.AlarmDealName = oprName;

                    dal.Update(log);
                }
                dal.Commit();
            }
            catch (Exception e)
            {
                dal.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                dal.Close();
            }
            return true;
        }

        public static bool UpdateLog(AlarmLog log)
        {
            AlarmLogDal dal = new AlarmLogDal();
            try
            {
                dal.BeginTranscation();
                dal.Update(log);
                dal.Commit();
            }
            catch (Exception e)
            {
                dal.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                dal.Close();
            }
            return true;
        }

        public static bool UpdateStatus(string szDeviceID, string channel, int enumType)
        {
            List<AlarmLog> logs = null;
            AlarmLogDal dal = new AlarmLogDal();
            try
            {
                dal.BeginTranscation();

                AlarmLog log;
                var query = dal.GetAlarmLogs();
                logs = query.Where(c => c.DeviceId.Equals(szDeviceID) && c.Channel.Equals(channel) && c.AlarmType==enumType).OrderByDescending(c=>c.AlarmTime).ToList();

                if (logs != null && logs.Count > 0)
                {
                    for (int i = 0; i < logs.Count; i++)
                    {
                        log = logs[i];

                        if (log.IsDel == 0)
                        {
                            log.IsDel = 1;//失效
                            log.DelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            dal.Update(log);
                        }
                    }
                    dal.Commit();
                }
            }
            catch (Exception e)
            {
                dal.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                dal.Close();
            }
            return true;
        }

        public static bool Save(AlarmMsg amsg)
        {
            AlarmLog log = new AlarmLog();
            log.AlarmType = (int)amsg.Type;
            log.DeviceId = amsg.DeviceId;
            log.Channel = amsg.Channel.ToString();
            log.Status = amsg.Status;
            log.Param = amsg.Param;
            log.IsDel = amsg.IsDeled?1:0;
            log.IsAlarmed = amsg.IsAlarmed?1:0;
            log.AlarmTime = amsg.AlarmTime.ToString("yyyy-MM-dd HH:mm:ss");
            log.Latitude = amsg.Latitude;
            log.Longitude = amsg.Longitude;
            log.Latitude2 = amsg.Latitude2;
            log.Longitude2 = amsg.Longitude2;
            log.UserName = amsg.UserName;
            log.Tel = amsg.Tel;
            log.Address = amsg.Address;
            log.ChannelName = amsg.ChannelName;
            log.MapPic = amsg.MapPic;

            AlarmLogDal dal = new AlarmLogDal();
            try
            {
                dal.BeginTranscation();
                dal.Save(log);
                dal.Commit();
            }
            catch (Exception e)
            {
                dal.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                dal.Close();
            }
            return true;
        }
    }
}