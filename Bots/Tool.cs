using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Bots
{
    public interface ITool
    {
        void Apply(Computer bot);
    }

    public class ToolFactory
    {
        public static ITool FromKey(string key)
        {
            switch(key)
            {
                case Pickaxe.Key: return new Pickaxe();
            }

            return null;
        }
    }

    public class Pickaxe : ITool
    {
        public const string Key = "minecraft:diamond_pickaxe";

        public void Apply(Computer bot)
        {
            bot.Mining = true;
        }
    }
}
