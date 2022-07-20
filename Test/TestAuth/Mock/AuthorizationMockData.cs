using Authorization.Models;

namespace Test.TestAuth.Mock;
public class AuthorizationMockData
{
    public static ResponseData<LoginResponse> GetLoginResponse() {
       var responseData = new ResponseData<LoginResponse>();
       responseData.Response = new LoginResponse {
          FirstName = "Basim",
          LastName = "E",
          Id = Guid.Parse("94fe60e4-20e6-47c0-b5a7-cf7f4257ce4f"),
          AccessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6Ijk0ZmU2MGU0LTIwZTYtNDdjMC1iNWE3LWNmN2Y0MjU3Y2U0ZiIsImVtYWlsIjoieWVzYmFzaW1AZ21haWwuY29tIiwiVGVuYW50Ijoicm9vdCIsIk5hbWUiOiJCYXNpbSBFIiwianRpIjoiNzZmMWVkZjItMjM0ZS00OTMwLThkNTItMmFhM2ZmZmY2NzIwIiwibmJmIjoxNjU2NzU1MTU5LCJleHAiOjE2NTY3NzY3NTgsImlhdCI6MTY1Njc1NTE1OSwiaXNzIjoibG1zcGxhdGZvcm0iLCJhdWQiOiJsbXNwbGF0Zm9ybSJ9.gAPRHS2EAwiKwSeNHx4CB6U4ABDvJ97oYEN7f85xgVfihuEZdbNsVyGBQ8BVcMJzxr_rZ6uc6rG9s5KLagbNBA",
          RefreshToken = ""

       };
       return responseData;
    }

    public static LoginUserRequest GetLoginRequest() {
       
       return new LoginUserRequest {
         Email = "basim.ind@gmail.com",
         Password = "Neoito#123"

       };
    }

      public static RegisterRequest GetRegisterRequest() {
       
       return new RegisterRequest {
         Email = "basim.ind@gmail.com",
         Password = "Neoito#123",
         FirstName = "Basim",
         LastName = "E",
         PhoneNumber = "9048594471",
         CreatedDate = DateTime.Now,
         CreatedUser = null

       };
    }

     public static ResponseData<RegisterResponse> GetRegisterResponse() {
       var responseData = new ResponseData<RegisterResponse>();
       responseData.Response =  new RegisterResponse {
         Id = Guid.NewGuid().ToString()
       };
       return responseData;
    }
}