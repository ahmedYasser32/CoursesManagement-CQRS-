using CoursesCQRS.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.StudentFeature.Models
{
  public class StudentCreateDTO: IMapFrom<Student>
  {
    

    [Required]
    public string Name { get; set; }
    public int GenderId { get; set; }

    [Required]
    public short Age { get; set; }


    public List<int>? CoursesIds { get; set; }
  }
}
