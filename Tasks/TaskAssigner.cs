using MineCraft.Bots;
using MineCraft.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MineCraft.Tasks
{
    public static class TaskAssigner
    {
        public static ITask GetOrder(Computer bot)
        {
            ITask defaultOrder = new StopListening();

            if (bot.Mining == true) defaultOrder = new Wait();

            using (var con = Db.GetConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = @"
delete from Task
output
	deleted.TaskName, deleted.Args
where TaskId in (select top 1 TaskId from Task where ComputerId = @computerId order by TaskId)
";
                command.AddLongParameter("computerId", bot.ComputerId);

                con.Open();
                var result = command.ExecuteReader();

                if (!result.Read()) return defaultOrder;
                else
                {
                    var data = (IDataRecord)result;
                    var task = data.AsString(0);
                    var args = data.AsString(1);

                    return Parse(task, args, bot, defaultOrder);
                }
            }
        }

        private static ITask Parse(string task, string args, Computer bot, ITask defaultTask)
        {
            ITask assigned = new BasicTask(bot, task);
            if (assigned == null) assigned = defaultTask;

            return assigned;
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
