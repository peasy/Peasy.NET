using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Orders.com.WPF.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //var number = value.ToString() == "0" ? string.Empty : value;
            //return value.ToString();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var number = string.IsNullOrWhiteSpace(value.ToString()) ? 0 : value;
            return Decimal.Parse(number.ToString());
        }
    }
}
