namespace BookStoreAPI.Domain.Global
{
    public class LoggingConfigurations
    {
        public LogLevel LogLevel { get; set; }
        public bool LogWriteTrace { get; set; }
        public bool LogWriteResponse { get; set; }
        public bool LogWriteRequest { get; set; }
        public LoggingConfigurations()
        {
            LogLevel = new LogLevel();
        }
    }
}
