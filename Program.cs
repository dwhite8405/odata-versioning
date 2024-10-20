// Program.cs
using Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.ModelBuilder.Core.V1;

var builder = WebApplication.CreateBuilder(args);

// This would be an updated version of an original model, where the update was
// to add a new Customers_v2 entity.
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Order>();
EntitySetConfiguration<Customer> customers = modelBuilder.EntitySet<Customer>("Customers");

// We add an annotation to show to the user that this entity is deprecated.
// I have to admit that this is a strange way to add a deprecated tag.
customers.HasRevisions(revisions => {
    revisions.HasKind(RevisionKind.Deprecated);
    revisions.HasVersion("v1"); // This refers to the schema version.
    // I'm not sure where the schema version would be recorded.
    // You can use $schemaversion in queries but I can't work out how to implement that.
    return revisions;
});

modelBuilder.EntitySet<Customer2>("Customers_v2");

// The next version of the model.
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