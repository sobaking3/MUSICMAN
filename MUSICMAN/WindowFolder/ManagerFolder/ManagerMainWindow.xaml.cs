﻿using MUSICMAN.ClassFolder;
using MUSICMAN.PageFolder.AdminPageFolder;
using MUSICMAN.PageFolder.ManagerPageFolder;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        public static Notifier notifier;

        public ManagerMainWindow()
        {
            InitializeComponent();

            notifier = App.GetWindowNotifer(this);
            EmpName.Text = App.GetCurrentWorkerInitials();
        }

        private void ListUserBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListEmployee());
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            var mediaElement = (MediaElement)sender;
            mediaElement.Position = TimeSpan.Zero;
            mediaElement.Play();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.MBLogOut(this);
        }

        private void ShopListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListShop());
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.MBExit();
        }

        private void PlastList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListPlate());
        }

        private void ListAuthor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListCompositors());
        }

        private void ListPublisher_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListPublisher());
        }

        private void ListProvider_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListProvider());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
            notifier.ShowInformation("Добро пожаловать!");
        }
    }
}