using System;
using System.Text;
using ItmoJobsFetchingBot.Helpers;

namespace ItmoJobsFetchingBot.Models
{
    public class ItmoJob
    {
        public string JobName { get; set; }
        public string CompanyName { get; set; }
        public string EndAdress { get; set; }
        public DateTime PublicationDate;
        public string SalaryInfo { get; set; } 
        public string Experience { get; set; }

        //TODO:3 Буквы верни
        public ItmoJob(string jName, string cName, string EAdress, DateTime date, string exp, string salary)
        {
            JobName = jName;
            CompanyName = cName;
            EndAdress = EAdress;
            PublicationDate = date;
            Experience = exp;
            SalaryInfo = salary;
        }
        public override string ToString()
        {
            //TODO:4 А зачем this?
            StringBuilder message = new StringBuilder();
            message.Append(this.PublicationDate.ToString("dd'/'MM'/'yyyy") + "\n");
            message.Append(this.JobName + "\n");
            message.Append("Компания : " + this.CompanyName + "\n");
            message.Append(this.Experience.ToString() + "\n");
            message.Append(this.SalaryInfo + "\n");
            message.Append(Configurations.StartReference + this.EndAdress + "\n");
            return message.ToString();
        }
    }
}
