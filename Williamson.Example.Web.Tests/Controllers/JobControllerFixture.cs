using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Williamson.Example.Web.Model;

namespace Williamson.Example.Web.Tests.Controllers
{
    /// <summary>
    /// Performs tests using the JobController
    /// </summary>
    [TestFixture]
    public class JobControllerFixture
    {
        App app = new App();

        [SetUp]
        public void Setup()
        {
            app.Start();
        }

        [TearDown]
        public void TearDown()
        {
            app.Stop();
        }

        /// <summary>
        /// No runs initially
        /// </summary>
        [Test]
        public void NoRunsInitially()
        {
            var client = new HttpClient();
            using (var result = client.GetAsync(new Uri(app.Uri,"api/run"), HttpCompletionOption.ResponseContentRead).Result)
            {
                Assert.AreEqual("application/json", result.Content.Headers.ContentType.MediaType);
                var runs = JsonConvert.DeserializeObject<Run[]>(result.Content.ReadAsStringAsync().Result);

                Assert.AreEqual(0, runs.Length);
            }
        }

        /// <summary>
        /// Tests that we can create a run and delete
        /// </summary>
        [Test]
        public void CreateARunGetAndDelete()
        {
            var client = new HttpClient();
            var content = new StringContent(
                JsonConvert.SerializeObject(new { Name="Foo"}), 
                Encoding.UTF8, "application/json");

            //create a run
            using (var response = client.PostAsync(new Uri(app.Uri, "api/run/start"), content).Result)
            {
                Assert.IsTrue(response.IsSuccessStatusCode);
                var c = JsonConvert.DeserializeObject <Guid>(response.Content.ReadAsStringAsync().Result);
            }

            //now, should have 1 run
            using (var response = client.GetAsync(new Uri(app.Uri, "api/run"), HttpCompletionOption.ResponseContentRead).Result)
            {
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                var runs = JsonConvert.DeserializeObject<Run[]>(response.Content.ReadAsStringAsync().Result);

                Assert.AreEqual(1, runs.Length , "Wrong number of runs");
            }
        }        
    }
}
