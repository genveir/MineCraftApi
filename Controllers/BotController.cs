using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MineCraft.Database;
using MineCraft.Lua;

namespace MineCraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private ComputerRepository bots;

        public BotController()
        {
            bots = new ComputerRepository();
        }

        [HttpGet("")]
        public string Test()
        {
            return "server is online";
        }

        [HttpPost("logon")]
        public ActionResult<string> Logon()
        {
            var id = bots.Add().ComputerId;

            return Ok(id);
        }

        [HttpPost("{id}/logon")]
        public ActionResult Logon(long id)
        {
            return Ok(id);
        }

        [HttpGet("GApiUpdate")]
        public ActionResult<string> GApiUpdate()
        {
            var program = new Packager().PackageGApi();

            return Ok(program);
        }

        [HttpGet("{id}/update")]
        public ActionResult<string> Update(long id)
        {
            var bot = bots.Get(id);

            var program = new Packager().Package(bot);

            return Ok(program);
        }

        [HttpPost("{id}/report")]
        public string Report(long id)
        {
            var body = new StreamReader(Request.Body).ReadToEnd();

            var bot = bots.Get(id);

            return "not implemented";
        }
    }
}