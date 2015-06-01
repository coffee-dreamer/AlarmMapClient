using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml.Linq;
using System.Xml;

namespace AlarmMapClient
{
    public partial class FrmUpdateUser : Form
    {
        public int tabind;
        
        private string path = "OprData.xml";
        private string root = "users";
        private string child = "user";
        private string username = "username";
        private string password = "password";
        public FrmUpdateUser()
        {
            InitializeComponent();
            this.tabind = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FrmUpdateUser_Load(object sender, EventArgs e)
        {
            user_tabControl.SelectTab(this.tabind - 1);
            XDocument doc = XDocument.Load(this.path);
            var query = from t in doc.Descendants(this.child)
                        select new
                        {
                            username = t.Element(this.username).Value,
                            password = t.Element(this.password).Value
                        };
            this.mod_comboBox.DataSource = query.ToList();
            this.del_comboBox.DataSource = query.ToList();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void mod_button_Click(object sender, EventArgs e)
        {
            string userName = this.mod_comboBox.Text;
            string pw = this.modpw_textBox.Text;
            string pw2 = this.modpw2_textBox.Text;

            if (!pw.Equals(pw2))//两次密码输入不一致
            {
                MessageBox.Show("密码输入不一致,请重新输入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (null == pw || null == pw2)
            {
                MessageBox.Show("密码不能为空,请重新输入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string node = "//" + this.root + "/" + this.child + "[" + this.username + "='" + userName + "']";
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(this.path);
                    XmlNode selectNode = xmlDoc.SelectSingleNode(node);
                    for (int i = 0; i < selectNode.ChildNodes.Count; i++)
                    {
                        string nodeName = selectNode.ChildNodes[i].Name;
                        if (this.password.Equals(nodeName))
                        {
                            selectNode.ChildNodes[i].InnerText = pw2;
                            break;
                        }
                    };
                    xmlDoc.Save(this.path);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            string userName = this.adduser_textBox.Text;
            string pw = this.addpw_textBox.Text;
            string pw2 = this.addpw2_textBox.Text;
            bool status = false;

            if (!pw.Equals(pw2))//两次密码输入不一致
            {
                MessageBox.Show("密码输入不一致,请重新输入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((null == pw || null == pw2) || userName == null)
            {
                MessageBox.Show("用户名或密码不能为空,请重新输入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                XDocument doc = XDocument.Load(this.path);
                var query = from t in doc.Descendants(this.child)
                            select new
                            {
                                username = t.Element(this.username).Value,
                                password = t.Element(this.password).Value
                            };
                foreach (var user in query)
                {
                    if (userName.Equals(user.username))
                    {
                        MessageBox.Show("该用户名已存在,请重新输入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        status = false;
                        break;
                    }
                    else
                    {
                        status = true;
                    }
                }

                if (status)
                {
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(path);
                        var root = xmlDoc.DocumentElement;//取到根结点
                        XmlElement userNode = xmlDoc.CreateElement(this.child);
                        XmlElement userNameNode = xmlDoc.CreateElement(this.username);
                        XmlElement pwNode = xmlDoc.CreateElement(this.password);
                        userNameNode.InnerText = userName;
                        userNode.AppendChild(userNameNode);
                        pwNode.InnerText = pw2;
                        userNode.AppendChild(pwNode);
                        root.AppendChild(userNode);
                        xmlDoc.Save(path);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void addcel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modcel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void delcel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void del_button_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = this.del_comboBox.Text;
                string node = "//" + this.root + "/" + this.child + "[" + this.username + "='" + userName + "']";
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(path);
                XmlDoc.SelectSingleNode(node).ParentNode.RemoveChild(XmlDoc.SelectSingleNode(node));
                XmlDoc.Save(path);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void del_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void user_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
