using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Website_log_analysis.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Website_log_analysis
{
    class Program
    {
        static string reportName = "";
        static List<string> bots = null;
        static void Main(string[] args)
        {
            bots = new Bots().GetBots().Select(x => x.Code).ToList();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("  **********网站日志分析工具**********");
            Console.WriteLine("          目前仅仅支持Nginx日志");
            Console.WriteLine("");
            Console.WriteLine("请输入日志文件目录：");

            var logFile = Console.ReadLine();

            var beforTime = DateTime.Now;
            Console.Write("开始：{0}", beforTime);
            Console.WriteLine("正在生成日志目录......");
            var reportDirectory = AppDomain.CurrentDomain.BaseDirectory + @"reports";
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }
            reportName = reportDirectory + @"\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".html";
            Console.WriteLine("正在生成分析报表......");
            GenerateFile(reportName);

            ProcessingLogContent(logFile);

            utils.appendEndTemplate(reportName);

            Console.WriteLine("报表地址：{0}", reportName);

            
            

            var afterTime = DateTime.Now;
            var ts = afterTime.Subtract(beforTime);
            Console.Write("分析结束：{0},总耗时：{1}毫秒", afterTime, ts.TotalMilliseconds);
            Console.WriteLine("按下任意键结束");
            Console.ReadKey();
        }

        private static void GenerateFile(string reportName)
        {
            if (File.Exists(reportName))
            {
                return;
            }
            

            using (var fileStream = new FileStream(reportName, FileMode.CreateNew, FileAccess.ReadWrite))
            {
                string content = utils.GetTemplateStart();
                byte[] data = Encoding.Default.GetBytes(content);
                fileStream.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 读取日志文件内容
        /// </summary>
        /// <param name="logFile"></param>
        /// <returns></returns>
        private static void ProcessingLogContent(string logFile)
        {
            string strLine = string.Empty ;
            int lineCount = 0;
            if (!File.Exists(logFile)) 
            {
                throw new Exception("文件未找到！！");
            }

            using (StreamReader sr = new StreamReader(logFile))
            {
                strLine = sr.ReadLine();
                while (!string.IsNullOrWhiteSpace(strLine))
                {
                    strLine = strLine.Trim();
                    ProcessingLogRow(strLine);

                    strLine = sr.ReadLine();
                    lineCount++;
                    
                }
            }
        }

        //处理日志行
        private static void ProcessingLogRow(string strLine)
        {
            if (string.IsNullOrWhiteSpace(strLine))
            {
                return;
            }

            strLine = strLine.Trim();

            var logInfo = new LogEntity()
            {
                IP = Regex.Match(strLine, @"((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))").Value,
                RequestTime = Regex.Match(strLine, @"(?<=\[)\d+\/\w+\/\d+:\d+:\d+:\d+").Value,
                HttpMethod = Regex.Match(strLine, @"(GET|HEAD|POST|PUT|PATCH|DELETE|OPTIONS|TRACE)").Value,
                RequestUrl = Regex.Match(strLine, @"(?<=(GET|HEAD|POST|PUT|PATCH|DELETE|OPTIONS|TRACE) ).*(?=\ HTTP/)").Value,
                ResponseCode = Regex.Match(strLine, @"(?<=HTTP/\d+.\d+.\s)\d*(?=\s\d+\s)").Value,
                //BotName = Regex.Match(strLine, @"(?<=compatible; ).*(?=/\d.\d)").Value,
                BotName = Regex.Match(strLine, @"("+ string.Join('|',bots) +")").Value,
                //BotVersion = Regex.Match(strLine, @"(?<=compatible;.*/).*(?=;)").Value,
                //UserAgent = Regex.Match(strLine, @"(?<=\s\S-\S\s\S).*/\d+.\d+").Value
            };

            string rowTemplate = @"
            <tr>
                <td>{IP}</td>
                <td>{HttpMethod}</td>
                <td>{ResponseCode}</td>
                <td>{RequestTime}</td>
                <td>{BotName}</td>
                <td>{RequestUrl}</td>
            </tr>";
            string rowContent = rowTemplate
                .Replace("{IP}", logInfo.IP)
                .Replace("{HttpMethod}", logInfo.HttpMethod)
                .Replace("{ResponseCode}", logInfo.ResponseCode)
                .Replace("{RequestTime}", logInfo.RequestTime)
                .Replace("{BotName}", logInfo.BotName)
                .Replace("{BotVersion}", logInfo.BotVersion)
                .Replace("{RequestUrl}", logInfo.RequestUrl)
               ;
            utils.AppendToFile(reportName, rowContent);
        }
    }
}
