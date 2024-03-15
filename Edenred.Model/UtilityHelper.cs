using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenred.Model
{
    public static class UtilityHelper
    {
        public static int ConcateCurrentMonthAndYear()
        {
            string monthYearString = $"{DateTime.Now.Month}{DateTime.Now.Year}";
            int monthYear = Convert.ToInt32(monthYearString);
            return monthYear;
        }
    }
}
