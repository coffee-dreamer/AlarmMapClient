using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using System.Data;

namespace MDS.Dal
{
    public class NHibernateHelper
    {
       private static readonly ISessionFactory sessionFactory;

        private NHibernateHelper() { }

        static NHibernateHelper()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
        }

        /// <summary>
        /// 取Session,需调用者关闭
        /// </summary>
        /// <returns></returns>
        public static ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }

        /// <summary>
        /// 取Session,需调用者关闭
        /// </summary>
        /// <param name="conn">用户自行管理连接池的Db连接对象</param>
        /// <returns></returns>
        public static ISession GetSession(IDbConnection conn)
        {
            return sessionFactory.OpenSession(conn);
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }

}