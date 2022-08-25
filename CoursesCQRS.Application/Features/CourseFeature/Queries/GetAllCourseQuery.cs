using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Features.CourseFeature.Models;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.CourseFeature.Queries
{
  public class GetAllCoursesQuery : IRequest<IEnumerable<CourseCreateDTO>>
  {

  }
  public class Handler : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseCreateDTO>>
  {
    private readonly IMapper mapper;
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db,IMapper mapper)
    {
      this.mapper = mapper;
      this.db = db;
    }

    public async Task<IEnumerable<CourseCreateDTO>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
      return await db.Course.ProjectTo<CourseCreateDTO>(mapper.ConfigurationProvider).ToListAsync();
    }
  }
}
