using System;
using Glasswall.CloudSdk.AWS.Analyse.Controllers;
using Glasswall.CloudSdk.Common;
using Glasswall.Core.Engine.Common.FileProcessing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Glasswall.CloudSdk.AWS.Analyse.Tests.AnalyseControllerTests.Signature
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void Valid_Arguments_Should_Construct()
        {
            var controller = new AnalyseController(
                Mock.Of<IGlasswallVersionService>(),
                Mock.Of<IFileTypeDetector>(),
                Mock.Of<IFileAnalyser>(),
                Mock.Of<IMetricService>(),
                Mock.Of<ILogger<AnalyseController>>());

            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void Null_VersionService_Should_Throw()
        {
            Assert.That(() => new AnalyseController(
                    null,
                    Mock.Of<IFileTypeDetector>(),
                    Mock.Of<IFileAnalyser>(),
                    Mock.Of<IMetricService>(),
                    Mock.Of<ILogger<AnalyseController>>()),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName))
                    .EqualTo("glasswallVersionService"));
        }

        [Test]
        public void Null_Detector_Should_Throw()
        {
            Assert.That(() => new AnalyseController(
                    Mock.Of<IGlasswallVersionService>(),
                    null,
                    Mock.Of<IFileAnalyser>(),
                    Mock.Of<IMetricService>(),
                    Mock.Of<ILogger<AnalyseController>>()),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName))
                    .EqualTo("fileTypeDetector"));
        }

        [Test]
        public void Null_Analyser_Should_Throw()
        {
            Assert.That(() => new AnalyseController(
                    Mock.Of<IGlasswallVersionService>(),
                    Mock.Of<IFileTypeDetector>(),
                    null,
                    Mock.Of<IMetricService>(),
                    Mock.Of<ILogger<AnalyseController>>()),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName))
                    .EqualTo("fileAnalyser"));
        }

        [Test]
        public void Null_MetricService_Should_Throw()
        {
            Assert.That(() => new AnalyseController(
                    Mock.Of<IGlasswallVersionService>(),
                    Mock.Of<IFileTypeDetector>(),
                    Mock.Of<IFileAnalyser>(),
                    null,
                    Mock.Of<ILogger<AnalyseController>>()),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName))
                    .EqualTo("metricService"));
        }

        [Test]
        public void Null_Logger_Should_Throw()
        {
            Assert.That(() => new AnalyseController(
                    Mock.Of<IGlasswallVersionService>(),
                    Mock.Of<IFileTypeDetector>(),
                    Mock.Of<IFileAnalyser>(),
                    Mock.Of<IMetricService>(),
                    null),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName))
                    .EqualTo("logger"));
        }
    }
}