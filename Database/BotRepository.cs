using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Database
{
    public class BotRepository
    {
        private List<Bot> Bots { get; }
        private string connectionString = "server=localhost;database=Minecraft;Integrated Security=true";

        public BotRepository()
        {
            Bots = new List<Bot>();
        }

        public string Add()
        {
            var bot = new Bot();
            Bots.Add(bot);

            return bot.Identifier;
        }

        public Bot Get(string id)
        {
            var bot = Bots.Where(b => b.Identifier == id).SingleOrDefault();

            if (bot == null)
            {
                bot = new Bot(id);
                Bots.Add(bot);
            }

            return bot;
        }
    }
}
