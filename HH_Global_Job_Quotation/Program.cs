using HH_Global_Job_Quotation.Helper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using HH_Global_Job_Quotation.Repository;
using HH_Global_Job_Quotation.Models.DTO;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<HelperLookup>();
builder.Services.AddScoped<IGetCostInvoice, GetCostInvoice>();
builder.Services.AddSingleton<JobItemCreateRequest>();
builder.Services.AddSingleton<JobItemResponse>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.Configure<AdditionalCost>(options => builder.Configuration.GetSection("AdditionalCost").Bind(options));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/CreateInvoice", (IGetCostInvoice getCostInvoice, [FromBody] JobItemCreateRequest jobItem) =>
{
    var response =  getCostInvoice.GetCostInvoiceJobItem(jobItem);

    if(response != null)
    {
        return Results.Ok(response);

    }
    else
    {
        return Results.BadRequest(new JobItemResponse());

    }


}).WithName("JobItemInvoice").Accepts<JobItemCreateRequest>("application/json").Produces<JobItemResponse>(201).Produces(400);

app.UseHttpsRedirection();




app.Run();

