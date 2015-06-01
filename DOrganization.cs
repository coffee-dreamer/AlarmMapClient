using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient
{
    public class DOrganization
    {
        //grade="0" id="0" modifiedTime="1388049789984"

        private List<DDepartment> departs;

        public List<DDepartment> Departs
        {
            get { return departs; }
            set { departs = value; }
        }
    }
}
