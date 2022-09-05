using CoursesCQRS.Domain.Entity;
using CoursesCQRS.Infrastructure.Extend;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Infrastructure
{
   public  class ApplicationContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }

    public DbSet<Category> Category { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<Student> Student { get; set; }
    public DbSet<Tokens> Token { get; set; }  
    public DbSet<Book> Book { get; set; }

  }
}


