using MUSICMAN.WindowFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;

namespace MUSICMAN.ClassFolder
{
    public class MBClass
    {
        public static void ErrorMB(Exception ex)
        {
          
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        public static void ErrorMB(string text)
        {
            bool? result = new MessageBoxCustom(text,
                MessageType.Error, MessageButtons.Ok).ShowDialog();
        }
        public static void InfoMB(string text)
        {
            bool? result = new MessageBoxCustom(text,
                MessageType.Info, MessageButtons.Ok).ShowDialog();
        }
        public static bool QuestionMB(string text)
        {
            bool? Result = new MessageBoxCustom(text, MessageType.Warning, MessageButtons.YesNo).ShowDialog();
            if (Result.Value)
            {
                return true;
            }
            else { return false; }

        }
        public static void MBExit()
        {
            bool? Result = new MessageBoxCustom("Вы уверены, что хотите выйти? ", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();

            if (Result.Value)
            {
                Application.Current.Shutdown();
            }
        }
        public static void MBLogOut(Window window)
        {
            bool? result = new MessageBoxCustom("Вы хотите выйти из аккаунта?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
            if (result == true)
            {
                Authorization loginWindow = new Authorization();
                window.Close();
                loginWindow.Show();
            }
        }
    }
}
