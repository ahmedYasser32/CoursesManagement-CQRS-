using CoursesCQRS.Application.Common.Exceptions;
using CoursesCQRS.Application.Features.StudentFeature.Commands.Create;
using CoursesCQRS.Application.Features.StudentFeature.Commands.Delete;
using CoursesCQRS.Application.Features.StudentFeature.Commands.Update;
using CoursesCQRS.Application.Features.StudentFeature.Models;
using CoursesCQRS.Application.Features.StudentFeature.Queries;
using CoursesCQRS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoursesCQRS.API.Controllers
{
  public class StudentsController : ControllerBase
  {
    private readonly IMediator mediator;
    private readonly ApplicationContext db;

    public StudentsController(IMediator mediator, ApplicationContext db)
    {
      this.mediator = mediator;
      this.db = db;

    }


    [Route("~/api/teachers/PostStudent")]
    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> PostStudent([FromBody] CreateStudentCommand command)
    {

      if (ModelState.IsValid)
      { 

      var data = await mediator.Send(command);

        return Ok(data);

      }
      return BadRequest();

    }


    [Route("~/api/teachers/EditStudent")]
    [HttpPut]
    //[Authorize]
    public async Task<IActionResult> EditStudent([FromBody] UpdateStudentCommand command)
    {

      if (ModelState.IsValid)
      {

        var data = await mediator.Send(command);
        

        return Ok(data);
      }

      return BadRequest();

    }


    [Route("~/api/courses/GetStudent")]
    [HttpGet]
    public async Task<IActionResult> GetStudent(GetStudentQuery  query)
    {
    

        var result = await mediator.Send(query);

        return Ok(result);
      }
  

    


    [Route("~/api/courses/GetStudents")]
    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetStudents(GetAllStudentsQuery query)
    {

      var result = await mediator.Send(query);

      return Ok(result);

    }


    [Route("~/api/courses/DeleteStudent")]
    [HttpDelete]
    public async Task<IActionResult> DeleteTeacher(DeleteStudentCommand command)
    {


      var data = await mediator.Send(command);
      if (data == 1) return NoContent();

      return BadRequest();


    }

  }
}
