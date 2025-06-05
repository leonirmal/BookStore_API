namespace BookStoreAPI.Domain.Global
{
    public class ConnectionStringConfigurations
    {
        public bool IsEncrypt { get; set; }

        private string connection;
        public string Connection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }
        private string connectionLite;
        public string ConnectionLite
        {
            get
            {
                return connectionLite;
            }
            set
            {
                connectionLite = value;
            }
        }
    }
}
