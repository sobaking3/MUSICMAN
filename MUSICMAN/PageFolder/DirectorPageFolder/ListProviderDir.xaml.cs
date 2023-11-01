﻿using MUSICMAN.ClassFolder;
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
    /// Логика взаимодействия для ListProviderDir.xaml
    /// </summary>
    public partial class ListProviderDir : Page
    {
        public ListProviderDir()
        {
            InitializeComponent();
            ListProviderDG.ItemsSource = DBEntities.GetContext()
                   .Provider.ToList().OrderBy(u => u.IdProvider);
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Updater();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void Updater()
        {
            ListProviderDG.ItemsSource = DBEntities.GetContext()
                                .Provider.Where(s => s.ProviderName
                                .StartsWith(SearchTb.Text))
                                .ToList().OrderBy(s => s.ProviderName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddProvider().ShowDialog();
            Updater();
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Provider Provider = ListProviderDG.SelectedItem as Provider;

                if (ListProviderDG.SelectedItem == null)
                {
                    MBClass.ErrorMB("Поставщик не выбран");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить поставщика " +
                    $"с названием {Provider.ProviderName}?"))
                    {
                        DBEntities.GetContext().Provider.Remove(ListProviderDG.SelectedItem as Provider);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Поставщик удален");
                        ListProviderDG.ItemsSource = DBEntities.GetContext()
            .Provider.ToList().OrderBy(u => u.ProviderName);
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
