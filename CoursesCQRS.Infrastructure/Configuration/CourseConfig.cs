using CoursesCQRS.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;






namespace CoursesCQRS.Infrastructure.Configuration;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
  public void Configure(EntityTypeBuilder<Course> builder)
  {
    builder.HasKey(t => t.Id);
    builder.Property(t=> t.TeacherId)
      .IsRequired().HasMaxLength(60);

  }
}