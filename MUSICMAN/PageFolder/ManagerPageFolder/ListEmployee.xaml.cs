using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.PageFolder.AdminPageFolder;
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

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListEmployee.xaml
    /// </summary>
    public partial class ListEmployee : Page
    {
        public ListEmployee()
        {
            InitializeComponent();
            ListEmployeeLB.ItemsSource = DBEntities.GetContext()
            .Workers.Where(u => u.User.Roles.NameRole != "Директор" && u.User.Roles.NameRole != "Администратор" && u.User.Roles.NameRole != "Менеджер")
            .ToList().OrderBy(u => u.LastName);
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
            ListEmployeeLB.ItemsSource = DBEntities.GetContext()
                    .Workers.Where(u => u.FirstName.StartsWith(SearchTb.Text) && u.User.Roles.NameRole != "Администратор" &&
                    u.User.Roles.NameRole != "Директор" && u.User.Roles.NameRole != "Менеджер")
                    .OrderBy(u => u.FirstName)
                    .ToList();
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Workers Workers = ListEmployeeLB.SelectedItem as Workers;

                if (ListEmployeeLB.SelectedItem == null)
                {
                    MBClass.ErrorMB("Пользователь не выбран");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить пользователя " +
                    $"с Фамилией {Workers.LastName}?"))
                    {
                        DBEntities.GetContext().Workers.Remove(ListEmployeeLB.SelectedItem as Workers);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddEmployee().ShowDialog();
            UpdateList();
        }
    }
}
