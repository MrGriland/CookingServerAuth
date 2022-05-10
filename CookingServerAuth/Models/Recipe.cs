using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace CookingServerAuth.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Deleted { get; set; }
    }
}