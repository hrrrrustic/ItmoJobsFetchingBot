using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Text.RegularExpressions;
using System.IO;
using ItmoJobsFetchingBot.Helpers;
using ItmoJobsFetchingBot.Models;

namespace ItmoJobsFetchingBot
{
    public class TelegramHandler
    {
        private static readonly ItmoParser Parser = new ItmoParser();
        private static readonly TelegramBotClient ItmoBotClient = new TelegramBotClient(Configurations.AccessToken);

        public void InitBot()
        {
            ItmoBotClient.OnMessage += Bot_OnMessage;
            ItmoBotClient.StartReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                Answer answerToUser = UserMessageHandling(e.Message.Text);

                await ItmoBotClient.SendTextMessageAsync(text: answerToUser.TextAnswer, chatId: e.Message.Chat);
            }
            catch(Exception ex)
            {
                string info = DateTime.Now.ToLongTimeString() + "\r\n" +
                    ex.Message + "\r\n" + ex.TargetSite + "\r\n--------------------\r\n";
                File.AppendAllText("errorlist.txt", info);
            }
        }

        private static Answer UserMessageHandling(string userMessage) //Стоит рзбить на подфункции
        {
            int intArg;
            Answer userAnswer = new Answer();
            bool anyArgs = TryParseArgument(userMessage, out string[] splittedMessage);

            //TODO:1 Очень странное решение. Не лучше ли сделать свитч по первому слову, а потом передавать массив как аргумент?
            if (anyArgs)
            {
                bool isItIntArg = int.TryParse(splittedMessage[1], out intArg);
                if (isItIntArg)
                {
                   userAnswer.TextAnswer = CommandHandling(splittedMessage[0], intArg);
                }
                else
                {
                    userAnswer.TextAnswer = CommandHandling(splittedMessage[0], splittedMessage[1]);
                }
            }
            else
            {
                intArg = 1;
                userAnswer.TextAnswer = CommandHandling(userMessage, intArg);
            }
            return userAnswer;
        }

        private static string CommandHandling(string command, int pageCount)
        {
            switch (command)
            {
                case ("/randpost"):
                    return ParsingOne(pageCount);
                case ("/allpost"):
                    return ParsingAll(pageCount);
                default:
                    return "Нет такой команды или это команда принимает числовой аргумент: с";
            }
        }
        private static string CommandHandling(string command, string stringToSearch) //Сам поиск пока не делал
        {
            if(command == "/search") // если будут еще команды с текстовым аргументом, то поменяю на свитч
            {
                return Search();
            }
            else
            {
                return "Нет такой команды или это команда принимает числовой аргумент :с";
            }
        }

        private static bool TryParseArgument(string userMessage, out string[] splittedMessage)
        {
            userMessage = userMessage.Trim();
            userMessage = Regex.Replace(userMessage, @"\s+", " ");
            string[] splittedUserMessage = userMessage.Split(' ');
            if (splittedUserMessage.Length == 2)
            {
                splittedMessage = splittedUserMessage;
                return true;
            }
            else
            {
                splittedMessage = null;
                return false;
            }
        }

        //TODO:2 Команды, кмк, стоит вынести в отдельный класс, чтобы они не сливались с логикой самого бота, который должен
        // работать на уровне: прочитал - вызвал метода - ответил
        private static string Search() //Для поиска в будущем
        {
            return "";
        }

        private static string ParsingOne(int pageNumber) 
        {
            ItmoJob job = Parser.ParseItmoJobs(pageNumber).RandomItem();
            return job.ToString();
        }
        private static string ParsingAll(int pageNumber)
        {
            List<ItmoJob> jobList = Parser.ParseItmoJobs(pageNumber);
            string answer = string.Empty;
            foreach (var job in jobList)
            {
                answer += job.ToString() + "--------------------------------------\n";
            }
            return answer;
        }
    }
}
