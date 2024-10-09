using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class EbookTransaction
    {
        public int EbookTransactionId { get; set; }

        public int CustomerId { get; set; }

        public int EbookId { get; set; }

        public int CoinFluctuations { get; set; }

        public DateTime? Date { get; set; }

        public string? Details { get; set; }

        public int? Status { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual Ebook? Ebook { get; set; }
    }
}
