using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.DirectorFolder;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private Notifier notifier;

        public Authorization()
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.CloseWindow(this);
            Task.Delay(500).ContinueWith(_ => // Задержка в 1 секунду
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Application.Current.Shutdown();
                });
            });
        }

        private DispatcherTimer timer = new DispatcherTimer();

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text) || string.IsNullOrEmpty(PasswordPb.Password))
            {
                notifier.ShowError("Пожалуйста, введите логин и пароль");
            }
            else if (timer.IsEnabled)
            {
                MBClass.InfoMB("Подождите, пока таймер истечет");
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext()
                        .User
                        .FirstOrDefault(u => u.Login == LoginTb.Text);
                    if (user == null)
                    {
                        notifier.ShowError("Пароль или логин введен неверно");
                        LoginTb.Focus();
                    }
                    else if (user.Password != PasswordPb.Password)
                    {
                        notifier.ShowError("Пароль или логин введен неверно");
                    }
                    else
                    {
                        App.CurrentUser = user;
                        Hide();
                        Window window = null;
                        switch (user.IdRole)
                        {
                            case 1:
                                window = new AdminFolder.AdminMainWindow();
                                break;

                            case 2:
                                window = new ManagerFolder.ManagerMainWindow();
                                break;

                            case 3:
                                window = new DirectorMainWindow();
                                break;

                            case 4:
                                window = new EmployeeMainWindow.EmployeeMainWindow();
                                break;
                        }

                        if (window != null)
                        {
                            //Application.Current.MainWindow = window;
                            window.Show();
                        }
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }
    }
}