using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MineCraft.Lua
{
    public class Packager
    {
        public string Package(Bot bot)
        {
            var botLua = "MineCraft.Lua.bot.lua";

            return Package(bot, "bot.lua", botLua);
        }

        public string PackageGApi()
        {
            var stream = Assembly.GetCallingAssembly().GetManifestResourceStream("MineCraft.Lua.httpApi.lua");
            return new StreamReader(stream).ReadToEnd();
        }

        private string Package(string name, string resource)
        {
            var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resource);

            var builder = new StringBuilder(name);
            builder.Append(";");

            var text = new StreamReader(stream).ReadToEnd();

            builder.Append(text);

            return builder.ToString();
        }

        private string Package(Bot bot, string name, string resource)
        {
            var text = Package(name, resource);

            text = text.Replace("{{BOTNAME}}", bot.Identifier);

            return text;
        }
    }
}
