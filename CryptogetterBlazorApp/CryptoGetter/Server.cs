namespace CryptogetterBlazorApp.CryptoGetter
{
    public class Server
    {
        public string Name { get; set; }
        public string FQN { get; set; }
        public string DBName { get; set; }
        public ServerType Type { get; set; }
    }

    public enum ServerType { Antares, Medtech };
}
