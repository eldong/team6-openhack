using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace InternalBusinessUsersBackend
{
    public static class TemplateUtils
    {

        static string Template
        {
            get
            {
                var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var rootDirectory = Path.GetFullPath(Path.Combine(binDirectory, ".."));

                ///then you can read the file as you would expect yew!
                return File.ReadAllText(rootDirectory + "/MailTemplate/MailTemplate.txt");
            }
        }

        public static string InsertProductsInTemplate(List<Product> products)
        {
            var sb = new StringBuilder();
            foreach (var product in products)
            {
                sb.AppendLine($"<tr><td>{product.ProductName}</td><td>{product.ProductDescription}</td><td>{product.ProductId}</td></tr>");
            }
            return string.Format(Template, sb.ToString());
        }

    }
        
}
