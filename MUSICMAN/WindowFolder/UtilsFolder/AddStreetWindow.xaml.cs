﻿using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Data.Entity.Validation;
using System.Windows;
using ToastNotifications;

namespace MUSICMAN.WindowFolder.UtilsFolder
{
    /// <summary>
    /// Логика взаимодействия для AddStreetWindow.xaml
    /// </summary>
    public partial class AddStreetWindow : Window
    {
        private Notifier notifier;
        private readonly Action<Streets> setStreet;

        public AddStreetWindow(Action<Streets> setStreet, string street = null)
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
            StreetTb.Text = street;
            this.setStreet = setStreet;
        }

        private void AddStreetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    Streets newStreet = DBEntities.GetContext().Streets.Add(new Streets
                    {
                        Street = StreetTb.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    setStreet(newStreet);
                    Close();
                }
                catch (DbEntityValidationException ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
            else
            {
                MBClass.ErrorMB("Вы не ввели все нужные данные!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}