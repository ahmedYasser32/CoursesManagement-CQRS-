using CoursesCQRS.Application.Features.AccountFeature.Commands;
using CoursesCQRS.Application.Features.AccountFeature.Models;
using CoursesCQRS.Infrastructure.Extend;
using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoursesCQRS.API.Controllers
{
  public class AccountController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> userManager;

    private readonly SignInManager<ApplicationUser> signInmanager;

    private readonly IMediator mediator;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInmanager, IMediator mediator)
    {
      this.userManager = userManager;
      this.signInmanager = signInmanager;
      this.mediator = mediator;
    }

    [HttpPost]

    [Route("~/api/accounts/register")]
    public async Task<IActionResult> Register([FromBody] RegistirationDTO model)
    { //manual mapping


      var user = new ApplicationUser()
      {
        UserName = model.Email,
        Email = model.Email,
        isAdmin = model.isAdmin,
        firstname = model.firstname,
        lastname = model.lastname
      };

      var result = await userManager.CreateAsync(user, model.password);
      if (result.Succeeded)
      {
        return Ok(user);
      }
      else
      {


        return BadRequest(result.Errors);

      }
    }

      [HttpPost]

      [Route("~/api/accounts/login")]
      public async Task<IActionResult> Login([FromBody] CreateTokenCommand command)
      {
        var token = await mediator.Send(command);

        if (token == null)
        {
          return Unauthorized();
        }
        Console.WriteLine(token);
        return Ok(token);


      }
  
  
  
  
  
  
  }
  }

