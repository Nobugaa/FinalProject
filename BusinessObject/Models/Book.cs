using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? AddedBy { get; set; }

    public virtual User? AddedByNavigation { get; set; }

    public virtual ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
