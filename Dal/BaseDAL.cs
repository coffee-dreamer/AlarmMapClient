using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace MDS.Dal
{
    public class BaseDAL
    {
        protected ISession session;

        public BaseDAL()
        {
            session = NHibernateHelper.GetSession();
        }

        public void Close()
        {
            session.Close();
        }

        public ITransaction BeginTranscation()
        {
           return session.BeginTransaction();
        }

        public void Commit()
        {
            session.Transaction.Commit();
        }

        public void Rollback()
        {
            if(session.Transaction!=null)
                session.Transaction.Rollback();
        }
    }
}