using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CookingServerAuth.Models
{
    public class FavoritesContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<ViewRecipe> GetViewFavoritesList(string uid)
        {
            List<ViewRecipe> recipes = new List<ViewRecipe>();
            string sqlExpression = "SELECT r.RecipeInfo_ID, r.RecipeInfo_Title, r.RecipeInfo_Description, r.RecipeInfo_TimeStamp" +
                " FROM FavoriteInfo f join RecipeInfo r on f.FavoriteInfo_RecipeID = r.RecipeInfo_ID where f.FavoriteInfo_UserID = @uid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@uid", uid);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader.GetValue(0);
                        string title = (string)reader.GetValue(1);
                        string description = (string)reader.GetValue(2);
                        DateTime date = (DateTime)reader.GetValue(3);
                        recipes.Add(new ViewRecipe { Id = id, Title = title, Description = description, Date = date, isFavorite = true });
                    }
                }
                reader.Close();
            }
            return recipes;
        }
        public void AddFavorite(int rid, string uid)
        {
            string sqlExpression = "insert into FavoriteInfo values (@rid,@uid)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@rid", rid);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@uid", uid);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }
        public void DeleteFavorite(int rid, string uid)
        {
            string sqlExpression = "delete from FavoriteInfo where FavoriteInfo_RecipeID = @rid and FavoriteInfo_UserID = @uid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@rid", rid);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@uid", uid);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }
        public bool IsFavorite(int rid, string uid)
        {
            string sqlExpression = "SELECT * from FavoriteInfo where FavoriteInfo_RecipeID = @rid and FavoriteInfo_UserID = @uid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@rid", rid);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@uid", uid);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    return true;
                }
                reader.Close();
            }
            return false;
        }

        public void DeleteFavorites(int rid)
        {
            string sqlExpression = "delete from FavoriteInfo where FavoriteInfo_RecipeID = @rid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@rid", rid);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }
    }
}