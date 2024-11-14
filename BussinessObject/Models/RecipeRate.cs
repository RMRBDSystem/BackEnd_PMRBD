using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    [PrimaryKey(nameof(RecipeId), nameof(AccountId))]
    public partial class RecipeRate
    {
        
        public int RecipeId { get; set; }
        public int AccountId { get; set; }
        public int RatePoint { get; set; }

        [JsonIgnore]
        public virtual Recipe? Recipe { get; set; }
        [JsonIgnore]
        public virtual Account? Account { get; set; }
    }
}
