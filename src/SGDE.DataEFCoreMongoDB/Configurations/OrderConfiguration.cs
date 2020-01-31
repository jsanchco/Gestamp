namespace SGDE.DataEFCoreMongoDB.Configurations
{
    public class OrderConfiguration : IOrderConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IOrderConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
