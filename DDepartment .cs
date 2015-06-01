using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient
{
    public class DDepartment
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string coding;

        public string Coding
        {
            get { return coding; }
            set { coding = value; }
        }

        private List<DDepartment> departs;

        public List<DDepartment> Departs
        {
            get { return departs; }
            set { departs = value; }
        }
        private List<DDevice> devices;

        public List<DDevice> Devices
        {
            get { return devices; }
            set { devices = value; }
        }
    }
}
