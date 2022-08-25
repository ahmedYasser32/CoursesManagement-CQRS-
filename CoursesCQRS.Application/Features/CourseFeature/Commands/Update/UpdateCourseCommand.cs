using CoursesCQRS.Application.Features.CourseFeature.Models;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CourseFeature.Commands.Update
{
  public class UpdateCourseCommand : CourseUpdateDTO, IRequest<CourseUpdateDTO>
  {
  }
  public class Handler : IRequestHandler<UpdateCourseCommand, CourseUpdateDTO>
  {
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }

    public async Task<CourseUpdateDTO> Handle(UpdateCourseCommand obj, CancellationToken cancellationToken)
    {
      try

      {
        var entity = await db.Course.FindAsync(obj.Id);

        entity.Name = obj.Name;
        entity.ModifiedAt = DateTime.UtcNow;


        entity.hours = obj.hours;

        entity.CategoryId = obj.CategoryId;
        entity.ModifiedAt = DateTime.Now;
        entity.TeacherId = obj.TeacherId;
      

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

