using CoursesCQRS.Application.Features.CategoryFeature.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CategoryFeature.Commands.Update
{
  public class UpdateCategoryCommand : CategoryDTO, IRequest<CategoryDTO>
  {
  }
  public class Handler : IRequestHandler<UpdateCategoryCommand, CategoryDTO>
  {
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db)
    {

      this.db = db;
    }

    public async Task<CategoryDTO> Handle(UpdateCategoryCommand obj, CancellationToken cancellationToken)
    {
      try

      {
        var entity = await db.Category.FindAsync(obj.Id);

        entity.Name = obj.Name;
        entity.ModifiedAt = DateTime.Now;
  
        

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

