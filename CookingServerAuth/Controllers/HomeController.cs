using CookingServerAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace CookingServerAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            RecipesContext recipesContext = new RecipesContext();
            if(User.Identity.IsAuthenticated)
                ViewBag.Recipes = recipesContext.GetViewRecipesListWithFavorite(User.Identity.GetUserId());
            else
                ViewBag.Recipes = recipesContext.GetViewRecipesList();
            return View();
        }

        [Authorize]
        public ActionResult Favorite()
        {
            FavoritesContext favoritesContext = new FavoritesContext();
            ViewBag.Recipes = favoritesContext.GetViewFavoritesList(User.Identity.GetUserId());
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult About()
        {
            RecipesContext recipesContext = new RecipesContext();
            ViewBag.Recipes = recipesContext.GetViewRecipesList();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Свяжитесь";
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddRecipe model)
        {
            RecipesContext recipesContext = new RecipesContext();
            InsertRecipe insertRecipe = new InsertRecipe()
            {
                OwnerId = User.Identity.GetUserId(),
                Title = model.Title,
                Description = model.Description,
                Date = DateTime.Now,
                Deleted = false
            };
            recipesContext.Add(insertRecipe);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string idSelectedItem)
        {
            RecipesContext recipesContext = new RecipesContext();
            ViewBag.recipe = recipesContext.GetRecipeByID(Int32.Parse(idSelectedItem));
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Edit(EditRecipe model)
        {
            RecipesContext recipesContext = new RecipesContext();
            recipesContext.Edit(model);
            return RedirectToAction("About");
        }
    }
}