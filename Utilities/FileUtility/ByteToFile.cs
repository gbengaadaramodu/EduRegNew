namespace EduReg.Utilities.FileUtility
{
    public class ByteToFile
    {
        public static void SaveByteArrayToFile(byte[] data, string filePath)
        {
            File.WriteAllBytes(filePath, data);
        }
    }
}
