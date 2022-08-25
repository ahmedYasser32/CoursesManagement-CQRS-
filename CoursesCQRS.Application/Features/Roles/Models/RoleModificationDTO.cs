
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.Roles.Models
{
  public class RoleModificationDTO
  {
    public string RoleName { get; set; }

    public string? RoleId { get; set; }

    public string[]? Ids { get; set; }

    
  }
}
