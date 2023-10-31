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
using System.Windows.Shapes;

namespace MUSICMAN.PageFolder.EmployeePageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListCompositorsEmp.xaml
    /// </summary>
    public partial class ListCompositorsEmp : Window
    {
        public ListCompositorsEmp()
        {
            InitializeComponent();
            ListCompositorsLB.ItemsSource = DBEntities.GetContext()
                .Composer.ToList().OrderBy(u => u.ComposerName);
        }

        private void UpdateList()
        {
            ListCompositorsLB.ItemsSource = DBEntities.GetContext()
                .Composer.Where(s => s.ComposerName
                .StartsWith(SearchTb.Text) || s.ComposerName
                .StartsWith(SearchTb.Text))
                .ToList().OrderBy(s => s.ComposerName);
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
    }
}
