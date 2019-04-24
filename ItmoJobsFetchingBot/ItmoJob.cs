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
        public int MinSalary { get; set; } 

        public Tuple<int,int> Experience { get; set; }
        public ItmoJob(string jName, string cName, string EAdress, DateTime date, Tuple<int,int> exp, int salary)
        {
            JobName = jName;
            CompanyName = cName;
            EndAdress = EAdress;
            PublicationDate = date;
            Experience = exp;
            MinSalary = salary;
        }
        public override string ToString()
        {
            string message = string.Empty;
            message += this.Experience.ToString() + "\n";
            message += this.PublicationDate.ToString("dd MMMM") + "\n";
            message += this.JobName + "\n";
            message += this.CompanyName + "\n";
            message += this.MinSalary == 0 ? "з/п требуется уточнить" : MinSalary.ToString() + "руб";
            message += "\n";
            message += Configurations.StartReference + this.EndAdress + "\n";
            return message;
        }
    }
}
