using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CourseFeature.Models
{
  public class CourseUpdateDTO
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public short hours { get; set; }

    public int? TeacherId { get; set; }

    public int? CategoryId { get; set; }
  }
}
