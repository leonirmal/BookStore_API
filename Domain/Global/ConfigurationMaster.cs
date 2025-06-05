namespace BookStoreAPI.Domain.Global
{
    public class ConfigurationMaster
    {
        public ConfigurationMaster()
        {
            Logging = new LoggingConfigurations();
            ConnectionStrings = new ConnectionStringConfigurations();
            JWTKeyes = new JWTKeyeConfigurations();
        }

        public LoggingConfigurations Logging { get; set; }
        public ConnectionStringConfigurations ConnectionStrings { get; set; }
        public JWTKeyeConfigurations JWTKeyes { get; set; }
    }
}
