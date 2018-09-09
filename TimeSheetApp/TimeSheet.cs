using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace TimeSheetApp
{
    public class TimeSheet
    {
        private string _employeeID;
        private DateTime _date;
        private string _project;    
        private double _workedHour;

        /// <summary>
        /// Time Sheet Obj Constructor
        /// </summary>
        public TimeSheet()
        {

        }

        /// <summary>
        /// Time Sheet Obj Constructor
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <param name="project"></param>
        /// <param name="Hours"></param>
        public TimeSheet(string employeeId, DateTime date, string project, double Hours)
        {

            _employeeID = employeeId;
            _date = date;
            _project = project;          
            _workedHour = Hours;

        }
     
        /// <summary>
        /// Method to create time sheet
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <param name="project"></param>
        /// <param name="Hours"></param>
        /// <returns></returns>
        public TimeSheet CreateTimeSheet(string employeeId, DateTime date, string project, double Hours)
        {
            if(!string.IsNullOrWhiteSpace(employeeId) && !string.IsNullOrWhiteSpace(project)  && Math.Sign(Hours) == 1)
            {
                                    
                TimeSheet entry = new TimeSheet(employeeId, date, project, Hours);
                return entry;                    
                
            }
            return null;

        }

        public string EmployeeID
        {
            get { return this._employeeID; }
        }

        public DateTime Date
        {
            get { return this._date; }
        }

        public string DateString
        {
            get { return this._date.ToShortDateString(); }
        }

        public string Project
        {
            get { return this._project; }
        }
       
        public double WorkedHour
        {
            get { return this._workedHour; }
        }

    }
}
