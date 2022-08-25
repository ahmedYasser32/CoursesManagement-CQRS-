using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Common.Exceptions;
using CoursesCQRS.Application.Features.CategoryFeature.Models;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using CoursesCQRS.Application.Common.Exceptions;
using CoursesCQRS.Application.Features.CategoryFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CategoryFeature.Queries
{
  public class GetCategoryQuery : IRequest<CategoryDTO>
  {
    public int Id { get; set; }
  }
  public class GetHandler : IRequestHandler<GetCategoryQuery,CategoryDTO>
  {
    private readonly ApplicationContext db;
    private readonly IMapper mapper;

    public GetHandler(ApplicationContext db, IMapper mapper)
    {

      this.db = db;
      this.mapper = mapper;
     }

    public async Task<CategoryDTO> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
      var entity = await db.Category.Where(x=>x.Id == request.Id).Include(a => a.Courses).ProjectTo<CategoryDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync();

      if (entity == null)
        throw new NotFoundException(nameof(Category), request.Id);

      return entity;
    }
  }
}
