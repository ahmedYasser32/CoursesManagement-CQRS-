using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.AccountFeature.Models
{
  public class RegistirationDTO
  {
    [Required(ErrorMessage = "Email Required")]
    [EmailAddress(ErrorMessage = "invalid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "fname Required")]
    public string firstname { get; set; }

    [Required(ErrorMessage = "lname Required")]
    public string lastname { get; set; }

    [MinLength(6, ErrorMessage = "Min len 6 ")]
    [Required(ErrorMessage = "password Required")]
    public string password { get; set; }

    [MinLength(6, ErrorMessage = "Min len 6 ")]
    [Required(ErrorMessage = "password Required")]
    [Compare("password", ErrorMessage = "Password does not match")]
    public string confirmpassword { get; set; }

    public bool isAdmin { get; set; }
  }
}
