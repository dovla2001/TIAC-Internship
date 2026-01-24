using Application;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.ExceptionHandlers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplication().RegisterInfrastructure(builder.Configuration);

builder.Services.SwaggerDocument();

builder.Services.AddFastEndpoints();

builder.Services.AddExceptionHandler<CustomApiExceptionHandler>();

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();

app.Map("/error", () => Results.Problem());

app.Run();
