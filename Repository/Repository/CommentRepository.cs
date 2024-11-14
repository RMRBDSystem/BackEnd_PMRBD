using BusinessObject.Models;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public async Task<Comment> GetCommentById(int id) => await CommentDAO.Instance.GetCommentById(id);
        public async Task<IEnumerable<Comment>> GetAllComments() => await CommentDAO.Instance.GetAllComments();
        public async Task AddComment(Comment comment) => await CommentDAO.Instance.AddComment(comment);
        public async Task UpdateComment(Comment comment) => await CommentDAO.Instance.UpdateComment(comment);
        public async Task DeleteComment(int id) => await CommentDAO.Instance.DeleteComment(id);
    }
}
