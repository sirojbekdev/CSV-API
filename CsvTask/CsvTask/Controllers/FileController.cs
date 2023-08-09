using CsvTask.Models;
using CsvTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace CsvTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public FileController(IFileService fileService,
            IUserService userService)
        {
            _fileService = fileService;
            _userService = userService;
        }

        [HttpGet("write-csv")]
        public async Task<IActionResult> Write(SortType sort = SortType.ASC, int limit = 10)
        {
            var users = await _userService.GetUsers(sort, limit);
            var memoryStream = _fileService.WriteCSV<User>(users);

            return new FileStreamResult(memoryStream, "text/csv")
            {
                FileDownloadName = $"{DateTime.Now}.csv"
            };
        }

        [HttpPost("read-csv")]
        public async Task<IActionResult> Read([FromForm] IFormFileCollection file)
        {
            var users = _fileService.ReadCSV<User>(file[0].OpenReadStream());
            var result = await _userService.AddUsers(users);
            return Ok(users);
        }
    }
}