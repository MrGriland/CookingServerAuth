using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CookingServerAuth.Models;
using Microsoft.AspNet.Identity;

namespace CookingServerAuth.Controllers
{
    [Authorize]
    public class RecipesController : ApiController
    {
        RecipesContext recipesContext = new RecipesContext();
        public List<Recipe> Get()
        {
            return recipesContext.GetRecipesList();
        }
        public InsertRecipe Post([FromBody] PostRecipe value)
        {
            InsertRecipe insertRecipe = new InsertRecipe() { OwnerId = User.Identity.GetUserId(),
                Title = value.Title, Description = value.Description, Date = DateTime.Now, Deleted = false};
            return insertRecipe;
        }
        public void Delete([FromBody] IDContainer value)
        {
            FavoritesContext favoritesContext = new FavoritesContext();
            favoritesContext.DeleteFavorites(value.id);
            recipesContext.Delete(value.id);
        }
    }
}
