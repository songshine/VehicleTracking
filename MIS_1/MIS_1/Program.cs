using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MIS_1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogForm lf = new LogForm();
            MainFram mf = new MainFram();
            if(mf.LoginMIS(lf))
                Application.Run(mf);
        }

    }
}