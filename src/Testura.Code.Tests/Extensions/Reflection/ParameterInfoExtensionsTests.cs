namespace Testura.Code.Tests.Extensions.Reflection;

using System.Linq;
using Code.Extensions.Reflection;
using NUnit.Framework;

[TestFixture]
public class ParameterInfoExtensionsTests
{
    public void ToParameter_WhenHavingAParamterInfo_ShouldGetParameterObject()
    {
        var parameterInfo = typeof(TestClass).GetMethods()
            .First()
            .GetParameters()
            .First();
        var parameter = parameterInfo.ToParameter();
        Assert.AreEqual(parameterInfo.Name, parameter.Name);
        Assert.AreEqual(parameterInfo.ParameterType, parameter.Type);
    }

    private class TestClass
    {
        public void Method(int firstPar)
        {
        }
    }
}
