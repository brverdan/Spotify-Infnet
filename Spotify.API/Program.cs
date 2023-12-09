using Microsoft.AspNetCore.Diagnostics;
using Spotify.API.ErrorHandling;
using Spotify.IoC.Repository;
using Spotify.IoC.Service;
using static Spotify.API.ErrorHandling.ErrorHandling;
using System.Net;
using Spotify.Core.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler(e => e.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

    if (exception is BusinessException businessException)
    {
        var errorResponse = new ErrorHandling();

        foreach (var item in businessException.Errors)
            errorResponse.Messages.Add(new ErrorMessage() { ErrorName = item.ErrorName, Message = item.ErrorMessage });

        context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { Error = exception?.Message });
    }
}));

app.MapControllers();

app.Run();
