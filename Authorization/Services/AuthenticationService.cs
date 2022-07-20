
using Common.Dtos.Authorization.Models;
using Common.Dtos.Models;

namespace Authorization.Services;
public class AuthService : IAuthService
{
    private readonly DapperContext _dbContext;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private readonly IConfiguration _config;
    public AuthService(DapperContext dbContext, ILogger<AuthService> logger, IConfiguration config)
    {
        _dbContext = dbContext;
        _logger = logger;
        _config = config;
    }
    public async Task<ResponseData<LoginResponse>> LoginAsync(LoginUserRequest request)
    {
       _logger.LogInformation($"Authentication service started for user {request.Email} ");
        var connection = _dbContext.CreateConnection();
        var response = new ResponseData<LoginResponse>();
        
        try
        {
            _logger.LogInformation($"Db Connection established for user {request.Email} ");
            var issuer = _config.GetValue<string>("Jwt:Issuer");
            var audience = _config.GetValue<string>("Jwt:Audience");
            var key = _config.GetValue<string>("Jwt:Key");
            var queryFindEmail = "SELECT * FROM [dbo].[User] WHERE Email = @Email";
            _logger.LogInformation($"Db querying email exist for user {request.Email} ");
            var result = await connection.QuerySingleOrDefaultAsync<User>(queryFindEmail, new { request.Email });
            if (result is null)
            {
                _logger.LogInformation($"{request.Email} doesn't exist");
                throw new Exception("Invalid credentials");
            }
             _logger.LogInformation($"password matching for user {request.Email} ");
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, result.Password);
             _logger.LogInformation($"password matching sucess for user {request.Email} ");
            if (!isPasswordValid)
            { 
                _logger.LogInformation($"password matching error for user {request.Email} ");
                throw new Exception("Invalid credentials");
            }
             _logger.LogInformation($"preparing auth token for user {request.Email} ");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var keyEncodedAscii = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", result.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, result.Email),
                new Claim("Tenant", result.Tenant),
                new Claim("Name", result.FirstName + " "+ result.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(6),
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyEncodedAscii), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            _logger.LogInformation($"token creation started for user {request.Email} ");
            var jwtToken = jwtTokenHandler.WriteToken(token);
            _logger.LogInformation($"token creation sucess for user {request.Email} ");
            response.Response = new LoginResponse();
            response.Response.AccessToken = jwtToken;
            response.Response.FirstName = result.FirstName;
            response.Response.LastName = result.LastName;
            response.Response.Id = result.Id;
            _logger.LogInformation($"Returning response for user {request.Email} ");
            return response;
        }
        catch(Exception ex)
        {
           response.Error.Message = ex.Message;
           response.Error.StatusCode = StatusCodes.Status417ExpectationFailed;
           _logger.LogError($"Error:- {ex.Data}{ex.Message} found for user");
           return response;
        }
        finally
        {
           connection.Dispose();
        }
        
    }

    public async Task<ResponseData<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        _logger.LogInformation($"Register service started for user {request.Email} ");
        var connection = _dbContext.CreateConnection();
        var responseData = new ResponseData<RegisterResponse>();
        try
        {
            _logger.LogInformation($"Db Connection established for user {request.Email} ");
            var queryFindEmail = "SELECT Email FROM [dbo].[User] WHERE Email = @Email";
            var queryFindPhone = "SELECT PhoneNumber From [dbo].[User] WHERE PhoneNumber = @PhoneNumber";
            var queryCreateUser = "INSERT INTO [dbo].[User](Email, Password, FirstName, LastName, PhoneNumber, IsActive, Tenant, ProfilePhotoURL, RefreshToken, CreatedDate, CreatedUser) OUTPUT INSERTED.[Id] VALUES (@Email, @Password, @FirstName, @LastName, @PhoneNumber, @IsActive, @Tenant, @ProfilePhotoURL, @RefreshToken, @CreatedDate, @CreatedUser)";

            _logger.LogInformation($"Email checking is already exist for user {request.Email} ");
            var resultEmailCheck = await connection.QuerySingleOrDefaultAsync<string>(queryFindEmail, new { request.Email });
            if (!string.IsNullOrEmpty(resultEmailCheck))
            {
                _logger.LogInformation($"Email already exist for user {request.Email} ");
                throw new Exception("Email already exist");
            }

            _logger.LogInformation($"Phone checking already exist for user {request.Email} ");
            var resultPhoneCheck = await connection.QuerySingleOrDefaultAsync<string>(queryFindPhone, new { request.PhoneNumber });

            if (!string.IsNullOrEmpty(resultPhoneCheck))
            {
                _logger.LogInformation($"Phone already exist for user {request.Email} ");
                throw new Exception("PhoneNumber already exist");
            }
            _logger.LogInformation($"Encrypting password for user {request.Email} ");
            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _logger.LogInformation($"Set created date for user {request.Email} ");
            request.CreatedDate = DateTime.Now;

            _logger.LogInformation($"Creating account for user {request.Email} ");
            var resultCreateUser = await connection.ExecuteScalarAsync(queryCreateUser, request);
            if (resultCreateUser is not Guid)
            {
                _logger.LogError($"Guid not returnd after creation for user {request.Email} ");
                throw new Exception("Error in creating user");
            }
            _logger.LogInformation($"Account created sucessfully for user {request.Email} ");
            responseData.Response = new RegisterResponse();
            responseData.Response.Id = resultCreateUser.ToString();
            return responseData;
           
        }
        catch (Exception ex)
        {
            _logger.LogError($" Error:- {ex.Data}{ex.Message} found for user {request.Email} ");
            responseData.Error.Message = ex.Message.ToString();
            responseData.Error.StatusCode = StatusCodes.Status417ExpectationFailed;
            return responseData;

        }
        finally
        {
            _logger.LogInformation($"Db connection disposed after request to create user {request.Email} ");
            connection.Dispose();
        }

    }
}
