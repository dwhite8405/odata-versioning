// Program.cs
using Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Order>();
modelBuilder.EntitySet<Customer>("Customers");
modelBuilder.EntitySet<Customer2>("Customers_v2");

var modelBuilder2 = new ODataConventionModelBuilder();
modelBuilder2.EntityType<Order>();
modelBuilder2.EntitySet<Customer2>("Customers");

builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null)
    .AddRouteComponents(
        "odata/v1",
        modelBuilder.GetEdmModel())
    .AddRouteComponents(
        "odata/v2",
        modelBuilder2.GetEdmModel())
    );

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();