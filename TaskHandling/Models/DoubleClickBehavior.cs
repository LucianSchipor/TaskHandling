using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace TaskHandling.Models
{
    public static class DoubleClickBehavior
    {
        public static readonly DependencyProperty DoubleClickCommandProperty =
       DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(DoubleClickBehavior), new PropertyMetadata(null, DoubleClickCommandChanged));

        public static ICommand GetDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DoubleClickCommandProperty);
        }

        public static void SetDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }

        private static void DoubleClickCommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var item = obj as TreeViewItem;
            if (item == null)
                return;

            if (e.NewValue != null && e.OldValue == null)
            {
                item.MouseDoubleClick += OnMouseDoubleClick;
            }
            else if (e.NewValue == null && e.OldValue != null)
            {
                item.MouseDoubleClick -= OnMouseDoubleClick;
            }
        }

        private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;
            var command = GetDoubleClickCommand(item);
            if (command != null && command.CanExecute(item.DataContext))
            {
                command.Execute(item.DataContext);
            }
        }
    }
}
