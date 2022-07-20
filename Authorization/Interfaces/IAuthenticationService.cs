
using Common.Dtos.Authorization.Models;
using Common.Dtos.Models;

namespace Authorization.Interfaces;
public interface IAuthService {

    public Task<ResponseData<LoginResponse>> LoginAsync(LoginUserRequest request);
    public Task<ResponseData<RegisterResponse>> RegisterAsync(RegisterRequest request);
}