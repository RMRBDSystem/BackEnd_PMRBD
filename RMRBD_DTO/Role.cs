using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public partial class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<AccountDTO> Accounts { get; set; } = new List<AccountDTO>();
}
