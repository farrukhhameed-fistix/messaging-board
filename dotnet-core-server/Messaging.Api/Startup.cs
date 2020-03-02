using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
//using Domain.AggregatesModel.MessageAgg;
using MediatR;
using Messaging.Api.Hubs;
using Messaging.Command;
using Messaging.Domain.AggregatesModel.MessageAgg;
using Messaging.Persistence;
using Messaging.Query.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Messaging.Api
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
      services.AddControllers();

      services.AddAutoMapper(typeof(CommandProfile));      
      services.AddMediatR(typeof(MessageCreateCommand), typeof(FetchAllMessagesQuery));

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
      });

      services.AddSignalR();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Messaging API", Version = "v1" });
      });

      services.AddScoped<IMessageRepository, MessageRepository>();

      services.AddDbContext<MessagingEfContext>
          (
          options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
          ); ;

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MessagingEfContext messagingEfContext)
    {
      messagingEfContext.Database.Migrate();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });

      //app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors("CorsPolicy");
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHub<MessagingHub>("/messagingHub");
      });      
    }
  }
}
