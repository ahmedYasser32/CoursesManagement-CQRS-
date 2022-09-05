using CoursesCQRS.Application.Features.Books.Commands;
using CoursesCQRS.Application.Features.TeacherFeature.Commands.Create;
using CoursesCQRS.Application.Features.TeacherFeature.Commands.Delete;
using CoursesCQRS.Application.Features.TeacherFeature.Commands.Update;
using CoursesCQRS.Application.Features.TeacherFeature.Models;
using CoursesCQRS.Application.Features.TeacherFeature.Queries;
using CoursesCQRS.Domain.Entity;
using CoursesCQRS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoursesCQRS.API.Controllers
{
  public class TeacherController : ControllerBase
  {
    private readonly IMediator mediator;
    private readonly ApplicationContext db;

    public TeacherController(IMediator mediator,ApplicationContext db)
    {
      this.mediator = mediator;
      this.db = db;

    }


    [Route("~/api/teachers/PostTeacher")]
    [HttpPost]
   // [Authorize]
    public async Task<IActionResult> PostTeacher([FromBody] TeacherCreateDTO model)
    {

      if (ModelState.IsValid)
      {


        var data = await mediator.Send(new CreateTeacherCommand()
        {
          Age = model.Age,
          Name = model.Name,
          ismanager = model.ismanager,
          Title = model.Title,
          ManagerId = model.ManagerId,
      
          GenderId = model.GenderId,
          ManagedIds= model.ManagedIds, 
        }) ;


        return Ok(data);


      }
      return BadRequest();

    }





    [Route("~/api/teachers/EditTeacher")]
    [HttpPut]
    //[Authorize]
    public async Task<IActionResult> EditTeacher([FromBody] TeacherUpdateDTO model)
    {

      if (ModelState.IsValid)
      {

       var data = await mediator.Send(new UpdateTeacherCommand()
        {
          Age = model.Age,
          Name = model.Name,
          ismanager = model.ismanager,
          Title = model.Title,
          ManagerId = model.ManagerId,
          GenderId = model.GenderId,
          Id = model.Id}
        );

        return Ok(data);
      }

      return BadRequest();

    }


    [Route("~/api/courses/GetTeacher")]
    [HttpGet]
    public async Task<IActionResult> GetTeacher(int id)
    {

      var result = await mediator.Send(new GetStudentQuery() { Id = id});

      return Ok(result);



    }
    
    [Route("~/api/courses/AddBOOK")]
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromForm]CreateBookCommand command)
    {

      var result = await mediator.Send(command);
      if (result == null) { return BadRequest(); }


      return Ok(result);



    }


    [Route("~/api/courses/GetTeachers")]
    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetTeachers()
    {

      var result = await mediator.Send(new GetAllTeachersQuery());

      return Ok(result);



    }


    [Route("~/api/courses/DeleteTeacher")]
    [HttpDelete]
    public async Task<IActionResult> DeleteTeacher(int id)
    {

    
      var data = await mediator.Send(new DeleteTeacherCommand() { Id = id});
      if (data == 1) return NoContent();

      return BadRequest();


    }

  }
}
