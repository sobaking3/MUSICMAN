using Microsoft.Win32;
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

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        Workers Workers = new Workers();
        User user = new User();
        Shop shop = new Shop();
        public AddEmployee()
        {
            InitializeComponent();
            RoleCb.ItemsSource = DBEntities.GetContext()
           .Roles.Except(DBEntities.GetContext().Roles.Where(r => r.NameRole == "Администратор"
           || r.NameRole == "Директор" || r.NameRole == "Менеджер"))
           .ToList();
            ShopCb.ItemsSource = DBEntities.GetContext().Shop.ToList();

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

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
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
                    IdShop = Int32.Parse(ShopCb.SelectedValue.ToString()),
                    IdUser = user.IdUser,
                    PhotoStaff = !string.IsNullOrEmpty(selectedFileName) ? ImageClass.ConvertImageToByteArray(selectedFileName) : null
                };
                try
                {
                    DBEntities.GetContext().Workers.Add(Workers);
                    DBEntities.GetContext().SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    // Обработка ошибок проверки сущностей
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine($"Ошибка в свойстве: {validationError.PropertyName}");
                            Console.WriteLine($"Сообщение об ошибке: {validationError.ErrorMessage}");
                        }
                    }
                }
            }
        }

        string selectedFileName = "";

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
            Close();
        }
    }
}
