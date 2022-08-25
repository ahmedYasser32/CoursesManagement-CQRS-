using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CourseFeature.Commands.Delete
{
  public class DeleteCourseCommand:  IRequest<int>
  {
    public int Id { get; set; } 
  }


  public class Handler : IRequestHandler<DeleteCourseCommand, int>
  {
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }
    public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
      try
      {

        var entity = db.Course.Find(request.Id);
        if (entity == null) return 0;
        db.Course.Remove(entity);
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
