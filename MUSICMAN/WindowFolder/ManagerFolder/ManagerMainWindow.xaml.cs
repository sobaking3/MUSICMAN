using MUSICMAN.ClassFolder;
using MUSICMAN.PageFolder.AdminPageFolder;
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

namespace MUSICMAN.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
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
            MainFrame.Navigate(new ListShop());
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
            MainFrame.Navigate(new ListPublisher());
        }

        private void ListProvider_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListProvider());
        }
    }
}
