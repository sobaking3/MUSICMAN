using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace MUSICMAN.ClassFolder
{
    class ElementsToolsClass
    {
        public static void ClearAllControls(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox)
                {
                    ((TextBox)child).Clear();
                }
                else if (child is Image)
                {
                    ((Image)child).Source = null;
                }
                else if (child is ComboBox)
                {
                    ((ComboBox)child).SelectedIndex = -1;
                }
                ClearAllControls(child); //рекурсивный вызов для очистки дочерних элементов
            }
        }
        public static bool AllFieldsFilled(DependencyObject parent)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(parent))
            {
                if (child is TextBox textBox)
                {
                    if (textBox.Tag != null && textBox.Tag.ToString() == "Optional")
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        return false;
                    }
                }
                else if (child is ComboBox comboBox)
                {
                    if (comboBox.Tag != null && comboBox.Tag.ToString() == "Optional")
                    {
                        continue;
                    }
                    if (comboBox.SelectedItem == null)
                    {
                        return false;
                    }
                }
                else if (child is DependencyObject dependencyObject)
                {
                    if (!AllFieldsFilled(dependencyObject))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
