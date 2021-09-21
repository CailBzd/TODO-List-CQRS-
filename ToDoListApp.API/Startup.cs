using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Controllers;
using ToDoListApp.Infrastructure.Contexts;
using ToDoListApp.Domain.AggregatesModel.CustomerAggregate;
using ToDoListApp.Domain.AggregatesModel.TaskAggregate;
using ToDoListApp.Infrastructure.Repositories;

namespace ToDoListApp.API
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
            services.AddControllers().AddApplicationPart(typeof(TasksController).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TODO.API", Version = "v1" });
            });

            //Ajout du context
            services.AddDbContext<ToDoContext>(options =>
            {
                options.UseInMemoryDatabase("TODOAPP");
            });

            //Ajout de MediatR
            services.AddMediatR(typeof(TasksController));

            //Ajout des repos
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ITaskItemRepository, TaskItemRepository>();

            //Ajout des services
            services.AddTransient<AuthenticateCustomer>();

            //Ajout des settings
            services.Configure<string>(Configuration.GetSection("Jwt"));

            //Ajout de l'authentification
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ToDoContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoListApp.API v1"));
            }

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedData(context);
        }

        private void SeedData(ToDoContext context)
        {
            Console.WriteLine("--> SeedData");

            var user = new Customer
            {
                Id = new Guid().ToString(),
                CustomerName = "Pierre",
                Password = "0123456789",
                Tasks = new List<TaskItem>()
                {
                    new TaskItem{
                    Id = new Guid(),
                    Name = "Todo item 1",
                    Completed = false,
                    DateCreated = DateTime.Now.AddDays(-1)
                    },
                    new TaskItem{
                    Id = new Guid(),
                    Name = "Todo item 2",
                    Completed = false,
                    DateCreated = DateTime.Now.AddHours(-1)
                    },
                    new TaskItem{
                    Id = new Guid(),
                    Name = "Todo item 3",
                    Completed = false,
                    DateCreated = DateTime.Now.AddYears(-1)
                    },
                    new TaskItem{
                    Id = new Guid(),
                    Name = "Todo item 4",
                    Completed = false,
                    DateCreated = DateTime.Now
                    }
                }
            };

            context.Customers.Add(user);

            context.SaveChanges();
        }
    }
}
