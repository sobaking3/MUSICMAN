using Microsoft.Win32;
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

namespace MUSICMAN.PageFolder.ManagerPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddCompositor.xaml
    /// </summary>
    public partial class AddCompositor : Window
    {
        Composer Composer = new Composer();
        public AddCompositor()
        {
            InitializeComponent();
            CountryCb.ItemsSource = DBEntities.GetContext().Country.ToList();
        }

        private void AddPhoto()
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
                    Composer.PhotoStaff = ImageClass.ConvertImageToByteArray(selectedFileName);
                    PosterPhoto.Source = ImageClass.ConvertByteArrayToImage(Composer.PhotoStaff);
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }


        private void AddCompositorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    ComposerInfoAdd();
                    MBClass.InfoMB("Автор добавлен");
                    ElementsToolsClass.ClearAllControls(this);
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

        private void ComposerInfoAdd()
        {

            var ComposerInfoAdd = new Composer()
            {
                ComposerName = NameTb.Text,
                MusicCount = Convert.ToInt32(TracksCountTb.Text),
                Rating = RateTb.Text,
                IdCountry = Int32.Parse(CountryCb.SelectedValue.ToString()),
                BitrhDate = DateBirthTb.SelectedDate.Value, 
                DeathDate = DateDeathTb.SelectedDate.Value,
                PhotoStaff = !string.IsNullOrEmpty(selectedFileName) ? ImageClass.ConvertImageToByteArray(selectedFileName) : null
            };
            DBEntities.GetContext().Composer.Add(ComposerInfoAdd);
            DBEntities.GetContext().SaveChanges();
        }
        string selectedFileName = "";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CountryCb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource; if (tb.SelectionStart != 0)
            {
                CountryCb.SelectedItem = null;
            }
            if (tb.SelectionStart == 0 && CountryCb.SelectedItem == null)
            {
                CountryCb.IsDropDownOpen = false;
            }
            CountryCb.IsDropDownOpen = true;
            if (CountryCb.SelectedItem == null)
            {
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CountryCb.ItemsSource);
                cv.Filter = s => (s as Country).Name.IndexOf(CountryCb.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
        }
    }
}
