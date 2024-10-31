using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public partial class RecipeRate
    {
        public int RecipeId { get; set; }
        public int AccountId { get; set; }
        public int RatePoint { get; set; }
        public virtual Recipe? Recipe { get; set; }
        public virtual Account? Account { get; set; }
    }
}
