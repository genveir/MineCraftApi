using MineCraft.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Bots
{
    public class Computer
    {
        public Computer()
        {
            
        }

        public Computer(long id) : this()
        {
            ComputerId = id;
        }

        public long ComputerId { get; set; }
        public string Label { get; set; }
        public string Location { get; set; }
        public long? Fuel { get; set; }
        public bool? Crafty { get; set; }
        public bool? Mining { get; set; }
        public bool? Farming { get; set; }
        public bool? Digging { get; set; }
        public bool? Melee { get; set; }
        public bool? Felling { get; set; }
        public bool? Mobile { get; set; }
        public string Category { get; set; }

        public ITool LeftTool { get; set; }
        public ITool RightTool { get; set; }

        public void ApplyTools()
        {
            Crafty = null;
            Mining = null;
            Farming = null;
            Digging = null;
            Melee = null;
            Felling = null;

            if (LeftTool != null) LeftTool.Apply(this);
            if (RightTool != null) RightTool.Apply(this);
        }

        public static Computer FromReader(SqlDataReader reader)
        {
            var hasData = reader.Read();

            Computer computer = null;
            if (hasData)
            {
                var data = (IDataRecord)reader;
;
                var id = data.AsLong(0);
                if (id.HasValue)
                {
                    computer = new Computer();
                    computer.ComputerId = id.Value;
                    computer.Label = data.AsString(1);
                    computer.Location = data.AsString(2);
                    computer.Crafty = data.AsBool(3);
                    computer.Mining = data.AsBool(4);
                    computer.Farming = data.AsBool(5);
                    computer.Digging = data.AsBool(6);
                    computer.Melee = data.AsBool(7);
                    computer.Felling = data.AsBool(8);
                    computer.Mobile = data.AsBool(9);
                    computer.Fuel = data.AsLong(10);
                    computer.Category = data.AsString(12);
                }
            }

            return computer;
        }
    }    
}
