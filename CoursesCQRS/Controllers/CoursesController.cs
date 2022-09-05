using CoursesCQRS.Application.Common.Exceptions;
using CoursesCQRS.Application.Features.CourseFeature.Commands.Create;
using CoursesCQRS.Application.Features.CourseFeature.Commands.Delete;
using CoursesCQRS.Application.Features.CourseFeature.Commands.Update;
using CoursesCQRS.Application.Features.CourseFeature.Models;
using CoursesCQRS.Application.Features.CourseFeature.Queries;
using CoursesCQRS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursesCQRS.API.Controllers
{
  public class CoursesController : ControllerBase
  {
    private readonly IMediator mediator;
    private readonly ApplicationContext db;


    public CoursesController(IMediator mediator, ApplicationContext db)
    {
      this.mediator = mediator;
      this.db = db;

    }

     
    [Route("~/api/teachers/PostCourse")]
    [HttpPost]
    // 
    public async Task<IActionResult> PostCourse([FromBody] CreateCourseCommand command)
    {

      if (ModelState.IsValid)
      { 

      var data = await mediator.Send(command);

        return Ok(data);

      }
      return BadRequest();

    }


    [Route("~/api/teachers/EditCourse")]
    [HttpPut]
    //[Authorize]
    public async Task<IActionResult> EditCourse([FromBody] UpdateCourseCommand command)
    {

      if (ModelState.IsValid)
      {

        var data = await mediator.Send(command);
        

        return Ok(data);
      }

      return BadRequest();

    }


    [Route("~/api/courses/GetCourse")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCourse(GetCourseQuery  query)
    {
    

        var result = await mediator.Send(query);

        return Ok(result);
      }
  

    


    [Route("~/api/courses/GetCourses")]
    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetCourses(GetAllCoursesQuery query)
    {

      var result = await mediator.Send(query);

      return Ok(result);

    }


    [Route("~/api/courses/DeleteCourse")]
    [HttpDelete]
    public async Task<IActionResult> DeleteTeacher(DeleteCourseCommand command)
    {


      var data = await mediator.Send(command);
      if (data == 1) return NoContent();

      return BadRequest();


    }

  }
}
