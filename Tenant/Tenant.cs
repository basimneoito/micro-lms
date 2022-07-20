using System;

namespace Tenants;

public class Tenant
{
   public Guid TenantId { get; set; }
   public Guid Owner { get; set; }
   public bool IsActive { get; set; }
   public string CompanyName { get; set; }
   public Guid SubscriptionId { get; set; }
}
