using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimeSheetApp
{
    class Program
    {
        private static List<TimeSheet> _timesheets = new List<TimeSheet>();
        private static List<Employee> _employees = new List<Employee>();
        private static Employee emp;
        private static TimeSheet ts;
             

        private static string _currentDirectory = Directory.GetCurrentDirectory();
        private static string _timesheetPath = System.IO.Path.Combine(_currentDirectory, "TimeSheet.txt");
        private static string _employeesPath = System.IO.Path.Combine(_currentDirectory, "Employees.txt");

        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 30);
            int userSelect = 12; //dummy          
            emp = new Employee();
            ts = new TimeSheet();

            ReadExistingTimeSheets(); // Read existing time sheet records from text file
            ReadExistingEmpRecords(); // Read existing employees records from text file

           
            Console.WriteLine("TIME SHEET APPLICATION ");
            Console.WriteLine("=================================================================================");
            while (userSelect != 0)
            {
               
                Console.WriteLine("=================================================================================");
                Console.WriteLine("Please select one of the following options : ");
                Console.WriteLine(" Enter \"1\" to CREATE NEW EMPLOYEE ");
                Console.WriteLine(" Enter \"2\" to ENTER TIME SHEET FOR SELECTED EMPLOYEE ");
                Console.WriteLine(" Enter \"3\" to CALCULATE WAGE FOR SELECTED EMPLOYEE ");
                Console.WriteLine(" Enter \"4\" to DISPLAY ALL EMPLOYEE INFO ");
                Console.WriteLine(" Enter \"5\" to DISPLAY ALL TIME SHEET RECORDS FOR SELECTED EMPLOYEE ");
                Console.WriteLine(" Enter \"0\" to exit the application ");
                Console.WriteLine("=================================================================================");

                userSelect = Convert.ToInt32(Console.ReadLine());
             

                switch (userSelect)
                {
                    case 1:
                        CreateEmployee();                 
                        break;
                    case 2:
                        EnterTimeSheet();                     
                        break;
                    case 3:
                        CalculateWage();
                        break;
                    case 4:
                        DisplayAllEmployeeInfo();
                        break;
                    case 5:
                        DisplayTimeSheetByEmployeeID();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please try again..");
                        break;

                }

            }
        }

        /// <summary>
        /// Method for creating a new employee
        /// </summary>
        private static void CreateEmployee()
        {
            Console.WriteLine("Creating New Employee");
            Console.WriteLine("=================================================================================");
            string id = string.Empty;
            Boolean isNewId = false;
             while (isNewId == false)
             {
                Console.Write("Please enter EmployeeID : ");
                id = Console.ReadLine();

                Employee emp = SearchEmployeeById(id);
                if (emp != null)
                {
                    Console.WriteLine("Same EmployeeID is already created. Please enter a new EmployeeID ");
                }
                else
                {
                    isNewId = true;
                }

             }

            Console.Write("Please enter First Name : ");
            string firstName = Console.ReadLine();

            Console.Write("Please enter Last Name : ");
            string lastName = Console.ReadLine();
          
            Console.Write("Please enter Hourly Rate [ number only ]  : ");
            double rate = Util.ReadDouble("Invalid value. Please try again");

            Console.Write("Please enter Address : ");
            string address = Console.ReadLine();
            
             string newRecord = id +"|"+firstName+" "+lastName+"|"+rate+"|"+address;

            Employee newEmployee = emp.CreateEmployee(id, firstName, lastName, rate, address);
           
            if (newEmployee != null)
            {
                _employees.Add(newEmployee);
                SaveEmployeeRecord(newRecord, _employeesPath);
                Console.WriteLine("Successfully created a new employee with Employee Id : " + id);
                Console.WriteLine("=================================================================================");
            }
            else
            {
                Console.WriteLine("Validation failed. Please try again..");
            }
        }


        /// <summary>
        /// Method for entering time sheet record for selected employee 
        /// </summary>
         private static void EnterTimeSheet()
         {
             Console.WriteLine("Adding Time Sheet for selected employee");
             Console.WriteLine("=================================================================================");       
             Employee emp = SearchEmployeeById(AcceptEmployeeId());
             if (emp != null)
             {

                 Console.Write("Please enter Date [ dd/MM/yyyy ] : ");
                 DateTime _date = Util.ReadDateTime("Invalid value. Please try again ");

                 Console.Write("Please enter Project : ");
                 string project = Console.ReadLine();

                 Console.Write("Please enter worked hours [ number only ] : ");
                 double workedHour = Util.ReadDouble("Invalid value. Please try again ");

                if (emp.AddTimeSheet(emp.EmployeeID, _date, project, workedHour))
                {

                    string newRecord = emp.EmployeeID + "|" + _date.ToShortDateString() + "|" + project + "|" + workedHour;
                    SaveTimeSheetRecord(newRecord, _timesheetPath);
                    Console.WriteLine("Successfully created a time sheet record for employee with Employee Id : " + emp.EmployeeID);
                    Console.WriteLine("=================================================================================");
                }
                else
                {
                    Console.WriteLine("Validation failed. Please try again..");
                }
             }
             else
             {
                 Console.WriteLine("Invalid Employee ID. Please try again ");
             }

         }

        /// <summary>
        /// Method for calculating wage for selected employee
        /// </summary>
         private static void CalculateWage()
         {
             double totalWage = 0.0;
             double totalHours = 0.0;

             Console.WriteLine("Calculating wage for selected employee");
             Console.WriteLine("=================================================================================");
             Employee emp = SearchEmployeeById(AcceptEmployeeId());
             if (emp != null)
             {
                 Console.Write("Please enter Start Date [ dd/MM/yyyy ] : ");
                 DateTime startDate = Util.ReadDateTime("Invalid value. Please try again ");
                 Console.Write("Please enter End Date [ dd/MM/yyyy ] : ");
                 DateTime endDate = Util.ReadDateTime("Invalid value. Please try again ");

                 Payroll.ProcessPayroll(emp, startDate, endDate, out totalWage, out totalHours);

                 Console.WriteLine("Successfully calculated Total Worked Hours and Total wages for employee with Employee Id : " + emp.EmployeeID);
                 Console.WriteLine("For Saturday , Time and a half of base hourly rate.");
                 Console.WriteLine("For Sunday , Double time of base hourly rate.");
                 Console.WriteLine("Maximum hourly rate is capped at $50 an hour.");
                Console.WriteLine("Employee ID {0},  Total worked hours : {1}  :  Total wages : {2} ", emp.EmployeeID, totalHours, totalWage);
                 Console.WriteLine("=================================================================================");
             }              
             else
             {
                 Console.WriteLine("Invalid Employee ID. Please try again ");
             }
         }

        /// <summary>
        /// Method to display all employee info
        /// </summary>
         private static void DisplayAllEmployeeInfo()
         {
             if (_employees.Count > 0)
             {
                 foreach (Employee emp in _employees)
                 {
                     Console.WriteLine(String.Format("Employee ID : {0,2} | Full Name : {1,18} | Hourly Rate : {2,2} |  Address : {3,2}", emp.EmployeeID, emp.FullName, emp.GetRate(), emp.Address));
                 }
             }
         }

        /// <summary>
        /// Method to display time sheet records of selected employee
        /// </summary>
         private static void DisplayTimeSheetByEmployeeID()
         {
              Console.WriteLine("Displaying Time Sheet Records for selected employee");
             Console.WriteLine("=================================================================================");       
             Employee emp = SearchEmployeeById(AcceptEmployeeId());
             if (emp != null)
             {
                 List<TimeSheet> entries = emp.GetOrderedTimeSheet();

                 if (entries != null)
                 {
                     foreach (TimeSheet entry in entries)
                     {                        
                         string info = String.Format("Employee ID :{0,2} ,  Date : {1,18}, Project : {2,20} ,  Hours  : {3,5}", entry.EmployeeID, entry.DateString, entry.Project, entry.WorkedHour);
                         Console.WriteLine(info);
                     }
                 }
             }              
             else
             {
                 Console.WriteLine("Invalid Employee ID. Please try again ");
             }
         }


        
        /// <summary>
        /// Helper method to accept employee id number
        /// </summary>
        /// <returns> employee id</returns>
         private static string AcceptEmployeeId()
         {
             Console.Write("Please enter Employee's ID number : ");
             string id = Console.ReadLine();
             return id.Trim();
         }         

        /// <summary>
        /// Helper method to search employee with employee id
        /// </summary>
        /// <param name="id"> employee id</param>
        /// <returns> employee obj </returns>
         private static Employee SearchEmployeeById(string id)
         {
             IEnumerable<Employee> emp = null;
             if (_employees.Count != 0)
             {
                 emp = _employees.Where(e => e.EmployeeID.Equals(id)).ToList();
                 if (emp.Count() > 0)
                 {
                     return emp.FirstOrDefault();
                 }                
             }
             return emp.FirstOrDefault();
         }

        /// <summary>
        /// Method to read existing employee records from text file
        /// </summary>
         private static void ReadExistingEmpRecords()
         {
             try
             {
                string line = string.Empty;
                string employeeID = string.Empty;
                string lastName = string.Empty;
                string firstName = string.Empty;
                string address = string.Empty;
                double baseRate = 0;
                 // Read file line by line.
                 using (System.IO.StreamReader file = new System.IO.StreamReader(_employeesPath))
                 {
                     _employees.Clear();

                     while ((line = file.ReadLine()) != string.Empty)
                     {
                         int counter = 0;

                         line = line.Trim();

                         while (line != string.Empty)
                         {
                             string entry = string.Empty;
                             if (line.IndexOf('|') > 0)
                             {
                                 entry = line.Substring(0, line.IndexOf('|'));
                             }
                             else
                             {
                                 entry = line;
                             }

                             if (entry != string.Empty)
                             {
                                 switch (counter)
                                 {
                                     case 0:
                                         employeeID = entry;
                                         break;
                                     case 1:
                                         var names = entry.Split(new[] { ' ' }, 2);
                                         firstName = names[0];
                                         lastName = names[1];
                                         break;
                                     case 2:
                                         if (!Double.TryParse(entry, out baseRate))
                                         {
                                         }
                                         break;
                                     case 3:
                                         address = entry;
                                         line = string.Empty;
                                         break;

                                 }
                                 counter++;
                             }
                             line = line.Trim();
                             int index = line.IndexOf('|') + 1;
                             line = line.Substring(index, line.Length - index);
                             line = line.Trim();
                         }



                         Employee newEmployee = emp.CreateEmployee(employeeID,firstName,lastName,baseRate,address);
                         if (newEmployee != null)
                         {
                            List<TimeSheet> records =  ReadTimeSheetByEmployeeId(newEmployee.EmployeeID);
                            if(records.Count > 0 )  newEmployee.AddTimeSheet(records);
                            _employees.Add(newEmployee);
                         }



                     }
                 }

             }
             catch (Exception ex)
             {
                 //todo
             }

         }

        /// <summary>
        /// Method to read existing time sheet records from text file
        /// </summary>
         private static void ReadExistingTimeSheets()
         {

             try
             {
                string line = string.Empty;
                string employeeID = string.Empty;
                DateTime date = DateTime.Now;
                string project = string.Empty;
                double workedHour = 0;

                 // Read file line by line.
                 using (System.IO.StreamReader file = new System.IO.StreamReader(_timesheetPath))
                 {
                     //_timesheets.Clear();

                     while ((line = file.ReadLine()) != null)
                     {
                         int counter = 0;

                         line = line.Trim();

                         while (line != string.Empty)
                         {
                             string entry = string.Empty;
                             if (line.IndexOf('|') > 0)
                             {
                                 entry = line.Substring(0, line.IndexOf('|'));
                             }
                             else
                             {
                                 entry = line;
                             }
                             if (entry != string.Empty)
                             {
                                 switch (counter)
                                 {
                                     case 0:
                                         employeeID = entry;
                                         break;
                                     case 1:
                                         if (DateTime.TryParse(entry, out date))
                                         {

                                         }
                                         break;
                                     case 2:
                                         project = entry;
                                         break;
                                     case 3:
                                         if (!Double.TryParse(entry, out workedHour))
                                         {

                                         }
                                         line = string.Empty;
                                         break;


                                 }
                                 counter++;
                             }
                             line = line.Trim();
                             int index = line.IndexOf('|') + 1;
                             line = line.Substring(index, line.Length - index);
                             line = line.Trim();
                         }
                         TimeSheet record = ts.CreateTimeSheet(employeeID, date, project, workedHour);
                         if( record != null ) _timesheets.Add(record);

                     }
                 }

             }
             catch (Exception ex)
             {
                 //todo
             }
         }


        /// <summary>
        /// Method to get the list of time sheet for the selected employee
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>List of time sheet records</returns>
         private static List<TimeSheet> ReadTimeSheetByEmployeeId(string id)
         {
             List<TimeSheet> records = null;
             if (_timesheets != null)
             {
                 records = _timesheets.Where(e => e.EmployeeID.Equals(id)).ToList();
             }

             return records;
         }


        /// <summary>
        /// Method to save time sheet record into text file
        /// </summary>
         /// <param name="record">time sheet record</param>
        /// <param name="timesheetFilePath">time sheet file path</param>
         private static void SaveTimeSheetRecord(string record, string timesheetFilePath)
         {
             Util.WriteFile(record, timesheetFilePath);

         }

        /// <summary>
        /// Method to save employee data into text file
        /// </summary>
        /// <param name="employeeData">employee data</param>
        /// <param name="employeeFilePath">employee file path</param>
         private static void SaveEmployeeRecord(string employeeData, string employeeFilePath)
         {
             Util.WriteFile(employeeData, employeeFilePath);
         }
    }
}
