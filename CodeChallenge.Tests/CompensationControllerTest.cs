using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{

    [TestClass]
    public class CompensationControllerTest
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                employee = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                effectiveDate = "11/11/11",
                salary = 10000
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.CompensationId);
            Assert.AreEqual(newCompensation.salary, 10000);
            Assert.AreEqual(newCompensation.effectiveDate, "11/11/11");
            Assert.AreEqual(newCompensation.employee, "16a596ae-edd3-4847-99fe-c4518e82c86f");

        }

        [TestMethod]
        public void GetCompensationById_Returns_Correctly()
        {
            string employeeID = "b7839309-3348-463b-a7e3-5de1c168beb3";
            // Arrange
            var compensation = new Compensation()
            {
                employee = employeeID,
                effectiveDate = "11/11/11",
                salary = 10000
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));

            var response = postRequestTask.Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var newCompensation = response.DeserializeContent<Compensation>();

            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeID}");
             response = getRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var foundCompensation = response.DeserializeContent<Compensation>();

            //Assert
            Assert.AreEqual(newCompensation.CompensationId, foundCompensation.CompensationId);
            Assert.AreEqual(newCompensation.effectiveDate, foundCompensation.effectiveDate);
            Assert.AreEqual(newCompensation.salary, foundCompensation.salary);
        }

        [TestMethod]
        public void Getting_An_Incorrect_Employee_Returns_NotFound()
        {
            var getRequestTask = _httpClient.GetAsync($"api/compensation/NotFound");
            var response = getRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void Creating_Compensation_With_Incorrect_ID_Returns_NotFound()
        {
            // Arrange
            var compensation = new Compensation()
            {
                employee = "NotFound",
                effectiveDate = "11/11/11",
                salary = 10000
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
