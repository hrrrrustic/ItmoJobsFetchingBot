using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using HtmlAgilityPack;

namespace ItmoJobsFetchingBot
{
    public class TelegramHandler
    {
        static TelegramBotClient ItmoBotClient = new TelegramBotClient(Configurations.AccessToken); // Хз пока что с этим сделать
        public void InitBot()
        {
            ItmoBotClient.OnMessage += Bot_OnMessage;
            ItmoBotClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }
        public static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            string answerToUser = MessageHandling(e.Message.Text);
            await ItmoBotClient.SendTextMessageAsync(text : answerToUser, chatId : e.Message.Chat);
        }   
        private static string GetMessageFromParsedData(HtmlNode nodeToMessage)
        {
            string message = string.Empty;
            message += nodeToMessage.ChildNodes["h6"].InnerText + "\n"; 
            message += nodeToMessage.ChildNodes["span"].InnerText.Split(',')[0] + "\n"; // Наверное не лучшая идея сплитить по запятой, там просто 
            message += "https://careers.itmo.ru";                                       // _название компании_ , _город_
            message += nodeToMessage.ChildNodes["h6"].ChildNodes["a"].Attributes["href"].Value;
            return message;
        }
        private static string GetMessageFromParsedData(HtmlNodeCollection nodesToMessage)
        {
            string message = string.Empty;
            foreach (var node in nodesToMessage) //Возможно все-таки стоит заморочиться со стрингбилдером
            {
                message += GetMessageFromParsedData(node) + "\n ------------------------\n";
            }
            return message;
        }
        private static string MessageHandling(string Usermessage)
        {
            string answerToUser = string.Empty;
            switch (Usermessage)
            {
                case "/start":
                    answerToUser = "Привет!\nВведи /, чтобы увидеть команды";
                    break;

                case "/randpost":
                    answerToUser = Parsing(false);
                    break;

                case "/allpost": //Вряд ли имеет какой-то толк, прост сделал, мб можно будет выводить фулл страницу конкретную
                    answerToUser = Parsing(true);
                    break;

                default:
                    answerToUser = "Нет такой команды :с\nПопробуй еще раз, чтобы увидеть команды, просто введи /";
                    break;
            }
            return answerToUser;
        }
        private static string Parsing(bool needAll) // От була надо будет уйти, пока не придумал как лучше сделать
        {
            ItmoParser parser = new ItmoParser();
            HtmlNodeCollection foundNodes = parser.ParseItmoJobs(); // Надо будет добавить ссылку на номер страницы в аргументы
            if(needAll)
            {
                return GetMessageFromParsedData(foundNodes);
            }
            else
            {
                return GetMessageFromParsedData(foundNodes[new Random().Next(0, foundNodes.Count)]);
            }
        }
    }
}
