﻿using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
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
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditPlate.xaml
    /// </summary>
    public partial class EditPlate : Window
    {
        Notifier notifier;
        public Plates Plate { get; set; }
        public EditPlate(Plates plate)
        {
            notifier = App.GetWindowNotifer(this);
            Plate = plate;

            InitializeComponent();
            
            ComboShop.ItemsSource = DBEntities.GetContext().Shop.ToList();
            ComboComposer.ItemsSource = DBEntities.GetContext().Composer.ToList();
            ComboPublisher.ItemsSource = DBEntities.GetContext().Publisher.ToList();
            ComboStock.ItemsSource = DBEntities.GetContext().Stocks.ToList();
            ComboProvider.ItemsSource = DBEntities.GetContext().Provider.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            Close(); // Закрытие окна
        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                // Если символ не является цифрой, отменяем его ввод
                e.Handled = true;
            }

            // Указываем максимальное количество символов в текстбоксе
            int maxLength = 5; // Здесь можно указать нужное значение
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length >= maxLength)
            {
                e.Handled = true;
            }
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

        private void PlateInfoEdit()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                    var plate = DBEntities.GetContext().Plates.FirstOrDefault(p => p.IdPlate == Plate.IdPlate);
                if (plate != null)
                {
                    plate.IdShop = Int32.Parse(ComboShop.SelectedValue.ToString());
                    plate.PlateName = NameTb.Text;
                    plate.IdComposer = Int32.Parse(ComboComposer.SelectedValue.ToString());
                    plate.IdPublisher = Int32.Parse(ComboPublisher.SelectedValue.ToString());
                    plate.Cost = CostTb.Text;
                    plate.Duration = DurationTP.Text;
                    plate.CreationDate = DatePickerTb.SelectedDate.Value;
                    plate.Count = Convert.ToInt32(CountTb.Text);
                    plate.IdStock = Int32.Parse(ComboStock.SelectedValue.ToString());
                    plate.IdProvider = Int32.Parse(ComboProvider.SelectedValue.ToString());
                    DBEntities.GetContext().SaveChanges();
                }


            }
        }

        private void EditPlateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    PlateInfoEdit();
                    notifier.ShowSuccess("Пластинка изменена");
                }
                catch (DbEntityValidationException ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
            else
            {
                notifier.ShowError("Вы не ввели все нужные данные!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }

        private void EditPlateBtn_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
