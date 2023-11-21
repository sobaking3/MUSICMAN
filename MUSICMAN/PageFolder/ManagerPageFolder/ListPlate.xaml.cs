using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.ManagerFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListPlate.xaml
    /// </summary>
    public partial class ListPlate : Page
    {
        public ListPlate()
        {
            InitializeComponent();
            ListPlatesDG.ItemsSource = DBEntities.GetContext()
                .Plates.ToList().OrderBy(u => u.IdPlate);
        }

        private void UpdateList()
        {
            ListPlatesDG.ItemsSource = DBEntities.GetContext()
                 .Plates.Where(s => s.PlateName
                 .StartsWith(SearchTb.Text) || s.Composer.ComposerName
                 .StartsWith(SearchTb.Text))
                 .ToList().OrderBy(s => s.PlateName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddPlate().ShowDialog();
            UpdateList();
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

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Plates Plates = ListPlatesDG.SelectedItem as Plates;

                if (ListPlatesDG.SelectedItem == null)
                {
                    MBClass.ErrorMB("Пластинка не выбрана");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить пластинку " +
                    $"с названием {Plates.PlateName}?"))
                    {
                        DBEntities.GetContext().Plates.Remove(ListPlatesDG.SelectedItem as Plates);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Пластинка удалена");
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
            Plates selectedPlate = ListPlatesDG.SelectedItem as Plates;
            if (selectedPlate != null)
            {
                new EditPlate(selectedPlate).ShowDialog();
                ListPlatesDG.ItemsSource = DBEntities.GetContext()
                .Plates.ToList().OrderBy(u => u.IdPlate);
            }
            else
            {
                ManagerMainWindow.notifier.ShowError("Вы не выюбрали пластинку");
            }
        }
    }
}