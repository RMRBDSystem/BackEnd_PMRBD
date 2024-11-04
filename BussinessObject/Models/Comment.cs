using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }

    public int? RootCommentId { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public int? Status { get; set; }

    public int? BookId { get; set; }

    public int? EbookId { get; set; }

    public int? CustomerId { get; set; }

    public int? RecipeId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Account? Customer { get; set; }

    public virtual Ebook? Ebook { get; set; }

    public virtual ICollection<Comment> InverseRootComment { get; set; } = new List<Comment>();

    public virtual Recipe? Recipe { get; set; }

    public virtual Comment? RootComment { get; set; }
}
