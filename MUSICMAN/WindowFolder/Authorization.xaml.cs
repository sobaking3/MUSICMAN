using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
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
using System.Windows.Threading;

namespace MUSICMAN.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private DispatcherTimer timer = new DispatcherTimer();


        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text) || string.IsNullOrEmpty(PasswordPb.Password))
            {
                MBClass.ErrorMB("Пожалуйста, введите логин и пароль");
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
                        MBClass.ErrorMB("Пароль или логин введен неверно");
                        LoginTb.Focus();
                    }
                    else if (user.Password != PasswordPb.Password)
                    {
                        MBClass.ErrorMB("Пароль или логин введен неверно");                       
                    }
                    else
                    {
                        App.CurrentUser = user;
                        
                        switch (user.IdRole)
                        {
                            case 1:
                                MBClass.InfoMB("Администратор");
                                new AdminFolder.AdminMainWindow().ShowDialog();
                                Hide();
                                break;
                            case 2:
                                MBClass.InfoMB("Менеджер");
                                new ManagerFolder.ManagerMainWindow().ShowDialog();
                                Hide();
                                break;
                                //case 3:
                                //    MBClass.InfoMB("Сотрудник");
                                //    new EmployeeFolder.EmployeeMainWindow().ShowDialog();
                                //    break;
                                //case 4:
                                //    MBClass.InfoMB("Директор");
                                //    new DirectorFolder.DirectorMainWindow().ShowDialog();
                                //    Hide();
                                //    break;
                        }
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
    }
}
