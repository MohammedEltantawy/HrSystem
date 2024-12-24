using HrSystem.Data.Models;
using HrSystem.Infrastructure.Context;
using HrSystem.InfraStructure.Repositories;
using HrSystem.Services;
using HrSystemApi.Serives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

static WebApplicationBuilder BuildApi(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddTransient<UserService>();
    builder.Services.AddTransient<CandidateService>();
    builder.Services.AddTransient<RequisitionService>();
    builder.Services.AddTransient<VacancyService>();
    builder.Services.AddTransient<IUnitOfWork<User>, UnitOfWork<User>>();
    builder.Services.AddTransient<IUnitOfWork<Candidate>, UnitOfWork<Candidate>>();
    builder.Services.AddTransient<IUnitOfWork<Requisition>, UnitOfWork<Requisition>>();
    builder.Services.AddTransient<IUnitOfWork<Vacancy>, UnitOfWork<Vacancy>>();
    builder.Services.AddTransient<HrSystemContext>();
    builder.Services.AddTransient<CandidateManager>();
    builder.Services.AddTransient<RequisitionManager>();
    builder.Services.AddTransient<VacancyManager>();



    IConfigurationRoot configuration = new ConfigurationBuilder()
                             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                             .AddJsonFile("appsettings.json").Build();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HrSystem", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"Enter 'Bearer' [space] and then your token. Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {new OpenApiSecurityScheme
                      { Reference = new OpenApiReference
                          { Type = ReferenceType.SecurityScheme,Id = "Bearer"
                          }, Scheme = "oauth2",Name = "Bearer",In = ParameterLocation.Header,}, new List<string>()
                      }
                  }
        );
    });

    builder.Services.AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            //option.ForwardSignIn = "api/login/login";
            option.SaveToken = true;
            option.RequireHttpsMetadata = false;
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });

    builder.Services.AddAuthorization();
    return builder;
}

static void BuildApp(WebApplicationBuilder builder)
{
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hr API V1");
    });


    app.UseCors(x => x
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());

    app.MapControllerRoute(
         name: "areas",
         pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=LandingPage}/{action=Index}/{id?}");


    app.Run();
}

WebApplicationBuilder builder = BuildApi(args);

BuildApp(builder);
