using CookingServerAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace CookingServerAuth.Controllers
{
    [Authorize]
    public class FavoriteController : ApiController
    {
        FavoritesContext favoritesContext = new FavoritesContext();
        public List<ViewRecipe> Get()
        {
            return favoritesContext.GetViewFavoritesList(User.Identity.GetUserId());
        }
        public void Post([FromBody] IDContainer value)
        {
            if(!favoritesContext.IsFavorite(value.id, User.Identity.GetUserId()))
                favoritesContext.AddFavorite(value.id, User.Identity.GetUserId());
        }
        public void Delete([FromBody] IDContainer value)
        {
            favoritesContext.DeleteFavorite(value.id, User.Identity.GetUserId());
        }
    }
}
