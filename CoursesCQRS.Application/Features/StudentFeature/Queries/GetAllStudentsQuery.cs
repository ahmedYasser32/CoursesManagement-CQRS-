using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Features.StudentFeature.Models;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.StudentFeature.Queries
{
  public class GetAllStudentsQuery : IRequest<IEnumerable<StudentCreateDTO>>
  {

  }
  public class Handler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentCreateDTO>>
  {
    private readonly IMapper mapper;
    private readonly ApplicationContext db;
    public Handler(ApplicationContext db,IMapper mapper)
    {
      this.mapper = mapper;
      this.db = db;
    }

    public async Task<IEnumerable<StudentCreateDTO>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
      return await db.Student.ProjectTo<StudentCreateDTO>(mapper.ConfigurationProvider).ToListAsync();
    }
  }
}
