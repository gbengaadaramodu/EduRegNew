namespace EduReg.Models.Dto
{
    public class FileUploadErrorResponse
    {
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public byte[] Bytes { get; set; }
    }
}
