using MineCraft.Bots;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Database
{
    public class ComputerRepository
    {
        private string connectionString = "server=localhost;database=Minecraft;Integrated Security=true";

        public Computer Add()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = con.CreateCommand();
                command.CommandText = @"
insert into computer output inserted.* default values;";
                con.Open();
                var reader = command.ExecuteReader();

                return Computer.FromReader(reader);
            }
        }

        public Computer Update(long id, string data)
        {
            return Get(id);
        }

        public Computer Get(long id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = con.CreateCommand();
                command.CommandText = @"
select * from computer where computerId = @id;";
                AddIdParam(command, id);

                con.Open();
                var reader = command.ExecuteReader();

                return Computer.FromReader(reader);
            }
            
        }

        private void AddIdParam(SqlCommand command, long id)
        {
            var param = command.CreateParameter();
            param.DbType = System.Data.DbType.Int64;
            param.ParameterName = "id";
            param.Value = id;

            command.Parameters.Add(param);
        }
    }
}
