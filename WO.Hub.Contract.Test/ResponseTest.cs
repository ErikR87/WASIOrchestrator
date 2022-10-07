using Microsoft.VisualStudio.TestTools.UnitTesting;
using WO.Hub.Contract;

namespace WO.Hub.Contract.Tests;

[TestClass()]
public class ResponseTest
{
    [TestMethod]
    public void AddErrorTest()
    {
        var response = new Response();

        const string errorText1 = "first error";
        const string errorText2 = "second error";

        response.AddError(errorText1);
        response.AddError(errorText2);

        Assert.AreEqual($"{errorText1}\n{errorText2}", response.ErrorText);
        Assert.IsTrue(response.HasError);
        Assert.AreEqual(ResultStatus.Error, response.Status);
        Assert.AreEqual(2, response.ErrorMessages.Count);
    }

    [TestMethod()]
    public void AddEntityTest()
    {
        var response = new Response();

        var testObject1 = new DateTime();

        response.AddEntity(testObject1);

        var firstResult = response.GetEntity<DateTime>();

        Assert.AreEqual(testObject1, firstResult);
        Assert.AreEqual(1, response.Entities.Count);
    }
}