using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using TodoApp1.Models;

namespace TodoApp1.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TodoStatus status)
            {
                return status switch
                {
                    TodoStatus.Todo => new SolidColorBrush(Color.FromRgb(255, 204, 203)),         // pastel red
                    TodoStatus.InProgress => new SolidColorBrush(Color.FromRgb(255, 249, 196)),   // pastel yellow
                    TodoStatus.Done => new SolidColorBrush(Color.FromRgb(204, 255, 204)),         // pastel green
                    _ => Brushes.Gray,
                };
            }

            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
