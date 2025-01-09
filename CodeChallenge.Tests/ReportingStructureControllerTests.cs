
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
    public class ReportingStructureControllerTests
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
        public void Individual_Contributor_Returns_Nothing()
        {
            //Arrange
            var newEmployee = new Employee()
            {
                Department = "Engineering",
                FirstName = "Danny",
                LastName = "Marshall",
                Position = "Developer VI",
            };
            var requestContent = new JsonSerialization().ToJson(newEmployee);
            //Execute
            var postRequestTask = _httpClient.PostAsync("api/employee",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            var employee = response.DeserializeContent<Employee>();

            var reportingStructureRequest = _httpClient.GetAsync($"api/reportingStructure/{employee.EmployeeId}");
            response = reportingStructureRequest.Result;

            var structure = response.DeserializeContent<ReportingStructure>();

            //Assert
            Assert.AreEqual(0, structure.numberofReports);

        }

        //This test will be verifying that John Lennon has 4 in their reporitng structure like the ReadMe says
        [TestMethod]
        public void Example_Case_Is_Correct()
        {

            //Execute
            var reportingStructureRequest = _httpClient.GetAsync($"api/reportingStructure/16a596ae-edd3-4847-99fe-c4518e82c86f");
            var response = reportingStructureRequest.Result;

            var structure = response.DeserializeContent<ReportingStructure>();

            //Assert
            Assert.AreEqual(4, structure.numberofReports);

        }

        [TestMethod]
        public void Getting_An_Incorrect_Employee_Returns_NotFound()
        {
            var getRequestTask = _httpClient.GetAsync($"api/reportingStructure/NotFound");
            var response = getRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
