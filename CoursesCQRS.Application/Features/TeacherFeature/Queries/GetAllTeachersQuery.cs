using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.TeacherFeature.Queries
{
  public class GetAllTeachersQuery : IRequest<IEnumerable<TeacherUpdateDTO>>
  {

  }
  public class Handler : IRequestHandler<GetAllTeachersQuery,IEnumerable<TeacherUpdateDTO>>
  {
    private readonly ApplicationContext db;
    private readonly IMapper mapper;

    public Handler(ApplicationContext db, IMapper mapper)
    {

      this.db = db;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<TeacherUpdateDTO>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
    {
      return await db.Teacher.Include("Courses").ProjectTo<TeacherUpdateDTO>(mapper.ConfigurationProvider).ToListAsync();
    }
  }
}
