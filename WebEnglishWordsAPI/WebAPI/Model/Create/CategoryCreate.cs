using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.Model
{
    public class CategoryCreate
    {        
        public string Name { get; set; }
    }
}
