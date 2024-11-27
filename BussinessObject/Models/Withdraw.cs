using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public partial class Withdraw
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WithdrawId { get; set; }
        public int AccountId { get; set; }
        public decimal? Coin { get; set; }
        public decimal? Money { get; set; }
        public int? Status { get; set; }
        public virtual Account? Account { get; set; }
    }
}
