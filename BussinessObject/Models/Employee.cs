using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Email { get; set; }

    public string Name { get; set; } = null!;

    public string? FrontIdcard { get; set; }

    public string? BackIdcard { get; set; }

    public string? Portrait { get; set; }

    public string? IdcardNumBer { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Status { get; set; }

    public int? EmployeeTypeId { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Ebook> Ebooks { get; set; } = new List<Ebook>();

    public virtual EmployeeType? EmployeeType { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<ServiceFeedBack> ServiceFeedBacks { get; set; } = new List<ServiceFeedBack>();
}
