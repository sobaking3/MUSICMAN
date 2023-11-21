using MUSICMAN.ClassFolder;
using MUSICMAN.PageFolder.EmployeePageFolder;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.WindowFolder.EmployeeMainWindow
{
    /// <summary>
    /// Логика взаимодействия для EmployeeMainWindow.xaml
    /// </summary>
    public partial class EmployeeMainWindow : Window
    {
        public static Notifier notifier;

        public EmployeeMainWindow()
        {
            InitializeComponent();
            notifier = App.GetWindowNotifer(this);
            EmpName.Text = App.GetCurrentWorkerInitials();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
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

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.MBLogOut(this);
        }

        private void PlastList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListPlateEmp());
        }

        private void ListAuthor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListCompositorsEmp());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
            notifier.ShowInformation("Добро пожаловать!");
        }
    }
}