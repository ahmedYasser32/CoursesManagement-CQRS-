using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Domain.Entity
{
  public class Teacher: BaseAuditableEntity
  {
    public Teacher()
    {
      Manages = new HashSet<Teacher>();
    }

    [Key]
    public int Id { get; set; } 
    public int GenderId { get; set; }

   
    public string Name { get; set; }

   
    public string Title { get; set; }



    public short Age { get; set; }

   
    public List<Course>? Courses { get; set; }

    public int? ManagerId { get; set; }

    public Teacher? Manager { get; set; }

    public bool ismanager { get; set; }

    public ICollection<Teacher>? Manages { get; set; }


  }
}
