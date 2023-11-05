using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab5
{
    public partial class MainWindow : Window
    {
        VkApp vkApp;

        public MainWindow()
        {
            InitializeComponent();
            vkApp = new VkApp(ref browser);
            DataContext = vkApp;
        }

        private void browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            vkApp.GetUserId(ref sender, ref e);
            if (vkApp.IsIDGot())
            {
                this.Dispatcher.Invoke(() =>
                {
                    Window1 vkAppWindow = new Window1(vkApp);
                    vkAppWindow.Show();
                    vkAppWindow.Focus();
                    this.Close();   
                });
            }
        }
    }
}
