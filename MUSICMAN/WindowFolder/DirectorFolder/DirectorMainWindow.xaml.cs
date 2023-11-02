using MUSICMAN.ClassFolder;
using MUSICMAN.PageFolder.AdminPageFolder;
using MUSICMAN.PageFolder.DirectorPageFolder;
using MUSICMAN.PageFolder.ManagerPageFolder;
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
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.WindowFolder.DirectorFolder
{
    /// <summary>
    /// Логика взаимодействия для DirectorMainWindow.xaml
    /// </summary>
    public partial class DirectorMainWindow : Window
    {
        Notifier notifier;
        public DirectorMainWindow()
        {
            InitializeComponent();
            notifier = App.GetWindowNotifer(this);
            EmpName.Text = App.GetCurrentWorkerInitials();
        }

        private void ListUserBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListEmployee());
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

        private void ShopListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListShopDir());
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
            MainFrame.Navigate(new ListPlate());
        }

        private void ListAuthor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListCompositors());
        }

        private void ListPublisher_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListPublisherDir());
        }

        private void ListProvider_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListProviderDir());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
            notifier.ShowInformation("Добро пожаловать!");
        }

        private void ListStock_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListStockDir());
        }
    }
}
