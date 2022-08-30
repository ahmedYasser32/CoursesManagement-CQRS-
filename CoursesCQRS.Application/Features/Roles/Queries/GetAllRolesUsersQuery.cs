using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Features.Roles.Models;
using CoursesCQRS.Infrastructure.Extend;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.Roles.Queries
{
  public class GetAllRolesUsersQuery : RoleModificationDTO, IRequest<RoleUsersDTO>
  {

  }
  public class QueryHandler : IRequestHandler<GetAllRolesUsersQuery, RoleUsersDTO>
  {

    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public QueryHandler(UserManager<ApplicationUser> userMgr, RoleManager<IdentityRole> roleMgr)
    {
      this.roleManager = roleMgr;
      this.userManager = userMgr;
    }


    public async Task<RoleUsersDTO> Handle(GetAllRolesUsersQuery request, CancellationToken cancellationToken)
    {
      IdentityRole role = await roleManager.FindByIdAsync(request.RoleId);
      List<ApplicationUser> members = new List<ApplicationUser>();
      List<ApplicationUser> nonMembers = new List<ApplicationUser>();
      foreach (ApplicationUser user in userManager.Users)
      {
        var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
        list.Add(user);
      }

      return (new RoleUsersDTO
      { 
        Role = role,
        Members = members,
        NonMembers = nonMembers
      });
    }
  }
}

