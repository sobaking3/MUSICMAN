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
    /// Логика взаимодействия для ListStock.xaml
    /// </summary>
    public partial class ListStock : Page
    {
        
        public ListStock()
        {
            InitializeComponent();
            ListStocksDG.ItemsSource = DBEntities.GetContext()
                .Stocks.ToList().OrderBy(u => u.IdStock);
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ListStocksDG.ItemsSource = DBEntities.GetContext()
                    .Stocks.Where(s => s.StockNumber
                    .StartsWith(SearchTb.Text) || s.IndividualAdress
                    .StartsWith(SearchTb.Text))
                    .ToList().OrderBy(s => s.StockNumber);
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}
