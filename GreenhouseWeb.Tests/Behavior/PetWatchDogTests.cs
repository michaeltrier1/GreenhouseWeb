using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenhouseWeb.Models;
using GreenhouseWeb.Tests.Mock;
using GreenhouseWeb.Services;
using System.Threading;

namespace GreenhouseWeb.Tests.Behavior
{
    /// <summary>
    /// Summary description for PetWatchDogTests
    /// </summary>
    [TestClass]
    public class PetWatchDogTests
    {

        private static GreenhouseDBContext db;
        private static Greenhouse greenhouse;
        private ClientMock client;

        public PetWatchDogTests()
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

            client.Stop();
            client = null;
            ServiceFacadeGetter.getInstance().clear();
        }


        [TestMethod]
        public void WatchdogIsPetted()
        {
            // Arrange
            string greenhouseID = client.ID;

            // Act
            client.petContinually();
            Thread.Sleep(25000);

            // Assert
            bool receivedRetry = client.RecievedRetryConnection;
            Assert.IsFalse(receivedRetry);
        }

        [TestMethod]
        public void WatchdogIsNotPetted()
        {
            // Arrange
            string greenhouseID = client.ID;
            client.pet();

            // Act
            Thread.Sleep(25000);

            // Assert
            bool receivedRetry = client.RecievedRetryConnection;
            Assert.IsTrue(receivedRetry);
        }
    }
}
