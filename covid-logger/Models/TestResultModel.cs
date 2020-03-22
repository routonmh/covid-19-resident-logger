using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ResidentLog.Models.Entities;
using ResidentLog.Utilities.DB;

namespace ResidentLog.Models
{
    public static class TestResultModel
    {
        public static async Task<List<TestResult>> GetTestResultTypes()
        {
            List<TestResult> testResultTypes = new List<TestResult>();

            using (LocalDB db = new LocalDB())
            {
                await db.OpenConnectionAsync();
                MySqlCommand cmd = db.CreateCommand();
                string query = "SELECT TestResultType, TestResultDescription FROM " +
                               "test_result ORDER BY TestResultType ASC;";

                cmd.CommandText = query;
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                    while (await reader.ReadAsync())
                        testResultTypes.Add(new TestResult(
                            reader["TestResultType"] as int? ?? 0,
                            reader["TestResultDescription"] as string ?? string.Empty));
            }

            return testResultTypes;
        }
    }
}