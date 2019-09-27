using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Bots
{
    public class ReportParser
    {
        public string ParseReport(Computer bot, string body)
        {
            var data = body.Split("<*>", StringSplitOptions.RemoveEmptyEntries);

            foreach (var datum in data)
            {
                var kvp = SplitKeyAndValue(datum);

                switch(kvp[0])
                {
                    case "tool left": bot.LeftTool = ToolFactory.FromKey(kvp[1]); break;
                    case "tool right": bot.RightTool = ToolFactory.FromKey(kvp[1]); break;
                    case "fuel": bot.Fuel = long.Parse(kvp[1]); break;
                    case "label": bot.Label = kvp[1]; break;
                }
            }

            return "";
        }

        private static string[] SplitKeyAndValue(string input)
        {
            return input.Split(new char[] { ':' }, 2);
        }
    }
}
