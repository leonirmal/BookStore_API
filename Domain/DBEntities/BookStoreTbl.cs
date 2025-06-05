using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPI.Domain.DBEntities;

[Table("BookStoreTbl", Schema = "BKS")]
public partial class BookStoreTbl
{
    public int Id { get; set; }
    public string? Publisher { get; set; }
    public string? Title { get; set; }
    public string? AuthorLastName { get; set; }
    public string? AuthorFirstName { get; set; }
    public decimal? Price { get; set; }
}
