using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient
{
    public class SnHwd
    {
        private string sn;

        public string Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        private int lhwd;

        public int Lhwd
        {
            get { return lhwd; }
            set { lhwd = value; }
        }

        public SnHwd(string sn,int hwd)
        {
            this.sn = sn;
            this.lhwd = hwd;
        }
    }
}
