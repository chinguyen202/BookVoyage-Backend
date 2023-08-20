using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities;

public class OrderDetail: BaseEntity
{
    [Required]
    public Guid OrderHeaderId { get; set; }
    [Required]
    public Guid BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
    public string ItemName { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double Price { get; set; }
}