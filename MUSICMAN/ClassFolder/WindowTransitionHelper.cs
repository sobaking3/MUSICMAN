using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace MUSICMAN.ClassFolder
{
    public static class WindowTransitionHelper
    {
        public static void OpenWindow(Window parentWindow, Window childWindow)
        {
            // Создаем анимацию для открытия окна
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            // Применяем анимацию к окну
            childWindow.BeginAnimation(UIElement.OpacityProperty, animation);
            // Открываем окно
            childWindow.Show();
        }
        public static void CloseWindow(Window window)
        {
            // Создаем анимацию для закрытия окна
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            // Применяем анимацию к окну
            window.BeginAnimation(UIElement.OpacityProperty, animation);
            // Закрываем окно через заданное время после окончания анимации
            animation.Completed += (s, _) => window.Close();
            window.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
