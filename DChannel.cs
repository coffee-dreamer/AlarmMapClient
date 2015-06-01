using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient
{
    public class DChannel
    {
        private int camera;

        public int Camera
        {
            get { return camera; }
            set { camera = value; }
        }
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
