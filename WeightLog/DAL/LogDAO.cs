using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WeightLog.Models;

namespace WeightLog.DAL
{
    public class LogDAO : ILogDAO
    {
        private readonly string connectionString;

        public LogDAO(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
        }

        public Set LogNewSet(Set set)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO set_log " +
                    "(date, lift_id, user_id, weight, reps)  " +
                    "VALUES" +
                    "(@date, @liftId, @userId, @weight, @reps); " +
                    "SELECT @@IDENTITY; ", conn);

                string date = $"{set.Date.Year}-{set.Date.Month}-{set.Date.Day}";

                cmd.Parameters.AddWithValue("@liftId", set.LiftId);
                cmd.Parameters.AddWithValue("@userId", set.UserId);
                cmd.Parameters.AddWithValue("@weight", set.Weight);
                cmd.Parameters.AddWithValue("@reps", set.Reps);
                cmd.Parameters.AddWithValue("@date", date);

                set.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return set;
        }

        public Set LookupSetById(int userId, int setId)
        {
            Set set = new Set();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT id, date, lift_id, user_id, weight, reps " +
                    "FROM set_log " +
                    "WHERE id = @setId"
                    , conn);

                cmd.Parameters.AddWithValue("@setId", setId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    set.Id = Convert.ToInt32(reader["id"]);
                    set.Date = Convert.ToDateTime(reader["date"]);
                    set.LiftId = Convert.ToInt32(reader["lift_id"]);
                    set.UserId = Convert.ToInt32(reader["user_id"]);
                    set.Weight = Convert.ToInt32(reader["weight"]);
                    set.Reps = Convert.ToInt32(reader["reps"]);
                }
            }

            return set;
        }
    }
}
