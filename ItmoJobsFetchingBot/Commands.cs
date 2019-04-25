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
        public string Search(string argument)
        {

            return "";
        }
        public string RandPost(string argument)
        {
            int pageNumber = CheckValidArg(argument);
            return Parser.ParseItmoJobs(pageNumber).RandomItem().ToString();
        }
        public string AllPost(string argument)
        {
            int pageNumber = CheckValidArg(argument);
            List<ItmoJob> jobList = Parser.ParseItmoJobs(pageNumber);
            string allJobsInOneMessage = string.Empty;
            foreach (ItmoJob job in jobList)
            {
                allJobsInOneMessage += job.ToString();
            }
            return allJobsInOneMessage;
        }
        private static int CheckValidArg(string argument)
        {
            bool IsIstInt = int.TryParse(argument, out int pageNumber);
            if (IsIstInt)
            {
                if (pageNumber < 1 || pageNumber > Parser.ParseItmoPagesCount())
                {
                    throw new WrongArgumentException("Такая страница не существует");
                }
                return pageNumber;
            }
            else
            {
                throw new WrongArgumentException("Неверный формат аргумента");
            }
        }
    }
}
