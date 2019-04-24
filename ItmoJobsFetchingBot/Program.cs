using System;

namespace ItmoJobsFetchingBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TelegramHandler telegramBot = new TelegramHandler();
            telegramBot.InitBot();
            Console.ReadKey();
        }
    }
}
