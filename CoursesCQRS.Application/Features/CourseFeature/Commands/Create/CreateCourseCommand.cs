

using CoursesCQRS.Application.Features.CourseFeature.Models;



namespace CoursesCQRS.Application.Features.CourseFeature.Commands.Create;
public class CreateCourseCommand :CourseCreateDTO, IRequest<int>
  {

  }
public class Handler : IRequestHandler<CreateCourseCommand, int>
{
  private readonly ApplicationContext db;
  public Handler(ApplicationContext db)
  {

    this.db = db;
  }

  public async Task<int> Handle(CreateCourseCommand obj, CancellationToken cancellationToken)
  {
    try
    {

      var entity = new Course()
      {
        Description = obj.Description,
        Name = obj.Name,
        hours = obj.hours,
        CategoryId = obj.CategoryId,
        TeacherId = obj.TeacherId,
        CreatedAt = DateTime.UtcNow,
      };
    
     

      await db.Course.AddAsync(entity);


      await db.SaveChangesAsync();

  

      return entity.Id;
    }

    catch (Exception)
    {

      throw;
    }
  }
}
