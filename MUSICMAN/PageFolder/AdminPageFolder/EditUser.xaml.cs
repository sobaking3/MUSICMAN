using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MUSICMAN.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Page
    {
        User user = new User();
        public EditUser(User user)
        {
            InitializeComponent();
            DataContext = user;
            RoleCb.ItemsSource = DBEntities.GetContext()
                .Roles.ToList();
            this.user.IdUser = user.IdUser;
        }

        private void SaveUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                user = DBEntities.GetContext().User.
                    FirstOrDefault(u => u.IdUser == user.IdUser);
                user.Login = LoginTb.Text;
                user.Password = PasswordPb.Text;
                user.Roles.IdRole = Int32.Parse
                    (RoleCb.SelectedValue.ToString());
                DBEntities.GetContext().SaveChanges();
                MBClass.InfoMB("Данные успешно сохранены");
                NavigationService.Navigate(new ListUser());
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}
