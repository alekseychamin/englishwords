using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.Model
{
    public class CategoryBL
    {
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "The length of category name is to long.")]
        [Required]
        public string Name { get; set; }

        public ICollection<EnglishWordBL> EnglishWords { get; set; }
    }
}
