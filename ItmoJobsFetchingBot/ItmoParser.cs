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
                jobList.Add(NodeToItmoJob(node.ChildNodes["h6"].ChildNodes["a"].Attributes["href"].Value));
            }
            return jobList;
        }
        public int ParseItmoPagesCount()
        {
 
            htmlDoc = web.Load(Configurations.StartReference + "/catalog/");
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectSingleNode("//*[contains(@class,'pagination pull-right')]").ChildNodes;
            return nodes.Count - 2; // Buttons > and <
        }
        private static ItmoJob NodeToItmoJob(string referenceToNode)
        {
            HtmlDocument jobPage = web.Load(Configurations.StartReference + referenceToNode);
            HtmlNode rootNode = jobPage.DocumentNode;
            string companyName = rootNode.SelectSingleNode("//h1").ChildNodes["small"].InnerText;
            string jobName = rootNode.SelectSingleNode("//h1").InnerText;
            HtmlNode UpdateDate = rootNode.SelectSingleNode("//*[contains(@class,'meta-left')]");
            DateTime date = DateTime.ParseExact(UpdateDate.InnerText.Split(':').Last(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string experienceInfo = rootNode.SelectSingleNode("//*[contains(@class, 'list-unstyled')]").ChildNodes[3].InnerText;
            string salaryInfo = rootNode.SelectSingleNode("//*[contains(@class, 'list-unstyled')]").ChildNodes[9].InnerText;
            MatchCollection num = Regex.Matches(experienceInfo, "\\d+");
            return new ItmoJob(jobName, companyName, referenceToNode, date, experienceInfo, salaryInfo);
        }
    }
}
