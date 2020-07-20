using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProAgil.Domain.Identity;
using ProAgil.Repository;

namespace ProAgil
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ProAgilContext>(options =>
					options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
					builder => builder.MigrationsAssembly("ProAgil.Repository")));
			IdentityBuilder identityBuilder = services.AddIdentityCore<User>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 4;
			});
			identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(Role), identityBuilder.Services);
			identityBuilder.AddEntityFrameworkStores<ProAgilContext>();
			identityBuilder.AddRoleValidator<RoleValidator<Role>>();
			identityBuilder.AddRoleManager<RoleManager<Role>>();
			identityBuilder.AddSignInManager<SignInManager<User>>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt => {
					opt.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
						.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
						ValidateIssuer = false,
						ValidateAudience = false

					};
				}
				
				);



			services.AddMvc(options =>
			{
				var policy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
			.AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			services.AddScoped<IProAgilRepository, ProAgilRepository>();
			services.AddAutoMapper();
			services.AddCors();
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts(); 
			}
			app.UseAuthentication();
			//app.UseHttpsRedirection();
			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseStaticFiles();
			app.UseMvc();
		}
	}
}
