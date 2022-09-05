using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.Books.Models
{
  public class CreateBookDTO
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Filepath { get; set; }
    
    public IFormFile  File { get; set; }

  }
}
