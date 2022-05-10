using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookingServerAuth.Models
{
    public class ViewRecipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool isFavorite { get; set; }
    }
}