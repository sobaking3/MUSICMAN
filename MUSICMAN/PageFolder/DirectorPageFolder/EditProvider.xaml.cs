using Microsoft.Win32;
using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.DirectorFolder;
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
using ToastNotifications.Position;

namespace MUSICMAN.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditProvider.xaml
    /// </summary>
    public partial class EditProvider : Window
    {
        private Notifier notifier;

        public EditProvider(Provider provider)
        {
            notifier = App.GetWindowNotifer(this);
            Provider = provider;
            InitializeComponent();
        }
        public Provider Provider { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
                    Close(); // Закрытие окна
        }

        private void ProviderEdit()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                Provider.ProviderName = NameTb.Text;
                Provider.ProviderUrAdress = AdressTb.Text;
                Provider.ContractTil = DateContractTb.SelectedDate.Value;
                DBEntities.GetContext().SaveChanges();
                DirectorMainWindow.notifier.ShowSuccess("Поставщик изменен!");
            }
            else
            {
                DirectorMainWindow.notifier.ShowError("Вы не заполнили все поля!");
            }
        }
    

        private void EditProviderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    ProviderEdit();
                    notifier.ShowSuccess("Поставщик изменен");
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
