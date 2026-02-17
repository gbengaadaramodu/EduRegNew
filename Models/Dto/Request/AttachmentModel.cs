namespace EduReg.Models.Dto.Request
{
    public class AttachmentModel
    {
        public string FileName { get; set; }
        public string Format { get; set; }
        public byte[] Content { get; set; }
    }
}
