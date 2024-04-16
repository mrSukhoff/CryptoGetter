namespace CryptoGetterLibrary
{
    internal class Server
    {
        public string Name { get; set; }
        public string FQN { get; set; }
        public string DBName { get; set; }
        public ServerType Type { get; set; }
    }

    internal enum ServerType { Antares, Medtech };
}
