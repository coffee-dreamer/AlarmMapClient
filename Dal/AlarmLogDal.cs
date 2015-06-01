using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Linq;
using MDS.Domain;

namespace MDS.Dal
{
    public class AlarmLogDal : BaseDAL
    {
        public IQueryable<AlarmLog> GetAlarmLogs()
        {
            DateTime lastday = DateTime.Now.AddDays(-30);
            var query = (from log in session.Query<MDS.Domain.AlarmLog>()
                         //where Convert.ToDateTime(log.AlarmTime) > lastday  
                         select log);
            return query;
        }

        public void Save(AlarmLog log)
        {
            session.Save(log);
            session.Flush();
        }

        public void Update(AlarmLog log)
        {
            session.Update(log);
            session.Flush();
        }

        public void Del(string sql)
        {
            session.Delete(sql);
            session.Flush();
            
        }
    }
}