using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDS.CLIENT;

namespace AlarmMapClient
{
    public partial class FrmLotPlans : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string docXml;
        private MDS.CLIENT.Domain.SchPlan plan;
        private DOrganization org;
        private TreeNode treeRootNode;
        public FrmLotPlans(MDS.CLIENT.Domain.SchPlan plan, string docXml)
        {
            InitializeComponent();
            this.docXml = docXml;
            this.plan = plan;
        }

        private void FrmLotPlans_Load(object sender, EventArgs e)
        {
            LoadPlans();

            device_treeView.Nodes.Clear();
            device_treeView.CheckBoxes = true;
            this.treeRootNode = new TreeNode("所有设备");
            treeRootNode.Tag = null;
            LoadDevices(this.treeRootNode,this.docXml);
            device_treeView.Nodes.Add(treeRootNode);

            ShowSelect();
        }

        private void ShowSelect()
        {
            List<MDS.CLIENT.Domain.SchDevice> devices = MDSUtils.GetDevicesByPlanId(plan.PlanId);

            try {
                if (devices != null && devices.Count > 0)
                {
                    foreach (MDS.CLIENT.Domain.SchDevice device in devices)
                    {
                        CheckDeviceId(this.device_treeView.Nodes, device.DeviceID);
                    }
                }
            }catch(Exception ex){
                log.Error(ex.Message);
            }
            
        }

        private void CheckDeviceId(TreeNodeCollection tnc,string deviceId)
        {
            try {
                foreach (TreeNode node in tnc)
                {
                    if (node.Tag is DDevice)
                    {
                        DDevice dev = (DDevice)node.Tag;
                        if (dev.Id == deviceId)
                        {
                            node.Checked = true;
                            return;
                        }
                    }
                    else
                    {
                        CheckDeviceId(node.Nodes, deviceId);
                    }
                }
            }catch (Exception ex) {
                log.Error(ex.Message);
            }
            
        }
        private void LoadPlans()
        {
            TreeNode node = new TreeNode();
            node.Text = this.plan.PlanName;
            node.Tag = this.plan;
            plan_treeView.Nodes.Add(node);
        }

        private void LoadDevices(TreeNode rootnode, string xml)
        {
            try
            {
                GroupXmlParser parser = new GroupXmlParser(xml);
                org = parser.Parse();

                foreach (DDepartment dd in org.Departs)
                {
                    loadDepartTree(rootnode, dd, null);
                }
                rootnode.Expand();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadDepartTree(TreeNode pnode, DDepartment dd,string name)
        {
            foreach (DDepartment depart in dd.Departs)
            {
                if ((depart.Departs != null && depart.Departs.Count > 0) ||
                    depart.Devices != null && depart.Devices.Count > 0)
                {
                    TreeNode node = new TreeNode(depart.Name);
                    node.Tag = depart;
                    pnode.Nodes.Add(node);

                    loadDepartTree(node, depart, name);
                }
            }

            foreach (DDevice dev in dd.Devices)
            {
                if (name != null && dev.Title.IndexOf(name) ==-1) continue;

                TreeNode node = new TreeNode(dev.Title);
                node.Tag = dev;
                node.SelectedImageIndex = 5;
                pnode.Nodes.Add(node);
            }
        }

        private void device_treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                CheckAllChildNodes(e.Node, e.Node.Checked);
                //选中父节点 
                bool bol = true;
                if (e.Node.Parent != null)
                {
                    for (int i = 0; i < e.Node.Parent.Nodes.Count; i++)
                    {
                        if (!e.Node.Parent.Nodes[i].Checked)
                            bol = false;
                    }
                    e.Node.Parent.Checked = bol;
                }
            }
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void getSelectDevice(TreeNodeCollection tnc, List<DDevice> devices)
        {
            foreach(TreeNode node in tnc)
            {
                if (node.Checked)
                {
                    if (node.Tag is DDevice)
                    {
                        DDevice device = (DDevice)node.Tag;
                        devices.Add(device);
                    }
                }
                getSelectDevice(node.Nodes, devices); 
            }
        }
        private void select_button_Click(object sender, EventArgs e)
        {
            List<DDevice> devices = new List<DDevice>();
            getSelectDevice(device_treeView.Nodes, devices);
            bool ret = false;

            List<MDS.CLIENT.Domain.SchDevice> deviceold = MDSUtils.GetDevicesByPlanId(plan.PlanId);

            if (deviceold != null && deviceold.Count > 0) {
                foreach(MDS.CLIENT.Domain.SchDevice device in deviceold){
                    ret = MDSUtils.DelSchDevice(device.SdId);
                }
            }

            foreach (DDevice device in devices)
            {
                ret = MDSUtils.AddSchDevice(device.Id,plan.PlanId);
            }

            if (ret)
            {
                MessageBox.Show("保存成功!");
            }
            else {
                MessageBox.Show("保存失败!");
            }
        }
    }
}
