namespace Common.Dtos.Authorization.Models;

public class LoginResponse
{
    public Guid Id { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
