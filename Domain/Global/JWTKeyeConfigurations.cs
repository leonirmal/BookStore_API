namespace BookStoreAPI.Domain.Global
{
    public class JWTKeyeConfigurations
    {
        public bool IsEncrypt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JWTKeyeConfigurations()
        {
            AllowedOrigins = new List<string>();
        }        
        private List<string> allowedOrigins;

        public List<string> AllowedOrigins
        {
            get
            {
                return allowedOrigins;
            }
            set
            {
                allowedOrigins = new List<string>();
                foreach (var item in value)
                {
                    allowedOrigins.Add(item);
                }

            }
        }
    }
}
