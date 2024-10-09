using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class BookTransaction
    {
        public int BookTransactionId { get; set; }

        public int? CustomerId { get; set; }

        public int? BookOrderId { get; set; }

        public decimal? MoneyFluctuations { get; set; }

        public int? CoinFluctuations { get; set; }

        public DateTime? Date { get; set; }

        public string? Details { get; set; }

        public int? Status { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual BookOrder? BookOrder { get; set; }
    }
}
