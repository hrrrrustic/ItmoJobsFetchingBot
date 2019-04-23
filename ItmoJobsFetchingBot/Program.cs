using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItmoJobsFetchingBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TelegramHandler telegramBot = new TelegramHandler();
            telegramBot.InitBot();
        }
    }
}
