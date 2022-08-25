

using CoursesCQRS.Application.Features.CategoryFeature.Models;



namespace CoursesCQRS.Application.Features.CategoryFeature.Commands.Create;
public class CreateCategoryCommand :CategoryDTO, IRequest<int>
  {

  }
public class Handler : IRequestHandler<CreateCategoryCommand, int>
{
  private readonly ApplicationContext db;
  public Handler(ApplicationContext db)
  {

    this.db = db;
  }

  public async Task<int> Handle(CreateCategoryCommand obj, CancellationToken cancellationToken)
  {
    try
    {

      var entity = new Category()
      {
       
        Name = obj.Name,
        CreatedAt=DateTime.Now,
      
      };
    
     

      await db.Category.AddAsync(entity);


      await db.SaveChangesAsync();

  

      return entity.Id;
    }

    catch (Exception)
    {

      throw;
    }
  }
}
