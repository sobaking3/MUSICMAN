using Microsoft.Win32;
using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
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

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddCompositor.xaml
    /// </summary>
    public partial class AddCompositor : Window
    {
        Composer Composer = new Composer();
        Notifier notifier;
        public AddCompositor()
        {
            InitializeComponent();
            notifier = App.GetWindowNotifer(this);
            CountryCb.ItemsSource = DBEntities.GetContext().Country.ToList();
        }

        private void AddPhoto()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = "";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                    "Portable Network Graphic (*.png)|*.png";

                if (op.ShowDialog() == true)
                {
                    selectedFileName = op.FileName;
                    Composer.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    PosterPhoto.Source = ImageClass.ConvertByteArrayToImage(Composer.PhotoStaff);
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }


        private void AddCompositorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    ComposerInfoAdd();
                    notifier.ShowSuccess("Исполнитель добавлен");
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

        private void ComposerInfoAdd()
        {

            var ComposerInfoAdd = new Composer()
            {
                ComposerName = NameTb.Text,
                MusicCount = Convert.ToInt32(TracksCountTb.Text),
                Rating = RateTb.Text,
                IdCountry = Int32.Parse(CountryCb.SelectedValue.ToString()),
                CreationDate = CreateDateTb.SelectedDate.Value,
                PhotoStaff = !string.IsNullOrEmpty(selectedFileName) ? ImageClass.ConvertImageToByteArray(selectedFileName) : null
            };
            DBEntities.GetContext().Composer.Add(ComposerInfoAdd);
            DBEntities.GetContext().SaveChanges();
        }
        string selectedFileName = "";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(500).ContinueWith(_ => // Задержка в 1 секунду
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Close(); // Закрытие окна
                });
            });
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
                if(cv.Count == 0)
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

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Country newCountry = null;
            // Открытик окна
            new AddCountryWindow(c => newCountry = c, CountryCb.Text).ShowDialog();

            if(newCountry == null)
            {
                return;
            }

            CountryCb.ItemsSource = DBEntities.GetContext().Country.ToList();
            CountryCb.SelectedItem = newCountry;

            notifier.ShowSuccess($"Страна {newCountry.Name} добавлена!");

            CountryCbHelpText.Visibility = Visibility.Hidden;
        }

        private void RateTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                // Если символ не является цифрой, отменяем его ввод
                e.Handled = true;
            }

            // Проверяем, чтобы вводимое число находилось в диапазоне от 1 до 100
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text + e.Text;
            int number;
            if (int.TryParse(newText, out number))
            {
                if (number < 1 || number > 100)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TracksCountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }

        private void NameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var grid = (Grid)textBox.Parent;
            if (grid != null)
            {
                var textBlock = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
                if (textBlock != null)
                {
                    textBlock.Text = textBox.Text.Length.ToString();
                }
            }
        }

        private void TracksCountTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var grid = (Grid)textBox.Parent;
            if (grid != null)
            {
                var textBlock = (TextBlock)VisualTreeHelper.GetChild(grid, 1);
                if (textBlock != null)
                {
                    textBlock.Text = textBox.Text.Length.ToString();
                }
            }
        }
    }
}
