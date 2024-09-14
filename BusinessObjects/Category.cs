using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects;

public partial class Category
{
    public int CategoryId { get; set; }
    [Required]
    [StringLength(40)]
    public string CategoryName { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
