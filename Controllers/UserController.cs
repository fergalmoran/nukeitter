using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Mvc;

namespace Nukitter.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
  [HttpGet("twitter")]
  public async Task TwitterSignIn() {
    await HttpContext.ChallengeAsync(TwitterDefaults.AuthenticationScheme, new AuthenticationProperties {
      RedirectUri = "https://nukeitter.dev.fergl.ie:5001/profile"
    });
  }
}