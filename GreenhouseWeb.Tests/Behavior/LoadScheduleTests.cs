using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenhouseWeb.Controllers;
using GreenhouseWeb.Models;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using GreenhouseWeb.Services;

namespace GreenhouseWeb.Tests.Behavior
{
    /// <summary>
    /// Summary description for LoadSchedule
    /// </summary>
    [TestClass]
    public class LoadScheduleTests
    {
        private static GreenhouseDBContext db;
        private static string scheduleID;
        private static string wrongScheduleID;
        private static int blueLight;
        private static int redLight;
        private static int temperature;
        private static int humidity;
        private static int waterlevel;

        public LoadScheduleTests()
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
            wrongScheduleID = "UnitTestSchedule1";

            blueLight = 20;
            redLight = 30;
            temperature = 40;
            humidity = 50;
            waterlevel = 10;

            for (int blockNumber = 1; blockNumber < 13; blockNumber++)
            {
                Schedule schedule = new Schedule();
                schedule.ScheduleID = scheduleID;
                schedule.Blocknumber = blockNumber;
                schedule.BlueLight = blueLight;
                schedule.RedLight = redLight;
                schedule.InternalTemperature = temperature;
                schedule.Humidity = humidity;
                schedule.WaterLevel = waterlevel;

                db.Schedules.Add(schedule);
                db.SaveChanges();
            }

            foreach (Schedule block in db.Schedules)
            {
                if (block.ScheduleID == wrongScheduleID)
                {
                    db.Schedules.Remove(block);
                }
            }

            db.SaveChanges();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            foreach (Schedule block in db.Schedules)
            {
                if (block.ScheduleID == scheduleID)
                {
                    db.Schedules.Remove(block);
                }
            }

            db.SaveChanges();

            ServiceFacadeGetter.getInstance().clear();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {

        }

        [TestMethod]
        public void LoadSuccess()
        {
            // Arrange
            JObject schedule = this.GetValidSchedule();
            HomeController controller = new HomeController();

            // Act
            JsonResult loadedSchedule = controller.loadSchedule(scheduleID);
            string[,] data = (string[,])loadedSchedule.Data;

            // Assert
            int numberOfBlocks = data.GetLength(0);
            bool schedulesAreEqual = true;
            for (int i = 0; i < 12; i++)
            {
                if (!data[i, 0].Equals(blueLight+""))
                {
                    schedulesAreEqual = false;
                    break;
                }

                if (!data[i, 1].Equals(redLight+""))
                {
                    schedulesAreEqual = false;
                    break;
                }

                if (!data[i, 2].Equals(temperature+""))
                {
                    schedulesAreEqual = false;
                    break;
                }

                if (!data[i, 3].Equals(humidity+""))
                {
                    schedulesAreEqual = false;
                    break;
                }

                if (!data[i, 4].Equals(waterlevel+""))
                {
                    schedulesAreEqual = false;
                    break;
                }
            }

            Assert.AreEqual(12, numberOfBlocks);
            Assert.IsTrue(schedulesAreEqual);
        }

        [TestMethod]
        public void LoadFail()
        {
            // Arrange
            JObject schedule = this.GetInvalidSchedule();
            HomeController controller = new HomeController();

            // Act
            JsonResult loadedSchedule = controller.loadSchedule(wrongScheduleID);
            string[,] data = (string[,])loadedSchedule.Data;

            // Assert
            bool schedulesAreNull = true;
            for (int i = 0; i < 12; i++)
            {
                if (data[i, 0] != null)
                {
                    schedulesAreNull = false;
                    break;
                }

                if (data[i, 1] != null)
                {
                    schedulesAreNull = false;
                    break;
                }

                if (data[i, 2] != null)
                {
                    schedulesAreNull = false;
                    break;
                }

                if (data[i, 3] != null)
                {
                    schedulesAreNull = false;
                    break;
                }

                if (data[i, 4] != null)
                {
                    schedulesAreNull = false;
                    break;
                }
            }

            Assert.IsTrue(schedulesAreNull);
        }

        private JObject GetInvalidSchedule() { return new JObject(); }

        private JObject GetValidSchedule() { return new JObject(); }
    }
}
