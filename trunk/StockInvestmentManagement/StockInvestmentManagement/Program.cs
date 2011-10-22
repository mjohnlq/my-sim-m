using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StockInvestmentManagement
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
            PersistenceLayer.Setting.Instance().DatabaseMapFile = "DatabaseMap.xml";
            Application.Run(new Main());
        }
    }
}
