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
        public Computer Add()
        {
            using (var con = Db.GetConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = @"
insert into 
    computer (LastActive) 
output 
    inserted.* 
values 
    (CURRENT_TIMESTAMP)";
                con.Open();
                var reader = command.ExecuteReader();

                return Computer.FromReader(reader);
            }
        }

        public Computer Update(Computer bot)
        {
            using (var con = Db.GetConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = @"
update computer
set
    label = @label,
    location = @location,
    crafty = @crafty,
    mining = @mining,
    farming = @farming,
    digging = @digging,
    melee = @melee,
    felling = @felling,
    mobile = @mobile,
    fuel = @fuel,
    lastActive = CURRENT_TIMESTAMP
output 
    inserted.* 
where
    computerId = @id
";
                command.AddStringParameter("label", bot.Label)
                    .AddStringParameter("location", bot.Location)
                    .AddBitParameter("crafty", bot.Crafty)
                    .AddBitParameter("mining", bot.Mining)
                    .AddBitParameter("farming", bot.Farming)
                    .AddBitParameter("digging", bot.Digging)
                    .AddBitParameter("melee", bot.Melee)
                    .AddBitParameter("felling", bot.Felling)
                    .AddBitParameter("mobile", bot.Mobile)
                    .AddLongParameter("fuel", bot.Fuel)
                    .AddLongParameter("id", bot.ComputerId);

                con.Open();
                var reader = command.ExecuteReader();

                return Computer.FromReader(reader);
            }
        }

        public Computer Get(long id)
        {
            using (var con = Db.GetConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = @"
update computer
set
    lastActive = CURRENT_TIMESTAMP
output 
    inserted.* 
where 
    computerId = @id;";
                command.AddLongParameter("id", id);

                con.Open();
                var reader = command.ExecuteReader();

                return Computer.FromReader(reader);
            }
            
        }
    }
}
