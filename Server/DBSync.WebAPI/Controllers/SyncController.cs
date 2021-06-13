using System.Threading.Tasks;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Mvc;

namespace DBSync.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : ControllerBase
    {
        public SyncController(WebServerManager webServerManager) => _webServerManager = webServerManager;

        /// <summary>
        /// This POST handler is mandatory to handle all the sync process
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task Post() => await _webServerManager.HandleRequestAsync(HttpContext);

        /// <summary>
        /// This GET handler is optional. It allows you to see the configuration hosted on the server
        /// The configuration is shown only if Environmenent == Development
        /// </summary>
        [HttpGet]
        public async Task Get() => await _webServerManager.HandleRequestAsync(HttpContext);

        private readonly WebServerManager _webServerManager;
    }
}
