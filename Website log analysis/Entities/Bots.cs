using System;
using System.Collections.Generic;
using System.Text;

namespace Website_log_analysis.Entities
{
    public class Bots
    {
        public string Code;
        public string Name;
        public List<Bots> GetBots()
        {
            return new List<Bots>() {
                   new Bots(){ Code="BaiduSpider",Name = "百度" },
                   new Bots(){ Code="Googlebot",Name = "谷歌" },
                   new Bots(){ Code="360Spider",Name = "360" },
                   new Bots(){ Code="Sogou",Name = "搜狗" },
                   new Bots(){ Code="bingbot",Name = "必应" },
                   new Bots(){ Code="Sosospider",Name = "SOSO蜘蛛" },
                   new Bots(){ Code="Slurp",Name = "雅虎蜘蛛" },
                   new Bots(){ Code="msnbot",Name = "MSN蜘蛛" },
                   new Bots(){ Code="bingbot",Name = "微软Bing" },
                   new Bots(){ Code="YoudaoBot",Name = "有道" },
                   new Bots(){ Code="EtaoSpider",Name = "一淘网" },
                   new Bots(){ Code="JikeSpider",Name = "即刻蜘蛛" },
                   new Bots(){ Code="EasouSpider",Name = "宜搜蜘蛛" },
                   new Bots(){ Code="YisouSpider",Name = "一搜蜘蛛" },
                   new Bots(){ Code="AhrefsBot",Name = "Ahrefs蜘蛛" },
                   new Bots(){ Code="DoCoMo",Name = "谷歌日本" },
                   new Bots(){ Code="YandexBot",Name = "俄罗斯" },
                   new Bots(){ Code="ia_archiver",Name = "Alexa蜘蛛" },
            };
        }
    }
}
