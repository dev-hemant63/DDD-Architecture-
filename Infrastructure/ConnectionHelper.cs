namespace Infrastructure
{
    public interface ConnectionHelper
    {
        string connectionString { get; set; }
    }
    public class ConnectionProvidor : ConnectionHelper
    {
        public string connectionString { get; set; } = string.Empty;
    }
}
