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
        private static readonly Commands commandHandler = new Commands();
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
                string[] splittedMessage = SplitUserMessage(e.Message.Text);
                string answerToUser = CommandHandling(command : splittedMessage[0], argument : splittedMessage[1]);
                await ItmoBotClient.SendTextMessageAsync(text: answerToUser, chatId: e.Message.Chat);
            }
            catch(Exception ex)
            {
                string info = DateTime.Now.ToLongTimeString() + "\r\n" +
                    ex.Message + "\r\n" + ex.TargetSite + "\r\n--------------------\r\n";
                File.AppendAllText("errorlist.txt", info);
            }
        }

        private static string CommandHandling(string command, string argument)
        {
            switch (command)
            {
                case "/search":

                    return commandHandler.Search(argument);

                case "/randpost":

                    return commandHandler.RandPost(argument);

                case "/allpost":

                    return commandHandler.AllPost(argument);

                default:
                    throw new WrongCommandException("Нет такой команды");
            }
        }

        private static string[] SplitUserMessage(string userMessage)
        {
            string defaultArg = "1";
            userMessage = userMessage.Trim();
            userMessage = Regex.Replace(userMessage, @"\s+", " ");
            string[] splittedMessage = userMessage.Split(' ');
            if (splittedMessage.Length == 2)
            {
                return splittedMessage;
            }
            else
            {
                return new string[] { splittedMessage[0], defaultArg };
            }
        }

       
    }
}
