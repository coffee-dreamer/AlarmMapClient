using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AlarmMapClient
{
    public class GroupXmlParser
    {
        protected XmlDocument doc = null;
        protected XmlElement root = null;
        protected XmlNamespaceManager nsmgr = null;

        public GroupXmlParser(string xml)
        {
            string uxml = xml.Trim().Replace("\0", "");
            if (uxml == null) return;

            byte[] encodedString = Encoding.UTF8.GetBytes(uxml);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(encodedString);
            ms.Flush();
            ms.Position = 0;

            this.doc = new XmlDocument();
            this.doc.Load(ms);

            root = doc.DocumentElement;
        }

        public DOrganization Parse()
        {
            if (root == null) return null;

            DOrganization org = new DOrganization();

            XmlNode node = root.SelectSingleNode("/Organization");
            if (node!= null)
            {
                //获取org属性
                XmlAttributeCollection attrs = node.Attributes;
                foreach (XmlAttribute attr in attrs)
                {

                }
                //获取子节点depart
                List<DDepartment> departs = ParseDepartment(null,node);

                org.Departs = departs;
            }

            return org;
        }

        protected List<DDepartment> ParseDepartment(DDepartment ddpart,XmlNode pnode)
        {
            List<DDepartment> dlist = new List<DDepartment>();

            XmlNodeList departs = GetNodes(pnode, "Department");
            if (departs != null && departs.Count > 0)
            {
                for (int i = 0; i < departs.Count; i++)
                {
                    DDepartment d = new DDepartment();

                    XmlNode depart = departs[i];

                    //获取department属性
                    XmlAttributeCollection attrs = depart.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        switch (attr.Name){
                            case "name":
                                d.Name = attr.Value;
                                break;
                            case "coding":
                                d.Coding = attr.Value;
                                break;
                        }
                    }
                    //获取子节点
                    string xml = depart.InnerXml;
                    if (xml != null && xml.Length > 0)
                    {
                        //递归解析
                        List<DDepartment> childs = ParseDepartment(d,depart);

                        d.Departs = childs;
                    }

                    dlist.Add(d);
                }
            }

            if(ddpart!=null){
                XmlNodeList devices = GetNodes(pnode, "Device");
                List<DDevice> devlist = new List<DDevice>();
                if (devices != null && devices.Count > 0)
                {
                    for (int i = 0; i < devices.Count; i++)
                    {
                        DDevice dev = new DDevice();

                        XmlNode device = devices[i];
                        //获取device属性
                        XmlAttributeCollection attrs = device.Attributes;
                        foreach (XmlAttribute attr in attrs)
                        {
                            switch (attr.Name)
                            {
                                case "id":
                                    dev.Id = attr.Value;
                                    break;
                                case "ip":
                                    dev.Ip = attr.Value;
                                    break;
                                case "title":
                                    dev.Title = attr.Value;
                                    break;
                                case "port":
                                    dev.Port = int.Parse(attr.Value);
                                    break;
                                case "user":
                                    dev.User = attr.Value;
                                    break;
                                case "password":
                                    dev.Password = attr.Value;
                                    break;
                                case "alertout":
                                    dev.Alertout = int.Parse(attr.Value);
                                    break;
                            }
                        }
                        //获取channel
                         List<DChannel> channels = ParseChannel(device);

                         dev.Channels = channels;

                         devlist.Add(dev);
                    }
                }

                ddpart.Devices = devlist;
            }

            return dlist;
        }

        protected List<DChannel> ParseChannel(XmlNode pnode)
        {
            List<DChannel> channellist = new List<DChannel>();

            XmlNodeList channels = GetNodes(pnode, "Channel");
            if (channels != null && channels.Count > 0)
            {
                for (int i = 0; i < channels.Count; i++)
                {
                    DChannel dchannel = new DChannel();

                    XmlNode channel = channels[i];
                    //获取device属性
                    XmlAttributeCollection attrs = channel.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        switch (attr.Name)
                        {
                            case "id":
                                dchannel.Id = attr.Value;
                                break;
                            case "title":
                                dchannel.Title = attr.Value;
                                break;
                            case "num":
                                dchannel.Num = int.Parse(attr.Value);
                                break;
                        }
                    }
                    channellist.Add(dchannel);
                }
            }

            return channellist;
        }

        protected XmlNodeList GetNodes(XmlNode pnode, string xPath)
        {
            XmlNodeList nodes = pnode.SelectNodes(xPath);
            return nodes;
        }
    }
}
