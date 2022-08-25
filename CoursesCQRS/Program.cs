


using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CoursesCQRS.Infrastructure;

using MediatR;
using CoursesCQRS.Application.Features.TeacherFeature.Commands.Create;
using CoursesCQRS.Application.Features.TeacherFeature.Commands.Update;
using CoursesCQRS.Application.Features.StudentFeature.Commands.Create;
using CoursesCQRS.Application;
using CoursesCQRS.Infrastructure.Extend;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add configuration services of the application layer
builder.Services.AddApplicationServices();

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "Courses API",
    Version = "v1",
    Description = "An API to perform Courses operations",
    TermsOfService = new Uri("https://example.com/terms"),

  });
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "Token",
    Scheme = "Bearer"
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

});

//database
builder.Services.AddDbContext<ApplicationContext>(
      options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
      );

//Identity addition Must be in the same order!
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider); //to create token for forget password

  
//Identity configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
  // Password settings.
  options.Password.RequireDigit = false;
  options.Password.RequireLowercase = false;
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequireUppercase = false;
  options.Password.RequiredLength = 6;
  options.Password.RequiredUniqueChars = 0;

  // Lockout settings.
  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
  options.Lockout.MaxFailedAccessAttempts = 5;
  options.Lockout.AllowedForNewUsers = true;

  // User settings.
  options.User.AllowedUserNameCharacters =
  "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
  options.User.RequireUniqueEmail = false;


}).AddEntityFrameworkStores<ApplicationContext>();


//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, ApplicationContext>();

//Add JWT authentication and configure it

builder.Services.AddAuthentication(x =>
{
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>

{
  var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
  o.SaveToken = true;
  o.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidAudience = builder.Configuration["JWT:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Key)

  };
});
builder.Services.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
    options => options.TokenValidationParameters = new TokenValidationParameters());

//builder.Services.AddAuthorization(options =>
//{
//  options.AddPolicy("RequireAdministratorRole",
//       policy => policy.RequireRole("Admin"));
//});

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly(),typeof(CreateTeacherCommand).Assembly,typeof(UpdateTeacherCommand).Assembly,typeof(CreateStudentCommand).Assembly);
//builder.Services.add(Assembly.GetExecutingAssembly());







var app = builder.Build();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


app.MapControllers();

app.Run();
