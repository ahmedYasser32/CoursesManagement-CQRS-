using CoursesCQRS.Application.Features.CourseFeature.Commands.Create;
using FluentValidation;


namespace CoursesCQRS.Application.Validators
{
  public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
  {
    public CreateCourseCommandValidator()
    
    {
      RuleFor(v => v.Name)
          .MaximumLength(50)
          .NotEmpty();
    }

  }
}
