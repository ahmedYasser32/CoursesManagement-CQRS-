using CoursesCQRS.Application.Common.Mapping;
using CoursesCQRS.Application.Features.CategoryFeature.Models;
using CoursesCQRS.Application.Features.StudentFeature.Models;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CourseFeature.Models
{
  public class CourseGetDTO:  IMapFrom<Course>
  {

    public CourseGetDTO()
    {
      Students = new HashSet<StudentCreateDTO>();
      CategoryDTO Category = new CategoryDTO();
      TeacherCreateDTO teacher = new TeacherCreateDTO();

    }

    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public short hours { get; set; }


    public int CategoryId { get; set; }

    public CategoryDTO? Category { get; set; }

    public IEnumerable<StudentCreateDTO>? Students { get; set; }

    public int? TeacherId { get; set; }

    public TeacherCreateDTO? Teacher { get; set; }
  }
}
