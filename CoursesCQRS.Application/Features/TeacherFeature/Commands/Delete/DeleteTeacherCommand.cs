using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.TeacherFeature.Commands.Delete
{
  public class DeleteTeacherCommand:  IRequest<int>
  {
    public int Id { get; set; }
  }


  public class Handler : IRequestHandler<DeleteTeacherCommand, int>
  {
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }
    public async Task<int> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
      try
      {

        var entity = db.Teacher.Find(request.Id);
        if (entity == null) return 0;
        db.Teacher.Remove(entity);
        await db.SaveChangesAsync();
        return 1;

      }

      catch (Exception)
      {

        throw;
      }
    }
  }
}
