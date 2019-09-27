using MineCraft.Bots;
using MineCraft.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Lua
{
    public static class TaskAssigner
    {
        public static string GetOrder(Computer bot)
        {
            string defaultOrder = "stopListening";

            if (bot.Mining == true) defaultOrder = "wait";

            using (var con = Db.GetConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = @"
delete from Task
output
	deleted.TaskName
where TaskId in (select top 1 TaskId from Task where ComputerId = @computerId order by TaskId)
";
                command.AddLongParameter("computerId", bot.ComputerId);

                con.Open();
                var result = command.ExecuteReader();

                if (!result.Read()) return defaultOrder;
                else
                {
                    var task = ((IDataRecord)result).AsString(0);

                    if (task == null) return defaultOrder;

                    return task;
                }
            }
        }

        public static void EnqueueIndividualTask(long computerId, string task)
        {
            using (var con = Db.GetConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = @"
insert into Task (ComputerId, TaskName) values (@computerId, @task)
";
                command.AddLongParameter("computerId", computerId)
                    .AddStringParameter("task", task);

                con.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void EnqueueGroupOrder(string botType, string task)
        {
            string dbType = "";
            switch(botType)
            {
                case "miner": dbType = "Mining"; break;
            }

            if (dbType != "")
            {
                using (var con = Db.GetConnection())
                {
                    var command = con.CreateCommand();
                    command.CommandText = string.Format(@"
insert into Task (ComputerId, TaskName) 
select 
    ComputerId, @task 
from 
    computer
where 
    {0} = 1 and 
    DATEADD(minute, 5, lastActive) > CURRENT_TIMESTAMP", dbType);

                    command.AddStringParameter("task", task);

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
