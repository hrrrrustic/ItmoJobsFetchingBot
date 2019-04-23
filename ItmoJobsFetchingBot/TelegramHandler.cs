using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ItmoJobsFetchingBot
{
    public class TelegramHandler
    {
        static TelegramBotClient ItmoBotClient = new TelegramBotClient(Configurations.AccessToken);
        public void InitBot()
        {
            ItmoBotClient.OnMessage += Bot_OnMessage;
            ItmoBotClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
        public static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

        }
    }
}
