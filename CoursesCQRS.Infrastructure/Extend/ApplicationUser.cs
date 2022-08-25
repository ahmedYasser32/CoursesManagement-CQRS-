using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Infrastructure.Extend
{
  public class ApplicationUser : IdentityUser
  {
    public string firstname { get; set; }
    public string lastname { get; set; }

    public bool isAdmin { get; set; }

  }
}
