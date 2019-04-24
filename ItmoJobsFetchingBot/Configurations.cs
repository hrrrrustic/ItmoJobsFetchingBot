using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MihaZupan;
namespace ItmoJobsFetchingBot
{
    public static class Configurations
    {
        public static readonly string AccessToken = "Token";
        public static HttpToSocks5Proxy Proxy;
        public const string StartAddress = "https://careers.itmo.ru/catalog/";
    }
}
