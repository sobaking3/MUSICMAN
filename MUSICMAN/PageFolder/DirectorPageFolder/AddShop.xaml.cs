using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MUSICMAN.DataFolder;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MUSICMAN.ClassFolder;
using System.Data.Entity.Validation;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddShop.xaml
    /// </summary>
    public partial class AddShop : Window
    {
        Notifier notifier;
        public AddShop()
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
        }
        private void AddShop_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    ShopInfoAdd();
                    notifier.ShowSuccess("Пластинка добавлена");
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(500).ContinueWith(_ => // Задержка в 1 секунду
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Close(); // Закрытие окна
                });
            });
        }
        private void ShopInfoAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Shop = new Shop()
                {
                    ShopName = NameShop.Text,
                    ShopPlace = AdressTb.Text,
                    OpeningTime = OpenTimeTP.Text,
                    ClosingTime = CloseTimeTP.Text,
                    PhoneOfDirector = PhoneDirTb.Text,
                };
                DBEntities.GetContext().Shop.Add(Shop);
                DBEntities.GetContext().SaveChanges();


            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }
    }
}
