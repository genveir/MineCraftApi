using MineCraft.Bots;
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
        public string Package(Computer computer)
        {
            IEnumerable<string> names = typeof(Packager).Assembly.GetManifestResourceNames();

            var baseScripts = names.Where(name => name.StartsWith("MineCraft.Lua.base"));
            var general = names.Where(name => name.StartsWith("MineCraft.Lua.general"));
            IEnumerable<string> specific = new List<string>();
            if (computer.Mining == true)
            {
                specific = names.Where(name => name.StartsWith("MineCraft.Lua.miner"));
            }
            names = baseScripts.Union(general.Union(specific));

            var task = "MineCraft.Lua.tasks." + TaskAssigner.GetOrder(computer) + ".lua";

            var programs = new List<string>();
            foreach(var name in names)
            {
                var split = name.Split(".");
                var folder = split[2] + "/";
                if (folder == "base/") folder = "";
                var basicName = folder + string.Join(".", split.Skip(3));

                programs.Add(Package(computer, basicName, name));
            }
            programs.Add(Package(computer, "general/bot.lua", task));
            return string.Join(";", programs);
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

            if (text.Contains(";")) throw new DividerInPackageTextException(name);

            builder.Append(text);

            return builder.ToString();
        }

        private string Package(Computer computer, string name, string resource)
        {
            var text = Package(name, resource);

            text = text.Replace("{{BOTNAME}}", computer.ComputerId.ToString());

            return text;
        }
    }
}
