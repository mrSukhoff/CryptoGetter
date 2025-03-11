namespace CryptogetterBlazorApp.CryptoGetter
{
    public class Server
    {
        public required string Name { get; set; }
        public required string FQN { get; set; }
        public required string DBName { get; set; }
        public ServerType Type { get; set; }
		public required string GS1Prefix { get; set; } // Добавлено поле для префикса GS1
	}

    public enum ServerType { Antares, Medtech };
}
