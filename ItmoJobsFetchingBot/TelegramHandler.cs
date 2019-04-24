using System;
using System.Threading;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace ItmoJobsFetchingBot
{
    public class TelegramHandler
    {
        static private ItmoParser parser = new ItmoParser();
        static private TelegramBotClient ItmoBotClient = new TelegramBotClient(Configurations.AccessToken);
        public void InitBot()
        {
            ItmoBotClient.OnMessage += Bot_OnMessage;
            ItmoBotClient.StartReceiving();
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            string answerToUser = MessageHandling(e.Message.Text);
            await ItmoBotClient.SendTextMessageAsync(text: answerToUser, chatId: e.Message.Chat);
        }
        private static string MessageHandling(string userMessage) //Стоит рзбить на подфункции
        {
            string answerToUser = string.Empty;
            int commandArgument = TryParseCommandArgument(userMessage);
            if (commandArgument == -1)
            {
                return "Нет такой страницы :с";
            }
            else
            {
                userMessage = Regex.Match(userMessage, "/[a-zA-Z]+").Value;
            }
            switch (userMessage)
            {
                case "/start":
                    answerToUser = "Привет!\nВведи / или тыкни на такую же кнопку, чтобы увидеть команды";
                    break;

                case "/randpost":
                    answerToUser = Parsing(commandArgument);
                    break;

                case "/allpost":
                    answerToUser = ParsingAll(commandArgument);
                    break;

                 
                default:
                    answerToUser = "Нет такой команды :с\nПопробуй еще раз, чтобы увидеть команды, просто введи / " +
                        "или тыкни на такую же кнопку";
                    break;
            }
            return answerToUser;
        }
        
        private static int TryParseCommandArgument(string userMessage)
        {
            Match number = Regex.Match(userMessage, "\\d+");
            if (number == null)
                return -1;
            if (number.Value == "")
                return 1;
            int requestedPage = int.Parse(number.Value);
            if(requestedPage < 1)
                return -1;
            int pageInFact = parser.ParseItmoPagesCount();
            return requestedPage < pageInFact ? requestedPage : -1;
        }
        private static string Parsing(int pageNumber) 
        {
            ItmoJob job = parser.ParseItmoJobs(pageNumber)[0];
            return job.ToString();
        }
        private static string ParsingAll(int pageNumber)
        {
            List<ItmoJob> jobList = parser.ParseItmoJobs(pageNumber);
            string answer = string.Empty;
            foreach (var job in jobList)
            {
                answer += job.ToString() + "-------------------\n";
            }
            return answer;
        }
    }
}
