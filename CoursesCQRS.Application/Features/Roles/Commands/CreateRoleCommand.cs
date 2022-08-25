using CoursesCQRS.Application.Features.Roles.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.Roles.Commands
{
  public class CreateRoleCommand : RoleModificationDTO, IRequest<String>
  {
  }

  public class Handler : IRequestHandler<CreateRoleCommand, String>
  {
    private readonly RoleManager<IdentityRole> roleManager;
    public Handler(RoleManager<IdentityRole> roleMgr )
    {

      this.roleManager = roleMgr;
    }
    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {

      try
      {
     
          IdentityResult result = await roleManager.CreateAsync(new IdentityRole(request.RoleName));
          if (result.Succeeded)
            return request.RoleName;

         return "Error,Not saved";
        }

      catch (Exception ex)
      {
        return ex.Message;
      }
    }
  }
  }

