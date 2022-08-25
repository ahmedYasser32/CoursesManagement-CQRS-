using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Features.Roles.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.Roles.Queries
{
  public class GetAllRolesQuery : IRequest<IEnumerable<RoleDTO>>
  {
  }
  public class Handler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDTO>>
  {
    private readonly IMapper mapper;
    private readonly RoleManager<IdentityRole> roleManager;
    public Handler(IMapper mapper, RoleManager<IdentityRole> roleMgr )
    {
        this.roleManager = roleMgr;
        this.mapper = mapper;
  
    }



    public  async Task<IEnumerable<RoleDTO>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
      return  roleManager.Roles.ProjectTo<RoleDTO>(mapper.ConfigurationProvider).ToList();
    }
  }
}

