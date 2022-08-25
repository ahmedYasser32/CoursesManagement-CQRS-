using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.AccountFeature.Models
{
  public class loginDTO
  {

    [Required(ErrorMessage = "Email Required")]
    [EmailAddress(ErrorMessage = "invalid email")]
    public string UserName { get; set; }


    [MinLength(6, ErrorMessage = "Min len 6 ")]
    [Required(ErrorMessage = "password Required")]
    public string password { get; set; }

  }
}
