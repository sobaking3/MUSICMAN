using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Windows.Controls;

namespace MUSICMAN.PageFolder.EmployeePageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListPlateEmp.xaml
    /// </summary>
    public partial class ListPlateEmp : Page
    {
        public ListPlateEmp()
        {
            InitializeComponent();
            ListPlatesDG.ItemsSource = DBEntities.GetContext()
               .Plates.ToList().OrderBy(u => u.IdPlate);
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
            ListPlatesDG.ItemsSource = DBEntities.GetContext()
                 .Plates.Where(s => s.PlateName
                 .StartsWith(SearchTb.Text) || s.Composer.ComposerName
                 .StartsWith(SearchTb.Text))
                 .ToList().OrderBy(s => s.PlateName);
        }
    }
}