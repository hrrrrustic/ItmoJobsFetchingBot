using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItmoJobsFetchingBot
{
    public class ItmoJob
    {
        public string JobName { get; set; }
        public string CompanyName { get; set; }
        public string EndAdress { get; set; }

        public DateTime PublicationDate;

        public static Dictionary<string, int> months = new Dictionary<string, int>()
        {
            {"янв", 1},
            {"фев" ,2},
            {"мар", 3},
            {"апр", 4},
            {"май", 5},
            {"июн", 6},
            {"июл", 7},
            {"авг", 8},
            {"сен", 9},
            {"окт", 10},
            {"ноя", 11},
            {"дек", 12},
        };
        public Tuple<int,int> Experience { get; set; }
        public ItmoJob(string jName, string cName, string EAdress, string pDay, string pMonth, Tuple<int,int> exp)
        {
            JobName = jName;
            CompanyName = cName;
            EndAdress = EAdress;
            PublicationDate = new DateTime(DateTime.Now.Year, months[pMonth], int.Parse(pDay));
            Experience = exp;
        }
        public override string ToString()
        {
            string message = string.Empty;
            message += this.Experience.ToString() + "\n";
            message += this.PublicationDate.ToString("dd MMMM") + "\n";
            message += this.JobName + "\n";
            message += this.CompanyName + "\n";
            message += Configurations.StartAddress + this.EndAdress + "\n";
            return message;
        }
    }
}
