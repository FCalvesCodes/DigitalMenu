using DigitalMenu.Api.Core.ApplicationModels;
using DigitalMenu.Api.Core.WorkUnits;
using DigitalMenu.Application.Core;
using DigitalMenu.Infrastructure.IoC.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseNHibernate(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutoMapperModule));
builder.Services.UseUnitOfWork(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.AddScoped(typeof(UnitOfWorkActionFilter));

builder.Services.Configure<MvcOptions>(opts =>
{
    opts.Filters.AddService(typeof(UnitOfWorkActionFilter));

    opts.Conventions.Add(new CustomControllerConvention(options =>
    {
        options.UsePlural = true;
    }));

    opts.Conventions.Add(new CustomActionConvention(options =>
    {
        options.SetPrefixe("GetAll", CustomConventionHttpVerbs.Get);
        options.SetPrefixe("Get", CustomConventionHttpVerbs.Get);
        options.SetPrefixe("Update", CustomConventionHttpVerbs.Put);
        options.SetPrefixe("Create", CustomConventionHttpVerbs.Post);
        options.SetPrefixe("Remove", CustomConventionHttpVerbs.Delete);
        options.SetPrefixe("Delete", CustomConventionHttpVerbs.Delete);
    }));

    opts.Conventions.Add(new CustomParameterConvention());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "api/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
