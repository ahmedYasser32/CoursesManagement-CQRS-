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

//for refrence check this repo https://github.com/mohamadlawand087/v8-refreshtokenswithJWT/blob/main/TodoApp/Controllers/AuthManagementController.cs

namespace CoursesCQRS.Application.Features.AccountFeature.Commands
{
  public class CreateTokenCommand : loginDTO, IRequest<Tokens>
  {
  }
  public class Handler : IRequestHandler<CreateTokenCommand, Tokens>
  {
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IConfiguration iconfiguration;
		private readonly SignInManager<ApplicationUser> signInmanager;
		private readonly UserManager<ApplicationUser> userManager;
		public Handler(IConfiguration iconfiguration, SignInManager<ApplicationUser> signInmanager, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			this.iconfiguration = iconfiguration;
			this.signInmanager = signInmanager;
			this.roleManager= roleManager;	
			this.userManager= userManager;

    }

    public async Task<Tokens> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
			var result = await signInmanager.PasswordSignInAsync(request.UserName, request.password, false, false);


			if (result.Succeeded)
			{
				var user = await userManager.FindByEmailAsync(request.UserName);
				// Else we generate JSON Web Token
				var tokenHandler = new JwtSecurityTokenHandler();
			  var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
				
				var claims = await GetValidClaims(user);

				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					Expires = DateTime.UtcNow.AddMinutes(5), // 5-10 
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

		private async Task<List<Claim>> GetValidClaims(ApplicationUser user)
		{
			IdentityOptions _options = new IdentityOptions();
			var claims = new List<Claim>
		{
				new Claim("Id", user.Id),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
				new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName),
		};

			var userClaims = await userManager.GetClaimsAsync(user);
			var userRoles = await userManager.GetRolesAsync(user);
			claims.AddRange(userClaims);
			foreach (var userRole in userRoles)
			{
				claims.Add(new Claim(ClaimTypes.Role, userRole));
				var role = await roleManager.FindByNameAsync(userRole);
				if (role != null)
				{
					var roleClaims = await roleManager.GetClaimsAsync(role);
					foreach (Claim roleClaim in roleClaims)
					{
						claims.Add(roleClaim);
					}
				}
			}
			return claims;
		}

	}
}
