using Api.Services.Developers;

namespace Test
{
    public class WebDeveloperDataTests
    {
        private IWebData webData;


        [SetUp]
        public void Setup()
        {
            webData = new WebDeveloperData();

        }

        [Test]
        public async Task GetWebDevelopersData_ReturnsExpectedData()
        {
            
                
                var result = await webData.GetWebDevelopersData();
                var developersList = result.ToList();
                Assert.That(developersList.Count, Is.EqualTo(1));
                var developer = developersList.First();
                Assert.That(developer.Text, Is.EqualTo("Lisandro Iraguen"));
                Assert.That(developer.Url, Is.EqualTo("https://www.linkedin.com/in/lisandroiraguen/"));
            
        }
    
    }
}