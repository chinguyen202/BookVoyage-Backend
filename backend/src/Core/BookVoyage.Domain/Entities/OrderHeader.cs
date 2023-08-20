using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities;

public class OrderHeader: AuditableBaseEntity
{
    [Required]
    public string PickupName { get; set; }
    [Required]
    public string PickupPhoneNumber { get; set; }
    [Required]
    public string PickupEmail { get; set; }
    public double OrderTotal { get; set; }
    public int OrderTotalQuantity { get; set; }
    public string Status { get; set; }
    public string StripePaymentIntentId { get; set; }
    // Navigation properties
    [Required]
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public AppUser AppUser { get; set; }

    public IEnumerable<OrderDetail> OrderItems { get; set; }
}