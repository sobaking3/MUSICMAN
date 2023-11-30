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
    /// Логика взаимодействия для ListPublisherDir.xaml
    /// </summary>
    public partial class ListPublisherDir : Page
    {
        public ListPublisherDir()
        {
            InitializeComponent();
            ListPublisherDG.ItemsSource = DBEntities.GetContext()
                 .Publisher.ToList().OrderBy(u => u.IdPublisher);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Updater();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void Updater()
        {
            ListPublisherDG.ItemsSource = DBEntities.GetContext()
                                .Publisher.Where(s => s.PublisherName
                                .StartsWith(SearchTb.Text))
                                .ToList().OrderBy(s => s.PublisherName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddPublisher().ShowDialog();
            Updater();
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Publisher Publisher = ListPublisherDG.SelectedItem as Publisher;

                if (ListPublisherDG.SelectedItem == null)
                {
                    MBClass.ErrorMB("Издатель не выбран");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить издателя " +
                    $"с названием {Publisher.PublisherName}?"))
                    {
                        DBEntities.GetContext().Publisher.Remove(ListPublisherDG.SelectedItem as Publisher);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Издатель удален");
                        ListPublisherDG.ItemsSource = DBEntities.GetContext()
            .Publisher.ToList().OrderBy(u => u.PublisherName);
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
            Publisher selectedPlate = ListPublisherDG.SelectedItem as Publisher;
            if (selectedPlate != null)
            {
                new EditPublisher(selectedPlate).ShowDialog();
                Updater();
            }
            else
            {
                ManagerMainWindow.notifier.ShowError("Вы не выюбрали поставщика");
            }
        }
    }
}