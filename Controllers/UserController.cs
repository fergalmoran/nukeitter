using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nukitter.Web.Areas.Identity.Data;

namespace Nukitter.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
  private readonly UserManager<NukeItterUser> _userManager;

  public UserController(UserManager<NukeItterUser> userManager) {
    _userManager = userManager;
  }

  [HttpGet("twitter")]
  public async Task TwitterSignIn() {
    await HttpContext.ChallengeAsync(TwitterDefaults.AuthenticationScheme, new AuthenticationProperties {
      RedirectUri = "https://nukeitter.dev.fergl.ie:5001/user/twitter-redirect"
    });
  }

  [HttpGet("twitter-redirect")]
  public async Task<IActionResult> TwitterRedirect() {
    var twitterHandle = HttpContext.User.Identity?.Name;
    if (string.IsNullOrEmpty(twitterHandle)) {
      return NotFound();
    }

    var user = await _userManager.FindByNameAsync(twitterHandle);

    if (user is null) {
      await _userManager.CreateAsync(new NukeItterUser(twitterHandle));
    }

    await HttpContext.SignInAsync(HttpContext.User);
    Console.WriteLine($"Let's see what we get?\n{HttpContext.User}");
    return Redirect("/setup");
  }
}
