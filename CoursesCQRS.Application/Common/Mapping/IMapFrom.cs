using AutoMapper;

namespace CoursesCQRS.Application.Common.Mapping;

public interface IMapFrom<T>
{
  void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}