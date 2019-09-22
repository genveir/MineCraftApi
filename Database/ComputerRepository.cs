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
insert into computer output inserted.* default values;
";
                con.Open();
                var reader = command.ExecuteReader();

                return Computer.FromReader(reader);
            }
        }

        public Computer Update(long id, string data)
        {
            return new Computer();
        }

        public Computer Get(long id)
        {

            return new Computer();
        }
    }
}
