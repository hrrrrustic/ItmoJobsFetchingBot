using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Globalization;
using ItmoJobsFetchingBot.Helpers;
using ItmoJobsFetchingBot.Models;

namespace ItmoJobsFetchingBot
{
    public class ItmoParser
    {
        private static readonly HtmlWeb Web = new HtmlWeb();

        public List<ItmoJob> ParseItmoJobs(int pageNumber)
        {
            HtmlDocument htmlDoc = Web.Load(Configurations.StartReference + $"/catalog/page-{pageNumber}.html");

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
            HtmlDocument htmlDoc = Web.Load(Configurations.StartReference + "/catalog/");
            HtmlNodeCollection nodes = htmlDoc
                                            .DocumentNode
                                            .SelectSingleNode("//*[contains(@class,'pagination pull-right')]")
                                            .ChildNodes;
            return nodes.Count - 2; // Buttons > and <
        }

        private static ItmoJob NodeToItmoJob(string referenceToNode)
        {
            HtmlDocument jobPage = Web.Load(Configurations.StartReference + referenceToNode);

            HtmlNode rootNode = jobPage.DocumentNode;

            string companyName = rootNode
                                        .SelectSingleNode("//h1")
                                        .ChildNodes["small"]
                                        .InnerText;
            string jobName = rootNode
                                    .SelectSingleNode("//h1")
                                    .InnerText;

            HtmlNode updateDate = rootNode.SelectSingleNode("//*[contains(@class,'meta-left')]");

            DateTime date = DateTime.ParseExact(updateDate.InnerText.Split(':').Last(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            string experienceInfo = rootNode
                                            .SelectSingleNode("//*[contains(@class, 'list-unstyled')]")
                                            .ChildNodes[3]
                                            .InnerText;
            string salaryInfo = rootNode
                                        .SelectSingleNode("//*[contains(@class, 'list-unstyled')]")
                                        .ChildNodes[9]
                                        .InnerText;

            return new ItmoJob(jobName, companyName, referenceToNode, date, experienceInfo, salaryInfo);
        }
    }
}
