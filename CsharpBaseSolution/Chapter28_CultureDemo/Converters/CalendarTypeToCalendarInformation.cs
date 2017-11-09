using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chapter28_CultureDemo.Converters
{
    public class CalendarTypeToCalendarInformation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Calendar)
            {
                Calendar c = value as Calendar;
                if (c == null) return null;
                StringBuilder sb = new StringBuilder(50);
                sb.Append(c.ToString());
                sb.Remove(0, 21);
                sb.Replace("Calendar", "");
                GregorianCalendar calendar = c as GregorianCalendar;
                if(calendar!=null)
                {
                    sb.AppendFormat("{0}", calendar.CalendarType.ToString());
                }
                return sb.ToString();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
