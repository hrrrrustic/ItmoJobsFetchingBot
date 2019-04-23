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
        public HtmlNodeCollection ParseItmoJobs() // Будет кушать ссылка на номер страницы
        {
            string htmlAdress = @"https://careers.itmo.ru/catalog/"; 
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(htmlAdress);
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//*[contains(@class,'jobs-item')]");
            return nodes;
        }
    }
}
