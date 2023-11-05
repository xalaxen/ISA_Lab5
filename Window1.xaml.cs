using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// System.IO.IOException: "Cannot locate resource 'mainwindow.xaml'."
    /// </summary>
    public partial class Window1 : Window
    {
        VkApp vkApp;

        public Window1(VkApp model)
        {
            InitializeComponent();
            vkApp = new VkApp(ref model, ref InfoTB);
        }

        private void GetProfileInfoBTN_Click(object sender, RoutedEventArgs e)
        {
            vkApp.GetProfileInfo();
        }

        private void GetFriendsListBTN_Click(object sender, RoutedEventArgs e)
        {
            vkApp.GetFriendsList();
        }
    }
}
