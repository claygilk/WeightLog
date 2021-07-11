using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WeightLog.Models;

namespace WeightLog.DAL
{
    public class MaxDAO : IMaxDAO
    {
        private readonly string connectionString;

        public MaxDAO(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
        }

        public Max GetLiftMax(int userId, string lift)
        {
            Max max = new Max();
            max.LiftName = lift;

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT " +
                    "m.max_weight AS max " +
                    "FROM maxes m " +
                    "JOIN lifts l " +
                    "ON l.id = m.lift_id " +
                    "WHERE l.name = @lift " +
                    "AND m.user_id = @userId;"
                    , conn);

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@lift", lift);

                max.MaxWeight = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return max;
        }

        public List<Max> GetMaxes(int userId)
        {
            List<Max> maxes = new List<Max>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT " +
                    "l.name, " +
                    "m.max_weight AS max " +
                    "FROM maxes m " +
                    "JOIN lifts l ON l.id = m.lift_id " +
                    "WHERE m.user_id = @userId;"
                    , conn);

                cmd.Parameters.AddWithValue("@userId", userId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Max max = new Max();

                    max.LiftName = Convert.ToString(reader["name"]);
                    max.MaxWeight = Convert.ToInt32(reader["max"]);

                    maxes.Add(max);
                }
            }

            return maxes;
        }
    }
}
