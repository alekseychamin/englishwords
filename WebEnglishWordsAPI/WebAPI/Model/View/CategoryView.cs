using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.Model
{
    public class CategoryView
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
