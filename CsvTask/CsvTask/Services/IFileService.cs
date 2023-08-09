namespace CsvTask.Services
{
    public interface IFileService
    {
        IEnumerable<T> ReadCSV<T>(Stream file);
        Stream WriteCSV<T>(List<T> records);
    }
}
