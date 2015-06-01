using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace AlarmMapClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        [HandleProcessCorruptedStateExceptions]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException); 
            Application.Run(new FrmLogin());
            //Application.Run(new FrmTest());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("没有捕获的错误！"+e.ExceptionObject.ToString(), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //作为示例，这里用消息框显示异常的信息 
            MessageBox.Show(e.Exception.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
