using CoursesCQRS.Application.Common.Mapping;
using CoursesCQRS.Application.Features.CourseFeature.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CategoryFeature.Models
{
  public class CategoryDTO :  IMapFrom<Category>
  {
    public CategoryDTO()
    {
      Courses = new HashSet<CourseCreateDTO>();
    }

    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }


    public IEnumerable<CourseCreateDTO>? Courses { get; set; }
  }
}
