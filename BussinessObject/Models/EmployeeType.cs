﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class EmployeeType
{
    public int EmployeeTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public int? Status { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
