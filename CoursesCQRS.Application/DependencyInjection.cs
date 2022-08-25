using CoursesCQRS.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application
{     //DependencyInjection for Mapper, MediatR, Validator
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
  
      services.AddAutoMapper(Assembly.GetExecutingAssembly());

      services.AddMediatR(Assembly.GetExecutingAssembly());
      
      //Validation pipeline setup from validators folder
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
      return services;
    }
  }
}
