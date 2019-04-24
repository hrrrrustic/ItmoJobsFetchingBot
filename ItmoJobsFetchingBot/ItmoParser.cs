using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ItmoJobsFetchingBot
{
    public class ItmoParser
    {
        private static HtmlWeb web = new HtmlWeb();
        private static HtmlDocument htmlDoc;
        public List<ItmoJob> ParseItmoJobs(int pageNumber)
        {
            htmlDoc = web.Load(Configurations.StartReference + $"/catalog/page-{pageNumber}.html");
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//*[contains(@class,'jobs-item')]");
            List<ItmoJob> jobList = new List<ItmoJob>();
            foreach (var node in nodes)
            {
                jobList.Add(NodeToItmoJob(node));
            }
            return jobList;
        }
        public int ParseItmoPagesCount()
        {
 
            htmlDoc = web.Load(Configurations.StartReference + "/catalog/");
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectSingleNode("//*[contains(@class,'pagination pull-right')]").ChildNodes;
            return nodes.Count - 2; // Buttons > and <
        }
        private static ItmoJob NodeToItmoJob(HtmlNode node)
        {
            string jobName = node.ChildNodes["h6"].InnerText;
            string companyName = node.ChildNodes["span"].InnerText.Split(',').First();
            string endReference = node.ChildNodes["h6"].ChildNodes["a"].Attributes["href"].Value;

            HtmlDocument jobPage = web.Load(Configurations.StartReference + endReference);
            HtmlNode UpdateDate = jobPage.DocumentNode.SelectSingleNode("//*[contains(@class,'meta-left')]");
            DateTime date = DateTime.ParseExact(UpdateDate.InnerText.Split(':').Last(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var exp = node.ChildNodes["p"].ChildNodes["br"].InnerText;
            MatchCollection num = Regex.Matches(exp, "\\d+");
            int zp = 0;
            if (num.Count > 2)
                zp = int.Parse(num[2].Value);
            Tuple<int, int> experience = (int.Parse(num[0].Value), int.Parse(num[1].Value)).ToTuple();
            return new ItmoJob(jobName, companyName, endReference, date, experience, zp);
        }
    }
}
