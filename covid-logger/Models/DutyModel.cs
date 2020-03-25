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
                        dutyTypes.Add(new Duty(
                            reader["DutyType"] as int? ?? 0,
                            reader["DutyDescription"] as string ?? string.Empty));
            }

            return dutyTypes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dutyAssignment"></param>
        /// <returns></returns>
        public static async Task AssignDuty(DutyAssignment dutyAssignment)
        {
            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "INSERT INTO duty_assignment (ResidentID, DutyType, DateStart, DateEnd, DateAssigned) " +
                               "VALUES (@ResidentID, @DutyType, @DateStart, @DateEnd, @DateAssigned)";

                cmd.Parameters.AddWithValue("@ResidentID", dutyAssignment.ResidentID);
                cmd.Parameters.AddWithValue("@DutyType", dutyAssignment.Duty.DutyType);
                cmd.Parameters.AddWithValue("@DateStart", dutyAssignment.DateStart);
                cmd.Parameters.AddWithValue("@DateEnd", dutyAssignment.DateEnd);
                cmd.Parameters.AddWithValue("@DateAssigned", dutyAssignment.DateAssigned);
                cmd.CommandText = query;

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}