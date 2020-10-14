using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace OhMyFollowBot
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "1014671579:AAFN84JsIt-CNeFcUoRNBIYciS2H1_2CAkY"; // Токен от отца ботов
            Dictionary<long, List<string>> msgs = new Dictionary<long, List<string>>(); // хранение сообщений для статистики

            TelegramBotClient bot = new TelegramBotClient(token); // создание бота и передача ему токена



            bot.OnMessage += (q, w) => // обработка сообщений
            {
                  if (w.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text) return; // если пишут 
                  //не текстовое сообщение, то ничего не делаем

                  var fromId = w.Message.From.Id; //определение пользователя
                  var username = w.Message.From.FirstName; //
                  var id = w.Message.Chat.Id; // определение чата
                  var msg = w.Message.Text; // получение сообщений


                  if (!msgs.ContainsKey(fromId)) msgs.Add(fromId, new List<string>()); //проверка наличия в словаре
                  //если нет, то сформировать


                  if (msg.ToLower().IndexOf("/getmystat") != -1) //если текст существует.
                  {
                        bot.SendTextMessageAsync(id, $"{username}: отправил сообщений{msgs[fromId].Count}"); 
                        string maxMsg = string.Empty; //присылает самое большое сообщение

                        foreach (var e in msgs[fromId]) if (e.Length > maxMsg.Length) maxMsg = e; 

                    bot.SendTextMessageAsync(id, //
                        String.Format("{0}: отправил сообщений {1}\nСамое большое сообщение: {2}",
                        username,
                        msgs[fromId].Count,
                        maxMsg
                        ));


                  }               


                  else msgs[fromId].Add(msg);
            };

            bot.StartReceiving(); // старт бота
            Console.ReadLine();
        }
    }
}
