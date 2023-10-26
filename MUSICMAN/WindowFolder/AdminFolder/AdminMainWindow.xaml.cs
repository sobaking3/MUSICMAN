using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.PageFolder.AdminPageFolder;
using System;
using System.Collections.Generic;
using System.IO;
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
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        Notifier notifier;      
        public AdminMainWindow()
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
            EmpName.Text = App.GetCurrentWorkerInitials();
            
        }

        private void ListUserBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListUser());
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.MBLogOut(this);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.MBExit();
        }

        private void PlastList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListShopBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListShop());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //App.Notifier.ShowInformation("Добро пожаловать суикн сын, рутинной работы!");
            notifier.ShowInformation("Добро пожаловать!");
        }
    }
}
