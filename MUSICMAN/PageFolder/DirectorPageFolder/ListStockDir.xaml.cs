using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.PageFolder.ManagerPageFolder;
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
    /// Логика взаимодействия для ListStockDir.xaml
    /// </summary>
    public partial class ListStockDir : Page
    {
        public ListStockDir()
        {
            InitializeComponent();
            ListStocksDG.ItemsSource = DBEntities.GetContext()
               .Stocks.ToList().OrderBy(u => u.IdStock);
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
            ListStocksDG.ItemsSource = DBEntities.GetContext()
                .Stocks.Where(s => s.StockNumber
                .StartsWith(SearchTb.Text) || s.IndividualAdress
                .StartsWith(SearchTb.Text))
                .ToList().OrderBy(s => s.StockNumber);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddStock().ShowDialog();
            UpdateList();
        }
        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stocks Stocks = ListStocksDG.SelectedItem as Stocks;

                if (ListStocksDG.SelectedItem == null)
                {
                    MBClass.ErrorMB("Склад не выбран");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить склад " +
                    $"с номером {Stocks.StockNumber}?"))
                    {
                        DBEntities.GetContext().Stocks.Remove(ListStocksDG.SelectedItem as Stocks);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Склад удален");
                        UpdateList();
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
