using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Windows.Controls;

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListPublisher.xaml
    /// </summary>
    public partial class ListPublisher : Page
    {
        public ListPublisher()
        {
            InitializeComponent();
            ListPublisherDG.ItemsSource = DBEntities.GetContext()
              .Publisher.ToList().OrderBy(u => u.IdPublisher);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ListPublisherDG.ItemsSource = DBEntities.GetContext()
                    .Publisher.Where(s => s.PublisherName
                    .StartsWith(SearchTb.Text))
                    .ToList().OrderBy(s => s.PublisherName);
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}