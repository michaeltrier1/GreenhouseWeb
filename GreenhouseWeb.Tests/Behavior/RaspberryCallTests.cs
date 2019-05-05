using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenhouseWeb.Models;
using GreenhouseWeb.Tests.Mock;
using GreenhouseWeb.Services;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace GreenhouseWeb.Tests
{
    /// <summary>
    /// Summary description for RaspberryCalls
    /// </summary>
    [TestClass]
    public class RaspberryCallTests
    {

        private static GreenhouseDBContext db;
        private string greenhouseID;
        private static Greenhouse greenhouse;
        private ClientMock client;
        private Schedule[] blocks;

        public RaspberryCallTests()
        {
        }

        private TestContext testContextInstance;
        private string scheduleID;

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
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            greenhouseID = "UnitTesting";
            greenhouse = new Greenhouse();
            greenhouse.GreenhouseID = greenhouseID;
            greenhouse.IP = "127.0.0.1";
            greenhouse.Password = "password";
            greenhouse.Port = 8070;

            scheduleID = greenhouse.GreenhouseID + "Schedule";

            db.Greenhouses.Add(greenhouse);
            db.SaveChanges();

            foreach (Schedule block in db.Schedules)
            {
                if (block.ScheduleID == scheduleID)
                {
                    db.Schedules.Remove(block);
                }
            }
            db.SaveChanges();


            client = new ClientMock(greenhouse.IP, greenhouse.Port);
            client.ID = greenhouse.GreenhouseID;
            client.ListenForCommunication();

            ServiceFacadeGetter.getInstance();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            foreach (Greenhouse house in db.Greenhouses)
            {
                if (house.GreenhouseID == greenhouseID)
                {
                    db.Greenhouses.Remove(house);
                }
            }
            db.SaveChanges();

            foreach (Schedule block in db.Schedules)
            {
                if (block.ScheduleID == scheduleID)
                {
                    db.Schedules.Remove(block);
                }
            }
            db.SaveChanges();

            foreach (GreenhouseSchedule greenhouseSchedule in db.GreenhouseSchedules)
            {
                if (greenhouseSchedule.GreenhouseID == greenhouseID)
                {
                    db.GreenhouseSchedules.Remove(greenhouseSchedule);
                }
            }
            db.SaveChanges();

            client.Stop();
            client = null;
            ServiceFacadeGetter.getInstance().clear();
        }





        [TestMethod]
        public void Startup()
        {
            // Arrange
            GreenhouseSchedule greenhouseSchedule = new GreenhouseSchedule();
            greenhouseSchedule.GreenhouseID = greenhouse.GreenhouseID;
            greenhouseSchedule.ScheduleID = scheduleID;
            greenhouseSchedule.StartDate = DateTime.Parse("Thu, 17 Aug 2000 23:32:32 GMT", System.Globalization.CultureInfo.InvariantCulture);

            db.GreenhouseSchedules.Add(greenhouseSchedule);
            db.SaveChanges();

            int startPort = 0;
            int newPort = 0;
            int sendPort = 8521;

            foreach (Greenhouse greenhouse in db.Greenhouses)
            {
                if (greenhouse.GreenhouseID == greenhouse.GreenhouseID)
                {
                    startPort = greenhouse.Port;
                }
            }
            db.SaveChanges();

            JObject expected = new JObject();

            blocks = new Schedule[12];
            for (int i = 1; i < 13; i++)
            {
                Schedule schedule = new Schedule();
                schedule.Blocknumber = i;
                schedule.BlueLight = 20;
                schedule.RedLight = 20;
                schedule.InternalTemperature = 20;
                schedule.Humidity = 20;
                schedule.WaterLevel = 20;
                schedule.ScheduleID = scheduleID;
                blocks[i - 1] = schedule;
            }
            db.Schedules.AddRange(blocks);
            db.SaveChanges();

            // Act
            JObject response = client.sendStartupMessage(sendPort);

            // Assert
            db = new GreenhouseDBContext();
            bool scheduleIsCorrect = true;
            JObject day = (JObject)response.GetValue("day1");

            for (int i = 1; i<13; i++)
            {
                JObject block = (JObject)day.GetValue("block"+i);

                if (blocks[i-1].BlueLight != (double)block.GetValue("light_blue"))
                {
                    scheduleIsCorrect = false;
                    break;
                }

                if (blocks[i - 1].RedLight != (double)block.GetValue("light_red"))
                {
                    scheduleIsCorrect = false;
                    break;
                }

                if (blocks[i - 1].InternalTemperature != (double)block.GetValue("temperature"))
                {
                    scheduleIsCorrect = false;
                    break;
                }

                if (blocks[i - 1].Humidity != (double)block.GetValue("humidity"))
                {
                    scheduleIsCorrect = false;
                    break;
                }

                if (blocks[i - 1].WaterLevel != (double)block.GetValue("waterlevel"))
                {
                    scheduleIsCorrect = false;
                    break;
                }
            }

            Assert.IsTrue(scheduleIsCorrect);

            foreach (Greenhouse greenhouse in db.Greenhouses)
            {
                if (greenhouse.GreenhouseID == greenhouse.GreenhouseID)
                {
                    newPort = greenhouse.Port;
                }
            }
            db.SaveChanges();

            Assert.AreNotEqual(startPort, newPort);
            Assert.AreEqual(sendPort, newPort);
        }

        [TestMethod]
        public void IPAddress()
        {
            // Arrange
            int startPort = 0;
            int newPort = 0;
            int sendPort = 8097;

            foreach (Greenhouse greenhouse in db.Greenhouses)
            {
                if (greenhouse.GreenhouseID == greenhouse.GreenhouseID)
                {
                    startPort = greenhouse.Port;
                }
            }
            db.SaveChanges();

            // Act
            client.sendIPAddress(8097);
            Thread.Sleep(1000);

            // Assert
            db = new GreenhouseDBContext();
            foreach (Greenhouse greenhouse in db.Greenhouses)
            {
                if (greenhouse.GreenhouseID == greenhouse.GreenhouseID)
                {
                    newPort = greenhouse.Port;
                }
            }
            db.SaveChanges();

            Assert.AreNotEqual(startPort, newPort);
            Assert.AreEqual(sendPort, newPort);
        }
    }
}
