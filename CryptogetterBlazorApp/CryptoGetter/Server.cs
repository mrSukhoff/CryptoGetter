namespace CryptogetterBlazorApp.CryptoGetter
{
    public class Server
    {
        public required string Name { get; set; }
        public required string FQN { get; set; }
        public required string DBName { get; set; }
        public ServerType Type { get; set; }
    }

    public enum ServerType { Antares, Medtech };
}
