using CoursesCQRS.Application.Features.Roles.Commands;
using CoursesCQRS.Application.Features.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoursesCQRS.API.Controllers
{
  public class RoleController : ControllerBase
  {
    private readonly IMediator mediator;
    private readonly RoleManager<IdentityRole> roleManager;
    public RoleController(IMediator mediator)
    {
      this.mediator = mediator;
    }


    [HttpPost]
    [Route("~/api/roles/Create")]
    public async Task<IActionResult> Create(CreateRoleCommand command)
    {

      if (ModelState.IsValid)
      {
        var data = await mediator.Send(command);
        return Ok(data);
      }

      return BadRequest();
    }


    [HttpGet]
    [Route("~/api/roles/View")]
    public async Task<IActionResult> GetRoles(GetAllRolesQuery query)
    {

      var data = await mediator.Send(query);
      if (data == null)
      {
        return BadRequest();
      }
      return Ok(data);
    }
     [HttpGet]
    [Route("~/api/roles/ViewUserRoles")]
    public async Task<IActionResult> GetUsersRoles(GetAllRolesUsersQuery query)
    {
      var data = await mediator.Send(query);
      if (data == null)
      {
        return BadRequest();
      }
      return Ok(data);
    }


    [Route("~/api/roles/Delete")]
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteRoleCommand command)
    {

      var data = await mediator.Send(command);
      if (data == 1) return NoContent();

      return BadRequest();

    }

    [Route("~/api/roles/AddUserRole")]
    [HttpPost]
    public async Task<IActionResult> AddUserRole(AddUserToRoleCommand command)
    {
      if (ModelState.IsValid)
      {
        var data = await mediator.Send(command);
        return Ok(data);
      }
      return BadRequest();
    }

    [Route("~/api/roles/RemoveUserRole")]
    [HttpPost]
    public async Task<IActionResult> RemoveUserRole(RemoveUserFromRoleCommand command)
    {
      if (ModelState.IsValid)
      {
        var data = await mediator.Send(command);
        return Ok(data);

      }

      return BadRequest();
    }

  }
  }

    
  

