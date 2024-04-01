using System.Security.Claims;

namespace IotRemoteLab.API.Services;

public interface IJwtService
{
    string GenerateAccessToken(List<Claim> claims);
}