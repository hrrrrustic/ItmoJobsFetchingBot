using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using HtmlAgilityPack;

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
            if(e.Message.Text == "/randpost")
            {
                ItmoParser parser = new ItmoParser();
                HtmlNodeCollection foundNodes = parser.ParseItmoJobs();
                string answer = GetMessageFromParsedData(foundNodes);

                await ItmoBotClient.SendTextMessageAsync(text : answer, chatId : e.Message.Chat);
            }
        }
        public static string GetMessageFromParsedData(HtmlNode nodeToMessage)
        {
            string message = "";
            return message;
        }
        public static string GetMessageFromParsedData(HtmlNodeCollection nodesToMessage)
        {
            string message = "";
            return message;
        }
    }
}
