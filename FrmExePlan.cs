using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MDS.CLIENT;

namespace AlarmMapClient
{
    public partial class FrmExePlan : Form
    {
        private string deviceId;
        private string userName;
        private string docXml;
        private static int xHeight = 20;
        private static int yHeight = 6;
        private string PlanName = "控制计划";
        private MDS.CLIENT.Domain.SchPlan currPlan = null;
        private MDS.CLIENT.Domain.SchPlan planDefault = null;
        private TreeNode nodeDefault = null;
        private List<MDS.CLIENT.Domain.SchPlan> plans = new List<MDS.CLIENT.Domain.SchPlan>();
        private bool planIsNull = false;
        private bool isSave = false;
        private ComboBox[] channelComboBoxs = null;
        private ComboBox[] weekComboBoxs = null;
        private bool isInit = false;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public FrmExePlan(string deviceId, string userId, string docXml)
        {
            InitializeComponent();
            this.deviceId = deviceId;
            this.userName = userId;
            this.docXml = docXml;
        }

        private void FrmExePlan_Load(object sender, EventArgs e)
        {
            channelComboBoxs = new ComboBox[] { wire, wireless, ele };
            weekComboBoxs = new ComboBox[] {this.wireweek,this.wirelessweek,this.eleweek };
            planName_textBox.Hide();
            plans = MDSUtils.GetSchPlans(this.userName);
            List<MDS.CLIENT.Domain.SchDevice> devices = MDSUtils.GetSchDevices(this.deviceId);
            MDS.CLIENT.Domain.SchDevice Selectdevice = new MDS.CLIENT.Domain.SchDevice();
            if (devices != null && devices.Count > 0)
            {
                devices = devices.OrderByDescending(s => s.AddTime).ToList<MDS.CLIENT.Domain.SchDevice>();
                Selectdevice = devices[0];
            }
            else {
                Selectdevice.PlanId = -1;
            }
            Init();
            if (plans != null && plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Tag = plans[i];
                    node.Text = plans[i].PlanName;
                    exePlan_treeView.Nodes.Add(node);
                    if (plans[i].PlanId == Selectdevice.PlanId)
                    {
                        exePlan_pictureBox.Visible = true;
                        exePlan_treeView.SelectedNode = node;
                        this.nodeDefault = node;
                        ShowDefaultPicture(node);
                        planDefault = plans[i];
                        ShowPlanCfg(planDefault.PlanId, planDefault.PlanName,true);
                    }
                }

                if (planDefault == null)
                {
                    ShowPlanCfg(plans[0].PlanId, plans[0].PlanName, true);
                }

            }
            else {
                planIsNull = true;
                exePlan_pictureBox.Visible = false;
            }
        }

