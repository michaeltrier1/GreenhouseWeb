using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenhouseWeb.Tests.Mock;
using GreenhouseWeb.Controllers;
using System.Web.Mvc;
using GreenhouseWeb.Models;

namespace GreenhouseWeb.Tests.Behavior
{
    /// <summary>
    /// Summary description for CreateSchedule
    /// </summary>
    [TestClass]
    public class CreateScheduleTests
    {

        private static  GreenhouseDBContext db;
        private static Greenhouse greenhouse;
        private ClientMock client;

        public CreateScheduleTests()
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
        public static void MyClassInitialize(TestContext testContext) {
            db = new GreenhouseDBContext();

            greenhouse = new Greenhouse();
            greenhouse.GreenhouseID = "UnitTesting";
            greenhouse.IP = "127.0.0.1";
            greenhouse.Password = "password";
            greenhouse.Port = 8070;

            db.Greenhouses.Add(greenhouse);
            db.SaveChanges();
        }

        [ClassCleanup()]
        public static void MyClassCleanup() {
            db.Greenhouses.Remove(greenhouse);
            db.SaveChanges();
        }

        [TestInitialize()]
        public void MyTestInitialize() {
            client = new ClientMock();
            client.ID = greenhouse.GreenhouseID;
        }

        [TestCleanup()]
        public void MyTestCleanup() {
            client.Stop();
            client = null;
        }


        [TestMethod]
        public void ValidScheduleAndGreenhouse()
        {
            // Arrange
            string schedule = this.GetValidSchedule();
            HomeController controller= new HomeController();
            string greenhouseID = client.ID;

            // Act
            controller.applySchedule(schedule, greenhouseID);

            // Assert
            //db.Schedules.Find();
            bool received = client.ReceivedSchedule;
            Assert.IsTrue(received);
        }

        [TestMethod]
        public void InvalidSchedule()
        {            
            // Arrange
            string greenhouseID = client.ID;
            string schedule = this.GetInvalidSchedule();
            HomeController controller = new HomeController();

            // Act
            controller.applySchedule(schedule, greenhouseID);

            // Assert
            //db.Schedules.Find();
            bool received = client.ReceivedSchedule;
            Assert.IsFalse(received);
        }

        [TestMethod]
        public void NoGreenhouse()
        {
            // Arrange
            string greenhouseID = client.ID+"1";
            string schedule = this.GetValidSchedule();
            HomeController controller = new HomeController();

            // Act
            controller.applySchedule(schedule, greenhouseID);

            // Assert
            //db.Schedules.Find();
            bool received = client.ReceivedSchedule;
            Assert.IsFalse(received);
        }

        private string GetValidSchedule()
        {
            string schedule = "{\"rawSchedule\":[[\"00.00-02.00\",20,20,20,20,20],[\"02.00-04.00\",20,20,20,20,20],[\"04.00-06.00\",20,20,20,20,20],[\"06.00-08.00\",20,20,20,20,20],[\"08.00-10.00\",20,20,20,20,20],[\"10.00-12.00\",20,20,20,20,20],[\"12.00-14.00\",20,20,20,20,20],[\"14.00-16.00\",20,20,20,20,20],[\"16.00-18.00\",20,20,20,20,20],[\"18.00-20.00\",20,20,20,20,20],[\"20.00-22.00\",20,20,20,20,20],[\"22.00-24.00\",20,20,20,20,20]]}";
            return schedule;
        }

        private string GetInvalidSchedule()
        {
            string schedule = "{\"rawSchedule\":[[\"00.00-02.00\",20,20,20,20,20],[\"02.00-04.00\",20,a,20,20,20],[\"04.00-06.00\",20,20,20,20,20],[\"06.00-08.00\",20,20,20,20,20],[\"08.00-10.00\",20,20,20,20,20],[\"10.00-12.00\",20,20,20,20,20],[\"12.00-14.00\",20,20,20,20,20],[\"14.00-16.00\",20,20,20,20,20],[\"16.00-18.00\",20,20,20,20,20],[\"18.00-20.00\",20,20,20,20,20],[\"20.00-22.00\",20,20,20,20,20],[\"22.00-24.00\",20,20,20,20,20]]}";
            return schedule;
        }

    }
}
