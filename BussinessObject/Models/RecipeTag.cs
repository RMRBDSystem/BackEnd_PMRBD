using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public partial class RecipeTag
    {
        public int RecipeId { get; set; }

        public int TagId { get; set; }


        public virtual Recipe? Recipe { get; set; }


        public virtual Tag? Tag { get; set; }
    }
}
