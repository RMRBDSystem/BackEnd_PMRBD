﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookOrderDetailRepository
    {
        Task<IEnumerable<BookOrderDetail>> GetAllBookOrderDetails();
        Task<IEnumerable<BookOrderDetail>> GetBookOrderDetailByOrderId(int orderId);
        Task<BookOrderDetail> GetBookOrderDetailByOrderIdAndBookId(int OrderId, int BookId);
        Task AddBookOrderDetail(BookOrderDetail bookOrderDetail);
        Task DeleteBookOrderDetail(int bookId, int orderId);
        Task UpdateBookOrderDetail(BookOrderDetail bookOrderDetail);
    }
}