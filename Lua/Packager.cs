using MineCraft.Bots;
using MineCraft.Tasks;
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
            var resources = new Resources();

            var baseScripts = resources.GetBase();
            var general = resources.GetGeneral();

            IEnumerable<string> role = resources.GetRole(computer);
            var specific = resources.GetSpecific(computer);
            
            var toPackage = baseScripts.Union(general.Union(role.Union(specific)));

            var programs = new List<string>();
            foreach(var name in toPackage)
            {
                var split = name.Split(".");
                var folder = split[2] + "/";
                if (folder == "base/") folder = "";
                var basicName = folder + string.Join(".", split.Skip(3));

                programs.Add(PackageResource(computer, basicName, name));
            }

            var task = TaskAssigner.GetOrder(computer);

            programs.Add(Package("general/bot.lua", task.GetLua(resources)));
            return string.Join(";", programs);
        }

        public string PackageGApi()
        {
            var stream = Assembly.GetCallingAssembly().GetManifestResourceStream("MineCraft.Lua.httpApi.lua");
            return new StreamReader(stream).ReadToEnd();
        }

        private string Package(string name, string lua)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");
            if (lua.Contains(";")) throw new DividerInPackageTextException(name);

            var builder = new StringBuilder(name);
            builder.Append(";");

            builder.Append(lua);

            return builder.ToString();
        }

        private string PackageResource(string name, string resource)
        {
            var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resource);

            var text = new StreamReader(stream).ReadToEnd();

            return Package(name, text);
        }

        private string PackageResource(Computer computer, string name, string resource)
        {
            var text = PackageResource(name, resource);

            text = text.Replace("{{BOTNAME}}", computer.ComputerId.ToString());

            return text;
        }
    }
}
