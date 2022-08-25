using CoursesCQRS.Application.Features.AccountFeature.Models;
using CoursesCQRS.Infrastructure.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoursesCQRS.Application.Features.AccountFeature.Commands
{
  public class CreateTokenCommand : loginDTO, IRequest<Tokens>
  {
  }
  public class Handler : IRequestHandler<CreateTokenCommand, Tokens>
  {
    
		private readonly IConfiguration iconfiguration;
		private readonly SignInManager<ApplicationUser> signInmanager;
		public Handler(IConfiguration iconfiguration, SignInManager<ApplicationUser> signInmanager)
		{
			this.iconfiguration = iconfiguration;
			this.signInmanager = signInmanager;

    }

    public async Task<Tokens> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
			var result = await signInmanager.PasswordSignInAsync(request.UserName, request.password, false, false);

			if (result.Succeeded)
			{
				// Else we generate JSON Web Token
				var tokenHandler = new JwtSecurityTokenHandler();
			  var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
			 new Claim(ClaimTypes.Email,request.UserName)
					}),
					Expires = DateTime.UtcNow.AddMinutes(10),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);

				return new Tokens { Token = tokenHandler.WriteToken(token) };


			}
			else
			{
				return null;
			}



		}
	}
}
