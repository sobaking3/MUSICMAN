using MUSICMAN.DataFolder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MUSICMAN
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }

        public static string GetCurrentWorkerInitials()
        {
            if(CurrentUser == null)
            {
                return string.Empty;
            }
            Workers worker = App.CurrentUser.Workers.FirstOrDefault();
            if(worker == null)
            {
                return "Сотрудник";
            }
            return $"{worker.LastName} {worker.FirstName[0]}." +
                (string.IsNullOrEmpty(worker.MiddleName) ? string.Empty : worker.LastName[0] + ".");
        }
    }
}
