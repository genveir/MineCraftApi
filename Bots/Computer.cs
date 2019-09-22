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
        public bool? Crafty { get; set; }
        public bool? Mining { get; set; }
        public bool? Farming { get; set; }
        public bool? Digging { get; set; }
        public bool? Melee { get; set; }
        public bool? Felling { get; set; }
        public bool? Mobile { get; set; }

        public static Computer FromReader(SqlDataReader reader)
        {
            reader.Read();
            
            var data = (IDataRecord)reader;
            var computer = new Computer();
            computer.ComputerId = AsLong(data[0]).Value;
            computer.Label = AsString(data[1]);
            computer.Location = AsString(data[2]);
            computer.Crafty = AsBool(data[3]);
            computer.Mining = AsBool(data[4]);
            computer.Farming = AsBool(data[5]);
            computer.Digging = AsBool(data[6]);
            computer.Melee = AsBool(data[7]);
            computer.Felling = AsBool(data[8]);
            computer.Mobile = AsBool(data[9]);

            return computer;
        }

        private static long? AsLong(object val)
        {
            return (long?)(NOV(val)?.Value);
        }

        private static string AsString(object val)
        {
            return NOV(val)?.Value.ToString();
        }

        private static bool? AsBool(object val)
        {
            return (bool?)(NOV(val)?.Value);
        }

        private static NullOrValue NOV(object val)
        {
            return val.GetType() == typeof(DBNull) ? null : new NullOrValue() { Value = val };
        }

        private class NullOrValue
        {
            public object Value;
        }
    }    
}
