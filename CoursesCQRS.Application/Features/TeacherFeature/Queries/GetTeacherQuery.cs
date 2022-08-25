using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoursesCQRS.Application.Common.Exceptions;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.TeacherFeature.Queries
{
  public class GetStudentQuery : IRequest<TeacherGetDTO>
  {
    public int Id { get; set; }
  }
  public class GetHandler : IRequestHandler<GetStudentQuery, TeacherGetDTO>
  {
    private readonly ApplicationContext db;
    private readonly IMapper mapper;

    public GetHandler(ApplicationContext db,IMapper mapper)
    {

      this.db = db;
      this.mapper = mapper;
    }

    public async Task<TeacherGetDTO> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
      var entity = await db.Teacher.Where(x=>x.Id == request.Id)
        .Include("Courses")
        .ProjectTo<TeacherGetDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(); 
      if (entity == null)
        throw new NotFoundException(nameof(Student), request.Id);

      //TeacherGetDTO teacher = new TeacherGetDTO()
      // {
      //   Id = entity.Id,
      //   Age = entity.Age,
      //   Name = entity.Name,
      //   ismanager = entity.ismanager,
      //   Title = entity.Title,
      //   ManagerId = entity.ManagerId,
      //   GenderId = entity.GenderId,
      //   Manager  = entity.Manager, //solved with auto mapper.
      //   Courses = entity.Courses,
      //   Manages = entity.Manages
      // };

      return entity; 
    }
  }
}
