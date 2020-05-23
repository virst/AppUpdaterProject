using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppUpdaterService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GetFileController
    {
        private readonly ILogger<GetFileController> _logger;

        public GetFileController(ILogger<GetFileController> logger)
        {
            _logger = logger;
        }


        [HttpGet("{app}/{fn}")]
        [Route("Stream")]
        public IActionResult Get(string app, int fn)
        {
            var a = Program.Ai[app];
            string fl =
            Path.Combine(a.DirectoryPath, a.Files[fn].FilePath.TrimStart('.').TrimStart(a.Splitter));
            Stream stream = File.OpenRead(fl);
            string mimeType = "application/bin";
            return new FileStreamResult(stream, mimeType)
            {
                FileDownloadName = Path.GetFileName(fl)
            };

        }
    }
}
