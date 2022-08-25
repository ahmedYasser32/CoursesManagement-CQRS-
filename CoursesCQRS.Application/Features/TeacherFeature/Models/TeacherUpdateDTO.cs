using CoursesCQRS.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.TeacherFeature.Models
{
  public class TeacherUpdateDTO:IMapFrom<Teacher>
  {

    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Title { get; set; }

    public int GenderId { get; set; }


    public short Age { get; set; }

    public int? ManagerId { get; set; }
    public bool ismanager { get; set; }

    public List<int>? ManagedIds { get; set; }


  }
}
