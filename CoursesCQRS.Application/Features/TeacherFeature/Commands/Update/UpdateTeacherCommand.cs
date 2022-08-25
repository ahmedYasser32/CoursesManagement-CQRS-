using CoursesCQRS.Application.Features.TeacherFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.TeacherFeature.Commands.Update
{
  public class UpdateTeacherCommand : Teacher, IRequest<Teacher>
  {
  }
  public class Handler : IRequestHandler<UpdateTeacherCommand, Teacher>
  {
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }

    public async Task<Teacher> Handle(UpdateTeacherCommand obj, CancellationToken cancellationToken)
    {
      try
      {

        obj.ModifiedAt = DateTime.Now;
        db.Entry(obj).State = EntityState.Modified;

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

