using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.DirectorFolder;
using System.Data.Entity.Validation;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Messages;

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