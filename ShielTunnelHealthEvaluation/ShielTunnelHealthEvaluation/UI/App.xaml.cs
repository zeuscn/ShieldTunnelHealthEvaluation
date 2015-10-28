using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ShielTunnelHealthEvaluation.UI;

namespace ShielTunnelHealthEvaluation
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application app = new Application();
            TestWnd win = new TestWnd();
            app.Run(win);
        }
    }
}
