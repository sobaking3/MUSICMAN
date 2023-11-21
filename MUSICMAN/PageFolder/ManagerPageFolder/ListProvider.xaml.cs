using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Windows.Controls;

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListProvider.xaml
    /// </summary>
    public partial class ListProvider : Page
    {
        public ListProvider()
        {
            InitializeComponent();
            ListProviderDG.ItemsSource = DBEntities.GetContext()
                .Provider.ToList().OrderBy(u => u.IdProvider);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ListProviderDG.ItemsSource = DBEntities.GetContext()
                    .Provider.Where(s => s.ProviderName
                    .StartsWith(SearchTb.Text))
                    .ToList().OrderBy(s => s.ProviderName);
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}