using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Infra.Contexts;
using Todo.Infra.Repositories;
using Todo.Domain.Repositories;
using Todo.Domain.Services;
using Todo.Infra.Repositories;

namespace Todo.Domain.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo.Api", Version = "v1" });
            });

            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<TodoHandler, TodoHandler>();
            services.AddTransient<AuthenticateUser>();

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                   };
               });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedData(context);
        }

        private void SeedData(DataContext context)
        {
            Console.WriteLine("-->  SeedData");

            var user = new User
            {
                Id = new Guid(),
                Username = "Pierre",
                Password = "0123456789",
                Todos = new List<TodoItem>()
                {
                    new TodoItem{
                    Id = new Guid(),
                    Title = "Todo item 1"
                    },
                    new TodoItem{
                    Id = new Guid(),
                    Title = "Todo item 2"
                    },
                    new TodoItem{
                    Id = new Guid(),
                    Title = "Todo item 3"
                    },
                    new TodoItem{
                    Id = new Guid(),
                    Title = "Todo item 4"
                    }
                }
            };

            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
