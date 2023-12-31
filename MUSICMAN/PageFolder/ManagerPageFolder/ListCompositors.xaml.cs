﻿using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.ManagerFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListCompositors.xaml
    /// </summary>
    public partial class ListCompositors : Page
    {
        public ListCompositors()
        {
            InitializeComponent();
            ListCompositorsLB.ItemsSource = DBEntities.GetContext()
                .Composer.ToList().OrderBy(u => u.ComposerName);
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
            ListCompositorsLB.ItemsSource = DBEntities.GetContext()
                .Composer.Where(s => s.ComposerName
                .StartsWith(SearchTb.Text) || s.ComposerName
                .StartsWith(SearchTb.Text))
                .ToList().OrderBy(s => s.ComposerName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddCompositor().ShowDialog();
            UpdateList();
        }

        private void DeleteM1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Composer Composer = ListCompositorsLB.SelectedItem as Composer;

                if (ListCompositorsLB.SelectedItem == null)
                {
                    MBClass.ErrorMB("Группа не выбрана");
                }
                else
                {
                    if (MBClass.QuestionMB($"Удалить группу " +
                    $"с именем {Composer.ComposerName}?"))
                    {
                        DBEntities.GetContext().Composer.Remove(ListCompositorsLB.SelectedItem as Composer);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InfoMB("Группа удалена");
                        ListCompositorsLB.ItemsSource = DBEntities.GetContext()
                    .Composer.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void EditM1_Click(object sender, RoutedEventArgs e)
        {
            Composer selectedPlate = ListCompositorsLB.SelectedItem as Composer;
            if (selectedPlate != null)
            {
                new EditComposer(selectedPlate).ShowDialog();
                UpdateList();
            }
            else
            {
                ManagerMainWindow.notifier.ShowError("Вы не выюбрали исполнителя");
            }
        }
    }
}