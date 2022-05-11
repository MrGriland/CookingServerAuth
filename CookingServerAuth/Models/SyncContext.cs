using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CookingServerAuth.Models
{
    public class SyncContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public DateTime GetVersion()
        {
            DateTime datetime = new DateTime();
            string sqlExpression = "select TOP 1 RecipeInfo_TimeStamp from RecipeInfo order by RecipeInfo_TimeStamp desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    datetime = (DateTime)sqlDataReader.GetValue(0);
                }
                return datetime;
            }
        }
        public List<Recipe> GetAfter(DateTime dateTime)
        {
            List<Recipe> recipes = new List<Recipe>();
            string sqlExpression = "select * from RecipeInfo where RecipeInfo_TimeStamp > @dateTime";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@dateTime", dateTime);
                sqlCommand.Parameters.Add(sqlParameter);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        int id = (int)sqlDataReader.GetValue(0);
                        string oid = (string)sqlDataReader.GetValue(1);
                        string title = (string)sqlDataReader.GetValue(2);
                        string description = (string)sqlDataReader.GetValue(3);
                        DateTime date = (DateTime)sqlDataReader.GetValue(4);
                        bool deleted = (bool)sqlDataReader.GetValue(5);
                        recipes.Add(new Recipe { Id = id, OwnerId = oid, Title = title, Description = description, Date = date, Deleted = deleted });
                    }
                }
                return recipes;
            }
        }
    }
}