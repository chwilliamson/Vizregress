using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Williamson.Example.Web.Models;

namespace Williamson.Example.Web.Tests.Controllers
{
    /// <summary>
    /// Performs tests using the JobController
    /// </summary>
    [TestFixture]
    public class JobControllerFixture
    {
        App app = new App(new AppCfg { Name="Tests"});

        /// <summary>
        /// Start the application server
        /// </summary>
        [SetUp]
        public void Setup()
        {
            app.Start();
        }

        /// <summary>
        /// Stop the application server
        /// </summary>
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
            using (var result = client.GetAsync(new Uri(app.Uri,"api/runs"), HttpCompletionOption.ResponseContentRead).Result)
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
            var runName = "Foo Run";

            var client = new HttpClient();
            var content = new StringContent(
                JsonConvert.SerializeObject(new { Name=runName}), 
                Encoding.UTF8, "application/json");

            int runId = 0;
            //create a run
            using (var response = client.PostAsync(new Uri(app.Uri, "api/runs/start"), content).Result)
            {
                Assert.IsTrue(response.IsSuccessStatusCode);
                runId = JsonConvert.DeserializeObject <int>(response.Content.ReadAsStringAsync().Result);
            }

            //now, should have 1 run
            using (var response = client.GetAsync(new Uri(app.Uri, "api/runs"), HttpCompletionOption.ResponseContentRead).Result)
            {
                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                var runs = JsonConvert.DeserializeObject<Run[]>(response.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(1, runs.Length , "Wrong number of runs");
                var runOne = runs[0];

                Assert.AreEqual(runName, runOne.Name);
            }

            //delete the run
            using (var response = client.DeleteAsync(new Uri(app.Uri, "api/runs/" + runId)).Result)
            {
                Assert.IsTrue(response.IsSuccessStatusCode,"Delete was NOT successful");               
            }

            //now, should have 0 run
            using (var response = client.GetAsync(new Uri(app.Uri, "api/runs"), HttpCompletionOption.ResponseContentRead).Result)
            {
                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                var runs = JsonConvert.DeserializeObject<Run[]>(response.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(0, runs.Length, "Expected no runs after deleted");
            }

        }

        /// <summary>
        ///<list type="bullet">
        ///  <item>Create a Run</item>
        ///  <item>Create a RunItem</item>
        ///  <item>Check we get the run item</item>
        ///  <item>Cleanup</item>
        /// </list>
        /// </summary>
        [Test]
        public void CreateARunAndARunItemAndValidationGetsThenDelete()
        {
            var runName = "Foo Run";

            var client = new HttpClient();
            var content = new StringContent(
                JsonConvert.SerializeObject(new { Name = runName }),
                Encoding.UTF8, "application/json");

            int runId = 0;
            
            //create a run
            using (var response = client.PostAsync(new Uri(app.Uri, "api/runs/start"), content).Result)
            {
                Assert.IsTrue(response.IsSuccessStatusCode);
                runId = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
            }

            //run items should be zero
            using (var result = client.GetAsync(new Uri(app.Uri, "api/runitems?runId=" + runId), HttpCompletionOption.ResponseContentRead).Result)
            {
                Assert.AreEqual("application/json", result.Content.Headers.ContentType.MediaType);
                var runItems = JsonConvert.DeserializeObject<RunItem[]>(result.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(0, runItems.Length);
            }

            int imageId = 0;
            //upload image
            using (var imageFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("Williamson.Example.Web.Tests.Resources.Code.png"))
            {
                var sc = new StreamContent(imageFile);
                sc.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Test.png"
                };
                using (var response = client.PostAsync(new Uri(app.Uri, "api/images"), sc).Result)
                {
                    Assert.IsTrue(response.IsSuccessStatusCode,"Failed to uploaded");
                    imageId = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
                }
            }

            //associate with run id

            //var runItemContent = new StringContent(
            //    JsonConvert.SerializeObject(new RunItem { ImageId=imageId, ExpectedId="firefox-image-001" }),
            //    Encoding.UTF8, "application/json");

            ////create a run
            //using (var response = client.PostAsync(new Uri(app.Uri, "api/runitems/create"), runItemContent).Result)
            //{
            //    Assert.IsTrue(response.IsSuccessStatusCode,"Failed to create");                
            //}


        }
    }
}
