using CsvHelper;
using CsvTask.Models;
using System.Globalization;
using System.Text;

namespace CsvTask.Services
{
    public class FileService : IFileService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<T>();
            return records;
        }

        public Stream WriteCSV<T>(List<T> records)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            var csvWriter = new CsvWriter(streamWriter,
                new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));

            csvWriter.WriteRecords(records);
            streamWriter.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
    }
}
