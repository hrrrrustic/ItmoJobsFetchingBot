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
                Answer ans = CommandHandling(command : splittedMessage[0], argument : splittedMessage[1]);
                await ItmoBotClient.SendTextMessageAsync(text: ans.TextAnswer, chatId: e.Message.Chat);
            }
            catch(Exception ex)
            {
                string info = DateTime.Now.ToLongTimeString() + "\r\n" +
                    ex.Message + "\r\n" + ex.TargetSite + "\r\n--------------------\r\n";
                File.AppendAllText("errorlist.txt", info);
            }
        }

        private static Answer CommandHandling(string command, string argument)
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
                    throw new ArgumentException();
            }
        }

        private static string[] SplitUserMessage(string userMessage)
        {
            string defaultArg = "1";
            userMessage = userMessage.Trim();
            userMessage = Regex.Replace(userMessage, @"\s+", " ");
            string[] split = userMessage.Split(' ');
            if (split.Length == 2)
            {
                return split;
            }
            else
            {
                return new string[] { split[0], defaultArg };
            }
        }

       
    }
}
