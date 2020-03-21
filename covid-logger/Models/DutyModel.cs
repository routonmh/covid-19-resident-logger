using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ResidentLog.Models.Entities;
using ResidentLog.Utilities.DB;

namespace ResidentLog.Models
{
    public static class DutyModel
    {
        public static async Task<List<Duty>> GetDutyTypes()
        {
            List<Duty> dutyTypes = new List<Duty>();

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT DutyType, DutyDescription FROM duty;";

                cmd.CommandText = query;
                DbDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                    while (await reader.ReadAsync())
                    {
                        int type = reader["DutyType"] as int? ?? 0;
                        string description = reader["DutyDescription"] as string ?? string.Empty;

                        dutyTypes.Add(new Duty(type, description));
                    }
            }

            return dutyTypes;
        }
    }
}