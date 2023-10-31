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
