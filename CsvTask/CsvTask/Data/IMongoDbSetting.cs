namespace CsvTask.Data
{
    public interface IMongoDbSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
