using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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