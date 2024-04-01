namespace IotRemoteLab.API;

public static class ConfigPath
{
    public const string Salt = "Auth:HashConfig:Salt";
    public const string NumberOfIterationForHash = "Auth:HashConfig:Iteration";
    public const string HashLength = "Auth:HashConfig:Length";
    public const string JwtKey = "Auth:JwtKey";
}