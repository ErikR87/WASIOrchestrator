using Microsoft.AspNetCore.Mvc;
using WO.Hub.Contract.Agent;
using WO.Hub.Controllers;
using WO.Hub.Services;

namespace WO.Hub.Tests
{
    [TestClass]
    public class AgentControllerTest
    {
        [TestMethod]
        public async Task ListTest()
        {
            const string NAME = "Bob";
            var agent = new Agent(NAME);

            var agentService = new AgentService();
            var registerResponse = await agentService.RegisterAsync(NAME, agent);

            if (registerResponse.HasError)
            {
                Assert.Fail();
            }

            var controller = new AgentController(agentService);
            var listResult = await controller.List();


            Console.WriteLine(listResult.ToString());
            Assert.IsNotNull(listResult, "Response is null");
            Assert.Equals(1, listResult.Value.Length);
        } 
    }
}