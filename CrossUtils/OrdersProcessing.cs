using System;

namespace CrossUtils
{
    public static class OrdersProcessing
    {
        public static string BatchIdFromFileName(string fileName)
        {
            return fileName.Substring(0, 14);
        }

        public static string FileTypeFromFileName(string fileName)
        {
            var fileTypeWithExtension = fileName[14..];
            return fileTypeWithExtension[0..^3];
        }

        public static string FileNameFromUrl(string storageUrl)
        {
            var splittedUrl = storageUrl.Split("/");
            return splittedUrl[splittedUrl.Length - 1];
        }
    }
}
