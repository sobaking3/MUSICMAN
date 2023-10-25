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
