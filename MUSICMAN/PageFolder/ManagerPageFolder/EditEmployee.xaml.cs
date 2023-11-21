﻿using Microsoft.Win32;
using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.DirectorFolder;
using MUSICMAN.WindowFolder.ManagerFolder;
using System;
using System.ComponentModel;
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
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        private Notifier notifier;
        private string selectedFileName = "";

        public EditEmployee(Workers worker)
        {
            notifier = App.GetWindowNotifer(this);
            Worker = worker;
            InitializeComponent();

            RoleCb.ItemsSource = DBEntities.GetContext()
            .Roles.Except(DBEntities.GetContext().Roles.Where(r => r.NameRole == "Администратор"
           || r.NameRole == "Директор" || r.NameRole == "Менеджер"))
           .ToList();
            ShopCb.ItemsSource = DBEntities.GetContext().Shop.ToList();
        }

        public Workers Worker { get; set; }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    //AddUser();
                    WorkerInfoAdd();

                    notifier.ShowSuccess("Сотрудник добавлен");
                    //ElementsToolsClass.ClearAllControls(this);
                }
                catch (DbEntityValidationException ex)
                {
                    MBClass.ErrorMB(ex);
                    if (ex.EntityValidationErrors != null)
                    {
                        foreach (var exdb in ex.EntityValidationErrors)
                        {
                            MBClass.ErrorMB(string.Join("\n\n", exdb.ValidationErrors.Select(x => x.ErrorMessage)));
                        }
                    }
                }
            }
            else
            {
                notifier.ShowError("Вы не ввели все нужные данные!");
            }
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
                    Worker.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    ImPhoto.Source = ImageClass.ConvertByteArrayToImage(Worker.PhotoStaff);
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            AddImage();
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

        private void EditUser()
        {
            Worker.User.Login = LoginTb.Text;
            Worker.User.Password = PasswordPb.Text;
            Worker.User.Roles = RoleCb.SelectedItem as Roles;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }

        private void WorkerInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                EditUser();
                Worker.DateOfBirth = DatePickerDP.SelectedDate.Value;
                Worker.Email = EmailTb.Text;
                Worker.FirstName = FirstNameTb.Text;
                Worker.LastName = LastNameTb.Text;
                Worker.MiddleName = MiddleNameTb.Text;
                Worker.Number = NumberTb.Text;
                //Worker.PhotoStaff = !string.IsNullOrEmpty(selectedFileName) ? ImageClass.ConvertImageToByteArray(selectedFileName) : null;
                Worker.Shop = ShopCb.SelectedItem as Shop;
                DBEntities.GetContext().SaveChanges();
                ManagerMainWindow.notifier.ShowSuccess("Сотрудник изменен!");
            }
            else
            {
                DirectorMainWindow.notifier.ShowError("Вы не заполнили все поля!");
            }
        }
    }
}