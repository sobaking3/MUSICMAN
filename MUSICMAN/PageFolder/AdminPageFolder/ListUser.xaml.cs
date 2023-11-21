using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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