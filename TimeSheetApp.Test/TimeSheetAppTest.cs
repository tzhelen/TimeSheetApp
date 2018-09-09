using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TimeSheetApp;


namespace TimeSheetApp.Test
{
     [TestFixture]  
    public class TimeSheetAppTest
    {

        private Employee emp;
        private TimeSheet ts;

       [SetUp]
        protected void Setup()
        {
           emp = new Employee();
            ts = new TimeSheet();

        }



        [Test,TestCaseSource(typeof(TestCasesDataFactory), nameof(TestCasesDataFactory.GetCreateEmployee_ValidTestCasesData))]
         public void CreateNewEmployeeTest_ValidTest(EmployeeData testData)
          {
           
            Employee newEmployee = emp.CreateEmployee(testData.Id, testData.fistName, testData.lastName, testData.baseRate, testData.address);

            Assert.IsInstanceOf<Employee>(newEmployee);
        }


        [Test,TestCaseSource(typeof(TestCasesDataFactory), nameof(TestCasesDataFactory.GetCreateEmployee_InvalidTestCasesData))]
        public void CreateNewEmployeeTest_InvalidTest(EmployeeData testData)
        {
         
            Employee newEmployee = emp.CreateEmployee(testData.Id, testData.fistName, testData.lastName, testData.baseRate, testData.address);

            Assert.IsNull(newEmployee);
         
        }

        [Test,TestCaseSource(typeof(TestCasesDataFactory), nameof(TestCasesDataFactory.GetCreateTimeSheet_ValidTestCasesData))]
        public void CreateTimeSheetTest_ValidTest(TimeSheetData testData)
        {
            TimeSheet newTimeSheet = ts.CreateTimeSheet(testData.Id, testData.date,testData.project,testData.workedhour);
                      
            Assert.IsInstanceOf<TimeSheet>(newTimeSheet);
        }

       
       
        [Test,TestCaseSource(typeof(TestCasesDataFactory), nameof(TestCasesDataFactory.GetCreateTimeSheet_InvalidTestCasesData))]
        public void CreateTimeSheetTest_InvalidTest(TimeSheetData testData)
        {
            TimeSheet newTimeSheet = ts.CreateTimeSheet(testData.Id, testData.date, testData.project, testData.workedhour);

            Assert.IsNull(newTimeSheet);
        }


        [Test,TestCaseSource(typeof(TestCasesDataFactory), nameof(TestCasesDataFactory.ProcessPayroll_TestCasesData))]
        public void ProcessPayrollTest(PayrollData testData)
        {
            double actualWage = 0;
            double actualHour = 0;
          
            Payroll.ProcessPayroll(testData.Id, testData.startdate, testData.enddate, out actualWage, out actualHour);

            Assert.AreEqual(testData.expectedWage, actualWage,"Calcuate wage return unexpected value.");
            Assert.AreEqual(testData.expectedHour, actualHour, "Calcuate total hour return unexpected value.");


        }


        [Test, TestCaseSource(typeof(TestCasesDataFactory), nameof(TestCasesDataFactory.GetRate_TestCasesData))]
        public void GetRateTest(HourlyRateData testData)
        {

            double actualHourlyRate =  Payroll.GetRate(testData.date, testData.baseRate);

            Assert.AreEqual(testData.expectedRate, actualHourlyRate, "Calculated hourly rate return unexpected value");
          
        }

       
    }
}
