using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.ManagerFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListEmployeeDir.xaml
    /// </summary>
    public partial class ListEmployeeDir : Page
    {
        public ListEmployeeDir()
        {
            InitializeComponent();
            ListEmployeeLB.ItemsSource = DBEntities.GetContext()
            .Workers.Where(u => u.User.Roles.NameRole != "Директор")
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
                    .Workers.Where(u => u.FirstName.StartsWith(SearchTb.Text) &&
                    u.User.Roles.NameRole != "Директор")
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
            new AddEmployeeDir().ShowDialog();
            UpdateList();
        }

        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            Workers selectedPlate = ListEmployeeLB.SelectedItem as Workers;
            if (selectedPlate != null)
            {
                new EditEmployeeDir(selectedPlate).ShowDialog();
                UpdateList();
            }
            else
            {
                ManagerMainWindow.notifier.ShowError("Вы не выюбрали сотрудника");
            }
        }
    }
}