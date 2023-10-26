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

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddPlate.xaml
    /// </summary>
    public partial class AddPlate : Window
    {
        public AddPlate()
        {
            InitializeComponent();
            ComboShop.ItemsSource = DBEntities.GetContext().Shop.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboShopCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource; if (tb.SelectionStart != 0)
            {
                ComboShop.SelectedItem = null;
            }
            if (tb.SelectionStart == 0 && ComboShop.SelectedItem == null)
            {
                ComboShop.IsDropDownOpen = false;
            }
            ComboShop.IsDropDownOpen = true;
            if (ComboShop.SelectedItem == null)
            {
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(ComboShop.ItemsSource);
                cv.Filter = s => (s as Shop).ShopName.IndexOf(ComboShop.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
        }

        private void AddCompositorBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
