using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ResidentLog.Models.Reports.Entities;
using ResidentLog.Utilities.DB;

namespace ResidentLog.Models.Reports
{
    public static class ResidentReportModel
    {
        public static async Task<List<ResidentReportItem>> GetAllResidents(string sortByFieldName,
            bool asc)
        {
            List<ResidentReportItem> items = new List<ResidentReportItem>();

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT ResidentID, FirstName, LastName, PGY, PhoneNumber, SymptomsDate, " +
                               "SymptomsDescription, Covid19TestDate, IsQuarantined, QuarantinedUntil, " +
                               "test_result.TestResultDescription AS Covid19TestResult " +
                               "FROM resident " +
                               "INNER JOIN test_result ON resident.Covid19TestResult = test_result.TestResultType " +
                               $"ORDER BY @FieldName {(asc ? "ASC" : "DESC")} ";

                cmd.Parameters.AddWithValue("@FieldName", sortByFieldName);
                cmd.CommandText = query;

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                items = await readResidentReportItems(reader);
            }

            return items;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sortByFieldName"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public static async Task<List<ResidentReportItem>> GetQuarantinedResidents(string sortByFieldName,
            bool asc)
        {
            List<ResidentReportItem> items = new List<ResidentReportItem>();

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT ResidentID, FirstName, LastName, PGY, PhoneNumber, SymptomsDate, " +
                               "SymptomsDescription, Covid19TestDate, IsQuarantined, QuarantinedUntil, " +
                               "test_result.TestResultDescription AS Covid19TestResult " +
                               "FROM resident " +
                               "INNER JOIN test_result ON resident.Covid19TestResult = test_result.TestResultType " +
                               "WHERE IsQuarantined = 1 " +
                               $"ORDER BY @FieldName {(asc ? "ASC" : "DESC")} ";

                cmd.Parameters.AddWithValue("@FieldName", sortByFieldName);
                cmd.CommandText = query;

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                items = await readResidentReportItems(reader);
            }

