using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItmoJobsFetchingBot.Helpers;
using ItmoJobsFetchingBot.Models;

namespace ItmoJobsFetchingBot
{

    public class Commands
    {
        private static readonly ItmoParser Parser = new ItmoParser();
        public Answer Search(string argument)
        {

            return new Answer();
        }
        public Answer RandPost(string argument)
        {

            return new Answer();
        }
        public Answer AllPost(string argument)
        {

            return new Answer();
        }
        private static string ParsingOne(int pageNumber)
        {
            ItmoJob job = Parser.ParseItmoJobs(pageNumber).RandomItem();
            return job.ToString();
        }
        private static string ParsingAll(int pageNumber)
        {
            List<ItmoJob> jobList = Parser.ParseItmoJobs(pageNumber);
            string answer = string.Empty;
            foreach (var job in jobList)
            {
                answer += job.ToString() + "--------------------------------------\n";
            }
            return answer;
        }
    }
}
