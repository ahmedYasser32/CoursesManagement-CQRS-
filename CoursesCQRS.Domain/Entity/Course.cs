using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Domain.Entity
{
  public class Course: BaseAuditableEntity
  {

    public Course()
    {
      Students = new HashSet<Student>();
      Category Category = new Category();
      Teacher teacher = new Teacher();

    }

    public int Id { get; set; }

    public string Name { get; set; }

    
    public string? Description { get; set; }

    
    public short hours { get; set; }


    public int? CategoryId { get; set; }

    public Category? Category { get; set; }

    public IEnumerable<Student>? Students { get; set; }

    public int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }


  }
}
