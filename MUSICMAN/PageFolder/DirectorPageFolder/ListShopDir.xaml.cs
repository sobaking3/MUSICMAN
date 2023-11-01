using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.PageFolder.AdminPageFolder;
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
    /// Логика взаимодействия для ListShopDir.xaml
    /// </summary>
    public partial class ListShopDir : Page
    {
        public ListShopDir()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddShop().ShowDialog();
            ListShopLB.ItemsSource = DBEntities.GetContext()
                .Shop.ToList().OrderBy(u => u.ShopName);

        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Shop shop = ListShopLB.SelectedItem as Shop;

                if (ListShopLB.SelectedItem == null)
                {
                    MBClass.ErrorMB("Магазин не выбран");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить магазин " +
                    $"с названием {shop.ShopName}?"))
                    {
                        DBEntities.GetContext().Shop.Remove(ListShopLB.SelectedItem as Shop);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Пользователь удален");
                        ListShopLB.ItemsSource = DBEntities.GetContext()
            .Shop.ToList().OrderBy(u => u.ShopName);
                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}
