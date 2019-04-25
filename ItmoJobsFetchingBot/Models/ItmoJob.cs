using System;
using System.Text;
using ItmoJobsFetchingBot.Helpers;

namespace ItmoJobsFetchingBot.Models
{
    public class ItmoJob
    {
        public string JobName { get; set; }
        public string CompanyName { get; set; }
        public string EndReference { get; set; }
        public DateTime PublicationDate;
        public string SalaryInfo { get; set; } 
        public string Experience { get; set; }
            
        public ItmoJob(string jobName, string companyName, string endAdress, DateTime publicationDate, string expDescription, string salaryDescription)
        {
            JobName = jobName;
            CompanyName = companyName;
            EndReference = endAdress;
            PublicationDate = publicationDate;
            Experience = expDescription;
            SalaryInfo = salaryDescription;
        }
        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.Append(PublicationDate.ToString("dd'/'MM'/'yyyy") + "\n");
            message.Append(JobName + "\n");
            message.Append("Компания : " + CompanyName + "\n");
            message.Append(Experience.ToString() + "\n");
            message.Append(SalaryInfo + "\n");
            message.Append(Configurations.StartReference + EndReference + "\n");
            return message.ToString();
        }
    }
}
