using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookingServerAuth.Models
{
    public class AddRecipe
    {
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}