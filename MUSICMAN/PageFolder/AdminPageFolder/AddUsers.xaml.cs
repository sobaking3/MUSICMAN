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

namespace MUSICMAN.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Window
    {
        public AddUsers()
        {
            InitializeComponent();
            RoleCb.ItemsSource = DBEntities.GetContext()
            .Roles.Except(DBEntities.GetContext().Roles.Where(r => r.NameRole == "Администратор" || r.NameRole == "Директор"))
            .ToList();
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
            }
            else if (string.IsNullOrEmpty(PasswordPb.Text))
            {
                MBClass.ErrorMB("Введите пароль");
            }
            else if (RoleCb.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Вы не выбрали роль");
            }
            else if (DBEntities.GetContext()
                        .User
                        .FirstOrDefault(
                u => u.Login == LoginTb.Text) != null)
            {
                MBClass.ErrorMB("Такой пользователь уже создан");

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
                MBClass.InfoMB("Пользователь создан");
                ElementsToolsClass.ClearAllControls(this);
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
