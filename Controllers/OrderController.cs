using Microsoft.AspNetCore.Mvc;
using MineCraft.Lua;
using MineCraft.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Controllers
{
    [Route("task")]
    public class TaskController : ControllerBase
    {

        [HttpPost("{botType}/{task}")]
        public ActionResult AddOrder(string botType, string task)
        {
            TaskAssigner.EnqueueGroupOrder(botType, task);

            return NoContent();
        }
    }
}
