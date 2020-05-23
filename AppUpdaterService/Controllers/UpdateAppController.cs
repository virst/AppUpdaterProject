using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppUpdaterService.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class UpdateAppController
    {
        private readonly ILogger<UpdateAppController> _logger;

        public UpdateAppController(ILogger<UpdateAppController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public AppsInformation Get()
        {
            return Program.Ai;
        }

        [HttpGet("{app}")]
        public AppInfo Get(string app)
        {
            return Program.Ai[app];
        }
    }
}
