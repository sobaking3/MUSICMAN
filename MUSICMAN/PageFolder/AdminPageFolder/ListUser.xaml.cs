using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MUSICMAN.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListUser.xaml
    /// </summary>
    public partial class ListUser : Page
    {
        public ListUser()
        {
            InitializeComponent();
            ListUserDG.ItemsSource = DBEntities.GetContext()
            .User.Where(u => u.Roles.NameRole != "Директор" && u.Roles.NameRole != "Администратор")
            .ToList().OrderBy(u => u.Login);
        }
        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            passwordBox.PasswordChar = '\0';
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                UpdateList();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void UpdateList()
        {
            ListUserDG.ItemsSource = DBEntities.GetContext()
                    .User.Where(u => u.Login.StartsWith(SearchTb.Text) && u.Roles.NameRole != "Администратор" && u.Roles.NameRole != "Директор")
                    .OrderBy(u => u.Login)
                    .ToList();
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = ListUserDG.SelectedItem as User;

                if (ListUserDG.SelectedItem == null)
                {
                    MBClass.ErrorMB("Пользователь не выбран");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить пользователя " +
                    $"с логином {user.Login}?"))
                    {
                        DBEntities.GetContext().User.Remove(ListUserDG.SelectedItem as User);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Пользователь удален");
                        UpdateList();
                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите пользователя для " +
                    "редактирования!");
            }
            else
            {
                NavigationService.Navigate(
                    new EditUser(
                        ListUserDG.SelectedItem as User));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddUsers().ShowDialog();
            UpdateList();
        }
    }
}
