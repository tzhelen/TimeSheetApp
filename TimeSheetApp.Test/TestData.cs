using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NUnit.Framework;

namespace TimeSheetApp.Test
{
    public class EmployeeData
    {
        public string Id { get; set; }
        public string fistName { get; set; }
        public string lastName { get; set; }
        public double baseRate { get; set; }
        public string address { get; set; }
        public Employee expectedResult { get; set; }
    }

    public class TimeSheetData
    {
        public string Id { get; set; }
        public DateTime date { get; set; }
        public string project { get; set; }
        public double workedhour { get; set; }
        public bool expectedResult { get; set; }
    }

    public class PayrollData
    {
        public Employee Id { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }       
        public double expectedWage { get; set; }
        public double expectedHour { get; set; }
     
    }

    public class HourlyRateData
    {
        public DateTime date { get; set; }
        public double baseRate { get; set; }
        public double expectedRate { get; set; }
    }

    public static class TestCasesDataFactory
    {
        public static IEnumerable GetCreateEmployee_InvalidTestCasesData
        {
            get
            {             
                yield return new TestCaseData(InvalidRateEmployeeTestData()).SetName("EmployeeTest_InvalidRateTestCase");
                yield return new TestCaseData(NullEmployeeTestData()).SetName("EmployeeTest_NullTestCase");
            }
        }

        public static IEnumerable GetCreateTimeSheet_InvalidTestCasesData
        {
            get
            {               
                yield return new TestCaseData(InvalidHourTimeSheetTestData()).SetName("TimeSheetTest_InvalidHourTestCase");
                yield return new TestCaseData(NullTimeSheetTestData()).SetName("TimeSheetTest_NullTestCase");
            }
        }

        public static IEnumerable GetCreateEmployee_ValidTestCasesData
        {
            get
            {
                yield return new TestCaseData(ValidEmployeeTestData()).SetName("EmployeeTest_ValidTestCase");
                
            }
        }

        public static IEnumerable GetCreateTimeSheet_ValidTestCasesData
        {
            get
            {
                yield return new TestCaseData(ValidTimeSheetTestData()).SetName("TimeSheetTest_ValidTestCase");
              
            }
        }

        public static IEnumerable ProcessPayroll_TestCasesData
        {
            get
            {
                yield return new TestCaseData(PayrollData()).SetName("PayrollTest_ValidTestCase");
                yield return new TestCaseData(PayrollData_InvalidEmployeeId()).SetName("PayrollTest_InvalidIdTestCase");
                yield return new TestCaseData(PayrollData_InvalidDates()).SetName("PayrollTest_InvalidDatesTestCase");
            }
        }

        public static IEnumerable GetRate_TestCasesData
        {
          

            get
            {
                yield return new TestCaseData(WeekDayRate()).SetName("WeekDayRateTestCase");
                yield return new TestCaseData(StaturdayRate()).SetName("StaturdayRateTestCase");
                yield return new TestCaseData(SundayRate()).SetName("SundayRateTestCase");
                yield return new TestCaseData(MaxHourlyRate()).SetName("MaxHourlyRateTestCase");
            }
        }


        private static EmployeeData ValidEmployeeTestData()
        {
            return new EmployeeData
            {
                Id = "1007",
                fistName = "Paul",
                lastName = "Street",
                baseRate = 15.00,
                address = "12 Bay Road , Sydney 2000",               
                expectedResult = new Employee("1007", "Paul", "Street", 15.00, "12 Bay Road , Sydney 2000")
            };
        }

        private static EmployeeData InvalidRateEmployeeTestData()
        {
            return new EmployeeData
            {
                Id = "abc",
                fistName = "123",
                lastName = "Street",
                baseRate = 0,
                address = " ",
                expectedResult = null

            };
        }

        private static EmployeeData NullEmployeeTestData()
        {
            return new EmployeeData
            {
                Id = "",
                fistName = "",
                lastName = "",
                baseRate = 0,
                address = "",
                expectedResult = null
            };
        }

        private static TimeSheetData ValidTimeSheetTestData()
        {
            return new TimeSheetData
            {
                Id = "1007",
                date = DateTime.Parse("12/03/2018"),
                project = "Test",
                workedhour = 7   ,
                expectedResult = true
            };
        }

        private static TimeSheetData InvalidHourTimeSheetTestData()
        {
            return new TimeSheetData
            {
                Id = "44",
                date = DateTime.Parse("12/03/2018"),
                project = "Test",
                workedhour = 0,
                expectedResult = false
            };
        }

        private static TimeSheetData NullTimeSheetTestData()
        {
            return new TimeSheetData
            {
                Id = "",
                date = DateTime.Parse("12/03/2018"),
                project = "",
                workedhour = 0,
                expectedResult = false
            };
        }

        private static PayrollData PayrollData()
        {
            Employee newEmployee = new Employee("1008", "Nick", "Du", 10.00, "12 Ann Road , Sydney 2000");
            newEmployee.AddTimeSheet("1008", DateTime.Parse("12/03/2018"), "Sale", 7);

            return new PayrollData
            {
                Id = newEmployee,
                startdate = DateTime.Parse("12/03/2018"),
                enddate = DateTime.Parse("13/03/2018"),               
                expectedWage = 70,
                expectedHour = 7                
            };
        }

        private static PayrollData PayrollData_InvalidEmployeeId()
        {
            return new PayrollData
            {
                Id = null,
                startdate = DateTime.Parse("12/03/2018"),
                enddate = DateTime.Parse("12/03/2018"),
                expectedWage = 0,
                expectedHour = 0
            };
        }

        private static PayrollData PayrollData_InvalidDates()
        {
            Employee newEmployee = new Employee("1007", "Paul", "Street", 15.00, "12 Bay Road , Sydney 2000");
            return new PayrollData
            {
                Id = newEmployee,
                startdate = DateTime.Parse("22/12/2017"),
                enddate = DateTime.Parse("01/12/2017"),
                expectedWage = 0,
                expectedHour = 0
            };
        }

       
        private static HourlyRateData WeekDayRate()
        {
            return new HourlyRateData
            {
                
                date = DateTime.Parse("10/09/2018"),
                baseRate = 10,
                expectedRate = 10
            };
        }

        private static HourlyRateData StaturdayRate()
        {
            return new HourlyRateData
            {

                date = DateTime.Parse("15/09/2018"),
                baseRate = 10,
                expectedRate = 15
            };
        }

        private static HourlyRateData SundayRate()
        {
            return new HourlyRateData
            {

                date = DateTime.Parse("16/09/2018"),
                baseRate = 10,
                expectedRate = 20
            };
        }

        private static HourlyRateData MaxHourlyRate()
        {
            return new HourlyRateData
            {

                date = DateTime.Parse("16/09/2018"),
                baseRate = 30,
                expectedRate = 50
            };
        }

    }


}
