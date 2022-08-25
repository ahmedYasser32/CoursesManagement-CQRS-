using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Common.Exceptions;
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
  public class GetCourseQuery : IRequest<CourseGetDTO>
  {
    public int Id { get; set; }
  }
  public class GetHandler : IRequestHandler<GetCourseQuery,CourseGetDTO>
  {
    private readonly ApplicationContext db;
    private readonly IMapper mapper;

    public GetHandler(ApplicationContext db, IMapper mapper)
    {

      this.db = db;
      this.mapper = mapper;
     }

    public async Task<CourseGetDTO> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
      var entity = await db.Course.Where(x=>x.Id == request.Id)
          .Include(a => a.Students)
          .Include(a => a.Teacher)
          .Include(a => a.Category)
        .ProjectTo<CourseGetDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync();

      if (entity == null)
        throw new NotFoundException(nameof(Course), request.Id);

      return entity;
    }
  }
}
