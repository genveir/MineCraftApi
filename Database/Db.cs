using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Database
{
    public static class Db
    {
        private const string connectionString = "server=localhost;database=Minecraft;Integrated Security=true";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static SqlCommand AddLongParameter(this SqlCommand command, string name, long? value)
        {
            var param = command.CreateParameter();
            param.DbType = System.Data.DbType.Int64;
            param.ParameterName = name;

            if (value.HasValue)
            {
                param.Value = value;
            }
            else
            {
                param.Value = DBNull.Value;
            }

            command.Parameters.Add(param);

            return command;
        }

        public static SqlCommand AddStringParameter(this SqlCommand command, string name, string value)
        {
            var param = command.CreateParameter();
            param.DbType = System.Data.DbType.String;
            param.ParameterName = name;

            if (!string.IsNullOrWhiteSpace(value))
            {
                param.Value = value;
            }
            else
            {
                param.Value = DBNull.Value;
            }

            command.Parameters.Add(param);

            return command;
        }

        public static SqlCommand AddBitParameter(this SqlCommand command, string name, bool? value)
        {
            var param = command.CreateParameter();
            param.DbType = System.Data.DbType.Boolean;
            param.ParameterName = name;

            if (value.HasValue)
            {
                param.Value = value;
            }
            else
            {
                param.Value = DBNull.Value;
            }

            command.Parameters.Add(param);

            return command;
        }

        public static long? AsLong(this IDataRecord data, int index)
        {
            var val = data[index];

            return (long?)(NOV(val)?.Value);
        }

        public static string AsString(this IDataRecord data, int index)
        {
            var val = data[index];

            return NOV(val)?.Value.ToString();
        }

        public static bool? AsBool(this IDataRecord data, int index)
        {
            var val = data[index];

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
