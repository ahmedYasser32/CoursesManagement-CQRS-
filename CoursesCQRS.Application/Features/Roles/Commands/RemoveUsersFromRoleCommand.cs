using CoursesCQRS.Application.Features.Roles.Models;
using CoursesCQRS.Infrastructure.Extend;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.Roles.Commands
{
  public class RemoveUserFromRoleCommand : RoleModificationDTO, IRequest<int>
  {

    public class Handler : IRequestHandler<RemoveUserFromRoleCommand, int>
    {
     // private readonly RoleManager<IdentityRole> roleManager;
      private readonly UserManager<ApplicationUser> userManager;
      public Handler( UserManager<ApplicationUser> userMgr) // RoleManager<IdentityRole> roleMgr,
      {

       // this.roleManager = roleMgr;
        this.userManager = userMgr;
      }

      public async Task<int> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
      {
        String Errors = "";
        try
        {
          IdentityResult result;

          foreach (string userId in request.Ids ?? new string[] { })
          {
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
              result = await userManager.RemoveFromRoleAsync(user, request.RoleName);
              if (!result.Succeeded)
                Errors = result.Errors.ToString();
            }

          }

          if (Errors.Length > 0)
            return 0;
          else
            return 1;
        }
        catch (Exception ex)
        {

          Console.WriteLine(ex.Message);

          return -1;
        }
      }
    }
  }
}

