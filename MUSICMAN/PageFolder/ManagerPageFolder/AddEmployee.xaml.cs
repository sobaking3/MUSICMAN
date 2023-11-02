﻿using Microsoft.Win32;
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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        Notifier notifier;
        Workers Workers = new Workers();
        User user = new User();
        Shop shop = new Shop();
        public AddEmployee()
        {

            notifier = App.GetWindowNotifer(this);
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
                    IdShop = Int32.Parse(ShopCb.SelectedValue.ToString()),
                    IdUser = user.IdUser,
                    PhotoStaff = !string.IsNullOrEmpty(selectedFileName) ? ImageClass.ConvertImageToByteArray(selectedFileName) : null
                };
                    DBEntities.GetContext().Workers.Add(Workers);
                    DBEntities.GetContext().SaveChanges();


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

            if (ElementsToolsClass.AllFieldsFilled(this))
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
    }
}
