using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CommentDAO : SingletonBase<CommentDAO>
    {
        public async Task<IEnumerable<Comment>> GetAllCommentsByRootCommentId(int id)
        {
            try
            {
                return await _context.Comments.Where(x => x.RootCommentId == id).ToListAsync();
            }catch (Exception ex)
            {
                throw new Exception("Failed to retrieve comments by root comment id", ex);
            }
        }

        public async Task<IEnumerable<Comment>> GetAllCommentByBookId(int id)
        {
            try
            {
                return await _context.Comments.Where(x => x.BookId == id).ToListAsync();
            }catch (Exception ex)
            {
                throw new Exception("Failed to retrieve comments by book id", ex);
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentByEBookId(int id)
        {
            try
            {
                return await _context.Comments.Where(x => x.EbookId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve comments by ebook id", ex);
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentByRecipeId(int id)
        {
            try
            {
                return await _context.Comments.Where(x => x.RecipeId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve comments by recipe id", ex);
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
                if (comment != null)
                {
                    _context.Entry(comment).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                throw new Exception("Failed to update comment", ex);
            }
        }
    }
}
