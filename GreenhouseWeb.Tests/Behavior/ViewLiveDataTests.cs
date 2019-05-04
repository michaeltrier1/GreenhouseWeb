using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenhouseWeb.Models;
using GreenhouseWeb.Tests.Mock;
using GreenhouseWeb.Services;
using GreenhouseWeb.Controllers;
using System.Threading;
using System.Web.Mvc;

namespace GreenhouseWeb.Tests.Behavior
{
    /// <summary>
    /// Summary description for ViewLiveDataTests
    /// </summary>
    [TestClass]
    public class ViewLiveDataTests
    {
        private static GreenhouseDBContext db;
        private static Greenhouse greenhouse;
        private ClientMock client;

        public ViewLiveDataTests()
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
            string internalTemperature = null;
            double externalTemperature = 20;
            double humidity = 20;
            double waterLevel = 20;

            client.setMeasurements(internalTemperature, externalTemperature.ToString(), humidity.ToString(), waterLevel.ToString());
            HomeController controller = new HomeController();
            string greenhouseID = client.ID;

            // Act
            controller.getNewestData(greenhouseID);
            Thread.Sleep(2000);

            // Assert
            bool sendingLiveData = client.SendLiveData;
            Assert.IsTrue(sendingLiveData);
            JsonResult data = controller.getNewestData(greenhouseID);

            var type = data.Data.GetType();
            var pinfo = type.GetProperty("internalTemperature");
            var readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("externalTemperature");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.AreEqual(externalTemperature, readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("humidity");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.AreEqual(humidity, readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("waterlevel");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.AreEqual(waterLevel, readValue);
        }

        [TestMethod]
        public void GreenhouseIsInactive()
        {
            // Arrange
            string wrongID = client.ID + 1;
            foreach (Greenhouse greenhouse in db.Greenhouses)
            {
                if (greenhouse.GreenhouseID == wrongID)
                {
                    db.Greenhouses.Remove(greenhouse);
                }
            }
            db.SaveChanges();

            double internalTemperature = 20;
            double externalTemperature = 20;
            double humidity = 20;
            double waterLevel = 20;
            client.setMeasurements(internalTemperature.ToString(), externalTemperature.ToString(), humidity.ToString(), waterLevel.ToString());

            HomeController controller = new HomeController();

            // Act
            controller.getNewestData(wrongID);
            Thread.Sleep(2000);

            // Assert
            bool sendingLiveData = client.SendLiveData;
            Assert.IsFalse(sendingLiveData);
            JsonResult data = controller.getNewestData(wrongID);

            var type = data.Data.GetType();
            var pinfo = type.GetProperty("internalTemperature");
            var readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("externalTemperature");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("humidity");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("waterlevel");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);
        }

        [TestMethod]
        public void WrongFormat()
        {
            // Arrange
            double externalTemperature = 20;
            double humidity = 20;
            double waterLevel = 20;

            client.setMeasurements("a", externalTemperature.ToString(), humidity.ToString(), waterLevel.ToString());
            HomeController controller = new HomeController();
            string greenhouseID = client.ID;

            // Act
            controller.getNewestData(greenhouseID);
            Thread.Sleep(2000);

            // Assert
            bool sendingLiveData = client.SendLiveData;
            Assert.IsTrue(sendingLiveData);
            JsonResult data = controller.getNewestData(greenhouseID);

            var type = data.Data.GetType();
            var pinfo = type.GetProperty("internalTemperature");
            var readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("externalTemperature");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("humidity");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);

            type = data.Data.GetType();
            pinfo = type.GetProperty("waterlevel");
            readValue = pinfo.GetValue(data.Data, null);
            Assert.IsNull(readValue);
        }

    }
}
