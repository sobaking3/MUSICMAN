using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Data.Entity.Validation;
using System.Windows;
using ToastNotifications;

namespace MUSICMAN.WindowFolder.UtilsFolder
{
    /// <summary>
    /// Логика взаимодействия для AddCountryWindow.xaml
    /// </summary>
    public partial class AddCountryWindow : Window
    {
        private Notifier notifier;
        private readonly Action<Country> setCountry;

        public AddCountryWindow(Action<Country> setCountry, string country = null)
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
            CountryTb.Text = country;
            this.setCountry = setCountry;
        }

        private void AddCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    Country newCountry = DBEntities.GetContext().Country.Add(new Country
                    {
                        Name = CountryTb.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    setCountry(newCountry);
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