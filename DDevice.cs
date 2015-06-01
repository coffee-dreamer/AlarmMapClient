using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmMapClient
{
    public class DDevice
    {
        private int channel;
        private string desc;
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        } 
        private int manufacturer; 
        private string model; 
        private string rights;
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        } 
        private string type;
        private int alertout;

        public int Alertout
        {
            get { return alertout; }
            set { alertout = value; }
        }
        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        private string user;

        public string User
        {
            get { return user; }
            set { user = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        } 
        private int alert; 
        private string coding;
        private int typeid; 

        private List<DChannel> channels;

        public List<DChannel> Channels
        {
            get { return channels; }
            set { channels = value; }
        }
    }
}
