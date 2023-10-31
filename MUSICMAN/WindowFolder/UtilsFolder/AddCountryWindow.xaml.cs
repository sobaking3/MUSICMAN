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

namespace MUSICMAN.WindowFolder.UtilsFolder
{
    /// <summary>
    /// Логика взаимодействия для AddCountryWindow.xaml
    /// </summary>
    public partial class AddCountryWindow : Window
    {
        Notifier notifier;
        private readonly Action<Country> setCountry;

        public AddCountryWindow(Action<Country> setCountry, string country = null)
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
            CountryTb.Text = country;
            this.setCountry = setCountry;
        }
        private void CountryAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                Country newCountry = DBEntities.GetContext().Country.Add(new Country
                {
                    Name = CountryTb.Text
                });
                DBEntities.GetContext().SaveChanges();
                setCountry(newCountry);
                Close();



            }
        }

        private void AddCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    Country newCountry = DBEntities.GetContext().Country.Add(new Country
                    {
                        Name = CountryTb.Text
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
