using CoursesCQRS.Application.Common.Mapping;
using CoursesCQRS.Application.Features.CourseFeature.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.TeacherFeature.Models
{
  public class TeacherGetDTO:IMapFrom<Teacher>
  {

    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Title { get; set; }

    public int GenderId { get; set; }

    [Required]
    public short Age { get; set; }
    public bool ismanager { get; set; }
    public ICollection<Teacher>? Manages { get; set; }

    public List<Course>? Courses { get; set; }
    public int? ManagerId { get; set; }

    public Teacher? Manager { get; set; }



  }
}
