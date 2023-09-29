//using DomainLayer.Data;
//using DomainLayer.Models;
//using System.ComponentModel.DataAnnotations;


using System.Reflection;
using CatalogAPI.Filters;
using CatalogAPI.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IStudentEnrollmentService, StudentEnrollmentService>();
builder.Services.AddSingleton<ITeacherAssignementsServices, TeacherAssignementsServices>();
builder.Services.AddSingleton<IMarksService, MarksService>();
builder.Services.AddSingleton<IAddressesService,AddressesService>();
builder.Services.AddSingleton<ITeachersService, TeachersService>();
builder.Services.AddSingleton<IStudentsService, StudentsService>();
builder.Services.AddSingleton<ISubjetcsService, SubjetcsService>();

builder.Services.AddControllers(options=>
{
    options.Filters.Add<InvalidIdExceptionFilter>();
    options.Filters.Add<DuplicateExceptionFilter>();

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>AddSwaggerDocumentation(o));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddSwaggerDocumentation(SwaggerGenOptions o)

{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}