
using CoursesCQRS.Application.Common.Mapping;
using Microsoft.AspNetCore.Identity;

namespace CoursesCQRS.Application.Features.Roles.Models
{
  public class RoleDTO : IMapFrom<IdentityRole>
  {
   public String Id { get; set; }  
   public string? Name { get; set; } 
  }
}
