using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MexicanFood.Core.ApplicationService;
using MexicanFood.Core.ApplicationService.Implementation;
using MexicanFood.Core.DomainService;
using MexicanFood.Core.Entities;
using MexicanFood.Entities;
using MexicanFood.Infrastructure.Data.Repositories;
using MexicanFood.Infrastructure.Data.Repositories.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace MexicanFood.RestApi
{
	public class Startup
	{
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
		{
            //Do we need a builder?
			Configuration = configuration;
			Environment = env;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Byte[] secretBytes = new byte[40];
			Random rand = new Random();
			rand.NextBytes(secretBytes);
			
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = false,
					//ValidAudience = "TodoApiClient",
					ValidateIssuer = false,
					//ValidIssuer = "TodoApi",
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
					ValidateLifetime = true, //validate the expiration and not before values in the token
					ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
				};
			});
			
			services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigin",
					builder => builder
						.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
				);
			});
			
			if (Environment.IsDevelopment())
			{
				services.AddDbContext<MexicanFoodContext>(
					opt => opt.UseSqlite("Data Source=mexicanFood.db"));
			}
			else
			{
				services.AddDbContext<MexicanFoodContext>(opt =>
					opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
			}

            services.AddScoped<IRepository<Meal>, MealRepository>();
            services.AddScoped<IMealService, MealService>();

            services.AddScoped<IRepository<User>, UserRepository>();
			services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddTransient<IDBInitializer, DBInitializer>();
			services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));
			
			services.AddMvc().AddJsonOptions(options => {
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			});
			
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				using (var scope = app.ApplicationServices.CreateScope())
				{	
					var services = scope.ServiceProvider;
					var ctx = services.GetService<MexicanFoodContext>();
					var dbInitializer = services.GetService<IDBInitializer>();
					ctx.Database.EnsureDeleted();
					ctx.Database.EnsureCreated();
					dbInitializer.SeedDb(ctx);
				}
			}
			else
			{
				
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<MexicanFoodContext>();
                    ctx.Database.EnsureCreated();
	                var dbInitializer = ctx.GetService<IDBInitializer>();
	                dbInitializer.SeedDb(ctx);
	                
                }

				app.UseHsts();
			}

            app.UseHttpsRedirection();
			app.UseCors("AllowSpecificOrigin");
			app.UseAuthentication();
            app.UseMvc();
		}
	}
}
