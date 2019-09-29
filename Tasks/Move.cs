using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MineCraft.Lua;

namespace MineCraft.Tasks
{
    public class Move : ITask
    {
        private string args;

        public Move(string args)
        {
            this.args = args;
        }

        public string GetLua(Resources resources)
        {
            var builder = new StringBuilder();
            
            var parts = Regex.Matches(args, "\\d*\\D");
            var numParts = parts.Count;
            
            for (int n = 0; n < numParts; n++)
            {
                var part = parts[n].ToString();
                Add(builder, part);
            }

            builder.AppendLine("os.reboot()");
            return builder.ToString();
        }

        private void Add(StringBuilder builder, string part)
        {
            var command = ParseCommand(part.Last());

            if (part.Length > 1)
            {
                var digits = part.Substring(0, part.Length - 1);
                builder.AppendLine(string.Format("for n=1,{0},1 do", digits));
                builder.Append("\t");
                builder.AppendLine(command);
                builder.AppendLine("end");
            }
            else
            {
                builder.Append(command);
            }
        }

        private string ParseCommand(char code)
        {
            switch(code)
            {
                case 'f': return "turtle.forward()";
                case 'b': return "turtle.back()";
                case 'l': return "turtle.turnLeft()";
                case 'r': return "turtle.turnRight()";
                default: throw new ArgumentException(code + " is not a valid command");
            }
        }
    }
}
