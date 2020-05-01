using Glasswall.Core.Engine.Common.PolicyConfig;

namespace Glasswall.Core.Engine.Common.FileProcessing
{
    public interface IFileProtector
    {
        //Task<byte[]> GetReportAsync(
        //    ContentManagementFlags contentManagementFlags,
        //    string fileType, 
        //    IByteReader byteReader,
        //    CancellationToken cancellationToken);
        IFileProtectResponse GetReport(ContentManagementFlags contentManagementFlags, string fileType, byte[] fileBytes);
    }
}