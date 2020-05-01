using System;
using Glasswall.CloudSdk.Common.Web.Models;
using NUnit.Framework;

namespace Glasswall.CloudSdk.AWS.Analyse.Tests.AnalyseControllerTests.AnalyseFromBase64Method
{
    [TestFixture]
    public class WhenEngineThrows : AnalyseControllerTestBase
    {
        private Exception _dummyException;

        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            CommonSetup();
            
            GlasswallVersionServiceMock.Setup(s => s.GetVersion())
                .Throws(_dummyException = new Exception());
        }

        [Test]
        public void Exception_Is_Rethrown()
        {
            Assert.That(() => ClassInTest.AnalyseFromBase64(new Base64Request
            {
                Base64 = "dGVzdA=="
            }), Throws.Exception.EqualTo(_dummyException));
        }
    }
}