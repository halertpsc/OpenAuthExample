using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAuthExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly GitHubClient _gitHubClient;

        public DataController(GitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient ?? throw new ArgumentNullException(nameof(gitHubClient));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepoInfo>>> GetData() 
        {
            var access_token = await HttpContext.GetTokenAsync("GitHub", "access_token");
            var data = await _gitHubClient.GetRepositories(access_token);
            return Ok(data);
        }
    }
}
