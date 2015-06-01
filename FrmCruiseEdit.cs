using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlarmMapClient
{
    public partial class FrmCruiseEdit : Form
    {
        private string cruise;
        private Int32 loginId;
        private int channelNo;
        public FrmCruiseEdit(Int32 loginId,int channelNo,string cruise)
        {
            InitializeComponent();
            this.cruise = cruise;
            this.loginId = loginId;
            this.channelNo = channelNo;
        }

        private void FrmCruiseEdit_Load(object sender, EventArgs e)
        {
            this.cruise_textBox.Text = this.cruise;
            this.preset_comboBox.SelectedIndex = 0;
        }

        private void addpreset_button_Click(object sender, EventArgs e)
        {
            int preset = int.Parse(this.preset_comboBox.Text);
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_ADDTOLOOP, cruise,preset ,0 , false);
        }

        private void delpreset_button_Click(object sender, EventArgs e)
        {
            int preset = int.Parse(this.preset_comboBox.Text);
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_DELFROMLOOP, cruise, preset, 0, false);
        }

        private void removecruise_button_Click(object sender, EventArgs e)
        {
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_DELFROMLOOP, cruise, 0, 0, false);
        }

        private void addpreset_button_MouseDown(object sender, MouseEventArgs e)
        {
            int preset = int.Parse(this.preset_comboBox.Text);
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_ADDTOLOOP, cruise, preset, 0, false);
        }

        private void addpreset_button_MouseUp(object sender, MouseEventArgs e)
        {
            int preset = int.Parse(this.preset_comboBox.Text);
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_ADDTOLOOP, cruise, preset, 0, true);
        }

        private void delpreset_button_MouseDown(object sender, MouseEventArgs e)
        {
            int preset = int.Parse(this.preset_comboBox.Text);
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_DELFROMLOOP, cruise, preset, 0, false);
        }

        private void delpreset_button_MouseUp(object sender, MouseEventArgs e)
        {
            int preset = int.Parse(this.preset_comboBox.Text);
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_DELFROMLOOP, cruise, preset, 0, true);
        }

        private void removecruise_button_MouseDown(object sender, MouseEventArgs e)
        {
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_DELFROMLOOP, cruise, 0, 0, false);
        }

        private void removecruise_button_MouseUp(object sender, MouseEventArgs e)
        {
            int cruise = int.Parse(this.cruise_textBox.Text);
            bool ret = NetSDK.H264_DVR_PTZControlEx(this.loginId, this.channelNo, (Int32)NetSDK.PTZ_ControlType.EXTPTZ_DELFROMLOOP, cruise, 0, 0, true);
        }
    }
}
