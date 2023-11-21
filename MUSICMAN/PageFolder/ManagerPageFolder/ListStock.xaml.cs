using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Windows.Controls;

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