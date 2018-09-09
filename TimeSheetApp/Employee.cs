using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheetApp
{
    public class Employee : IPerson
    {


        private string _employeeID;
        private string _lastName;
        private string _firstName;
        private string _address;
        private double _baseRate;

        private List<TimeSheet> _timeSheetEntries;
        private TimeSheet _timesheet  = new TimeSheet();

        /// <summary>
        /// Employee Obj Constructor
        /// </summary>
        public Employee()
        {
        }

        /// <summary>
        /// Method to create employee obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="baseRate"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public Employee CreateEmployee(string id, string firstname, string lastname, double baseRate, string address)
        {

            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(firstname) && !string.IsNullOrWhiteSpace(lastname) && (Math.Sign(baseRate) == 1) && !string.IsNullOrWhiteSpace(address))
            {
               
                Employee newEmployee = new Employee(id, firstname, lastname, baseRate, address);                   
                return newEmployee;
                
            }
            return null;
        }

        /// <summary>
        /// Employee Obj Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="baseRate"></param>
        /// <param name="address"></param>
        public Employee(string id, string firstname, string lastname, double baseRate, string address)
        {
            _firstName = firstname;
            _lastName = lastname;
            _address = address;
            _baseRate = baseRate;
            _employeeID = id;
            _timeSheetEntries = new List<TimeSheet>();
        }

        /// <summary>
        /// Method to Add Time Sheet record
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <param name="project"></param>
        /// <param name="Hours"></param>
        public bool AddTimeSheet(string employeeId, DateTime date, string project, double Hours)
        {

            TimeSheet record = _timesheet.CreateTimeSheet(employeeId, date, project, Hours);
            if (record != null)
            {
                 _timeSheetEntries.Add(record);
                return true;
            }
            return false;

        }

        /// <summary>
        /// Overloading method to add a list of time sheet records        
        /// </summary>
        /// <param name="records"> time sheet records</param>
        public void AddTimeSheet(List<TimeSheet> records)
        {
            _timeSheetEntries = records;
        }

        public string EmployeeID
        {
            get { return this._employeeID; }
        }     

        public string FullName
        {
            get { return this._firstName + " " + this._lastName; }
        }

        public double GetRate()
        {
            return this._baseRate;
        }

        public string Address
        {
            get { return this._address; }
        }

        public List<TimeSheet> GetOrderedTimeSheet()
        {
            _timeSheetEntries = (List<TimeSheet>)_timeSheetEntries.OrderByDescending(x => x.Date).ToList();
            return _timeSheetEntries;
        }


       
        

    }
}
