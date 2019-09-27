using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MineCraft.Bots;
using MineCraft.Database;
using MineCraft.Lua;

namespace MineCraft.Controllers
{
    [Route("api/bot")]
    [Route("computer")]
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
            var computer = bots.Get(id);

            if (computer == null) id = bots.Add().ComputerId;

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
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Report(long id, [FromForm]string data)
        {
            var bot = bots.Get(id);

            var reporter = new ReportParser();
            var result = reporter.ParseReport(bot, data);

            bot.ApplyTools();
            bots.Update(bot);

            return Ok(result);
        }
    }
}