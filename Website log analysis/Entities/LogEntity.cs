using System;

namespace Website_log_analysis.Entities
{
    public class LogEntity
    {
        public string IP;
        public string HttpMethod;
        public string ResponseCode;
        private string _requestTime;



        public string BotName;
        public string BotVersion;
        
        public string RequestUrl;
        public string UserAgent;

        public string RequestTime {
            //get => Convert.ToDateTime(_requestTime).ToString("yyyy-MM-dd HH:mm:ss");
            get => _requestTime;
            set => _requestTime = value;
        }
    }
}
