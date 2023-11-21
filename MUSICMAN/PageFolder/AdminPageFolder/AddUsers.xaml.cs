using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Window
    {
        private Notifier notifier;

        public AddUsers()
        {
            InitializeComponent();
            notifier = App.GetWindowNotifer(this);
            RoleCb.ItemsSource = DBEntities.GetContext()
            .Roles.Except(DBEntities.GetContext().Roles.Where(r => r.NameRole == "Администратор" || r.NameRole == "Директор"))
            .ToList();
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text))
            {
                notifier.ShowError("Введите логин");
            }
            else if (string.IsNullOrEmpty(PasswordPb.Text))
            {
                notifier.ShowError("Введите пароль");
            }
            else if (RoleCb.SelectedIndex == -1)
            {
                notifier.ShowError("Вы не выбрали роль");
            }
            else if (DBEntities.GetContext()
                        .User
                        .FirstOrDefault(
                u => u.Login == LoginTb.Text) != null)
            {
                notifier.ShowError($"Пользователь {LoginTb.Text} уже создан");

                LoginTb.Focus();
            }
            else
            {
                var context = DBEntities.GetContext();
                User user = new User();
                user.Login = LoginTb.Text;
                user.Password = PasswordPb.Text;
                user.IdRole = Int32.Parse
                    (RoleCb.SelectedValue.ToString());
                context.User.Add(user);
                context.SaveChanges();
                notifier.ShowSuccess($"Пользователь {LoginTb.Text} создан");
                ElementsToolsClass.ClearAllControls(this);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(500).ContinueWith(_ => // Задержка в 1 секунду
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Close(); // Закрытие окна
                });
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }
    }
}