namespace FinanceTracker.Application.Dto.ResponseModels
{
    public class ExportFileResultResponse
    {
        public byte[] FileBytes { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public string FileName { get; set; } = default!;
    }
}
