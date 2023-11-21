using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Windows.Controls;

namespace MUSICMAN.PageFolder.EmployeePageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListCompositorsEmp.xaml
    /// </summary>
    public partial class ListCompositorsEmp : Page
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