using Flurl.Http.Testing;
using Glasswall.CloudSdk.AWS.Analyse.Controllers;
using Glasswall.CloudSdk.Common;
using Glasswall.Core.Engine.Common.FileProcessing;
using Microsoft.Extensions.Logging;
using Moq;

namespace Glasswall.CloudSdk.AWS.Analyse.Tests.AnalyseControllerTests
{
    public class AnalyseControllerTestBase
    {
        /// <summary>
        /// For mocking URL to url analyse GET/PUT
        /// </summary>
        protected HttpTest HttpTest;

        protected const string DummyXml = "<xml></xml>";

        protected AnalyseController ClassInTest;
        protected Mock<IGlasswallVersionService> GlasswallVersionServiceMock;
        protected Mock<IFileTypeDetector> FileTypeDetectorMock;
        protected Mock<IFileAnalyser> FileAnalyserMock;
        protected Mock<IMetricService> MetricServiceMock;
        protected Mock<ILogger<AnalyseController>> LoggerMock;

        protected void CommonSetup()
        {
            GlasswallVersionServiceMock = new Mock<IGlasswallVersionService>();
            FileTypeDetectorMock = new Mock<IFileTypeDetector>();
            FileAnalyserMock = new Mock<IFileAnalyser>();
            MetricServiceMock = new Mock<IMetricService>();
            LoggerMock = new Mock<ILogger<AnalyseController>>();

            ClassInTest = new AnalyseController(
                GlasswallVersionServiceMock.Object,
                FileTypeDetectorMock.Object,
                FileAnalyserMock.Object,
                MetricServiceMock.Object,
                LoggerMock.Object
            );

            HttpTest = new HttpTest();
        }
    }
}