using System;

namespace CrossUtils
{
    public static class OrdersProcessing
    {
        public static string BatchId(string fileName)
        {
            return fileName.Substring(0, 14);
        }
    }
}
