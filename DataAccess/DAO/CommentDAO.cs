using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CommentDAO : SingletonBase<CommentDAO>
    {
        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            try
            {
                return await _context.Comments.ToListAsync();
            }catch (Exception ex)
            {
                throw new Exception("Failed to retrieve comments", ex);
            }
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            try
            {
                return await _context.Comments.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve comment by id", ex);
            }
        }

        public async Task AddComment(Comment comment)
        {
            try
            {
                if (comment != null)
                {
                    await _context.Comments.AddAsync(comment);
                    await _context.SaveChangesAsync();
                }              
            }catch (Exception ex)
            {
                throw new Exception("Failed to add comment", ex);
            }
        }

        public async Task UpdateComment(Comment comment)
        {
            try
            {
                var existingItem = await GetCommentById(comment.CommentId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(comment);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to update comment", ex);
            }
        }
        public async Task DeleteComment(int id)
        {
            try
            {
                var comment = await GetCommentById(id);
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update comment", ex);
            }
        }
    }
}
