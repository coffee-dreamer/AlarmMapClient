using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDS.CLIENT;
using MDS.Request;
using MDS.Reponse;
using MDS.CLIENT.Domain;
using MDS.ConstTag;

namespace AlarmMapClient
{
    public partial class FrmDeviceInfo : Form
    {
        private string deviceId ;
        private string deviceName;

        public FrmDeviceInfo(string deviceId, string deviceName)
        {
            InitializeComponent();

            this.deviceId = deviceId;
            this.deviceName = deviceName;

            this.Text = this.deviceName;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            bool ret = MDSUtils.UpdateUserInfo(this.deviceId, this.txt_username.Text, this.txt_address.Text, this.txt_tel.Text);

            if (ret)
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存异常！");
            }
        }

        private void FrmChannelInfo_Load(object sender, EventArgs e)
        {
            Device dev = MDSUtils.GetDeviceInfo(this.deviceId);
            if (dev != null)
            {
                this.txt_username.Text = dev.TrueName;
                this.txt_address.Text = dev.Address;
                this.txt_tel.Text = dev.Tel;
            }
            else
            {
                MessageBox.Show("加载异常！");
            }
        }
    }
}
