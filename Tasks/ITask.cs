using MineCraft.Bots;
using MineCraft.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Tasks
{
    public interface ITask
    {
        string GetLua(Resources resources);
    }
}
