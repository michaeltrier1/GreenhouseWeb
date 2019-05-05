using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenhouseWeb.Models;
using GreenhouseWeb.Tests.Mock;
using GreenhouseWeb.Services;
using System.Threading;
using System.Globalization;

namespace GreenhouseWeb.Tests.Behavior
{
    /// <summary>
    /// Summary description for UploadDataTests
    /// </summary>
    [TestClass]
    public class UploadDataTests
    {

        private static GreenhouseDBContext db;
        private static Greenhouse greenhouse;
        private ClientMock client;
        private Datalog datalog;

        public UploadDataTests()
        {
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
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {

        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            greenhouse = new Greenhouse();
            greenhouse.GreenhouseID = "UnitTesting";
            greenhouse.IP = "127.0.0.1";
            greenhouse.Password = "password";
            greenhouse.Port = 8070;

            db.Greenhouses.Add(greenhouse);
            db.SaveChanges();

            datalog = new Datalog();
            datalog.Greenhouse_ID = greenhouse.GreenhouseID;
            datalog.TimeOfReading = DateTime.Now;
            datalog.InternalTemperature = 20;
            datalog.ExternalTemperature = 20;
            datalog.Humidity = 20;
            datalog.Waterlevel = 20;

            foreach (Datalog log in db.Datalogs)
            {
                if (log.Greenhouse_ID == datalog.Greenhouse_ID)
                {
                    db.Datalogs.Remove(log);
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
            db.Greenhouses.Remove(greenhouse);
            db.SaveChanges();

            foreach (Datalog log in db.Datalogs)
            {
                if (log.Greenhouse_ID == datalog.Greenhouse_ID)
                {
                    db.Datalogs.Remove(log);
                }
            }

            db.SaveChanges();

            client.Stop();
            client = null;
            ServiceFacadeGetter.getInstance().clear();
        }




        [TestMethod]
        public void DataIsValid()
        {
            // Arrange
            string greenhouseID = client.ID;

            String dateString = datalog.TimeOfReading.ToString("r", DateTimeFormatInfo.InvariantInfo);
            //   Sun May 05 14:00:21 CEST 2019
            //r :Thu, 17 Aug 2000 23:32:32 GMT
            client.uploadDataContinually(dateString, datalog.InternalTemperature.ToString(), datalog.ExternalTemperature, datalog.Humidity, datalog.Waterlevel);
            Thread.Sleep(5000);

            // Assert
            bool foundDatalog = false;
            foreach (Datalog log in db.Datalogs)
            {
                if (log.Greenhouse_ID == datalog.Greenhouse_ID)
                {
                    foundDatalog = true;
                    break;
                }
            }

            Assert.IsTrue(foundDatalog);
        }

        [TestMethod]
        public void DataIsInvalid()
        {
            // Arrange
            string greenhouseID = client.ID;

            datalog = new Datalog();
            datalog.Greenhouse_ID = greenhouse.GreenhouseID;
            datalog.TimeOfReading = new DateTime();
            datalog.ExternalTemperature = 20;
            datalog.Humidity = 20;
            datalog.Waterlevel = 20;

            // Act
            client.uploadDataContinually("", "a", datalog.ExternalTemperature, datalog.Humidity, datalog.Waterlevel);
            Thread.Sleep(5000);

            // Assert
            bool foundDatalog = false;
            foreach (Datalog log in db.Datalogs)
            {
                if (log == datalog)
                {
                    foundDatalog = true;
                }
            }

            Assert.IsFalse(foundDatalog);
        }
    }
}
