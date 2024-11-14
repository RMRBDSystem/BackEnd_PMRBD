using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public partial class RecipeRate
    {
        public int RecipeId { get; set; }
        public int AccountId { get; set; }
        public int RatePoint { get; set; }

        [JsonIgnore]
        public virtual Recipe? Recipe { get; set; }
        [JsonIgnore]
        public virtual AccountDTO? Account { get; set; }
    }
}
