using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheetApp
{
   
   public static class Payroll
    {
        private const double MaxHourlyRate = 50.00;

        /// <summary>
        /// Method to get Hourly Rate base on the provided date and base hourly rate
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="baseRate">Base Rate</param>
        /// <returns>calculated hourly rate </returns>
        public  static double GetRate(DateTime date, double baseRate)
        {
            double hourlyRate = baseRate;
            DayOfWeek day = date.DayOfWeek;


            if (day == DayOfWeek.Saturday) // For Saturday 
            {
                hourlyRate = ((baseRate * 1.5) < MaxHourlyRate) ? baseRate * 1.5 : MaxHourlyRate;
            }
            else if (day == DayOfWeek.Sunday) // For Sunday
            {
                hourlyRate = ((baseRate * 2.0) < MaxHourlyRate) ? baseRate * 2.0 : MaxHourlyRate;
            }
            else
            { // for week day
                hourlyRate = (baseRate > MaxHourlyRate) ? MaxHourlyRate : baseRate;
            }

            return hourlyRate;
        }

        /// <summary>
        /// Method to processs pay roll for provided employee, dates
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="totalWage"></param>
        /// <param name="totalHours"></param>
        public static void ProcessPayroll(Employee employee , DateTime startDate, DateTime endDate, out double totalWage, out double totalHours)
        {
            totalWage = 0.0;
            totalHours = 0.0;


            if (employee != null )
            {

                List<TimeSheet> entries = employee.GetOrderedTimeSheet();

                for (int i = 0; i < entries.Count; i++)
                    {

                        if (entries[i].Date <= endDate && entries[i].Date >= startDate)
                        {
                            //Add all worked hours
                            totalHours += entries[i].WorkedHour;

                            //Get hourly rate for the date
                            double hourlyRate = GetRate(entries[i].Date, employee.GetRate());

                            //total wage = rate * worked hour
                            totalWage += hourlyRate * entries[i].WorkedHour;   
                        }

                }
              
            }

        }

    }
}
