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

            if (computer.Mining != false)
            {
                names = names.Where(name => name.StartsWith("MineCraft.Lua.miner"));
            }

            var programs = new List<string>();
            foreach(var name in names)
            {
                var basicName = string.Join(".", name.Split(".").Skip(3));

                programs.Add(Package(computer, basicName, name));
            }
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
