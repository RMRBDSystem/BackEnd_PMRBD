using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class BookOrderStatus
    {
        public int BookOrderStatusId { get; set; }
        public int BookOrderId { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }
        public virtual BookOrder? BookOrder { get; set; }
    }
}
