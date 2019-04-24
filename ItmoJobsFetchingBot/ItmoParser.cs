using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ItmoJobsFetchingBot
{
    public class ItmoParser
    {
        private static HtmlWeb web = new HtmlWeb();
        private static HtmlDocument htmlDoc;
        public List<ItmoJob> ParseItmoJobs(int pageNumber)
        {
            htmlDoc = web.Load(Configurations.StartAddress + $"page-{pageNumber}.html");
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//*[contains(@class,'jobs-item')]");
            List<ItmoJob> jobList = new List<ItmoJob>();
            foreach (var node in nodes)
            {
                string jobName = node.ChildNodes["h6"].InnerText;
                string companyName = node.ChildNodes["span"].InnerText.Split(',')[0];
                string address = node.ChildNodes["h6"].ChildNodes["a"].Attributes["href"].Value;
                string day = node.SelectSingleNode("//*[contains(@class, 'date')]").InnerText.Split(' ')[0];
                string month = node.SelectSingleNode("//*[contains(@class, 'date')]").ChildNodes["span"].InnerText;
                jobList.Add(new ItmoJob(jobName, companyName, address, day, month.ToLower()));
            }
            return jobList;
        }
        public int ParseItmoPagesCount()
        {
            htmlDoc = web.Load(Configurations.StartAddress);
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectSingleNode("//*[contains(@class,'pagination pull-right')]").ChildNodes;
            return nodes.Count - 2;
        }
    }
}
