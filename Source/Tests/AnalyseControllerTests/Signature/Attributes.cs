using System.Linq;
using System.Reflection;
using Glasswall.CloudSdk.AWS.Analyse.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Glasswall.CloudSdk.AWS.Analyse.Tests.AnalyseControllerTests.Signature
{
    [TestFixture]
    public class Attributes
    {
        [Test]
        public void Valid_Arguments_Should_Construct()
        {
            var attributes = typeof(AnalyseController).GetCustomAttributes().ToArray();

            Assert.That(attributes, Has.Exactly(2).Items);

            Assert.That(attributes,
                Has.Exactly(1)
                    .InstanceOf<RouteAttribute>()
                    .With
                    .Property(nameof(RouteAttribute.Template))
                    .EqualTo("api/[controller]"));

            Assert.That(attributes,
                Has.Exactly(1)
                    .InstanceOf<ControllerAttribute>());
        }
    }
}