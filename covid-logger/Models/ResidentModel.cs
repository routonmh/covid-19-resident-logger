using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ResidentLog.Models.Entities;
using ResidentLog.Utilities.DB;

namespace ResidentLog.Models
{
    public static class ResidentModel
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="resident"></param>
        /// <returns></returns>
        public static async Task<int> CreateResidentEntry(Resident resident)
        {
            int residentId = -1;

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "INSERT INTO resident (FirstName, LastName, PGY, PhoneNumber, " +
                               "SymptomsDate, SymptomsDescription, Covid19TestDate, Covid19TestResult, " +
                               "IsQuarantined, QuarantinedUntil) VALUES (@FirstName, @LastName, @PGY, " +
                               "@PhoneNumber, @SymptomsDate, @SymptomsDescription, @Covid19TestDate, " +
                               "@Covid19TestResult, @IsQuarantined, @QuarantinedUntil);";

                cmd.Parameters.AddWithValue("@FirstName", resident.FirstName);
                cmd.Parameters.AddWithValue("@LastName", resident.LastName);
                cmd.Parameters.AddWithValue("@PGY", resident.PGY);
                cmd.Parameters.AddWithValue("@PhoneNumber", resident.PhoneNumber);
                cmd.Parameters.AddWithValue("@SymptomsDate", resident.SymptomsDate);
                cmd.Parameters.AddWithValue("@SymptomsDescription", resident.SymptomsDescription);
                cmd.Parameters.AddWithValue("@Covid19TestDate", resident.Covid19TestDate);
                cmd.Parameters.AddWithValue("@Covid19TestResult", resident.Covid19TestResult.TestResultType);
                cmd.Parameters.AddWithValue("@IsQuarantined", resident.IsQuarantined);
                cmd.Parameters.AddWithValue("@QuarantinedUntil", resident.QuarantinedUntil);

                cmd.CommandText = query;

                await cmd.ExecuteNonQueryAsync();
                residentId = Convert.ToInt32(cmd.LastInsertedId);
            }

            return residentId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="residentId"></param>
        /// <param name="resident"></param>
        /// <returns></returns>
        public static async Task UpdateResidentEntry(int residentId, Resident resident)
        {
            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "UPDATE resident SET FirstName = @FirstName, LastName = @LastName, " +
                               "PGY = @PGY, PhoneNumber = @PhoneNumber, SymptomsDate = @SymptomsDate, " +
                               "Covid19TestDate = @Covid19TestDate, Covid19TestResult = @Covid19TestResult, " +
                               "IsQuarantined = @IsQuarantined, QuarantinedUntil = @QuarantinedUntil " +
                               "WHERE ResidentID = @ResidentID;";

                cmd.Parameters.AddWithValue("@ResidentID", residentId);
                cmd.Parameters.AddWithValue("@FirstName", resident.FirstName);
                cmd.Parameters.AddWithValue("@LastName", resident.LastName);
                cmd.Parameters.AddWithValue("@PGY", resident.PGY);
                cmd.Parameters.AddWithValue("@PhoneNumber", resident.PhoneNumber);
                cmd.Parameters.AddWithValue("@SymptomsDate", resident.SymptomsDate);
                cmd.Parameters.AddWithValue("@SymptomsDescription", resident.SymptomsDescription);
                cmd.Parameters.AddWithValue("@Covid19TestDate", resident.Covid19TestDate);
                cmd.Parameters.AddWithValue("@Covid19TestResult", resident.Covid19TestResult.TestResultType);
                cmd.Parameters.AddWithValue("@IsQuarantined", resident.IsQuarantined);
                cmd.Parameters.AddWithValue("@QuarantinedUntil", resident.QuarantinedUntil);
                cmd.CommandText = query;

                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="residentId"></param>
        /// <returns></returns>
        public static async Task<Resident> GetResidentEntry(int residentId)
        {
            Resident resident = null;

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT ResidentID, FirstName, LastName, PGY, PhoneNumber, " +
                               "SymptomsDate, SymptomsDescription, Covid19TestDate, Covid19TestResult, " +
                               "IsQuarantined, QuarantinedUntil, TestResultDescription " +
                               "FROM resident INNER JOIN test_result ON test_result.TestResultType = " +
                               "resident.Covid19TestResult WHERE ResidentID = @ResidentID;";

                cmd.Parameters.AddWithValue("@ResidentID", residentId);
                cmd.CommandText = query;

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    resident = new Resident(
                        residentId,
                        reader["FirstName"] as string ?? string.Empty,
                        reader["LastName"] as string ?? string.Empty,
                        reader["PGY"] as string ?? string.Empty,
                        reader["PhoneNumber"] as string ?? string.Empty,
                        reader["SymptomsDate"] as DateTime?,
                        reader["SymptomsDescription"] as string ?? string.Empty,
                        reader["Covid19TestDate"] as DateTime?,
                        new TestResult(
                            reader["Covid19TestResult"] as int? ?? 0,
                            reader["TestResultDescription"] as string ?? string.Empty),
                        Convert.ToBoolean(reader["IsQuarantined"] as sbyte? ?? 0),
                        reader["QuarantinedUntil"] as DateTime?);
                }
            }

            return resident;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="residentId"></param>
        /// <returns></returns>
        public static async Task DeleteResidentEntry(int residentId)
        {
            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "DELETE FROM resident WHERE ResidentID = @ResidentID";

                cmd.Parameters.AddWithValue("@ResidentID", residentId);
                cmd.CommandText = query;

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task<LocalIDSet<int>> PreviousAndNextResidentIds(int residentId)
        {
            LocalIDSet<int> localSet = new LocalIDSet<int>();

            List<int> residentIds = new List<int>();
            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT ResidentID FROM resident ORDER BY ResidentID ASC";

                cmd.CommandText = query;
                DbDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                    residentIds.Add((int) reader["ResidentID"]);
            }

            int idx = residentIds.IndexOf(residentId);

            int previousId = -1;
            int nextId = -1;

            int previousIdx = idx - 1;
            int nextIdx = idx + 1;

            if (previousIdx >= 0)
                previousId = residentIds[previousIdx];

            if (nextIdx < residentIds.Count)
                nextId = residentIds[nextIdx];

            localSet.PreviousID = previousId;
            localSet.NextID = nextId;

            return localSet;
        }
    }
}