        private void Init() {
            this.isInit = true;
            
            wireweek.SelectedIndex = 0;
            wirelessweek.SelectedIndex = 0;
            eleweek.SelectedIndex = 0;
            wire.SelectedIndex = 0;
            wireless.SelectedIndex = 0;
            ele.SelectedIndex = 0;
            
            planName_textBox.Hide();
            for (int k = 0; k < channelComboBoxs.Length; k++)
            {
                for (int i = 0; i < 4; i++)
                {
                    CheckBox checkBox = (CheckBox)this.Controls.Find(channelComboBoxs[k].Name + i.ToString(), true)[0];
                    checkBox.Checked = false;
                    TextBox start1 = (TextBox)this.Controls.Find(channelComboBoxs[k].Name + i.ToString() + "0", true)[0];
                    start1.Text = "00";
                    TextBox start2 = (TextBox)this.Controls.Find(channelComboBoxs[k].Name + i.ToString() + "1", true)[0];
                    start2.Text = "00";
                    TextBox end1 = (TextBox)this.Controls.Find(channelComboBoxs[k].Name + i.ToString() + "2", true)[0];
                    end1.Text = "24";
                    TextBox end2 = (TextBox)this.Controls.Find(channelComboBoxs[k].Name + i.ToString() + "3", true)[0];
                    end2.Text = "00";
                }
            }
            this.isInit = false;
        }
        private void exePlan_treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = exePlan_treeView.SelectedNode;
                MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
                bool isNew = checkIsNew(plan.PlanId);
                if (isNew)
                {
                    this.selectMany_ToolStripMenuItem.Enabled = false;
                }
            }else {
                this.selectMany_ToolStripMenuItem.Enabled = true;
            }
        }

        private void exePlan_treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = exePlan_treeView.SelectedNode;
            MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
            currPlan = plan;
        }

        private void enableButton(bool isEnable) {
            if (isEnable)
            {
                this.exePlan_splitContainer.Enabled = true;
                this.add_button.Enabled = true;
                this.del_button.Enabled = true;
                this.save_button.Enabled = true;
            }
            else {
                this.exePlan_splitContainer.Enabled = false;
                this.add_button.Enabled = false;
                this.del_button.Enabled = false;
                this.save_button.Enabled = false;
            }
        }
        private void exePlan_treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = exePlan_treeView.SelectedNode;
            MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
            currPlan = plan;
            bool ret = checkIsNew(plan.PlanId);
            if(!ret){
                ShowPlanCfg(plan.PlanId,plan.PlanName,false);
            }else{
                planName_label.Text = plan.PlanName;
                planName_label.Show();
                Init();
            }
        }

        private void ShowDefaultPicture(TreeNode node) {
            exePlan_pictureBox.Visible = true;
            int x = node.Bounds.Right + xHeight;
            int y = node.Bounds.Bottom + yHeight;
            Point point = new Point(x, y);
            exePlan_pictureBox.Location = point;
        }

        private void ShowPlanCfg(int planId,string planName,bool isinit) {
            planName_label.Text = planName;
            planName_label.Show();
            planName_textBox.Hide();
            List<MDS.CLIENT.Domain.SchPlanCfg> planCfgs = MDSUtils.GetSchPlanCfgs(planId);
            Init();
            if (isinit)
            {
                this.isInit = true;
            }
            if (planCfgs != null && planCfgs.Count > 0)
            {
                int wireChannelId = -1;
                int wirelessChannelId = -1;
                int eleChannelId = -1;

                int wireWeekId = -1;
                int wirelessWeekId = -1;
                int eleWeekId = -1;

                int wireNum = 0;
                int wirelessNum = 0;
                int eleNum = 0;

                foreach(MDS.CLIENT.Domain.SchPlanCfg plancfg in planCfgs)
                {//先找到要显示的通道跟星期
                    if (plancfg.PlanType == 0 && wireChannelId == -1)
                    {
                        wireChannelId = plancfg.ChannelId;
                        wireWeekId = plancfg.Week;
                    }
                    else if (plancfg.PlanType == 1 && wirelessChannelId == -1)
                    {
                        wirelessChannelId = plancfg.ChannelId;
                        wirelessWeekId = plancfg.Week;
                    }
                    else if (plancfg.PlanType == 2 && eleChannelId == -1)
                    {
                        eleChannelId = plancfg.ChannelId;
                        eleWeekId = plancfg.Week;
                    }

                    if (wireChannelId != -1 && wirelessChannelId != -1 && eleChannelId != -1)
                    {
                        break;
                    }
                }
                
                for (int i = 0; i < planCfgs.Count; i++)
                {
                    if (planCfgs[i].PlanType == 0)
                    {
                        if (wireChannelId == planCfgs[i].ChannelId && wireWeekId == planCfgs[i].Week)
                        {
                            wire.SelectedIndex = planCfgs[i].ChannelId;
                            wireweek.SelectedIndex = planCfgs[i].Week;
                            string tsbegin = planCfgs[i].TsBegin;
                            string tsend = planCfgs[i].TsEnd;
                            CheckBox checkBox = (CheckBox)this.Controls.Find(wire.Name + wireNum.ToString(), true)[0];

                            checkBox.Checked = true;

                            TextBox start1 = (TextBox)this.Controls.Find(wire.Name + wireNum.ToString() + "0", true)[0];
                            TextBox start2 = (TextBox)this.Controls.Find(wire.Name + wireNum.ToString() + "1", true)[0];
                            TextBox end1 = (TextBox)this.Controls.Find(wire.Name + wireNum.ToString() + "2", true)[0];
                            TextBox end2 = (TextBox)this.Controls.Find(wire.Name + wireNum.ToString() + "3", true)[0];

                            start1.Text = tsbegin.Substring(0, 2);
                            start2.Text = tsbegin.Substring(3, 2);
                            end1.Text = tsend.Substring(0, 2);
                            end2.Text = tsend.Substring(3, 2);
                            wireNum++;
                        }
                    }
                    else if (planCfgs[i].PlanType == 1)
                    {
                        if (wirelessChannelId == planCfgs[i].ChannelId && wirelessWeekId == planCfgs[i].Week)
                        {
                            wireless.SelectedIndex = planCfgs[i].ChannelId;
                            wirelessweek.SelectedIndex = planCfgs[i].Week;
                            string tsbegin = planCfgs[i].TsBegin;
                            string tsend = planCfgs[i].TsEnd;
                            CheckBox checkBox = (CheckBox)this.Controls.Find(wireless.Name + wirelessNum.ToString(), true)[0];

                            checkBox.Checked = true;

                            TextBox start1 = (TextBox)this.Controls.Find(wireless.Name + wirelessNum.ToString() + "0", true)[0];
                            TextBox start2 = (TextBox)this.Controls.Find(wireless.Name + wirelessNum.ToString() + "1", true)[0];
                            TextBox end1 = (TextBox)this.Controls.Find(wireless.Name + wirelessNum.ToString() + "2", true)[0];
                            TextBox end2 = (TextBox)this.Controls.Find(wireless.Name + wirelessNum.ToString() + "3", true)[0];

                            start1.Text = tsbegin.Substring(0, 2);
                            start2.Text = tsbegin.Substring(3, 2);
                            end1.Text = tsend.Substring(0, 2);
                            end2.Text = tsend.Substring(3, 2);
                            wirelessNum++;
                        }
                    }
                    else if (planCfgs[i].PlanType == 2)
                    {
                        if (eleChannelId == planCfgs[i].ChannelId && eleWeekId == planCfgs[i].Week)
                        {
                            ele.SelectedIndex = planCfgs[i].ChannelId;
                            eleweek.SelectedIndex = planCfgs[i].Week;
                            string tsbegin = planCfgs[i].TsBegin;
                            string tsend = planCfgs[i].TsEnd;
                            CheckBox checkBox = (CheckBox)this.Controls.Find(ele.Name + eleNum.ToString(), true)[0];

                            checkBox.Checked = true;

                            TextBox start1 = (TextBox)this.Controls.Find(ele.Name + eleNum.ToString() + "0", true)[0];
                            TextBox start2 = (TextBox)this.Controls.Find(ele.Name + eleNum.ToString() + "1", true)[0];
                            TextBox end1 = (TextBox)this.Controls.Find(ele.Name + eleNum.ToString() + "2", true)[0];
                            TextBox end2 = (TextBox)this.Controls.Find(ele.Name + eleNum.ToString() + "3", true)[0];

                            start1.Text = tsbegin.Substring(0, 2);
                            start2.Text = tsbegin.Substring(3, 2);
                            end1.Text = tsend.Substring(0, 2);
                            end2.Text = tsend.Substring(3, 2);
                            eleNum++;
                        }
                    }
                }
            }
            else {
                Init();
            }

            if(isinit){
                this.isInit = false;
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            TreeNode node = exePlan_treeView.SelectedNode;
            if(node == null){
                MessageBox.Show("请选择相应的计划名称!");
                return;
            }

            if ("".Equals(node.Text))
            {
                MessageBox.Show("执行计划名称不能为空!");
                return;
            }
            enableButton(false);
            bool ret = false;
            try
            {
                List<MDS.CLIENT.Domain.SchPlanCfg> planCfgs = new List<MDS.CLIENT.Domain.SchPlanCfg>();
                MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
                for (int k = 0; k < channelComboBoxs.Length; k++)
                {
                    int channelId = channelComboBoxs[k].SelectedIndex;
                    ComboBox weekbox = (ComboBox)this.Controls.Find(channelComboBoxs[k].Name + "week", true)[0];
                    int week = weekbox.SelectedIndex;
                    for (int i = 0; i < 4; i++)
                    {
                        CheckBox checkBox = (CheckBox)this.Controls.Find(channelComboBoxs[k].Name + i.ToString(), true)[0];

                        if (checkBox.Checked)
                        {
                            string comboBoxName = channelComboBoxs[k].Name;
                            TextBox start1 = (TextBox)this.Controls.Find(comboBoxName + i.ToString() + "0", true)[0];
                            TextBox start2 = (TextBox)this.Controls.Find(comboBoxName + i.ToString() + "1", true)[0];
                            TextBox end1 = (TextBox)this.Controls.Find(comboBoxName + i.ToString() + "2", true)[0];
                            TextBox end2 = (TextBox)this.Controls.Find(comboBoxName + i.ToString() + "3", true)[0];

                            MDS.CLIENT.Domain.SchPlanCfg planCfg = new MDS.CLIENT.Domain.SchPlanCfg();
                            planCfg.ChannelId = channelId;
                            planCfg.Week = week;
                            planCfg.PlanType = k;
                            planCfg.PlanId = plan.PlanId;
                            planCfg.TsBegin = String.Format("{0}:{1}", start1.Text, start2.Text);
                            planCfg.TsEnd = String.Format("{0}:{1}", end1.Text, end2.Text);
                            planCfgs.Add(planCfg);
                        }
                    }
                }

                //保存到服务端
                bool isNew = checkIsNew(plan.PlanId);
                if (isNew)
                {
                    if (currPlan.PlanId == plan.PlanId)
                    {
                        long planIdt = MDSUtils.AddSchPlan(plan.PlanName, this.userName);
                        if (planIdt == -1)
                        {
                            MessageBox.Show("保存失败!");
                            enableButton(true);
                            return;
                        }

                        plan.PlanId = (int)planIdt;
                        for (int i = 0; i < planCfgs.Count; i++)
                        {
                            ret = MDSUtils.AddSchPlanCfg(plan.PlanId,
                                                    planCfgs[i].PlanType,
                                                    planCfgs[i].ChannelId,
                                                    planCfgs[i].Week,
                                                    planCfgs[i].TsBegin,
                                                    planCfgs[i].TsEnd);
                        }
                        node.Tag = plan;
                        isSave = true;
                    }
                    else
                    {
                        MessageBox.Show("请先保存执行计划内容!");
                        enableButton(true);
                        return;
                    }
                }
                else
                {
                    if (currPlan.PlanId == plan.PlanId)
                    {
                        ret = MDSUtils.UpdataSchPlan(plan);
                        List<MDS.CLIENT.Domain.SchPlanCfg> planCfgold = MDSUtils.GetSchPlanCfgs(plan.PlanId);
                        string spcIds = "";
                        if (planCfgold != null && planCfgold.Count > 0)
                        {
                            for (int i = 0; i < planCfgold.Count; i++)
                            {
                                foreach (MDS.CLIENT.Domain.SchPlanCfg planCfgnew in planCfgs)
                                {
                                    if (planCfgnew.ChannelId == planCfgold[i].ChannelId && planCfgnew.Week == planCfgold[i].Week &&
                                        planCfgnew.PlanType == planCfgold[i].PlanType)
                                    {
                                        if (!spcIds.Contains(planCfgold[i].SpcId.ToString()))
                                        {
                                            spcIds += planCfgold[i].SpcId + ",";
                                        }
                                        //ret = MDSUtils.DelSchPlanCfg(planCfgold[i].SpcId);
                                    }
                                }
                            }
                        }

                        if ((planCfgold != null && planCfgold.Count > 0) && planCfgs.Count == 0)
                        {
                            foreach (MDS.CLIENT.Domain.SchPlanCfg cfg in planCfgold)
                            {
                                if (!spcIds.Contains(cfg.SpcId.ToString()))
                                {
                                    spcIds += cfg.SpcId + ",";
                                }
                            }
                        }

                        if (!"".Equals(spcIds))
                        {
                            spcIds = spcIds.Substring(0, spcIds.Length - 1);
                            ret = MDSUtils.DelSchPlanCfgBySpcId(spcIds);
                        }

                        for (int i = 0; i < planCfgs.Count; i++)
                        {
                            ret = MDSUtils.AddSchPlanCfg(planCfgs[i].PlanId,
                                                planCfgs[i].PlanType,
                                                planCfgs[i].ChannelId,
                                                planCfgs[i].Week,
                                                planCfgs[i].TsBegin,
                                                planCfgs[i].TsEnd);
                        }
                    }
                    else
                    {
                        ret = MDSUtils.UpdataSchPlan(plan);
                    }
                }
            }catch(Exception ex){
                log.Error(ex.Message);
                enableButton(true);
                ret = false;
            }
            if (ret)
            {
                MessageBox.Show("保存成功!");
                plans = MDSUtils.GetSchPlans(this.userName);
            }
            else {
                MessageBox.Show("保存失败!");
            }

            enableButton(true);
        }

        private void exePlan_treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            TreeNodeCollection nodes = exePlan_treeView.Nodes;
            TreeNode node = exePlan_treeView.SelectedNode;
            MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
            string aftervalue = e.Label;
            string beforevalue = node.Text;
            bool isNew = checkIsNew(plan.PlanId);
            if (!"".Equals(beforevalue) && "".Equals(aftervalue))
            {
                MessageBox.Show("执行计划名称不能为空!");
                e.CancelEdit = true;
                return;
            }else if (aftervalue == null && beforevalue != null) {
                if (!isNew)
                {
                    return;
                }
                else {
                    aftervalue = beforevalue;
                }
            }

            plan.PlanName = aftervalue;
            
            
            foreach (TreeNode curNode in nodes)
            {
                if (curNode.Text.Equals(plan.PlanName) && curNode != node)
                {
                    MessageBox.Show("名称重复,请重新命名!");
                    plan.PlanName = beforevalue;
                    e.CancelEdit = true;
                    return;
                }
            }

            node.Tag = plan;
            node.Text = aftervalue;
            if (!isNew)
            {
                planName_label.Text = aftervalue;
                planName_label.Show();
                planName_textBox.Hide();
            }
            
            if (planDefault != null && plan.PlanId == planDefault.PlanId)
            {
                ShowDefaultPicture(node);
            }
        }

        private bool checkIsNew(int planid){
            bool ret = true;
            if (plans == null || plans.Count == 0)
            {
                return ret;
            }
            for (int i = 0; i < plans.Count;i++)
            {
                if (plans[i].PlanId == planid)
                {
                    ret = false;
                    break;
                }
            }

            return ret;
        }

        private void checkValue(int type,TextBox t) {
            string value = t.Text;
            if (value.Length != 2) {
                MessageBox.Show("输入格式错误,请重新输入!");
                t.Focus();
                return;
            }
            int v = -1;
            try { 
                v = int.Parse(value);
            }catch(Exception e){
                MessageBox.Show("输入格式错误,请输入数字!");
                t.Focus();
                return;
            }
            if (type == 0)
            {
                if (v < 0 || v > 24)
                {
                    MessageBox.Show("输入数值错误,请输入(00-24)数值!");
                    t.Focus();
                    return;
                }
            }
            else if(type == 1){
                if (v < 0 || v > 60)
                {
                    MessageBox.Show("输入数值错误,请输入(00-60)数值!");
                    t.Focus();
                    return;
                }
            }
            
        }

        private void changeComboBox(ComboBox comboBox,int planType) {
            if (this.isInit)
            {
                return;
            }
            TreeNode node = exePlan_treeView.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请选择一个执行计划!");
                return;
            }

            MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
            int channelid = -1;
            int weekid = -1;
            string name = "";
            try
            {
                if (comboBox.Name.Contains("week"))
                {
                    name = comboBox.Name.Substring(0, comboBox.Name.Length - 4);
                    weekid = comboBox.SelectedIndex;
                    channelid = channelComboBoxs[planType].SelectedIndex;
                }
                else
                {
                    name = comboBox.Name;
                    channelid = comboBox.SelectedIndex;
                    weekid = weekComboBoxs[planType].SelectedIndex;
                }

                bool isNew = checkIsNew(plan.PlanId);

                if (!isNew)
                {
                    List<MDS.CLIENT.Domain.SchPlanCfg> planCfgs = MDSUtils.GetSchPlanCfgs(plan.PlanId);
                    if (planCfgs != null && planCfgs.Count > 0)
                    {
                        int num = 0;
                        foreach (MDS.CLIENT.Domain.SchPlanCfg planCfg in planCfgs)
                        {
                            if (planCfg.ChannelId == channelid && planCfg.PlanType == planType && planCfg.Week == weekid)
                            {
                                string tsbegin = planCfg.TsBegin;
                                string tsend = planCfg.TsEnd;
                                CheckBox checkBox = (CheckBox)this.Controls.Find(name + num.ToString(), true)[0];

                                checkBox.Checked = true;

                                TextBox start1 = (TextBox)this.Controls.Find(name + num.ToString() + "0", true)[0];
                                TextBox start2 = (TextBox)this.Controls.Find(name + num.ToString() + "1", true)[0];
                                TextBox end1 = (TextBox)this.Controls.Find(name + num.ToString() + "2", true)[0];
                                TextBox end2 = (TextBox)this.Controls.Find(name + num.ToString() + "3", true)[0];

                                start1.Text = tsbegin.Substring(0, 2);
                                start2.Text = tsbegin.Substring(3, 2);
                                end1.Text = tsend.Substring(0, 2);
                                end2.Text = tsend.Substring(3, 2);
                                num++;
                            }
                        }

                        if (num == 0)
                        {//初始化
                            for (int i = 0; i < 4; i++)
                            {
                                CheckBox checkBox = (CheckBox)this.Controls.Find(name + i.ToString(), true)[0];
                                checkBox.Checked = false;
                                TextBox start1 = (TextBox)this.Controls.Find(name + i.ToString() + "0", true)[0];
                                start1.Text = "00";
                                TextBox start2 = (TextBox)this.Controls.Find(name + i.ToString() + "1", true)[0];
                                start2.Text = "00";
                                TextBox end1 = (TextBox)this.Controls.Find(name + i.ToString() + "2", true)[0];
                                end1.Text = "24";
                                TextBox end2 = (TextBox)this.Controls.Find(name + i.ToString() + "3", true)[0];
                                end2.Text = "00";
                            }
                        }
                    }
                }
            }catch(Exception ex){
                log.Error(ex.Message);
            }
        }
        

        private void add_button_Click(object sender, EventArgs e)
        {
            addPlan();
        }

        private void mod_button_Click(object sender, EventArgs e)
        {
            modPlan();
        }

        private void del_button_Click(object sender, EventArgs e)
        {
            TreeNode node = exePlan_treeView.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请选择一个执行计划!");
                return;
            }

            try
            {
                this.enableButton(false);
                MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
                bool isNew = checkIsNew(plan.PlanId);
                bool ret = false;
                if (!isNew)
                {
                    if (planDefault != null && plan.PlanId == planDefault.PlanId)
                    {
                        exePlan_pictureBox.Visible = false;

                        List<MDS.CLIENT.Domain.SchDevice> devices = MDSUtils.GetSchDevices(this.deviceId);

                        for (int i = 0; i < devices.Count; i++)
                        {
                            ret = MDSUtils.DelSchDevice(devices[i].SdId);
                        }
                    }

                    List<MDS.CLIENT.Domain.SchPlanCfg> planCfgs = MDSUtils.GetSchPlanCfgs(plan.PlanId);

                    ret = MDSUtils.DelSchPlan(plan.PlanId);

                    ret = MDSUtils.DelSchPlanCfgByPlanId(plan.PlanId);

                    //for (int i = 0; i < planCfgs.Count; i++)
                    //{
                    //    ret = MDSUtils.DelSchPlanCfg(planCfgs[i].SpcId);
                    //}
                }

                exePlan_treeView.SelectedNode.Remove();
                exePlan_treeView.Refresh();

                if (!isNew)
                {
                    if (ret)
                    {
                        MDS.CLIENT.Domain.SchPlan plank = (MDS.CLIENT.Domain.SchPlan)this.nodeDefault.Tag;
                        if (plank.PlanId == plan.PlanId) {
                            this.nodeDefault = null;
                        }
                        MessageBox.Show("删除成功!");
                        plans = MDSUtils.GetSchPlans(this.userName);
                        if (plans == null || plans.Count == 0)
                        {
                            this.planIsNull = true;
                        }

                        this.planName_label.Text = " ";
                        Init();
                    }
                    else
                    {
                        MessageBox.Show("删除失败!");
                    }
                }
                else
                {
                    MessageBox.Show("删除成功!");
                }

                if (this.nodeDefault != null)
                {
                    ShowDefaultPicture(this.nodeDefault);
                }
                else {
                    this.exePlan_pictureBox.Visible = false;
                }
                this.enableButton(true);
            }catch(Exception ex){
                log.Error(ex.Message);
                this.enableButton(true);
            }
        }

        private void select_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = exePlan_treeView.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请选择一个执行计划!");
                return;
            }

            try
            {
                MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
                bool isNew = checkIsNew(plan.PlanId);
                bool ret = false;
                if (isNew)
                {
                    if (isSave)
                    {
                        ret = MDSUtils.AddSchDevice(this.deviceId, plan.PlanId);
                    }
                    else
                    {
                        MessageBox.Show("请先保存执行计划内容!");
                        return;
                    }

                }
                else
                {
                    ret = MDSUtils.AddSchDevice(this.deviceId, plan.PlanId);
                }

                if (ret)
                {
                    planDefault = plan;
                    MessageBox.Show("选用成功!");
                    this.nodeDefault = node;
                    ShowDefaultPicture(node);
                }
                else
                {
                    MessageBox.Show("选用失败!");
                }
            }catch(Exception ex){
                log.Error(ex.Message);
            }
        }

        private void planName_textBox_Leave(object sender, EventArgs e)
        {
            checkName();
        }

        private void planName_label_Click(object sender, EventArgs e)
        {
            TreeNode node = exePlan_treeView.SelectedNode;
            if (node == null)
            {
                return;
            }
            planName_textBox.Show();
            planName_textBox.Text = planName_label.Text;
            planName_label.Hide();
        }

        private void planName_textBox_MouseLeave(object sender, EventArgs e)
        {
            checkName();
        }

        private void checkName() {
            planName_textBox.Hide();
            planName_label.Text = planName_textBox.Text;
            planName_label.Show();
            TreeNodeCollection nodes = exePlan_treeView.Nodes;
            TreeNode node = exePlan_treeView.SelectedNode;
            MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
            string aftervalue = planName_textBox.Text;
            string beforevalue = node.Text;
            if (!"".Equals(beforevalue) && "".Equals(aftervalue))
            {
                MessageBox.Show("执行计划名称不能为空!");
                planName_textBox.Show();
                planName_textBox.Undo();
                return;
            }
            else if (aftervalue == null && beforevalue != null)
            {
                bool isNew = checkIsNew(plan.PlanId);
                if (!isNew)
                {
                    return;
                }
                else
                {
                    aftervalue = beforevalue;
                }
            }

            plan.PlanName = aftervalue;

            foreach (TreeNode curNode in nodes)
            {
                if (curNode.Text.Equals(plan.PlanName) && curNode != node)
                {
                    MessageBox.Show("名称重复,请重新命名!");
                    planName_textBox.Show();
                    planName_textBox.Undo();
                    return;
                }
            }

            node.Tag = plan;
            node.Text = aftervalue;
            if (planDefault != null && plan.PlanId == planDefault.PlanId)
            {
                ShowDefaultPicture(node);
            }
        }

        private void addPlan() {
            TreeNode node = new TreeNode();
            int planId1 = -1;
            int planId2 = -1;
            int planId = -1;
            TreeNodeCollection nodes = exePlan_treeView.Nodes;
            try
            {
                enableButton(false);
                if (nodes == null || nodes.Count == 0)
                {
                    planId1 = 0;
                }
                else
                {
                    foreach (TreeNode nodetmp in nodes)
                    {
                        MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)nodetmp.Tag;
                        if (plan.PlanId > planId1)
                        {
                            planId1 = plan.PlanId;
                        }
                    }
                }

                if (this.planIsNull)
                {
                    planId2 = 0;
                }
                else
                {//获取最大PlanId
                    if (plans != null && plans.Count > 0)
                    {
                        plans = plans.OrderByDescending(s => s.PlanId).ToList<MDS.CLIENT.Domain.SchPlan>();
                        planId2 = plans[0].PlanId;
                    }
                    else
                    {
                        planId2 = 0;
                    }
                }

                Init();
                planId = planId1 >= planId2 ? planId1 : planId2;
                planName_label.Hide();
                MDS.CLIENT.Domain.SchPlan newPlan = new MDS.CLIENT.Domain.SchPlan();
                newPlan.Status = 0;
                newPlan.UserName = userName;
                newPlan.PlanId = planId + 1;
                newPlan.PlanName = this.PlanName + DateTime.Now.ToString();
                node.Text = newPlan.PlanName;
                node.Tag = newPlan;
                exePlan_treeView.Nodes.Add(node);
                exePlan_treeView.SelectedNode = node;
                isSave = false;
                node.BeginEdit();
                enableButton(true);
            }catch(Exception ex){
                log.Error(ex.Message);
                enableButton(true);
            }
        }
        private void addPlan_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addPlan();
        }

        private void modPlan_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modPlan();
        }

        private void modPlan() {
            TreeNode node = exePlan_treeView.SelectedNode;
            MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
            currPlan = plan;
            bool ret = checkIsNew(plan.PlanId);
            if (!ret)
            {
                ShowPlanCfg(plan.PlanId, plan.PlanName,false);
            }
            else
            {
                planName_label.Text = plan.PlanName;
                Init();
            }
        }

        private void wire_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeComboBox(wire,0);
        }

        private void wireless_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeComboBox(wireless, 1);
        }

        private void ele_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeComboBox(ele,2);
        }
        private void wireweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeComboBox(wireweek, 0);
        }

        private void wirelessweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeComboBox(wirelessweek, 1);
        }

        private void eleweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeComboBox(eleweek, 2);
        }
        private void wire00_Leave(object sender, EventArgs e)
        {
            checkValue(0, wire00);
        }

        private void wire01_Leave(object sender, EventArgs e)
        {
            checkValue(1, wire01);
        }

        private void wire02_Leave(object sender, EventArgs e)
        {
            checkValue(0, wire02);
        }

        private void wire03_Leave(object sender, EventArgs e)
        {
            checkValue(1, wire03);
        }

        private void wire10_Leave(object sender, EventArgs e)
        {
            checkValue(0, wire10);
        }

        private void wire11_Leave(object sender, EventArgs e)
        {
            checkValue(1, wire11);
        }

        private void wire12_Leave(object sender, EventArgs e)
        {
            checkValue(0, wire12);
        }

        private void wire13_Leave(object sender, EventArgs e)
        {
            checkValue(1, wire13);
        }

        private void wire20_Leave(object sender, EventArgs e)
        {
            checkValue(0, wire20);
        }

        private void wire21_Leave(object sender, EventArgs e)
        {
            checkValue(1, wire21);
        }

        private void wire22_Leave(object sender, EventArgs e)
        {
            checkValue(0, wire22);
        }

        private void wire23_Leave(object sender, EventArgs e)
        {
            checkValue(1, wire23);
        }

        private void wire30_Leave(object sender, EventArgs e)
        {
            checkValue(0, wire30);
        }

        private void wire31_Leave(object sender, EventArgs e)
        {
            checkValue(1, wire31);
        }

        private void wire32_Leave(object sender, EventArgs e)
        {
            checkValue(0, wire32);
        }

        private void wire33_Leave(object sender, EventArgs e)
        {
            checkValue(1, wire33);
        }

        private void wireless00_Leave(object sender, EventArgs e)
        {
            checkValue(0, wireless00);
        }

        private void wireless01_Leave(object sender, EventArgs e)
        {
            checkValue(1, wireless01);
        }

        private void wireless02_Leave(object sender, EventArgs e)
        {
            checkValue(0, wireless02);
        }

        private void wireless03_Leave(object sender, EventArgs e)
        {
            checkValue(1, wireless03);
        }

        private void wireless10_Leave(object sender, EventArgs e)
        {
            checkValue(0, wireless10);
        }

        private void wireless11_Leave(object sender, EventArgs e)
        {
            checkValue(1, wireless11);
        }

        private void wireless12_Leave(object sender, EventArgs e)
        {
            checkValue(0, wireless12);
        }

        private void wireless13_Leave(object sender, EventArgs e)
        {
            checkValue(1, wireless13);
        }

        private void wireless20_Leave(object sender, EventArgs e)
        {
            checkValue(0, wireless20);
        }

        private void wireless21_Leave(object sender, EventArgs e)
        {
            checkValue(1, wireless21);
        }

        private void wireless22_Leave(object sender, EventArgs e)
        {
            checkValue(0, wireless22);
        }

        private void wireless23_Leave(object sender, EventArgs e)
        {
            checkValue(1, wireless23);
        }

        private void wireless30_Leave(object sender, EventArgs e)
        {
            checkValue(0, wireless30);
        }

        private void wireless31_Leave(object sender, EventArgs e)
        {
            checkValue(1, wireless31);
        }

        private void wireless32_Leave(object sender, EventArgs e)
        {
            checkValue(0, wireless32);
        }

        private void wireless33_Leave(object sender, EventArgs e)
        {
            checkValue(1, wireless33);
        }

        private void ele00_Leave(object sender, EventArgs e)
        {
            checkValue(0, ele00);
        }

        private void ele01_Leave(object sender, EventArgs e)
        {
            checkValue(1, ele01);
        }

        private void ele02_Leave(object sender, EventArgs e)
        {
            checkValue(0, ele02);
        }

        private void ele03_Leave(object sender, EventArgs e)
        {
            checkValue(1, ele03);
        }

        private void ele10_Leave(object sender, EventArgs e)
        {
            checkValue(0, ele10);
        }

        private void ele11_Leave(object sender, EventArgs e)
        {
            checkValue(1, ele11);
        }

        private void ele12_Leave(object sender, EventArgs e)
        {
            checkValue(0, ele12);
        }

        private void ele13_Leave(object sender, EventArgs e)
        {
            checkValue(1, ele13);
        }

        private void ele20_Leave(object sender, EventArgs e)
        {
            checkValue(0, ele20);
        }

        private void ele21_Leave(object sender, EventArgs e)
        {
            checkValue(1, ele21);
        }

        private void ele22_Leave(object sender, EventArgs e)
        {
            checkValue(0, ele22);
        }

        private void ele23_Leave(object sender, EventArgs e)
        {
            checkValue(1, ele23);
        }

        private void ele30_Leave(object sender, EventArgs e)
        {
            checkValue(0, ele30);
        }

        private void ele31_Leave(object sender, EventArgs e)
        {
            checkValue(1, ele31);
        }

        private void ele32_Leave(object sender, EventArgs e)
        {
            checkValue(0, ele32);
        }

        private void ele33_Leave(object sender, EventArgs e)
        {
            checkValue(1, ele33);
        }

        private void selectMany_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = exePlan_treeView.SelectedNode;

            if (node == null)
            {
                return;
            }
            MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;

            FrmLotPlans lotPlans = new FrmLotPlans(plan, this.docXml);
            lotPlans.ShowDialog();
        }

        private void wire_panel_Paint(object sender, PaintEventArgs e)
        {
            paint(wire_panel, e);
        }

        private void wireless_panel_Paint(object sender, PaintEventArgs e)
        {
            paint(wireless_panel,e);
        }

        private void ele_panel_Paint(object sender, PaintEventArgs e)
        {
            paint(ele_panel, e);
        }

        private void paint(Panel panel, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,panel.ClientRectangle,
                                         Color.Black,1,
                                         ButtonBorderStyle.Solid,
                                         Color.Black,1,
                                         ButtonBorderStyle.Solid,
                                         Color.Black,1,
                                         ButtonBorderStyle.Solid,
                                         Color.Black,1,
                                         ButtonBorderStyle.Solid);
        }

        private void cancelselect_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = this.exePlan_treeView.SelectedNode;
            MDS.CLIENT.Domain.SchPlan plan = (MDS.CLIENT.Domain.SchPlan)node.Tag;
            if (!this.nodeDefault.Text.Equals(node.Text))
            {
                return;
            }

            enableButton(false);
            try
            {
                List<MDS.CLIENT.Domain.SchDevice> devices = MDSUtils.GetDevicesByPlanId(plan.PlanId);
                MDS.CLIENT.Domain.SchDevice schDevice = new MDS.CLIENT.Domain.SchDevice();
                foreach (MDS.CLIENT.Domain.SchDevice device in devices)
                {
                    if (device.DeviceID.Equals(this.deviceId))
                    {
                        schDevice = device;
                        break;
                    }
                }

                bool ret = MDSUtils.DelSchDevice(schDevice.SdId);

                if (ret)
                {
                    MessageBox.Show("取消选用成功!");
                    this.nodeDefault = null;
                    this.exePlan_pictureBox.Visible = false;
                }
                else {
                    MessageBox.Show("取消选用失败!");
                }

                enableButton(true);
            }catch(Exception ex){
                log.Error(ex.Message);
                MessageBox.Show("取消选用失败!");
                enableButton(true);
            }
        }
    }
}
