using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CookingServerAuth.Models
{
    public class RecipesContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<Recipe> GetRecipesList()
        {
            List<Recipe> recipes = new List<Recipe>();
            string sqlExpression = "SELECT * FROM RecipeInfo where RecipeInfo_isDeleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader.GetValue(0);
                        string oid = (string)reader.GetValue(1);
                        string title = (string)reader.GetValue(2);
                        string description = (string)reader.GetValue(3);
                        DateTime date = (DateTime)reader.GetValue(4);
                        bool deleted = (bool)reader.GetValue(5);
                        recipes.Add(new Recipe { Id = id, OwnerId = oid, Title = title, Description = description, Date = date, Deleted = deleted });
                    }
                }
                reader.Close();
            }
            return recipes;
        }

        public List<ViewRecipe> GetViewRecipesListWithFavorite(string uid)
        {
            List<ViewRecipe> recipes = new List<ViewRecipe>();
            FavoritesContext favoritesContext = new FavoritesContext();
            string sqlExpression = "SELECT * FROM RecipeInfo where RecipeInfo_isDeleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader.GetValue(0);
                        string title = (string)reader.GetValue(2);
                        string description = (string)reader.GetValue(3);
                        DateTime date = (DateTime)reader.GetValue(4);
                        recipes.Add(new ViewRecipe { Id = id, Title = title, Description = description, Date = date, isFavorite = favoritesContext.IsFavorite(id, uid)});
                    }
                }
                reader.Close();
            }
            return recipes;
        }

        public List<ViewRecipe> GetViewRecipesList()
        {
            List<ViewRecipe> recipes = new List<ViewRecipe>();
            string sqlExpression = "SELECT * FROM RecipeInfo where RecipeInfo_isDeleted = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader.GetValue(0);
                        string title = (string)reader.GetValue(2);
                        string description = (string)reader.GetValue(3);
                        DateTime date = (DateTime)reader.GetValue(4);
                        recipes.Add(new ViewRecipe { Id = id, Title = title, Description = description, Date = date, isFavorite = false });
                    }
                }
                reader.Close();
            }
            return recipes;
        }

        public void Add(InsertRecipe recipe)
        {
            string sqlExpression = "insert into RecipeInfo values (@oid,@title,@description,@date,@deleted)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@oid", recipe.OwnerId);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@title", recipe.Title);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@description", recipe.Description);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@date", recipe.Date);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@deleted", recipe.Deleted);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }
        public void Delete(int id)
        {
            string sqlExpression = "update RecipeInfo set RecipeInfo_TimeStamp = @time, RecipeInfo_isDeleted = 1 where RecipeInfo_ID = @rid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@time", DateTime.Now);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@rid", id);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }
        public Recipe GetRecipeByID(int id)
        {
            Recipe recipe = new Recipe();
            string sqlExpression = "select * from RecipeInfo where RecipeInfo_ID = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@id", id);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    recipe.Id = (int)reader.GetValue(0);
                    recipe.OwnerId = (string)reader.GetValue(1);
                    recipe.Title = (string)reader.GetValue(2);
                    recipe.Description = (string)reader.GetValue(3);
                    recipe.Date = (DateTime)reader.GetValue(4);
                    recipe.Deleted = (bool)reader.GetValue(5);
                }
                reader.Close();
            }
            return recipe;
        }

        public void Edit(EditRecipe recipe)
        {
            string sqlExpression = "update RecipeInfo set RecipeInfo_Title = @title, RecipeInfo_Description = @description, RecipeInfo_TimeStamp = @time where RecipeInfo_ID = @rid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlParameter = new SqlParameter("@title", recipe.Title);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@description", recipe.Description);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@time", DateTime.Now);
                command.Parameters.Add(sqlParameter);
                sqlParameter = new SqlParameter("@rid", recipe.Id);
                command.Parameters.Add(sqlParameter);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }
    }
}