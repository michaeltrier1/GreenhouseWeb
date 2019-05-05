using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenhouseWeb.Models;
using GreenhouseWeb.Tests.Mock;
using GreenhouseWeb.Services;
using GreenhouseWeb.Controllers;
using System.Threading;

namespace GreenhouseWeb.Tests.Behavior
{
    /// <summary>
    /// Summary description for StopLiveDataTests
    /// </summary>
    [TestClass]
    public class StopLiveDataTests
    {

        private static GreenhouseDBContext db;
        private static Greenhouse greenhouse;
        private ClientMock client;

        public StopLiveDataTests()
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

        [TestInitialize()]
        public void MyTestInitialize()
        {
            db = new GreenhouseDBContext();

            greenhouse = new Greenhouse();
            greenhouse.GreenhouseID = "UnitTesting";
            greenhouse.IP = "127.0.0.1";
            greenhouse.Password = "password";
            greenhouse.Port = 8070;

            db.Greenhouses.Add(greenhouse);
            db.SaveChanges();

            client = new ClientMock(greenhouse.IP, greenhouse.Port);
            client.ID = greenhouse.GreenhouseID;
            client.ListenForCommunication();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            db.Greenhouses.Remove(greenhouse);
            db.SaveChanges();

            client.Stop();
            client = null;
            ServiceFacadeGetter.getInstance().clear();
        }

        [TestMethod]
        public void GreenhouseIsActive()
        {
            // Arrange
            double internalTemperature = 20;
            double externalTemperature = 20;
            double humidity = 20;
            double waterLevel = 20;

            client.setMeasurements(internalTemperature.ToString(), externalTemperature.ToString(), humidity.ToString(), waterLevel.ToString());
            HomeController controller = new HomeController();
            string greenhouseID = client.ID;
            controller.getNewestData(greenhouseID);
            Thread.Sleep(1000);

            // Act
            controller.StopLiveData(greenhouseID);
            Thread.Sleep(1000);

            // Assert
            bool haveSentData = client.isSentNewLiveData();
            Assert.IsTrue(haveSentData);

            Thread.Sleep(1500);

            bool haveSentMoreData = client.isSentNewLiveData();
            Assert.IsFalse(haveSentMoreData);
        }

        [TestMethod]
        public void GreenhouseIsInactive()
        {
            // Arrange
            double internalTemperature = 20;
            double externalTemperature = 20;
            double humidity = 20;
            double waterLevel = 20;

            client.setMeasurements(internalTemperature.ToString(), externalTemperature.ToString(), humidity.ToString(), waterLevel.ToString());
            HomeController controller = new HomeController();
            string greenhouseID = client.ID;
            string wrongGreenhouseID = client.ID+1;
            controller.getNewestData(greenhouseID);
            Thread.Sleep(1000);

            // Act
            controller.StopLiveData(wrongGreenhouseID);
            Thread.Sleep(1000);

            // Assert
            bool haveSentData = client.isSentNewLiveData();
            Assert.IsTrue(haveSentData);

            Thread.Sleep(1500);

            bool haveSentMoreData = client.isSentNewLiveData();
            Assert.IsTrue(haveSentMoreData);
        }
    }
}
