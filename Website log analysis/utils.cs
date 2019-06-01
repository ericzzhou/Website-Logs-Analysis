using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Website_log_analysis.Properties;

namespace Website_log_analysis
{
    public class utils
    {
        public static string GetTemplateStart()
        {
            return Resources.startTemplate;
        }
        public static string GetTemplateEnd()
        {
            return Resources.endTemplate;
        }

        public static void appendEndTemplate(string filepath)
        {
            AppendToFile(filepath, Resources.endTemplate);
        }

        public static void AppendToFile(string filepath,string content)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Append))
            {
                byte[] data = Encoding.Default.GetBytes(content);
                fileStream.Write(data, 0, data.Length);
            }
        }
    }
}
