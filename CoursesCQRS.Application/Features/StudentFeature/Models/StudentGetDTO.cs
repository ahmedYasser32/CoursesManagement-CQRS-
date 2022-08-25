using CoursesCQRS.Application.Common.Mapping;
using CoursesCQRS.Application.Features.CourseFeature.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.StudentFeature.Models
{
  public class StudentGetDTO : IMapFrom<Student>
  {
    public StudentGetDTO()
    {
      Courses = new HashSet<CourseCreateDTO>();
    }
    
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public int GenderId { get; set; } 

    [Required]
    public short Age { get; set; }


    public ICollection<CourseCreateDTO> Courses { get; set; }



  }
}
