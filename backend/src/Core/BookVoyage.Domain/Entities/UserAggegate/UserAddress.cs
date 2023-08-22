using BookVoyage.Domain.Common;

namespace BookVoyage.Domain.Entities.UserAggegate;

public class UserAddress: Address
{
    public string Id { get; set; }
}