using System;
using System.Collections.Generic;
using System.Linq;
using ItmoJobsFetchingBot.Models;

namespace ItmoJobsFetchingBot.Helpers
{
    public static class Tools
    {
        private static Random rand = new Random();

        public static ItmoJob RandomItem(this ICollection<ItmoJob> collection)
        {
            int randNumber = rand.Next(0, collection.Count);
            return collection.ElementAt(randNumber);
        }
    }
}
