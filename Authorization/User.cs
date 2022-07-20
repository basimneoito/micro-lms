namespace Authorization.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string IsActive { get; set; }

    public string Tenant { get; set; }

    public string ProfilePhotoURL { get; set; }

    public string RefreshToken { get; set; }
}