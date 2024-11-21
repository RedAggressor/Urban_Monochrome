namespace Nitifacation.Host.Configs
{
    public class CredentialConfig
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Host { get; set; } = null!;
        public int Port { get; set; }            
        public string? Name { get; set; }
    }
}
