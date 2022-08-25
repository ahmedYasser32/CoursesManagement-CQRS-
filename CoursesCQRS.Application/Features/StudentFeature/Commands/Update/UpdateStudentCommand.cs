using CoursesCQRS.Application.Features.StudentFeature.Models;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.StudentFeature.Commands.Update
{
  public class UpdateStudentCommand : StudentUpdateDTO, IRequest<StudentUpdateDTO>
  {
  }
  public class Handler : IRequestHandler<UpdateStudentCommand, StudentUpdateDTO>
  {
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }

    public async Task<StudentUpdateDTO> Handle(UpdateStudentCommand obj, CancellationToken cancellationToken)
    {
      try

      {
        var entity = await db.Student.FindAsync(obj.Id);

        entity.Age = obj.Age;
        entity.Name = obj.Name;
        entity.ModifiedAt = DateTime.UtcNow;
        entity.GenderId = obj.GenderId;
  
        

        if (obj.CoursesIds != null)
        {
          foreach (var x in obj.CoursesIds)
          {
            var course = await db.Course.FindAsync(x);

            entity.Courses.Add(course);
          }
        }



        entity.ModifiedAt = DateTime.Now;
       

        await db.SaveChangesAsync();
        return obj;

      }

      catch (Exception)
      {

        throw;
      }
    }
  }
}

