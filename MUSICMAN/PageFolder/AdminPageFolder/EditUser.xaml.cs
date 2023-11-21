using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MUSICMAN.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Page
    {
        private User user = new User();

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