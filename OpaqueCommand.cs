using System;
using System.Windows.Forms;

namespace AlarmMapClient
{
    class OpaqueCommand
    {
        private UCOpaqueLayer m_OpaqueLayer = null;//半透明蒙板层

        //private OpaqueCommand() { }
        //private static OpaqueCommand instance;
        //public static OpaqueCommand Instance
        //{
        //   get 
        //  {
        //      if (instance == null)
        //      {
        //          instance = new OpaqueCommand();
        //      }
        //      return instance; 
        //  } 
        //}

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="alpha">透明度</param>
        /// <param name="isShowLoadingImage">是否显示图标</param>
        public void ShowOpaqueLayer(Control control, int alpha, bool isShowLoadingImage)
        {
            try
            {
                if (this.m_OpaqueLayer == null)
                {
                    this.m_OpaqueLayer = new UCOpaqueLayer(alpha, isShowLoadingImage);
                    control.Controls.Add(this.m_OpaqueLayer);
                    this.m_OpaqueLayer.Dock = DockStyle.Fill;
                    this.m_OpaqueLayer.BringToFront();
                }
                this.m_OpaqueLayer.Enabled = true;
                this.m_OpaqueLayer.Visible = true;
            }
            catch { }
        }

        /// <summary>
        /// 隐藏遮罩层
        /// </summary>
        public void HideOpaqueLayer()
        {
            try
            {
                if (this.m_OpaqueLayer != null)
                {
                    this.m_OpaqueLayer.Visible = false;
                    this.m_OpaqueLayer.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