            return items;
        }

        public static async Task<List<ResidentReportItem>> GetAssignments(string sortByFieldName,
            bool asc)
        {
            List<ResidentReportItem> items = new List<ResidentReportItem>();

            DateTime now = DateTime.Now;

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT resident.ResidentID, FirstName, LastName, PGY, PhoneNumber, SymptomsDate, " +
                               "SymptomsDescription, Covid19TestDate, IsQuarantined, QuarantinedUntil, " +
                               "test_result.TestResultDescription AS Covid19TestResult," +
                               "DutyDescription, DateStart, DateEnd " +
                               "FROM resident " +
                               "INNER JOIN test_result ON resident.Covid19TestResult = test_result.TestResultType " +
                               "INNER JOIN duty_assignment ON resident.ResidentID = duty_assignment.ResidentID " +
                               "INNER JOIN duty ON duty.DutyType = duty_assignment.DutyType ";

                               cmd.Parameters.AddWithValue("@FieldName", sortByFieldName);
                cmd.CommandText = query;

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                items = await readResidentReportItemsWithDuties(reader);
            }

            return items;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sortByFieldName"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public static async Task<List<ResidentReportItem>> GetAvailableResidents(string sortByFieldName,
            bool asc)
        {
            List<ResidentReportItem> items = new List<ResidentReportItem>();

            DateTime now = DateTime.Now;
            DateTime todayStart = now.AddHours(-now.Hour);
            DateTime todayEnd = now.AddHours(24 - now.Hour);

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT resident.ResidentID, FirstName, LastName, PGY, PhoneNumber, SymptomsDate, " +
                               "SymptomsDescription, Covid19TestDate, IsQuarantined, QuarantinedUntil, " +
                               "test_result.TestResultDescription AS Covid19TestResult," +
                               "DutyDescription, DateStart, DateEnd " +
                               "FROM resident " +
                               "LEFT JOIN duty_assignment ON duty_assignment.ResidentID = resident.ResidentID " +
                               "left JOIN duty ON duty.DutyType = duty_assignment.DutyType " +
                               "INNER JOIN test_result ON resident.Covid19TestResult = test_result.TestResultType " +
                               "WHERE DateStart IS NULL " +
                               "OR (@TodayStart < DateStart OR @TodayEnd > DateEnd) " +
                               $"ORDER BY @FieldName {(asc ? "ASC" : "DESC")} ";

                cmd.Parameters.AddWithValue("@FieldName", sortByFieldName);
                cmd.Parameters.AddWithValue("@TodayStart", todayStart);
                cmd.Parameters.AddWithValue("@TodayEnd", todayEnd);
                cmd.CommandText = query;

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                items = await readResidentReportItemsWithDuties(reader);
            }

            return items;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sortByFieldName"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public static async Task<List<ResidentReportItem>> GetOnDutyResidents(string sortByFieldName,
            bool asc)
        {
            List<ResidentReportItem> items = new List<ResidentReportItem>();

            DateTime now = DateTime.Now;
            DateTime todayStart = now.AddHours(-now.Hour);
            DateTime todayEnd = now.AddHours(24 - now.Hour);

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT resident.ResidentID, FirstName, LastName, PGY, PhoneNumber, SymptomsDate, " +
                               "SymptomsDescription, Covid19TestDate, IsQuarantined, QuarantinedUntil, " +
                               "test_result.TestResultDescription AS Covid19TestResult," +
                               "DutyDescription, DateStart, DateEnd " +
                               "FROM resident " +
                               "LEFT JOIN duty_assignment ON duty_assignment.ResidentID = resident.ResidentID " +
                               "LEFT JOIN duty ON duty.DutyType = duty_assignment.DutyType " +
                               "INNER JOIN test_result ON resident.Covid19TestResult = test_result.TestResultType " +
                               "(@TodayStart > DateStart AND @TodayEnd < DateEnd) " +
                               $"ORDER BY @FieldName {(asc ? "ASC" : "DESC")} ";

                cmd.Parameters.AddWithValue("@FieldName", sortByFieldName);
                cmd.Parameters.AddWithValue("@TodayStart", todayStart);
                cmd.Parameters.AddWithValue("@TodayEnd", todayEnd);
                cmd.CommandText = query;

                DbDataReader reader = await cmd.ExecuteReaderAsync();

                items = await readResidentReportItemsWithDuties(reader);
            }

            return items;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static async Task<List<ResidentReportItem>> readResidentReportItems(DbDataReader reader)
        {
            List<ResidentReportItem> items = new List<ResidentReportItem>();

            while (await reader.ReadAsync())
            {
                ResidentReportItem reportItem = new ResidentReportItem
                {
                    ResidentID = (reader["ResidentID"] as int? ?? 0).ToString(),
                    FirstName = reader["FirstName"] as string ?? string.Empty,
                    LastName = reader["LastName"] as string ?? string.Empty,
                    PGY = reader["PGY"] as string ?? string.Empty,
                    PhoneNumber = reader["PhoneNumber"] as string ?? string.Empty,
                    SymptomsDate = formatDateTimeForReport(reader["SymptomsDate"] as DateTime?),
                    SymptomsDescription = reader["SymptomsDescription"] as string ?? string.Empty,
                    Covid19TestDate = formatDateTimeForReport(reader["Covid19TestDate"] as DateTime?),
                    Covid19TestResult = reader["Covid19TestResult"] as string ?? string.Empty,
                    IsQuarantined = Convert.ToBoolean(reader["IsQuarantined"] as sbyte? ?? 0) ? "YES" : "NO",
                    QuarantinedUntil = formatDateTimeForReport(reader["QuarantinedUntil"] as DateTime?)
                };

                items.Add(reportItem);
            }

            return items;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static async Task<List<ResidentReportItem>> readResidentReportItemsWithDuties(DbDataReader reader)
        {
            List<ResidentReportItem> items = new List<ResidentReportItem>();

            while (await reader.ReadAsync())
            {
                ResidentReportItem reportItem = new ResidentReportItem
                {
                    ResidentID = (reader["ResidentID"] as int? ?? 0).ToString(),
                    FirstName = reader["FirstName"] as string ?? string.Empty,
                    LastName = reader["LastName"] as string ?? string.Empty,
                    PGY = reader["PGY"] as string ?? string.Empty,
                    PhoneNumber = reader["PhoneNumber"] as string ?? string.Empty,
                    SymptomsDate = formatDateTimeForReport(reader["SymptomsDate"] as DateTime?),
                    SymptomsDescription = reader["SymptomsDescription"] as string ?? string.Empty,
                    Covid19TestDate = formatDateTimeForReport(reader["Covid19TestDate"] as DateTime?),
                    Covid19TestResult = reader["Covid19TestResult"] as string ?? string.Empty,
                    IsQuarantined = Convert.ToBoolean(reader["IsQuarantined"] as sbyte? ?? 0) ? "YES" : "NO",
                    QuarantinedUntil = formatDateTimeForReport(reader["QuarantinedUntil"] as DateTime?),
                    DutyDescription = reader["DutyDescription"] as string ?? string.Empty,
                    DutyStartDate = formatDateTimeForReport(reader["DateStart"] as DateTime?),
                    DutyEndDate = formatDateTimeForReport(reader["DateEnd"] as DateTime?)
                };

                items.Add(reportItem);
            }

            return items;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static string formatDateTimeForReport(DateTime? date)
        {
            string formattedDate = "N/A";
            if (date != null)
            {
                DateTime dt = (DateTime) date;
                formattedDate = dt.ToString("d");
            }

            return formattedDate;
        }
    }
}