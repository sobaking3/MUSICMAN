using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.DirectorFolder;
using MUSICMAN.WindowFolder.UtilsFolder;
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
using ToastNotifications.Position;

namespace MUSICMAN.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditShop.xaml
    /// </summary>
    public partial class EditShop : Window
    {
        private Notifier notifier;
        private Adress Adress = new Adress();

        public EditShop(Shop shop)
        {
            notifier = App.GetWindowNotifer(this);
            Shop = shop;
            InitializeComponent();
            CountryCb.ItemsSource = DBEntities.GetContext().Country.ToList();
            CityCb.ItemsSource = DBEntities.GetContext().City.ToList();
            StreetCb.ItemsSource = DBEntities.GetContext().Streets.ToList();
        }

        public Shop Shop { get; set; }

        private void AddShop_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    EditAdress();
                    ShopInfoEdit();
                    notifier.ShowSuccess("Магазин изменен");
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessage = "Ошибка валидации сущностей:" + Environment.NewLine;
                    foreach (var entityValidationError in ex.EntityValidationErrors)
                    {
                        var entity = entityValidationError.Entry.Entity;
                        var validationErrors = entityValidationError.ValidationErrors;
                        foreach (var validationError in validationErrors)
                        {
                            var propertyName = validationError.PropertyName;
                            var error = validationError.ErrorMessage;
                            errorMessage += $"Сущность: {entity.GetType().Name}, Свойство: {propertyName}, Ошибка: {error}" + Environment.NewLine;
                        }
                    }
                    MessageBox.Show(errorMessage, "Ошибка валидации сущностей", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                notifier.ShowError("Вы не ввели все нужные данные!");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Закрытие окна
        }

        private void ShopInfoEdit()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                EditAdress();
                Shop.ShopName = NameShopTb.Text;
                Shop.OpeningTime = OpenTimeTP.Text;
                Shop.ClosingTime = CloseTimeTP.Text;
                Shop.IdShopPlace = Adress.IdShopPlace;
                Shop.PhoneOfDirector = PhoneDirTb.Text;
                DBEntities.GetContext().SaveChanges();
                DirectorMainWindow.notifier.ShowSuccess("Магазин изменен!");
            }
        }

        private void EditAdress()
        {
            var AdressAdd = new Adress()
            {
                IdCountry = Int32.Parse(CountryCb.SelectedValue.ToString()),
                IdCity = Int32.Parse(CityCb.SelectedValue.ToString()),
                IdStreet = Int32.Parse(StreetCb.SelectedValue.ToString()),
                HouseNumber = HouseNumberTb.Text,
            };
            DBEntities.GetContext().Adress.Add(AdressAdd);
            DBEntities.GetContext().SaveChanges();
            Adress.IdShopPlace = AdressAdd.IdShopPlace;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }

        private void CityCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource; if (tb.SelectionStart != 0)
            {
                CityCb.SelectedItem = null;
            }
            if (tb.SelectionStart == 0 && CityCb.SelectedItem == null)
            {
                CityCb.IsDropDownOpen = false;
            }
            CityCb.IsDropDownOpen = true;
            if (CityCb.SelectedItem == null)
            {
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CityCb.ItemsSource);
                cv.Filter = s => (s as City).Name.IndexOf(CityCb.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
                if (cv.Count == 0)
                {
                    //CountryCb.IsDropDownOpen = false;
                    CityCbHelpText.Visibility = Visibility.Visible;
                    CityCb.IsDropDownOpen = false;
                }
                else
                {
                    CityCbHelpText.Visibility = Visibility.Hidden;
                }
            }
        }

        private void CityCb_DropDownOpened(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)((ComboBox)sender).Template.FindName("PART_EditableTextBox", (ComboBox)sender);
            textBox.SelectionStart = ((ComboBox)sender).Text.Length;
            textBox.SelectionLength = 0;
        }

        private void StreetCb_DropDownOpened(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)((ComboBox)sender).Template.FindName("PART_EditableTextBox", (ComboBox)sender);
            textBox.SelectionStart = ((ComboBox)sender).Text.Length;
            textBox.SelectionLength = 0;
        }

        private void StreetCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource; if (tb.SelectionStart != 0)
            {
                StreetCb.SelectedItem = null;
            }
            if (tb.SelectionStart == 0 && StreetCb.SelectedItem == null)
            {
                StreetCb.IsDropDownOpen = false;
            }
            StreetCb.IsDropDownOpen = true;
            if (StreetCb.SelectedItem == null)
            {
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(StreetCb.ItemsSource);
                cv.Filter = s => (s as Streets).Street.IndexOf(StreetCb.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
                if (cv.Count == 0)
                {
                    //CountryCb.IsDropDownOpen = false;
                    StreetsCbHelpText.Visibility = Visibility.Visible;
                    StreetCb.IsDropDownOpen = false;
                }
                else
                {
                    StreetsCbHelpText.Visibility = Visibility.Hidden;
                }
            }
        }

        private void CountryCb_DropDownOpened(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)((ComboBox)sender).Template.FindName("PART_EditableTextBox", (ComboBox)sender);
            textBox.SelectionStart = ((ComboBox)sender).Text.Length;
            textBox.SelectionLength = 0;
        }

        private void CountryCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource; if (tb.SelectionStart != 0)
            {
                CountryCb.SelectedItem = null;
            }
            if (tb.SelectionStart == 0 && CountryCb.SelectedItem == null)
            {
                CountryCb.IsDropDownOpen = false;
            }
            CountryCb.IsDropDownOpen = true;
            if (CountryCb.SelectedItem == null)
            {
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CountryCb.ItemsSource);
                cv.Filter = s => (s as Country).Name.IndexOf(CountryCb.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
                if (cv.Count == 0)
                {
                    //CountryCb.IsDropDownOpen = false;
                    CountryCbHelpText.Visibility = Visibility.Visible;
                    CountryCb.IsDropDownOpen = false;
                }
                else
                {
                    CountryCbHelpText.Visibility = Visibility.Hidden;
                }
            }
        }

        private void CityCbHelpText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            City newCity = null;
            // Открытик окна
            new AddCityWindow(c => newCity = c, CityCb.Text).ShowDialog();

            if (newCity == null)
            {
                return;
            }

            CityCb.ItemsSource = DBEntities.GetContext().City.ToList();
            CityCb.SelectedItem = newCity;

            notifier.ShowSuccess($"Город {newCity.Name} добавлен!");

            CityCbHelpText.Visibility = Visibility.Hidden;
        }

        private void StreetsCbHelpText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Streets newStreet = null;
            // Открытик окна
            new AddStreetWindow(c => newStreet = c, StreetCb.Text).ShowDialog();

            if (newStreet == null)
            {
                return;
            }

            StreetCb.ItemsSource = DBEntities.GetContext().Streets.ToList();
            StreetCb.SelectedItem = newStreet;

            notifier.ShowSuccess($"Улица {newStreet.Street} добавлена!");

            StreetsCbHelpText.Visibility = Visibility.Hidden;
        }

        private void CountryCbHelpText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Country newCountry = null;
            // Открытик окна
            new AddCountryWindow(c => newCountry = c, CountryCb.Text).ShowDialog();

            if (newCountry == null)
            {
                return;
            }

            CountryCb.ItemsSource = DBEntities.GetContext().Country.ToList();
            CountryCb.SelectedItem = newCountry;

            notifier.ShowSuccess($"Страна {newCountry.Name} добавлена!");

            CountryCbHelpText.Visibility = Visibility.Hidden;
        }
    }
}