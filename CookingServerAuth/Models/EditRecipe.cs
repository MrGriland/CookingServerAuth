using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookingServerAuth.Models
{
    public class EditRecipe
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}