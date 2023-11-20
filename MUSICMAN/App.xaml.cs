using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using WpfUtils;

namespace MUSICMAN
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IdleDetector idleDetector;
        public static User CurrentUser { get; set; }
        public byte[] PhotoStaff { get; set; }

        public static Func<int, Window> GetCurrentWindow = (_) =>
        {
            return Application.Current.MainWindow;
        };

        public static string GetCurrentWorkerInitials()
        {
            if (CurrentUser == null)
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
        public static Notifier Notifier { get; set; } = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProviderCustom(
                parentWindow: GetCurrentWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);


            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public static Notifier GetLastNotifier { get; set; }

        public static Notifier GetWindowNotifer(Window window)
        {
            return GetLastNotifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: window,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        public App()
        {
            this.InitializeComponent();
            idleDetector = new IdleDetector(TimeSpan.FromSeconds(60), TimeSpan.FromMilliseconds(500));
            idleDetector.IdleDetect += IdleDetector_IdleDetect;
        }

        private void IdleDetector_IdleDetect(IdleDetector sender, IdleTimeInfo idleTimeInfo)
        {
            MessageBox.Show($"Обнаруженно бездействие в течении минуты, вы живы?\n{idleTimeInfo.IdleTime}") ;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new WindowFolder.Authorization().Show();
            idleDetector.Start();
        }
    }
}
