using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.StudentFeature.Commands.Delete
{
  public class DeleteStudentCommand:  IRequest<int>
  {
    public int Id { get; set; }
  }


  public class Handler : IRequestHandler<DeleteStudentCommand, int>
  {
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }
    public async Task<int> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
      try
      {

        var entity = db.Student.Find(request.Id);
        if (entity == null) return 0;
        db.Student.Remove(entity);
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
