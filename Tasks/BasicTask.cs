using MineCraft.Bots;
using MineCraft.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Tasks
{
    public class BasicTask : ITask
    {
        private Computer bot;
        private string taskName;

        public BasicTask(Computer bot, string taskName)
        {
            this.bot = bot;
            this.taskName = taskName;
        }

        public string GetLua(Resources resources)
        {
            var mostSpecific = resources.GetMostSpecific(bot, "roof");
            if (mostSpecific == null) return null;

            var progNameParts = mostSpecific.Split(".").TakeLast(3).ToArray();
            var progName = progNameParts[0] + "/" + progNameParts[1] + "." + progNameParts[2];

            return string.Format(@"
if fs.exists({0}) then
    shell.run({0})
    os.reboot()
else
    print('file does not exist')
end",

                "\"" + progName + "\"").Replace('\'', '"');
        }
    }
}
