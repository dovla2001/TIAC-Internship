using Infrastructure.EntityFramework;
using Infrastructure;
using Application;
using FastEndpoints.Swagger;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Presentation.ExceptionHandlers;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplication().RegisterInfrastructure(builder.Configuration);

builder.Services.SwaggerDocument();

builder.Services.AddFastEndpoints();

builder.Services.AddExceptionHandler<CustomApiExceptionHandler>();  

// Add services to the container.
//builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();

app.UseStaticFiles();

app.UseExceptionHandler("/error");

app.UseSwaggerGen();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();

//app.UseHttpsRedirection();

app.Map("/error", () => Results.Problem());

app.Run();
