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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
