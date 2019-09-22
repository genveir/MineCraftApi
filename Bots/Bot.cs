using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft
{
    public class Bot
    {
        public static int BotId = 0;

        public string Identifier;

        public Position Position;

        public Bot()
        {
            Identifier = "Bot" + BotId++;
        }

        public Bot(string id)
        {
            Identifier = id;
        }
    }
}
