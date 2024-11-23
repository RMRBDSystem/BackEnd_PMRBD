using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    [PrimaryKey(nameof(OrderId), nameof(BookId))]
    public class BookOrderDetail
    {
        public int OrderId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int Status { get; set; }

        public virtual Book? Book { get; set; }

        public virtual BookOrder? BookOrder { get; set; }
    }
}
