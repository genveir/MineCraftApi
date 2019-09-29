using MineCraft.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Lua
{
    public class Resources
    {
        private IEnumerable<string> resources;

        public Resources()
        {
            resources = typeof(Packager).Assembly.GetManifestResourceNames();
        }

        public IEnumerable<string> GetBase()
        {
            return resources.Where(name => name.StartsWith("MineCraft.Lua.base"));
        }

        public IEnumerable<string> GetGeneral()
        {
            return resources.Where(name => name.StartsWith("MineCraft.Lua.general"));
        }

        public IEnumerable<string> GetRole(Computer bot)
        {
            IEnumerable<string> role = new List<string>();
            if (bot.Mining == true)
            {
                role = resources.Where(name => name.StartsWith("MineCraft.Lua.miner"));
            }

            return role;
        }

        public IEnumerable<string> GetSpecific(Computer bot)
        {
            IEnumerable<string> specific = new List<string>();
            if (bot.Category != null)
            {
                specific = resources.Where(name => name.StartsWith("MineCraft.Lua." + bot.Category));
            }
            return specific;
        }

        public string GetMostSpecific(Computer bot, string name)
        {
            if (!name.EndsWith(".lua")) name = name + ".lua";
            var mostSpecific = GetSpecific(bot).Where(resource => resource.EndsWith(name));
            if (mostSpecific.Count() == 0) mostSpecific = GetRole(bot).Where(resource => resource.EndsWith(name));
            if (mostSpecific.Count() == 0) mostSpecific = GetGeneral().Where(resource => resource.EndsWith(name));
            if (mostSpecific.Count() == 0) mostSpecific = GetBase().Where(resource => resource.EndsWith(name));
            return mostSpecific.SingleOrDefault();
        }
    }
}
