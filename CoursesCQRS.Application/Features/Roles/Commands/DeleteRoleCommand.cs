using CoursesCQRS.Application.Features.Roles.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.Roles.Commands
{
  public class DeleteRoleCommand : RoleDTO, IRequest<int>
  {
  }

  public class DeleteHandler : IRequestHandler<DeleteRoleCommand, int>
  {
    private readonly RoleManager<IdentityRole> roleManager;
    public DeleteHandler(RoleManager<IdentityRole> roleMgr)
    {

      this.roleManager = roleMgr;
    }
    public async Task<int> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
      try
      {
        IdentityRole role = await roleManager.FindByIdAsync(request.Id);
        if (role != null)
        {
          IdentityResult result = await roleManager.DeleteAsync(role);
          if (result.Succeeded)
            return 1;
        }
        return 0;
      }
      catch (Exception ex)
      {
        return -1;
   
      }
    }
  }
}

