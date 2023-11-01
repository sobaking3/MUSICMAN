using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddPlate.xaml
    /// </summary>
    public partial class AddPlate : Window
    {
        Notifier notifier;
        public AddPlate()
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
            ComboShop.ItemsSource = DBEntities.GetContext().Shop.ToList();
            ComboComposer.ItemsSource = DBEntities.GetContext().Composer.ToList();
            ComboPublisher.ItemsSource = DBEntities.GetContext().Publisher.ToList();
            ComboStock.ItemsSource = DBEntities.GetContext().Stocks.ToList();
            ComboProvider.ItemsSource = DBEntities.GetContext().Provider.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void PlateInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Plates = new Plates()
                {
                    IdShop = Int32.Parse(ComboShop.SelectedValue.ToString()),
                    PlateName = NameTb.Text,
                    IdComposer = Int32.Parse(ComboComposer.SelectedValue.ToString()),
                    IdPublisher = Int32.Parse(ComboPublisher.SelectedValue.ToString()),
                    Cost = CostTb.Text,
                    Duration = DurationTP.Text,
                    CreationDate = DatePickerTb.SelectedDate.Value,
                    Count = Convert.ToInt32(CountTb.Text),
                    IdStock = Int32.Parse(ComboStock.SelectedValue.ToString()),
                    IdProvider = Int32.Parse(ComboProvider.SelectedValue.ToString()),
                };
                DBEntities.GetContext().Plates.Add(Plates);
                DBEntities.GetContext().SaveChanges();


            }
        }

        private void AddPlateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    PlateInfoAdd();
                    notifier.ShowSuccess("Пластинка добавлена");
                    ElementsToolsClass.ClearAllControls(this);
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
    }
}
