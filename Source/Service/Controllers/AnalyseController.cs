using System;
using System.Diagnostics;
using Glasswall.CloudSdk.Common.Web.Abstraction;
using Glasswall.CloudSdk.Common.Web.Models;
using Glasswall.Core.Engine.Common.FileProcessing;
using Glasswall.Core.Engine.Common.PolicyConfig;
using Glasswall.Core.Engine.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Glasswall.CloudSdk.AWS.Analyse.Controllers
{
    public class AnalyseController : CloudSdkController<AnalyseController>
    {
        private readonly IFileTypeDetector _fileTypeDetector;
        private readonly IFileAnalyser _fileAnalyser;

        public AnalyseController(
            IFileTypeDetector fileTypeDetector,
            IFileAnalyser fileAnalyser,
            ILogger<AnalyseController> logger) : base(logger)
        {
            _fileTypeDetector = fileTypeDetector ?? throw new ArgumentNullException(nameof(fileTypeDetector));
            _fileAnalyser = fileAnalyser ?? throw new ArgumentNullException(nameof(fileAnalyser));
        }

        [HttpPost("base64")]
        public IActionResult AnalyseFromBase64([FromBody]Base64Request request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Logger.LogInformation("{0} method invoked", nameof(AnalyseFromBase64));
                
                return TryGetBase64File(request.Base64, out var bytes) 
                    ? AnalyseFromBytes(request.FileName, bytes, request.ContentManagementFlags)
                    : new BadRequestObjectResult("Could not parse base 64 file.");
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Exception occured processing file: {e.Message}");
                throw;
            }
        }

        [HttpPost("sas")]
        public IActionResult AnalyseFromSas([FromBody] SasRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                Logger.LogInformation("{0} method invoked", nameof(AnalyseFromSas));

                var fileName = GetFileNameFromUrl(request.SasUrl.AbsolutePath);

                return TryGetFile(request.SasUrl, out var bytes)
                    ? AnalyseFromBytes(fileName, bytes, request.ContentManagementFlags)
                    : new BadRequestObjectResult($"Could not download file from SAS: {request.SasUrl}.");
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Exception occured processing file: {e.Message}");
                throw;
            }
        }

        private IActionResult AnalyseFromBytes(string fileName, byte[] bytes, ContentManagementFlags contentManagementFlags)
        {
            contentManagementFlags = contentManagementFlags.ValidatedOrDefault();
            var fileType = _fileTypeDetector.DetermineFileType(bytes);

            if (fileType.FileType == FileType.Unknown)
            {
                Logger.LogInformation("Unknown file type detected.");
                return new UnprocessableEntityObjectResult("File could not be determined to be a supported file");
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = _fileAnalyser.GetReport(contentManagementFlags, fileType.FileTypeName, bytes);

            stopwatch.Stop();
            Logger.Log(LogLevel.Information, $"File '{fileName}' GetReport call took {stopwatch.Elapsed:c}");

            return new OkObjectResult(response);
        }
    }
}