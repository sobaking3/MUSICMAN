using Microsoft.Win32;
using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private Notifier notifier;
        private Workers Workers = new Workers();
        private User user = new User();
        private Shop shop = new Shop();
        private Gender gender = new Gender();

        public AddEmployee()
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
            RoleCb.ItemsSource = DBEntities.GetContext()
           .Roles.Except(DBEntities.GetContext().Roles.Where(r => r.NameRole == "Администратор"
           || r.NameRole == "Директор" || r.NameRole == "Менеджер"))
           .ToList();
            ShopCb.ItemsSource = DBEntities.GetContext().Shop.ToList();
            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            AddImage();
        }

        private void AddImage()
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
                    Workers.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    ImPhoto.Source = ImageClass.ConvertByteArrayToImage(Workers.PhotoStaff);
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void WorkerInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Workers = new Workers()
                {
                    LastName = LastNameTb.Text,
                    FirstName = FirstNameTb.Text,
                    MiddleName = MiddleNameTb.Text,
                    DateOfBirth = DatePickerDP.SelectedDate.Value,
                    Number = NumberTb.Text,
                    Email = EmailTb.Text,
                    Age = Convert.ToInt32(AgeCountTb.Text),
                    IdGender = Int32.Parse(GenderCb.SelectedValue.ToString()),
                    IdShop = Int32.Parse(ShopCb.SelectedValue.ToString()),
                    IdUser = user.IdUser,
                    PhotoStaff = !string.IsNullOrEmpty(selectedFileName) ? ImageClass.ConvertImageToByteArray(selectedFileName) : null
                };
                DBEntities.GetContext().Workers.Add(Workers);
                DBEntities.GetContext().SaveChanges();
            }
        }

        private string selectedFileName = "";

        private void AddUser()
        {
            var userAdd = new User()
            {
                Login = LoginTb.Text,
                Password = PasswordPb.Text,
                IdRole = Int32.Parse(RoleCb.SelectedValue.ToString())
            };
            DBEntities.GetContext().User.Add(userAdd);
            DBEntities.GetContext().SaveChanges();
            user.IdUser = userAdd.IdUser;
        }

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

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DBEntities.GetContext()
               .Workers.FirstOrDefault(w => w.LastName == LastNameTb.Text
               && w.FirstName == FirstNameTb.Text && w.MiddleName == MiddleNameTb.Text) != null)
            {
                notifier.ShowError($"Такой пользователь уже создан");

                FirstNameTb.Focus();
                LastNameTb.Focus();
                MiddleNameTb.Focus();
            }
            else if (DBEntities.GetContext().Workers.FirstOrDefault(u =>
            u.Number == NumberTb.Text) != null)
            {
                notifier.ShowError($"Пользователь c номером {NumberTb.Text} уже создан");

                NumberTb.Focus();
            }
            else if (DBEntities.GetContext()
                        .User
                        .FirstOrDefault(
                u => u.Login == LoginTb.Text) != null)
            {
                notifier.ShowError($"Пользователь {LoginTb.Text} уже создан");

                LoginTb.Focus();
            }
            else if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    AddUser();
                    WorkerInfoAdd();

                    notifier.ShowSuccess("Сотрудник добавлен");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }

        private void FirstNameTb_TextChanged(object sender, TextChangedEventArgs e)
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

        private void LastNameTb_TextChanged(object sender, TextChangedEventArgs e)
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

        private void MiddleNameTb_TextChanged(object sender, TextChangedEventArgs e)
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

        private void LoginTb_TextChanged(object sender, TextChangedEventArgs e)
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

        private void PasswordPb_TextChanged(object sender, TextChangedEventArgs e)
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

        private void EmailTb_TextChanged(object sender, TextChangedEventArgs e)
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

        private void AgeCountTb_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void AgeCountTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
    }
}