using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Data.Entity.Validation;
using System.Windows;
using ToastNotifications;

namespace MUSICMAN.WindowFolder.UtilsFolder
{
    /// <summary>
    /// Логика взаимодействия для AddCityWindow.xaml
    /// </summary>
    public partial class AddCityWindow : Window
    {
        private Notifier notifier;
        private readonly Action<City> setCity;

        public AddCityWindow(Action<City> setCity, string city = null)
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
            CityTb.Text = city;
            this.setCity = setCity;
        }

        private void AddCityBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    City newCity = DBEntities.GetContext().City.Add(new City
                    {
                        Name = CityTb.Text
                    });
                    DBEntities.GetContext().SaveChanges();
                    setCity(newCity);
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

        private void CityAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                City newCity = DBEntities.GetContext().City.Add(new City
                {
                    Name = CityTb.Text,
                });
                DBEntities.GetContext().SaveChanges();
                setCity(newCity);
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}