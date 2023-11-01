using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.PageFolder.DirectorPageFolder;
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

namespace MUSICMAN.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListShop.xaml
    /// </summary>
    public partial class ListShop : Page
    {
        public ListShop()
        {
            InitializeComponent();
            ListShopLB.ItemsSource = DBEntities.GetContext()
                .Shop.ToList().OrderBy(u => u.ShopName);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ListShopLB.ItemsSource = DBEntities.GetContext()
                    .Shop.Where(s => s.ShopName
                    .StartsWith(SearchTb.Text) || s.ShopName
                    .StartsWith(SearchTb.Text))
                    .ToList().OrderBy(s => s.ShopName);
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

    }
}
