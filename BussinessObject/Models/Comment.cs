using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }

    public int? RootCommentId { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; } = DateTime.Now;

    public int? Status { get; set; }

    public int? BookId { get; set; }

    public int? EbookId { get; set; }

    public int? CustomerId { get; set; }

    public int? RecipeId { get; set; }

    [JsonIgnore]
    public virtual Book? Book { get; set; }

    [JsonIgnore]
    public virtual Account? Customer { get; set; }

    [JsonIgnore]
    public virtual Ebook? Ebook { get; set; }

    public virtual ICollection<Comment> InverseRootComment { get; set; } = new List<Comment>();

    [JsonIgnore]
    public virtual Recipe? Recipe { get; set; }

    [JsonIgnore]
    public virtual Comment? RootComment { get; set; }
}
