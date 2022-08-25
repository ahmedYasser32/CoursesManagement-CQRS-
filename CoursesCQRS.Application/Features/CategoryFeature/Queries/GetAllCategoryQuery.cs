using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Features.CategoryFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CategoryFeature.Queries
{
  public class GetAllCategorysQuery : IRequest<IEnumerable<CategoryDTO>>
  {

  }
  public class Handler : IRequestHandler<GetAllCategorysQuery, IEnumerable<CategoryDTO>>
  {
    private readonly IMapper mapper;
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db,IMapper mapper)
    {
      this.mapper = mapper;
      this.db = db;
    }

    public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategorysQuery request, CancellationToken cancellationToken)
    {
      return await db.Category.Include(a=>a.Courses).ProjectTo<CategoryDTO>(mapper.ConfigurationProvider).ToListAsync();
    }
  }
}
