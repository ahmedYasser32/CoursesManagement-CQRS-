using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Domain.Entity
{
  public class Category : BaseAuditableEntity
  {
    public Category()
    {
      Courses = new HashSet<Course>();
    }

    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }

    public ICollection<Course>? Courses { get; set; }

  }
}
