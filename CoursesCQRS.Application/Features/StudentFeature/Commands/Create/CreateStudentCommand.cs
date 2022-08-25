

using CoursesCQRS.Application.Features.StudentFeature.Models;



namespace CoursesCQRS.Application.Features.StudentFeature.Commands.Create;
public class CreateStudentCommand :StudentCreateDTO, IRequest<int>
  {

  }
public class Handler : IRequestHandler<CreateStudentCommand, int>
{
  private readonly ApplicationContext db;
  public Handler(ApplicationContext db)
  {

    this.db = db;
  }

  public async Task<int> Handle(CreateStudentCommand obj, CancellationToken cancellationToken)
  {
    try
    {

      var entity = new Student
      {
        Age = obj.Age,
        Name = obj.Name,
        CreatedAt = DateTime.UtcNow,
        GenderId = obj.GenderId,
      };

      List<Course> courses = new();

      if (obj.CoursesIds != null)
      {
        foreach (var x in obj.CoursesIds)
        {
          var course = await db.Course.FindAsync(x);

          entity.Courses.Add(course);
        }
      }
     

      await db.Student.AddAsync(entity);


      await db.SaveChangesAsync();

  

      return entity.Id;
    }

    catch (Exception)
    {

      throw;
    }
  }
}
