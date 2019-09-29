using MineCraft.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Tasks
{
    public class Wait : ITask
    {
        public string GetLua(Resources resources)
        {
            return @"os.sleep(5)
os.reboot()";
        }
    }
}
