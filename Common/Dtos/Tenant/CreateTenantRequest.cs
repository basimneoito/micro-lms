namespace Common.Dtos.Tenant;

public class CreateTenantRequest
{
   public Guid Owner { get; set; }
   public bool IsActive { get; set; }
   public string CompanyName { get; set; }
   public Guid SubscriptionId { get; set; }
}