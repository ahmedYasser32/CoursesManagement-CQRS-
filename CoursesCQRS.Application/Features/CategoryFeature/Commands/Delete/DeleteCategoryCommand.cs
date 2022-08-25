using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CategoryFeature.Commands.Delete
{
  public class DeleteCategoryCommand:  IRequest<int>
  {
    public int Id { get; set; }
  }


  public class Handler : IRequestHandler<DeleteCategoryCommand, int>
  {
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }
    public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
      try
      {

        var entity = db.Category.Find(request.Id);
        if (entity == null) return 0;
        db.Category.Remove(entity);
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
