using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Common.Exceptions;
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
  public class GetStudentQuery : IRequest<StudentGetDTO>
  {
    public int Id { get; set; }
  }
  public class GetHandler : IRequestHandler<GetStudentQuery, StudentGetDTO>
  {
    private readonly ApplicationContext db;
    private readonly IMapper mapper;

    public GetHandler(ApplicationContext db,IMapper mapper)
    {

      this.db = db;
      this.mapper = mapper; 
    }

    public async Task<StudentGetDTO> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
      var entity= await db.Student.Where(x => x.Id == request.Id).Include(a=>a.Courses).ProjectTo<StudentGetDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(); 
      if (entity == null)
        throw new NotFoundException(nameof(Student),request.Id);

      return entity;

   
    }
  }
}
