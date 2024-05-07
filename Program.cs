

using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialApp.data;
using SocialApp.Domain.Entities;
using SocialApp.Hubs;
using SocialApp.IdentityServices;
using SocialApp.IRepository;
using SocialApp.IServices;
using SocialApp.IServices.CommentsServices;
using SocialApp.IServices.ImageServices;
using SocialApp.IServices.PostsServices;
using SocialApp.IServices.UserServices;
using SocialApp.Middleware.Exceptions;
using SocialApp.Repositories;
using SocialApp.Repositories.Users;
using SocialApp.Services;
using SocialApp.Services.CommentsServices;
using SocialApp.Services.ImageServices;
using SocialApp.Services.PostServices;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IRoleService, RoleServices >();
builder.Services.AddScoped<IIdendtityServices, IdentityService>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IPostServices, PostServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICommentService, CommentServices>();
builder.Services.AddScoped<IImageServices, ImageServices>();
// signalR
builder.Services.AddSignalR();
//add auto mapper
builder.Services.AddAutoMapper(typeof(Program));
//set jwt config
builder.Configuration.GetSection("JWT").Get<JWT>();
builder.Services.AddControllers();


// add sql connections
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

// add identity
builder.Services.AddIdentity<User, Role>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    
})

.AddEntityFrameworkStores<ApplicationDbContext>()

.AddDefaultTokenProviders();


//add jwt auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata=false;
    opt.SaveToken = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
//builder.Services.AddAuthorization(opt =>
//{
//    opt.AddPolicy("admin", new AuthorizationPolicyBuilder()
//                            .RequireRole("admin")
//                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//                            .RequireAuthenticatedUser()
//                            .Build());
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social API", Version = "v1" });

    // Configure Swagger to use JWT bearer authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
        {
            { securityScheme, new[] { "Bearer" } }
        };

    c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Front",
                      builder =>
                      {
                          builder.AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .WithOrigins("http://localhost:3000")
                                 .AllowCredentials();
                                 
                                 
                      });
});

// add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
});
app.UseCors("Front");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.MapHub<PostNotificationHub>("/post-notification");
app.Run();
