using CoursesCQRS.Application.Features.CategoryFeature.Commands.Create;
using CoursesCQRS.Application.Features.CategoryFeature.Commands.Delete;
using CoursesCQRS.Application.Features.CategoryFeature.Commands.Update;
using CoursesCQRS.Application.Features.CategoryFeature.Queries;
using CoursesCQRS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursesCQRS.API.Controllers
{
  [Authorize(Roles ="Admin")]
  public class CategorysController : ControllerBase
  {
    private readonly IMediator mediator;
    private readonly ApplicationContext db;

    public CategorysController(IMediator mediator, ApplicationContext db)
    {
      this.mediator = mediator;
      this.db = db;

    }


    [Route("~/api/Categories/PostCategory")]
    [HttpPost]
  
    public async Task<IActionResult> PostCategory([FromBody]CreateCategoryCommand command)
    {
      try
      {
        if (ModelState.IsValid)
        {

          var data = await mediator.Send(command);

          return Ok(data);

        }
        return BadRequest();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);

      }
    }

    


    [Route("~/api/Categories/EditCategory")]
    [HttpPut]
    //[Authorize]
    public async Task<IActionResult> EditCategory([FromBody] UpdateCategoryCommand command)
    {

      if (ModelState.IsValid)
      {

        var data = await mediator.Send(command);

        return Ok(data);

      }

      return BadRequest();

    }


    [Route("~/api/Categorys/GetCategory")]
    [HttpGet]
    public async Task<IActionResult> GetCategory(GetCategoryQuery  query)
    {
    

        var result = await mediator.Send(query);

        return Ok(result);
      }
  

    


    [Route("~/api/Categorys/GetCategorys")]
    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetCategorys(GetAllCategorysQuery query)
    {

      var result = await mediator.Send(query);

      return Ok(result);

    }


    [Route("~/api/Categorys/DeleteCategory")]
    [HttpDelete]
    public async Task<IActionResult> DeleteTeacher(DeleteCategoryCommand command)
    {


      var data = await mediator.Send(command);
      if (data == 1) return NoContent();

      return BadRequest();


    }

  }
}
