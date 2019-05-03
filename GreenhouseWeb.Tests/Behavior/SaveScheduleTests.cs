using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenhouseWeb.Models;
using GreenhouseWeb.Tests.Mock;
using GreenhouseWeb.Controllers;

namespace GreenhouseWeb.Tests.Behavior
{
    /// <summary>
    /// Summary description for SaveSchedule
    /// </summary>
    [TestClass]
    public class SaveScheduleTests
    {

        private static GreenhouseDBContext db;
        private static string scheduleID;

        public SaveScheduleTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            db = new GreenhouseDBContext();
            scheduleID = "UnitTestSchedule";

            foreach (Schedule block in db.Schedules)
            {
                if (block.ScheduleID == scheduleID)
                {
                    db.Schedules.Remove(block);
                }
            }

            db.SaveChanges();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            foreach (Schedule block in db.Schedules)
            {
                if (block.ScheduleID == scheduleID)
                {
                    db.Schedules.Remove(block);
                }
            }

            db.SaveChanges();
        }


        [TestMethod]
        public void ScheduleIsValid()
        {
            // Arrange
            string schedule = this.GetValidSchedule();
            HomeController controller = new HomeController();

            // Act
            controller.saveSchedule(schedule, scheduleID);

            // Assert
            int blocks = 0;
            foreach (Schedule block in db.Schedules)
            {
                if (block.ScheduleID == scheduleID)
                {
                    blocks++;
                }
            }

            Assert.AreEqual(12, blocks);
        }

        [TestMethod]
        public void ScheduleIsInvalid()
        {
            // Arrange
            string schedule = this.GetInvalidSchedule();
            HomeController controller = new HomeController();

            // Act
            controller.saveSchedule(schedule, scheduleID);

            // Assert
            int blocks = 0;
            foreach (Schedule block in db.Schedules)
            {
                if (block.ScheduleID == scheduleID)
                {
                    blocks++;
                }
            }

            Assert.AreEqual(0,blocks);
        }

        private string GetValidSchedule()
        {
            string schedule = "{\"data\":[[\"00.00-02.00\",20,20,20,20,20],[\"02.00-04.00\",20,20,20,20,20],[\"04.00-06.00\",20,20,20,20,20],[\"06.00-08.00\",20,20,20,20,20],[\"08.00-10.00\",20,20,20,20,20],[\"10.00-12.00\",20,20,20,20,20],[\"12.00-14.00\",20,20,20,20,20],[\"14.00-16.00\",20,20,20,20,20],[\"16.00-18.00\",20,20,20,20,20],[\"18.00-20.00\",20,20,20,20,20],[\"20.00-22.00\",20,20,20,20,20],[\"22.00-24.00\",20,20,20,20,20]]}";
            return schedule;
        }

        private string GetInvalidSchedule()
        {
            string schedule = "{\"data\":[[\"00.00-02.00\",20,20,a,20,20],[\"02.00-04.00\",20,20,20,20,20],[\"04.00-06.00\",20,20,20,20,20],[\"06.00-08.00\",20,20,20,20,20],[\"08.00-10.00\",20,20,20,20,20],[\"10.00-12.00\",20,20,20,20,20],[\"12.00-14.00\",20,20,20,20,20],[\"14.00-16.00\",20,20,20,20,20],[\"16.00-18.00\",20,20,20,20,20],[\"18.00-20.00\",20,20,20,20,20],[\"20.00-22.00\",20,20,20,20,20],[\"22.00-24.00\",20,20,20,20,20]]}";
            return schedule;
        }




    }
}
