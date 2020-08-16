using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Time
{
    public class DateTimeManager
    {
        public static DateTime GetNextDayOfWeek(DateTime startDate, DayOfWeek dow, Boolean includeStartDate = false )
        {
            if(startDate.DayOfWeek == dow && !includeStartDate)
            {
                return startDate.AddDays(7);
            }
            
            int daysToAdd = ((int)dow - (int)startDate.DayOfWeek + 7) % 7;
            return startDate.AddDays(daysToAdd);
        }
    }
}
