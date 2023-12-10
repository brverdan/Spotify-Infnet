using Microsoft.AspNetCore.Diagnostics;
using Spotify.Core.Exceptions;
using Spotify.Streaming.API.ErrorHandling;
using Spotify.Streaming.IoC.Repository;
using Spotify.Streaming.IoC.Service;
using System.Net;
using static Spotify.Streaming.API.ErrorHandling.ErrorHandling;

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
