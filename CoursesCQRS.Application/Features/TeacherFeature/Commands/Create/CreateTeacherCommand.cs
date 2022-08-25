

using CoursesCQRS.Application.Features.TeacherFeature.Models;


namespace CoursesCQRS.Application.Features.TeacherFeature.Commands.Create;
public class CreateTeacherCommand :TeacherCreateDTO, IRequest<int>
  {

  }
public class Handler : IRequestHandler<CreateTeacherCommand, int>
{
  private readonly ApplicationContext db;
  public Handler(ApplicationContext db)
  {

    this.db = db;
  }

  public async Task<int> Handle(CreateTeacherCommand obj, CancellationToken cancellationToken)
  {
    try
    {

      var entity = new Teacher
      {
        Age = obj.Age,
        Name = obj.Name,
        ismanager = obj.ismanager,
        Title = obj.Title,
        ManagerId = obj.ManagerId,
        CreatedAt = DateTime.UtcNow,
        GenderId = obj.GenderId,

      };

      List<Teacher> manages = new();

      if (obj.ManagedIds != null)
      {
        foreach (var x in obj.ManagedIds)
        {
          var teacher = db.Teacher.Find(x);

          entity.Manages.Add(teacher);
        }
      }
     

      await db.Teacher.AddAsync(entity);


      await db.SaveChangesAsync();

  

      return entity.Id;
    }

    catch (Exception)
    {

      throw;
    }
  }
}
